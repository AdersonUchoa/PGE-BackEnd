using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjetoPGE.Domain.Account;
using ProjetoPGE.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly PgedbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticateService(PgedbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.LoginPessoa.ToLower() == username.ToLower());
            if (pessoa == null)
            {
                return false;
            }

            using var hmac = new HMACSHA512(pessoa.passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for(int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != pessoa.passwordHash[i])
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<string> GenerateToken(int id, string loginPessoa)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                throw new Exception("Usuário não encontrado.");
            }
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("loginPessoa", loginPessoa),
                new Claim("tipoPessoa", pessoa.TipoPessoa),
                new Claim("senha", pessoa.passwordHash.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(1);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Pessoa> GetPessoaByLogin(string username)
        {
            return await _context.Pessoas.Where(p => p.LoginPessoa.ToLower() == username.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<bool> pessoaExiste(string username)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.LoginPessoa.ToLower() == username.ToLower());
            if (pessoa == null)
            {
                return false;
            }
            return true;
        }
    }
}
