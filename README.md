# CompWeek - Blazor

Aplicação de teste Blazor para apresentação na CompWeek

## Criacao do banco de dados

Será utilizado o podman como ferramenta de container. Essa ferramenta deve estar instalada no computador

### Criar o container de banco de dados

Instruções foram executadas no Linux

sudo mkdir /home/jobvitral/db-compweek

sudo podman run --name db-compweek --network=podman  -v /home/jobvitral/db-compweek:/var/lib/mysql --env MARIADB_USER=compweek --env MARIADB_PASSWORD=Compweek001 -e MARIADB_ROOT_PASSWORD=Compweek001 --ip=10.88.0.2 -p 40579:3306 -d mariadb:latest

### Criar o banco de dados

sudo podman exec -it db-compweek mariadb --user root -p

create database compweek;

grant all privileges on compweek.* TO 'compweek'@'%' identified by 'Compweek001';

### Configurar o SDK do dotnet

É nescessário instalar o SDK do dotnet 6.0 na maquina. https://dotnet.microsoft.com/en-us/download

Após instalar a ferramenta do entity framework para fazer o migration do banco de dados

dotnet tool install --global dotnet-ef

### Inicializando o banco de dados

Navegue ate a pasta do projeto de infraestrutura: cd CompWeek.Infrastructure

Execute: dotnet ef database update --startup-project ../CompWeek.Api

Será criado o usuário:

Documento: 05502112688
Senha: 123456

### Criando o container do servidor de identidade e da API

Navegue até a pasta raiz do projeto

Execute o arquivo deploy,sh

Selecione o projeto que deseja fazer o deploy