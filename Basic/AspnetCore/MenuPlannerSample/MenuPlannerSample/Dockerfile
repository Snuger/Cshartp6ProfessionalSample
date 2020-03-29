FROM microsoft/dotnet:2.0-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 24501
EXPOSE 44390

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY MenuPlannerSample/MenuPlannerSample.csproj MenuPlannerSample/
RUN dotnet restore MenuPlannerSample/MenuPlannerSample.csproj
COPY . .
WORKDIR /src/MenuPlannerSample
RUN dotnet build MenuPlannerSample.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MenuPlannerSample.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MenuPlannerSample.dll"]
