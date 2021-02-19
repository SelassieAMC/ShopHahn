#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Hahn.ApplicationProcess.February2021.Test/Hahn.ApplicationProcess.February2021.Test.csproj", "Hahn.ApplicationProcess.February2021.Test/"]
COPY ["Hahn.ApplicationProcess.February2021.Web/Hahn.ApplicationProcess.February2021.Web.csproj", "Hahn.ApplicationProcess.February2021.Web/"]
COPY ["Hahn.ApplicationProcess.February2021.Data/Hahn.ApplicationProcess.February2021.Data.csproj", "Hahn.ApplicationProcess.February2021.Data/"]
COPY ["Hahn.ApplicationProcess.February2021.Domain/Hahn.ApplicationProcess.February2021.Domain.csproj", "Hahn.ApplicationProcess.February2021.Domain/"]
RUN dotnet restore "Hahn.ApplicationProcess.February2021.Test/Hahn.ApplicationProcess.February2021.Test.csproj"
COPY . .
WORKDIR "/src/Hahn.ApplicationProcess.February2021.Test"
ENTRYPOINT ["dotnet", "test"]