# Finance - Projeto de Estudo

## Sobre o Projeto
<p>Projeto de controle de finança pessoal, com objetivo de treinar programação, camada de software, modelagem, coletas de requisitos e técnicas para construir software que sejam fácil de manter manutenção. O projeto não refrete nada da vida real, é algo totalmente pessoal. O projeto foi dividido em duas parte: Back-End e Front-End. Tanto o Back-End quanto o Front-End, foi criado usando o Framework Dotnet na versão 7. Na parte do Back-End foi usado o modelo WebApi e Front-End foi utilizado o modelo Blazor WebAssembly.</p>

> [!WARNING]
> O projeto ainda está em desenvolvimento e sem previsão de término.

## Requisitos para rodar o projeto

* Docker: [Link](https://www.docker.com/products/docker-desktop/)

> [!WARNING]
> Se por alguma razão o docker não suportar em seu computador ou algum problema ao executar o docker compose, basta instalar as ferramentas individualmente.

* Dotnet SDK versão 7 ou superior: [Link](https://dotnet.microsoft.com/pt-br/download)
* PostgresSQL versão 14 ou superior: [Link](https://www.postgresql.org/download/)

<p>Caso queira somente executar, não precisa instalar nenhum editor de código simples ou IDE. Basta usar o terminal do seu sistema operacional para rodar e ver o funcionamento do projeto.</p>

## Rodando aplicação.
<p>Tendo o docker instalado, basta executar os seguintes comandos: </p>

```bash
docker-compose build --no-cache
```

```bash
docker-compose up
```
<p>Será executando nos seguintes Hosts: </p>

* Back-End: http://localhost:5000
* Front-End: http://localhost:5003


> [!WARNING]
> Caso o docker compose não funcionar, deve ter instalado antes o Dotnet e PostgreSQL para executar de forma manual. Como essa aplicação foi fatiada em duas, terá que executar o back-end e front-end separadamente.

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

## Orientações extras caso o Docker não funcione

<p>Caso o docker não funcionar, será preciso rodar de forma completamente manual. Verifique a senha de acesso ao SGBD PostgreSQL e coloque na configuração na parte do Back-end da aplicação, arquivo appsettings.json. Deve colocar o usuário e senha configurado no PostgreSQL no momento da instalação.</p>

```json
{
  ...
  "ConnectionStrings": {
    "PostgresConnect": "Server=127.0.0.1;Port=5432;Database=FinanceiroAPI;User Id={{USERNAME}};Password={{PASSWORD}};"
  }
  ...
}
```


### Aplicando as Migrations no Banco de Dados.
<p>Para aplicar as migrations, deve-se utilizar o seguinte comando:</p>

```
dotnet ef database update -p Finance.API
```

> Deve aparecer algo similar a essa imagem abaixo:
>> ![image](https://github.com/danilosantanaa/Finance/assets/38994152/890f9d83-2671-4773-ad49-69bea98e3ee8)

### Rodando o Back-End.
<p>Abra a pasta do projeto <strong>Finance/Back/src</strong> no terminal e execute o seguite comando:</p>

```
dotnet watch run -p Finance.API
```

> Deve aparecer algo similar a essa imagem abaixo:
>> ![image](https://github.com/danilosantanaa/Finance/assets/38994152/06613127-770f-4693-acef-7c5afb72ec48)

> [!NOTE] 
> Será necessário criar usuário via API usando o Swagger na rota de registrar. No front-end ainda não foi criado a tela de registrar.
### Rodando o Front-End
<p>Abra a pasta do projeto <strong>Finance/Front/FinanceWasm</strong> no terminal e execute o seguite comando:</p>

```
dotnet watch run
```

> Deve aparecer algo similar a essa imagem abaixo:
>> ![image](https://github.com/danilosantanaa/Finance/assets/38994152/ae756f24-9c7b-4847-9e3f-1b1dd7989d6f)
