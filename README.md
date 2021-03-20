# 2. Documentação .Net Core API - Server

 Utilizado .NET Core 3.1

## 2.1 Processo de desenvolvimento

 

### 2.1.1 Padrão adotado

Foi adotado o ***Padrão CQRS, com o desenvolvimento em camadas e DDD.***

### 2.1.2 Gerenciamento de usuários

Foi utilizado o ***IdentityServer na versão 2.2.0***

### 2.1.3 Persitência dos dados

Para persistência dos dados foi utilizado a ORM ***Entityframework Core na versão 5.0.4***

Para criação da base de dados foi tutilizado ***migrations***.

### 2.1.4 Base de dados

Para armazenar foi utilizado a base de dados ***SQL Server 13.0.4001.0***

As tabelas foram geradas pelo framework ***IdentityServer.***

- Tabelas

![https://s3-us-west-2.amazonaws.com/secure.notion-static.com/18d940c5-3ce2-4149-a637-8a0314c6cece/Untitled.png](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/18d940c5-3ce2-4149-a637-8a0314c6cece/Untitled.png)

### 2.1.5 EventBus

Para criação do EventBus, foi utilizado o pacote ***MediatR na versão 9.0.0***

### 2.1.6 Criptografia utilizada

Foi utilizado o algoritmo de hash ***HMACSHA256.***

## 2.2 Iniciar Projeto:

Para iniciar o projeto é necessário executar os seguintes comandos:

```jsx
add-migrations <mensagem> 

update-database
```
