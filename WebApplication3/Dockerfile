#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["WebApplication3/Api.csproj", "WebApplication3/"]
#COPY ["Migrations/Migrations.csproj", "Migrations/"]
#COPY ["Contex/Context.csproj", "Contex/"]
#RUN dotnet restore "WebApplication3/Api.csproj"
#COPY . .
#WORKDIR "/src/WebApplication3"
#RUN dotnet build "Api.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Api.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Api.dll"]

#FROM mcr.microsoft.com/dotnet/aspnet:5.0
#
#ADD ./ /app
#ENV PORT=8081
#ENV ASPNETCORE_URLS=http://*:${PORT}
#
#WORKDIR /app
#ENTRYPOINT ["dotnet", "Api.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ENV PORT=8081
ENV ASPNETCORE_URLS=http://*:${PORT}

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["/WebApplication3/Api.csproj", "Api/"]
RUN dotnet restore "Api/Api.csproj"
COPY . .
RUN dotnet build "WebApplication3/Api.csproj" -c Release -o /app/build

FROM base AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "Api.dll"]