🏦 **AcademyIO - Plataforma de Educação Online**

Bem-vindo ao AcademyIO, um projeto desenvolvido no **MBA DevXpert Full Stack .NET** para o módulo 4. O AcademyIO é uma solução inovadora para Educação Online, permitindo que os usuários acompanhem seus cursos, e certificados de maneira intuitiva através de uma API RESTful robusta.  

🚀 **Sobre o Projeto**

A plataforma foi criada para proporcionar uma experiência fluida e segura no controle dos cursos e matricula, oferecendo:  
- Registro de cursos   
- Pagamento e faturamento 📊  
- Autenticação segura via JWT 🔒  
- Registro e pesquisa de alunos por curso 🔍  


👥**Equipe de Desenvolvimento**

- Fabiano Marcolin Maciel  
- Breno Francisco Morais  
- Caio Gustavo Rodrigues  
- Luis Felipe da Silva Sousa  
- Thiago Albuquerque Severo  
- Viliane Oliveira  

🛠️ **Tecnologias Utilizadas**  
Back-End:  
- C#  
- ASP.NET Core Web API (.NET 8.0)  
- Entity Framework Core (EF Core 8.0.10)  
- SQL Server / SQLite  
- ASP.NET Core Identity + JWT  

**Documentação:**
Swagger 📄

📂 **Estrutura do Projeto**

src/

 ├── AcademyIO.API/      # API RESTful  
 ├── AcademyIO.Core/     # Regras de negócio e validações  
 ├── ManagementCourses/   # Bounded context de Cursos  
 ├── ManagementStudentsy/ # Bounded context de alunos  
 
README.md             # Documentação do projeto  
FEEDBACK.md           # Consolidação de feedbacks  
.gitignore            # Configuração do Git  
------------------------------------------------------------

▶️ **Como Executar o Projeto**  
📌 
.NET SDK 8.0 ou superior  
SQL Server ou SQLite  
Visual Studio 2022 ou VS Code  
Git

💻 **Passos para Execução**

1️⃣ **Clone o Repositório:**  
git clone https://github.com/ProfinProject/AcademyIO.git
cd AcademyIO  

2️⃣ **Configuração do Banco de Dados:**  
No arquivo appsettings.json, configure a string de conexão para SQL Server ou SQLite.  
Execute o projeto para que a configuração do Seed crie e popule o banco automaticamente.

3️⃣ **Executar a API (.NET 8.0):**  
cd AcademyIO/src/AcademyIO.API  
dotnet run  
📌 Acesse a API em: http://localhost:5005 ou https://localhost:7092 (HTTPS).


🔑 **Configuração de Segurança**  
Autenticação JWT: Configurada no appsettings.json.  
Migração do Banco: Gerenciada pelo EF Core, com Seed de dados automático.  

📜 **Documentação da API**  
A API está documentada via Swagger: 📌 Acesse em: http://localhost:5005/swagger


📌 **Considerações Finais** 
Este projeto faz parte de um curso acadêmico e não aceita contribuições externas. Para dúvidas ou feedbacks, utilize a aba Issues do repositório. O arquivo FEEDBACK.md contém avaliações do instrutor e deve ser modificado apenas por ele.

🚀 Gostou do projeto? Deixe uma estrela ⭐ no repositório!  
🔗 Conecte-se com a equipe no LinkedIn! #dotnet #fullstack #finanças #fabianoIO #DDD #CQRS #webdevelopment
