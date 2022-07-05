# DBC_API

> Neste projeto você poderá cadastrar novos usuarios e fazer pesquisa refinadas de todos os usuarios cadastrados.

## 💻 Pré-requisitos
Antes de começar, verifique se você atendeu aos seguintes requisitos:
* Instalar o Sql Server Express LocalDB => https://docs.microsoft.com/pt-br/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16
* Executar o script no Management Studio.

CREATE DATABASE BrasilCashDB
GO

USE BrasilCashDB

CREATE TABLE ACCOUNT (
Id_account INT PRIMARY KEY NOT NULL IDENTITY(1,1),
Name VARCHAR(60) not null,
Tax_id VARCHAR(50) not null,
Password VARCHAR(12) not null,
Phone_number VARCHAR(11) null,
Postal_code VARCHAR(8) null,
Status VARCHAR(15) not null,
Created_at smalldatetime not null
)
GO

USE [master]
GO
CREATE LOGIN [BrasilCash_user] WITH PASSWORD=N'*brasilcashuser*', DEFAULT_DATABASE=[BrasilCashDB], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
USE [BrasilCasDB]
GO
CREATE USER [BrasilCash_user] FOR LOGIN [BrasilCash_user]
GO
USE [BrasilCasDB]
GO
ALTER USER [BrasilCash_user] WITH DEFAULT_SCHEMA=[dbo]
GO
USE [BrasilCasDB]
GO
ALTER ROLE [db_datareader] ADD MEMBER [BrasilCash_user]
GO
USE [BrasilCasDB]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [BrasilCash_user]
GO
