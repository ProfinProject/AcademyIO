# --------------------------
# 1. Base runtime
# --------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Define URLs padrão para Kestrel
ENV ASPNETCORE_URLS=http://+:80;https://+:443
ENV DOTNET_RUNNING_IN_CONTAINER=true

# --------------------------
# 2. Build
# --------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia solution e projetos
COPY *.sln ./
COPY src/ src/

# Restaura dependências
RUN dotnet restore "src/services/AcademyIO.Auth.API/AcademyIO.Auth.API.csproj"

# Build
RUN dotnet build "src/services/AcademyIO.Auth.API/AcademyIO.Auth.API.csproj" -c Release -o /app/build

# --------------------------
# 3. Publish
# --------------------------
FROM build AS publish
RUN dotnet publish "src/services/AcademyIO.Auth.API/AcademyIO.Auth.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --------------------------
# 4. Final
# --------------------------
FROM base AS final
WORKDIR /app

# Cria diretório de certificados
RUN mkdir -p /root/.aspnet/https

# Copia certificado da máquina local para dentro do container
COPY ./src/certs/academyio-auth.pfx /root/.aspnet/https/academyio-auth.pfx

# Configura variáveis de ambiente do Kestrel
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/root/.aspnet/https/academyio-auth.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=senha123

# Copia arquivos publicados da build
COPY --from=publish /app/publish .

# Define entrypoint
ENTRYPOINT ["dotnet", "AcademyIO.Auth.API.dll"]
