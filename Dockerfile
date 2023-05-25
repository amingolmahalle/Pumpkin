# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
ARG PUB_ENV=Staging
# --------------------------------------------------
# > PREPARE
# --------------------------------------------------
WORKDIR /source
COPY Pumpkin.sln .
COPY Src/. ./Src/

# --------------------------------------------------
# > BUILD
# --------------------------------------------------
WORKDIR /source/Src/Presentation/ClientWebApi
RUN dotnet build -c $PUB_ENV

# --------------------------------------------------
# > PUBLISH
# --------------------------------------------------
RUN dotnet publish -c $PUB_ENV -o /app --no-build

# --------------------------------------------------
# > DEPLOY 
# --------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build /app ./

RUN #apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENTRYPOINT ["dotnet", "ClientWebApi.dll"]
