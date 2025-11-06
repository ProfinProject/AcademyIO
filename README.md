## 🏦 AcademyIO – Plataforma de Educação Online

Bem-vindo ao **AcademyIO**, um projeto desenvolvido no **MBA DevXpert Full Stack .NET** (Módulo 4) com foco em arquitetura moderna, escalabilidade e boas práticas de desenvolvimento.  
O AcademyIO é uma plataforma de ensino digital que permite aos usuários gerenciar cursos, matrículas, pagamentos e certificados por meio de uma **API RESTful robusta, segura e bem documentada**.

---

### 🚀 Sobre o Projeto

Construído com uma abordagem baseada em microsserviços e princípios de **Domain-Driven Design (DDD)**, o AcademyIO oferece uma experiência integrada e segura para alunos e administradores, com funcionalidades como:

- 📚 **Cadastro e gestão de cursos**
- 💳 **Processamento de pagamentos e faturamento**
- 🔒 **Autenticação e autorização seguras com JWT e ASP.NET Core Identity**
- 👥 **Registro, listagem e busca de alunos por curso**
- 📄 **Emissão e acompanhamento de certificados**

---

### 👥 Equipe de Desenvolvimento

- Fabiano Marcolin Maciel
- Breno Francisco Morais
- Caio Gustavo Rodrigues
- Luis Felipe da Silva Sousa
- Thiago Albuquerque Severo
- Viliane Oliveira

---

### 🛠️ Tecnologias Utilizadas

**Back-End:**

- C# 12
- ASP.NET Core Web API (.NET 8.0)
- Entity Framework Core 8.0.10
- SQL Server / SQLite
- ASP.NET Core Identity + JWT
- RabbitMQ (comunicação assíncrona via message bus)

**Documentação:**

- Swagger/OpenAPI – disponível em `http://localhost:5005/swagge`

### 📂 Estrutura do Projeto

```
academyio/
├── 📁 src/
│   ├── 📁 ApiGateways/
│   │   └── 📁 AcademyIO.Bff/              # 🌉 API Gateway (Backend for Frontend)
│   │
│   ├── 📁 BuildingBlocks/                 # 🧱 Blocos reutilizáveis
│   │   ├── 📁 Core/
│   │   │   └── 📁 AcademyIO.Core/         # 📐 Entidades base, interfaces, validações
│   │   ├── 📁 MessageBus/
│   │   │   └── 📁 AcademyIO.MessageBus/   # 📨 Comunicação assíncrona (RabbitMQ)
│   │   └── 📁 Services/
│   │       └── 📁 AcademyIO.WebAPI.Core/  # ⚙️ Middlewares, Identity, extensões comuns
│   │
│   ├── 📁 Services/                        # 🧩 Microsserviços independentes
│   │   ├── 📁 Auth/
│   │   │   └── 📁 AcademyIO.Auth.API/     # 🔐 Autenticação e autorização (JWT + Identity)
│   │   ├── 📁 Courses/
│   │   │   └── 📁 AcademyIO.Courses.API/  # 📚 Gestão de cursos
│   │   ├── 📁 Payments/
│   │   │   └── 📁 AcademyIO.Payments.API/ # 💳 Processamento de pagamentos
│   │   └── 📁 Students/
│   │       └── 📁 AcademyIO.Students.API/ # 👥 Gestão de alunos e matrículas
│   │
│   ├── 📁 Tests/                           # 🧪 Projetos de testes automatizados
│   │
│   └── 📁 Web/                             # 🌐 Aplicação frontend (Angular)
│
├── 📄 README.md                            # 📖 Documentação principal
├── 📄 FEEDBACK.md                          # 💬 Feedback do instrutor
└── 📄 .gitignore                           # 🚫 Arquivos ignorados pelo Git

```
 
 📄 FEEDBACK.md    💬 Feedback do instrutor
## ▶️ Como Executar o Projeto

### 📌 Pré-requisitos

- [.NET SDK 8.0 ou superior](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server ou SQLite](https://www.sqlite.org/index.html)
- [Git](https://git-scm.com/)
- [Node.js (v18 ou superior)](https://nodejs.org/) — necessário para o frontend Angular
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (com WSL2 habilitado, se estiver no Windows) — necessário para o RabbitMQ e Portainer
### 💻 Passos para Execução

#### 1️⃣ Clone o Repositório

```
git clone https://github.com/ProfinProject/AcademyIO.git
```

#### 2️⃣ Configure o Banco de Dados

- Acesse os arquivos `appsettings.json` dos microsserviços (`Auth`, `Courses`, `Payments`, `Students`) e do `BFF`.
- Defina a string de conexão para **SQL Server** ou **SQLite**, conforme sua preferência.
- Ao executar o projeto pela primeira vez, o **Entity Framework Core** aplicará as migrações e executará o **Seed** automaticamente.
#### 3️⃣ Suba os Serviços de Mensageria (RabbitMQ)

```
docker run -d --hostname rabbit-host --name rabbit-academyio -p 15672:15672 -p 5672:5672 rabbitmq:management
```

- Painel Web: [http://localhost:15672](http://localhost:15672/)
	- Usuário: `guest`
	- Senha: `guest`
_(Opcional)_ Se quiser gerenciar os containers visualmente.

🔌 Conexão AMQP (aplicação): amqp://guest:guest@localhost:5672/
### 4️⃣ Execute as APIs (.NET 8.0)
O projeto é composto por vários microsserviços e um **BFF (Backend for Frontend)**. Você pode executá-los de duas formas:

#### 🖥️ **Opção 1: Via Visual Studio (Múltiplos Projetos de Inicialização)**

1. Abra a solução `AcademyIO.sln` no **Visual Studio 2022**.
2. Clique com o botão direito na solução no **Solution Explorer** e selecione **Propriedades**.
3. Em **Common Properties > Startup Project**, escolha **Multiple startup projects**.
4. Defina a ação **Start** para os seguintes projetos:
	- `AcademyIO.Auth.API`
	- `AcademyIO.Courses.API`
	- `AcademyIO.Payments.API`
	- `AcademyIO.Students.API`
	- `AcademyIO.Bff`
5. Clique em **OK** e pressione **F5** ou o botão **Iniciar**.

⚠️ Certifique-se de que o **RabbitMQ** está rodando (veja seção 3 — Mensageria).

A documentação Swagger estará disponível em:  
🔗 [http://localhost:5005/swagger](http://localhost:5005/swagger)

#### 💻 **Opção 2: Via CLI (Command Line Interface)**

Se preferir rodar os serviços manualmente pelo terminal (útil em ambientes sem Visual Studio ou em Linux/macOS/WSL), siga os passos:

1. Abra um terminal na raiz do projeto (`AcademyIO`).
2.  Execute **cada microsserviço em um terminal separado** com os comandos abaixo:

⚠️ Executar um comando por Terminal

```
# Serviço de Autenticação
dotnet run --project src/Services/AcademyIO.Auth.API/AcademyIO.Auth.API.csproj

# Serviço de Cursos
dotnet run --project src/services/AcademyIO.Courses.API/AcademyIO.Courses.API.csproj

# Serviço de Pagamentos
dotnet run --project src/Services/AcademyIO.Payments.API/AcademyIO.Payments.API.csproj

# Serviço de Alunos
dotnet run --project src/Services/AcademyIO.Students.API/AcademyIO.Students.API.csproj

# API Gateway (BFF)
dotnet run  --project src/api-gateways/AcademyIO.Bff/AcademyIO.Bff.csproj
```

Ambas as abordagens exigem que:
- O **banco de dados** esteja configurado corretamente (SQL Server ou SQLite).
- O **RabbitMQ** esteja em execução (caso utilize funcionalidades baseadas em mensageria).

#### 5️⃣ Execute o Frontend (Angular) Terminal

Pasta do Projeto:
```
cd src/Front-End
```

Instalar Dependências:
```
npm install --legacy-peer-deps
```

Inicializar Aplicação:
```
npm start
```

A aplicação frontend estará disponível em:  
🌐 [http://localhost:4200](http://localhost:4200/)

## 👥 Credenciais de Acesso

| Perfil         | Nome        | E-mail                 | Senha       |
|----------------|-------------|------------------------|-------------|
| Administrador  | Admin       | admin@academyio.com    | Teste@123   |
| Aluno          | Student1    | aluno1@academyio.com   | Teste@123   |
| Aluno          | Student2    | aluno2@academyio.com   | Teste@123   |

📌 **Considerações Finais** Este projeto faz parte de um curso acadêmico e não aceita contribuições externas. Para dúvidas ou feedbacks, utilize a aba Issues do repositório. O arquivo FEEDBACK.md contém avaliações do instrutor e deve ser modificado apenas por ele.
