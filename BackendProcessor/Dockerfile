FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

RUN dotnet nuget locals all --clear

COPY BackendProcessor/*.csproj ./
RUN dotnet restore

COPY BackendProcessor/ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BackendProcessor.dll"]