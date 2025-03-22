# ProjetoPGE

- Framework utilizado: ASP.NET CORE

- Padrão utilizado: Clean architecture.

- Separação de responsabilidades:
1. API (Controllers) lida com requisições.
2. Application contém serviços, DTOs e seus mapeamentos, gerenciamento de
respostas e lógica de aplicação.
3. Domain define regras de negócio e contratos de repositórios.
4. Infra.Data cuida da persistência de dados.
5. Infra.Ioc gerencia a injeção de dependências.

- Tecnologias utilizadas:
1. ASP.NET Core para a API.
2. Entity Framework Core para acesso ao banco de dados.
3. AutoMapper para conversões entre DTOs e Models.
4. Identity Framework para autenticação/autorização.

