#推荐大家使用此基础镜像构建，理由见下文。该镜像通过海外构建已安装libgdiplus库。
FROM ccr.ccs.tencentyun.com/magicodes/aspnetcore-runtime:latest AS base

WORKDIR /src

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /src
RUN ls
COPY ["Magicodes.IE.Excel.Sampkes.csproj", "Magicodes.IE.Excel.Sampkes/"]
COPY /src/Magicodes.IE.Excel.Sampkes/fonts/simsun.ttc /usr/share/fonts/simsun.ttc
RUN dotnet restore "src/Magicodes.IE.Excel.Sampkes/Magicodes.IE.Excel.Sampkes.csproj"
COPY . .
WORKDIR "src/Magicodes.IE.Excel.Sampkes"

RUN dotnet build "Magicodes.IE.Excel.Sampkes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Magicodes.IE.Excel.Sampkes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from= publish /app/publish .


ENTRYPOINT ["dotnet", "Magicodes.IE.Excel.Sampkes.dll"]