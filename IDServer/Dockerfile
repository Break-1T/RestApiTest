#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["IDServer/IDServer.csproj", "IDServer/"]
#COPY ["Contex/Context.csproj", "Contex/"]
#RUN dotnet restore "IDServer/IDServer.csproj"
#COPY . .
#WORKDIR "/src/IDServer"
#RUN dotnet build "IDServer.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "IDServer.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "IDServer.dll"]

#FROM mcr.microsoft.com/dotnet/aspnet:5.0
#
#ADD ./ /app
#ENV PORT=8080
#ENV ASPNETCORE_URLS=http://*:${PORT}
#
#WORKDIR /app
#ENTRYPOINT ["dotnet", "IDServer.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:5.0.12-buster-slim-amd64 AS base
WORKDIR /app
#WORKDIR /src
ENV PORT=8080
ENV ASPNETCORE_URLS=http://*:${PORT}

FROM mcr.microsoft.com/dotnet/sdk:5.0.403-buster-slim-amd64 AS build
WORKDIR /src
COPY ["/IDServer/IDServer.csproj", "IDServer/"]
RUN dotnet restore "IDServer/IDServer.csproj"
COPY . .
RUN dotnet build "IDServer/IDServer.csproj" -c Release -o /app/build
# c - configuration
# o - output
FROM base AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "IDServer.dll"]