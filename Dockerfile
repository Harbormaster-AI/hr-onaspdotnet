FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY hronaspdotnet/Api.csproj hronaspdotnet/
RUN dotnet restore hronaspdotnet/Api.csproj

COPY hronaspdotnet/ hronaspdotnet/

RUN dotnet publish hronaspdotnet/Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]