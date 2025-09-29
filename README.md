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

src/<br>
 ├── ApiGateways/<br>
 │    └── AcademyIO.Bff/              # API Gateway (Backend For Frontend)<br>
 │<br>
 ├── BuildingBlocks/                  # Blocos reutilizáveis<br>
 │    ├── Core/<br>
 │    │    └── AcademyIO.Core/        # Entidades base, interfaces, validações<br>
 │    ├── MessageBus/<br>
 │    │    └── AcademyIO.MessageBus/  # Comunicação assíncrona entre serviços (mensageria)<br>
 │    └── Services/<br>
 │         └── AcademyIO.WebAPI.Core/ # Middlewares, Identity, extensões para APIs<br>
 │<br>
 ├── Services/                        # Microsserviços da aplicação<br>
 │    ├── Auth/<br>
 │    │    └── AcademyIO.Auth.API/    # Serviço de Autenticação e Autorização (JWT, Identity)<br>
 │    ├── Courses/<br>
 │    │    └── AcademyIO.Courses.API/ # Serviço de Cursos<br>
 │    ├── Payments/<br>
 │    │    └── AcademyIO.Payments.API/# Serviço de Pagamentos<br>
 │    └── Students/<br>
 │         └── AcademyIO.Students.API/# Serviço de Alunos<br>
 │<br>
 ├── Tests/                           # Projetos de teste automatizados<br>
 │<br>
 └── Web/                             #  Frontend <br>

 
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

3️⃣ **Executar as APIs (.NET 8.0):**  
-Startup multiple projects escolhendo as APIs: Auth, Course, Student, Payment, e o BFF para startar. (Você precisa configurar a parte referente ao RabbitMQ explicada abaixo)


🔑 **Configuração de Segurança**  
Autenticação JWT: Configurada no appsettings.json.  
Migração do Banco: Gerenciada pelo EF Core, com Seed de dados automático.  

📜 **Documentação da API**  
A API está documentada via Swagger: 📌 Acesse em: http://localhost:5005/swagger

📬 Mensageria — Setup com Docker, Portainer e RabbitMQ
✅ Pré-requisitos

🐳 Docker Desktop para Windows (com WSL2 habilitado)
Download: https://www.docker.com/products/docker-desktop/

🧭 (Opcional) Portainer — painel para gerenciar containers

O que é? 🖥️ O Portainer é um painel web para administrar o Docker: criar/gerenciar containers, imagens, volumes e redes, visualizar logs e status — ótimo para acompanhar o RabbitMQ em desenvolvimento.

💠 PowerShell
docker stop portainer 2>$null
docker rm portainer 2>$null
docker volume create portainer_data
docker run -d `
  -p 8000:8000 `
  -p 9443:9443 `
  --name portainer `
  --restart=always `
  -v /var/run/docker.sock:/var/run/docker.sock `
  -v portainer_data:/data `
  portainer/portainer-ce:latest

🧱 CMD (uma linha)
-docker stop portainer >nul 2>&1 && docker rm portainer >nul 2>&1 && docker volume create portainer_data && docker run -d -p 8000:8000 -p 9443:9443 --name portainer --restart=always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer-ce:latest


🌐 Acesse: https://localhost:9443

🔐 No primeiro acesso, crie o usuário admin. Senha sugerida: Portainer1234! (altere fora do ambiente local).

🐰 RabbitMQ — broker de mensagens (com painel)

O que é? 📨 O RabbitMQ é um message broker (AMQP). Ele recebe mensagens em exchanges, roteia para filas e permite que consumidores as processem de forma assíncrona. A imagem rabbitmq:management inclui o painel web.

▶️ Subir o container
docker run -d --hostname rabbit-host --name rabbit-academyio -p 15672:15672 -p 5672:5672 rabbitmq:management


📊 Painel (Management): http://localhost:15672/

🔑 login: guest — senha: guest

🔌 Conexão AMQP (aplicação): amqp://guest:guest@localhost:5672/

⚠️ Produção: crie um usuário próprio e evite guest/guest.

📌 **Considerações Finais** 
Este projeto faz parte de um curso acadêmico e não aceita contribuições externas. Para dúvidas ou feedbacks, utilize a aba Issues do repositório. O arquivo FEEDBACK.md contém avaliações do instrutor e deve ser modificado apenas por ele.

🚀 Gostou do projeto? Deixe uma estrela ⭐ no repositório!  
🔗 Conecte-se com a equipe no LinkedIn! #dotnet #fullstack #finanças #fabianoIO #DDD #CQRS #webdevelopment
