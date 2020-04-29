# Backend

O estado da aplicação tem em conta algumas bases de conhecimento sobre o funcionamento do sistema. 

# API

### `POST` /api/login
### `POST` /api/logout

### `GET` /api/user 
### `POST` /api/user
### `DELETE` /api/user
### `PUT` /api/user
### `GET` /api/user/{id_user}/tickets
<!-- ### `GET` /api/user/{id_user}/history -->

### `GET` /api/ticket

### `GET` /api/ticket_type

### `POST` /api/validator

### `POST` /api/kiosk


___
## Entidades

Existem no sistema algumas entidades que se devem conhecer como:

### User

Representa o utilizador

[user]:
 `User`
```json
{ 
    "id_user" : "aXXXXX",
    "email" : "aXXXXX@exemplo.pt",
    "permissions" : 1,
    "name" : "Nome de Exemplo"
}
```
> `id_user` Representa o numero de aluno ( identificador único que também pode ser numero de professor/funcionário etc ... ), é usado como chave primária de identificação do utilizador

> `email` Email do utilizador

> `permissions` Representa as permissões do utilizador no sistema (ver [Permissões](#Permissões))\

> `name` Nome do utilizador



### Ticket

Representa a senha utilizada em refeições

[ticket]:
`Ticket`
```json
{ 
    "id_ticket" : "XXXXXXXXXXXXXXXX",
    "id_user" : "aXXXXX",
    "type" : 1,
    "used" : false
}
```
> `id_ticket` Identificador da senha 

> `id_user` Identificador do utilizador

> `type` Representa o tipo de 'ticket'. Este tipo é um inteiro porque serve como identificador para o `TicketType` que é um objeto que indica informações sobre o tipo da senha



### TicketType
[ticket]:
`Ticket`
```json
{
    "type": 1,
    "price": 1.8,
    "name": "simple"
}
```
> `type` Identificador do tipo de senha 

> `price` Preço em euros

> `name` Nome por extenso do tipo de senha (dá uma descriçao do que representa esse tipo de senha)


### Permissões

As permissões que se têm em conta sao: 
* `User` - Destinado ao utilizador normal que faz compras, utiliza senhas e quer ver 
informações como a ementa da cantina e coisas assim...

* `Validator` - Uma conta que valida as senhas tem um comportamento especial que permite a este de 
validar os códigos de senha de quem passar na cantina e marca essa senha como *utilizada*

* `Admin` - Um administrador de sistema que tem permissão sobre tudo e ainda funcionalidades 
extra de management do sistema

* `Content Provider` - (WIP) Provavelmente mais tarde irá haver alguém responsável por 
atualizar a informação do sistema a partir de uma plataforma própria. Ele atualizaria coisas como a ementa da cantina  

Cada utilizador geral tem a possibilidade de ter mais que uma permissão simultâneamente , 
ou seja , um utilizador pode  ser `User` e `Validator` ou `User` e `Admin` ...


#### Implementação

A implementação das permissões é um simples *bitmap* que tem uma ordem que define que 
permissões esse valor se refere.

```
USER             = 0001
VALIDATOR        = 0010
ADMIN            = 0100
CONTENT_PROVIDER = 1000
```

Por exemplo, se um utilizador tem como valor de permissões `5` (0101)
então este tem permissões de `User` e `Admin`





___
# Endpoints

Para já estão implementados estes endpoints:
* [Serviço de Autenticação](#Serviço-de-Autenticação)
* [Endpoint do Utilizador](#Endpoint-do-Utilizador)
* [Endpoint das Senhas](#Endpoint-das-Senhas)
* [Endpoint dos Tipos de Senhas](#Endpoint-dos-Tipos-de-Senhas)
* [Endpoint do Validador](#Endpoint-do-Validador)


___
## Serviço de Autenticação
<!-- ```
localhost:5000/api/login
``` -->

### Login
### `POST` /api/login

Autentica no servidor e retorna um *token* de sessão (Bearer Token) 

> JSON Params

|            | Type   | Example |
|:----------:|:------:|:-------:|
|**Username**| string | aXXXXX |
|**Password**| string | XXXXXXXXXXXXXXXX |


```
// Basic Auth
{
    Username = aXXXXX
    Password = XXXXXXXXXXXXXXXX
}
```

### Logout - Any Auth
### `POST` /api/logout

Invalida um préviamente autenticado (Bearer Token)


___
### Endpoint do Utilizador
<!-- ```
localhost:5000/api/user
``` -->

| Requests permitidos | `GET` | `POST` | `PUT` | `DELETE` |
|:-------------------:|:-----:|:------:|:-----:|:--------:|
| Not Authenticated   |       |        |       |          |
| User                |   ✔️   |        |   ✔️  |          |
| Validator           |   ✔️   |        |   ✔️  |          |
| Admin               |   ✔️   |    ✔️  |   ✔️   |    ✔️    |
| Content Provider    |   ✔️   |        |   ✔️  |          |

### Listar utilizadores - Admin / User
### `GET` /api/user

Lista a informação de todos os utilizadores do sistema , caso tenha o argumento `id_user` então retorna apenas a informação do user identificado por esse identificador.

Um `User` apenas pode pedir informação de si mesmo

Um `Admin` pode pedir informação de tudo e todos

> Query String Params

```
All parameters to this endpoint are optional.
```

|            | Type   | Example |
|:----------:|:------:|:-------:|
|**id_user** | string | aXXXXX  |

## Adicionar utilizador - Admin
### `POST` /api/user

Adiciona um utilizador

> JSON Params

|            | Type   | Example |
|:----------:|:------:|:-------:|
|**id_user**  | string | aXXXXX  |
|**email**    | string | aXXXXX@uminho.pt  |
|**password** | string | XXXXXXXXXXXXXXXX  |
|**name**     | string | XXXXX |

**Exemplo**: 
```json
{
	"id_user" : "aTESTE",
	"email" : "aTESTE@uminho.pt",
	"password": "epah_mas_que_chatice",
	"name" : "NOME DE TESTE"
}
```

## Apagar utilizador - Admin
### `DELETE` /api/user

Apaga um utilizador

> JSON Params

|            | Type   | Example |
|:----------:|:------:|:-------:|
|**id_user** | string | aXXXXX |

**Exemplo**: 
```json
{
	"id_user" : "aTESTE"
}
```

## Alterar password (WIP) - Admin / User 
### `PUT` /api/user

Altera a password a um utilizador

> Query String Params

|            | Type   | Example |
|:----------:|:------:|:-------:|
|**old_password** | string | XXXXXXXXXXXXXXXXXXXXXX |
|**new_password** | string | YYYYYYYYYYYYYYYYYYYYYY |

**Exemplo**: 
```json
{
    "old_password" : "XXXXXXXXXXXXXXXXXXXXXX",
    "new_password" : "YYYYYYYYYYYYYYYYYYYYYY"
}
```

## Listar senhas do utilizador (WIP) - User
### `GET` /api/user/{id_user}/tickets

Retorna as senhas do utiliziador

> Query String Params

```
All parameters to this endpoint are optional.
```

|            | Type   | Example |
|:----------:|:------:|:-------:|
|**id_ticket** | string | XXXXXXXXXXXXXXXX |

**Exemplo**: 
```json
{
    "id_ticket" : "XXXXXXXXXXXXXXXX"
}
```


___
## Endpoint das Senhas
WIP

| Requests permitidos | `GET` | `POST` | `PUT` | `DELETE` |
|:-------------------:|:-----:|:------:|:-----:|:--------:|
| Not Authenticated   |       |        |       |          |
| User                |   ✔️   |        |      |          |
| Validator           |   ✔️   |        |      |          |
| Admin               |   ✔️   |        |       |         |
| Content Provider    |   ✔️   |        |      |          |

### Listar senhas - Admin
### `GET` /api/ticket

Lista a informação de todos as senhas do sistema , caso tenha o argumento `id_ticket` então retorna apenas a informação da senha identificado por esse identificador.

Um `Admin` pode pedir informação de tudo e todos

> Query String Params

```
All parameters to this endpoint are optional.
```

|            | Type   | Example |
|:----------:|:------:|:-------:|
|**id_ticket** | string | XXXXXXXXXXXXXXXX  |

**Exemplo**: 
```json
{
    "id_ticket" : "XXXXXXXXXXXXXXXX"
}
```

___
## Endpoint dos Tipos de Senhas
WIP

| Requests permitidos | `GET` | `POST` | `PUT` | `DELETE` |
|:-------------------:|:-----:|:------:|:-----:|:--------:|
| Not Authenticated   |       |        |       |          |
| User                |   ✔️   |        |      |          |
| Validator           |   ✔️   |        |      |          |
| Admin               |   ✔️   |        |       |         |
| Content Provider    |   ✔️   |        |      |          |

### Listar tipos de senhas - User
### `GET` /api/ticket_type

Lista a informação de todos os tipos de senhas do sistema , caso tenha o argumento `ticket_type` então retorna apenas a informação do tipo senha identificado por esse identificador.

> Query String Params

```
All parameters to this endpoint are optional.
```

|            | Type   | Example |
|:----------:|:------:|:-------:|
|**ticket_type** | integer | 1  |

**Exemplo**: 
```json
{
    "ticket_type" : 1
}
```

___
## Endpoint do Validador


| Requests permitidos | `GET` | `POST` | `PUT` | `DELETE` |
|:-------------------:|:-----:|:------:|:-----:|:--------:|
| Not Authenticated   |       |        |       |          |
| User                |       |        |      |          |
| Validator           |       |   ✔️    |      |          |
| Admin               |       |   ✔️    |      |          |
| Content Provider    |       |        |      |          |

### Validar senha - Validator
### `POST` /api/validator

Passa pelo processo de validar uma senha, atualizando o sistema segundo esse pedido

|            | Type   | Example |
|:----------:|:------:|:-------:|
|**id_user** | string | aXXXXX |
|**id_ticket** | string | XXXXXXXXXXXXXXXX  |

> Query String Params

**Exemplo**: 
```json
{
    "id_user" : "aXXXXX",
    "id_ticket" : "XXXXXXXXXXXXXXXX"
}
```

## Endpoint de Compras
WIP

| Requests permitidos | `GET` | `POST` | `PUT` | `DELETE` |
|:-------------------:|:-----:|:------:|:-----:|:--------:|
| Not Authenticated   |       |        |       |          |
| User                |       |   ✔️    |      |          |
| Validator           |       |        |      |          |
| Admin               |       |   ✔️    |      |          |
| Content Provider    |       |        |      |          |

### Comprar senha - User
### `POST` /api/kiosk

Passa pelo processo de validar uma senha, atualizando o sistema segundo esse pedido

|            | Type   | Example |
|:----------:|:------:|:-------:|
|**ticket_type** | int | 1 |
|**amount** | int | 23  |

> Query String Params

**Exemplo**: 
```json
{
    "ticket_type" : 1,
    "amount" : 23
}
```