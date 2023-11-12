# Finance - Projeto de Estudo

## Sobre o Projeto
<p>Projeto de controle de finança pessoal, com objetivo de treinar programação, camada de software, modelagem, coletas de requisitos e técnicas para construir software que sejam fácil de manter manutenção. O projeto não refrete nada da vida real, é algo totalmente pessoal. O projeto foi dividido em duas parte: Back-End e Front-End. Tanto o Back-End quanto o Front-End, foi criado usando o Framework Dotnet na versão 7. Na parte do Back-End foi usado o modelo WebApi e Front-End foi utilizado o modelo Blazor WebAssembly.</p>

## Requisitos para rodar o projeto

* Docker: [Link](https://www.docker.com/products/docker-desktop/)
* .NET 7 Core ou Superior: [Link](https://dotnet.microsoft.com/en-us/download)

## Rodando aplicação.
<p>Tendo o docker instalado, basta executar os seguintes comandos: </p>

```bash
docker-compose build --no-cache
```

```bash
docker-compose up
```
<p>Será executado nos seguintes hosts: </p>

* Back-End: http://localhost:5000
* Front-End: http://localhost:5003

## Rodando migration

### Instalar a ferramenta EF 
<p>Instale o Entity Framewrok Core Tools para pode executar as migrations pelo Dotnet CLI. Para instalar basta rodar o seguinte comando: </p>

```
dotnet tool install --global dotnet-ef
```

### Executando Migrations
<p>Após rodar o comando do docker compose e finalizar a execução, agora é preciso aplicar as migrations no container do PostgreSQL. Execute o seguinte comando: </p>

```
dotnet ef database update -p Finance.API --connection 'User ID=postgres;Password=postgres;Host=localhost;Port=5433;Database=FinanceiroAPI'
```
