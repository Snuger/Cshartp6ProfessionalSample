FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY MvcApplicationSample/MvcApplicationSample.csproj MvcApplicationSample/
RUN dotnet restore MvcApplicationSample/MvcApplicationSample.csproj
COPY . .
WORKDIR /src/MvcApplicationSample
RUN dotnet build MvcApplicationSample.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MvcApplicationSample.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MvcApplicationSample.dll"]
