# ViraVenda

Plataforma Vira Venda  

## Endereços do container
### Local
> Host: localhost  
> Porta: 5002

### Remoto
> Host: 10.88.0.3
> Porta: 80

## Rotas
Rotas da API de usuários

### v1/authentication
> **[POST]** /v1/authentication/token - Efetua login do identity server e retorna um token de acesso

### v1/roles
> **[GET]** /v1/roles - Retorna uma lista de grupos de usuário  
> **[GET]** /v1/roles/:id - Retorna um grupo de usuário buscado por ID  
> **[POST]** /v1/roles - Insere um grupo de usuário  
> **[PUT]** /v1/roles - Atualiza um grupo de usuário  
> **[DELETE]** /v1/roles/:id - Deleta um grupo de usuário

### v1/users
> **[GET]** /v1/users - Retorna uma lista de usuários  
> **[GET]** /v1/users/:id - Retorna um usuário buscado por ID  
> **[POST]** /v1/users - Insere um usuário  
> **[PUT]** /v1/users - Atualiza um usuário  
> **[DELETE]** /v1/users/:id - Deleta um usuário por ID    

### v1/password
> **[PUT]** v1/password - Atualiza uma senha de usuário  
> **[POST]** v1/password/recover - Solicita uma recuperacao de senha  
> **[PUT]** v1/password/recover - Atualiza uma senha por recuperação de senha
