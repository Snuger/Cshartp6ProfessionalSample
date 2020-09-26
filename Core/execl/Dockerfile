FROM  172.19.30.186:8000/pub/dotnetcore-runtime:v3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM 172.19.30.186:8000/pub/dotnetcore-sdk:v3.1 AS build
WORKDIR /src

COPY ["Megicodes.execl.Sample/Megicodes.execl.Sample.csproj", "Megicodes.execl.Sample/"]

COPY . .
WORKDIR "/src/Megicodes.execl.Sample"
RUN dotnet build "Megicodes.execl.Sample.csproj"  -o /app/build


FROM build AS publish
RUN dotnet publish "Megicodes.execl.Sample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Megicodes.execl.Sample.dll"]