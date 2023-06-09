# Start Project Step By Step With Dockerfile:

First we should up [docker-compose](../../docker-compose.yml) that is next to the project:
>sudo docker-compose up -d webapi

Then we should create user and database Ourselves according to what that is in [appsettings.json](../../Src/Presentation/WebApi/appsettings.json)

see [SqlServer DockerFile](../../Containers/services/sqlserver/Dockerfile) to get master database config.
just execute below codes in master database:
~~~~sql
CREATE database TinyShopDb;
GO
USE TinyShopDb;
GO
CREATE LOGIN human WITH PASSWORD = '123$%^789qaz'
GO
CREATE USER human FOR LOGIN human;
GO
GRANT CREATE TABLE TO human;
GO
GRANT SELECT, INSERT, UPDATE, DELETE  TO human;
GO
GRANT ALTER ON SCHEMA::dbo TO human;
~~~~

After we should apply migration on database:

Please see: [EF Migration Commands](./EF_Migration_Commands.md)

At the end call below address and test endpoint:

[Click Here And Enjoy](http://localhost:2030/swagger/index.html)