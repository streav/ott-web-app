FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
RUN apk update
RUN apk upgrade
RUN apk add --no-cache icu-libs krb5-libs libgcc libintl libssl3 libstdc++ zlib
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_HTTP_PORTS=""
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM node:21-alpine AS node
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
COPY --from=node . .
WORKDIR /src
RUN npm install -g pnpm
COPY ["src/OttWebApp/OttWebApp.csproj", "src/OttWebApp/"]
RUN dotnet restore "src/OttWebApp/OttWebApp.csproj"
COPY . .
WORKDIR "/src/src/OttWebApp"
RUN dotnet build "OttWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OttWebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY --from=publish /app/publish /app
ENTRYPOINT ["dotnet", "OttWebApp.dll"]