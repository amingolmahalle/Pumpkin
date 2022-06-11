# Start Project Step By Step Without Dockerfile:

First we should up [docker-compose](../../docker-compose.yml) that is next to the project:
>sudo docker-compose up -d mssql-srv redis-srv rabbitmq-srv elk-es01 elk-es02 elk-es03 elk-kibana

Then we should create user and database Ourselves according to what that is in [appsetting.json](../../Src/Presentation/WebApi/appsettings.json)

Please see [SqlServer DockerFile](../../Containers/services/sqlserver/Dockerfile) to get master database config.

Now just execute below codes in master database:

~~~~sql
CREATE database TestDb;
GO
USE TestDb;
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

At the end run project and call below address and test endpoint:

[Click Here And Enjoy](http://localhost:6020/swagger/index.html)