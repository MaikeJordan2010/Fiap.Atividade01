# Here, we include the dotnet core SDK as the base to build our app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["1 - Apresentacao/Atividade01/Atividade01.csproj","1 - Apresentacao/Atividade01/"]
COPY ["2 - Aplicacao/Atividade01.Aplicacao/Atividade01.Aplicacao.csproj","2 - Aplicacao/Atividade01.Aplicacao/"]
COPY ["3 - Dominio/Atividade01.Dominio/Atividade01.Dominio.csproj","3 - Dominio/Atividade01.Dominio/"]
COPY ["4 - Infraestrutura/Atividade01.Repositorio/Atividade01.Repositorio.csproj","4 - Infraestrutura/Atividade01.Repositorio/"]
COPY ["4 - Infraestrutura/Atividade.CrossCutting/Atividade.CrossCutting.csproj","4 - Infraestrutura/Atividade.CrossCutting/"]
COPY ["5 - Testes/Atividade01.Testes/Atividade01.Testes.csproj","5 - Testes/Atividade01.Testes/"]
RUN dotnet restore "1 - Apresentacao/Atividade01/Atividade01.csproj"
COPY . .
WORKDIR /src/1 - Apresentacao/Atividade01
RUN dotnet build "Atividade01.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Atividade01.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT [ "dotnet", "Atividade01.dll" ]

# # We then get the base image for Nginx and set the 
# # work directory 
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# # We'll copy all the contents from wwwroot in the publish
# # folder into nginx/html for nginx to serve. The destination
# # should be the same as what you set in the nginx.conf.
COPY --from=publish /app/publish/wwwroot /usr/local/webapp/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80