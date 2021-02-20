#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
RUN apt-get update -y &&\
    apt-get install -y curl 
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash 
RUN apt-get install -y nodejs
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
RUN apt-get update &&\
    apt-get install curl 
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash 
RUN apt-get install -y nodejs

WORKDIR /src
COPY ["Hahn.ApplicationProcess.February2021.Web/Hahn.ApplicationProcess.February2021.Web.csproj", "Hahn.ApplicationProcess.February2021.Web/"]
COPY ["Hahn.ApplicationProcess.February2021.Data/Hahn.ApplicationProcess.February2021.Data.csproj", "Hahn.ApplicationProcess.February2021.Data/"]
COPY ["Hahn.ApplicationProcess.February2021.Domain/Hahn.ApplicationProcess.February2021.Domain.csproj", "Hahn.ApplicationProcess.February2021.Domain/"]
RUN dotnet restore "Hahn.ApplicationProcess.February2021.Web/Hahn.ApplicationProcess.February2021.Web.csproj"
COPY . .
WORKDIR "/src/Hahn.ApplicationProcess.February2021.Web"
RUN dotnet build "Hahn.ApplicationProcess.February2021.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hahn.ApplicationProcess.February2021.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hahn.ApplicationProcess.February2021.Web.dll"]