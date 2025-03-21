using System.ComponentModel.DataAnnotations;

namespace ProjetoPGE.API.Token
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O campo login é obrigatório.")]
        public string loginPessoa { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public string password { get; set; }
    }
}
