#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Project.WebAPI/Project.WebAPI.csproj", "Project.WebAPI/"]
COPY ["Project.Repository/Project.Repository.csproj", "Project.Repository/"]
COPY ["Project.Repository.Common/Project.Repository.Common.csproj", "Project.Repository.Common/"]
COPY ["Project.DAL/Project.DAL.csproj", "Project.DAL/"]
COPY ["Project.Common/Project.Common.csproj", "Project.Common/"]
COPY ["Project.Service/Project.Service.csproj", "Project.Service/"]
COPY ["Project.Service.Common/Project.Service.Common.csproj", "Project.Service.Common/"]
COPY ["Project.Model.Common/Project.Model.Common.csproj", "Project.Model.Common/"]
COPY ["Project.Model/Project.Model.csproj", "Project.Model/"]
RUN dotnet restore "Project.WebAPI/Project.WebAPI.csproj"
COPY . .
WORKDIR "/src/Project.WebAPI"
RUN dotnet build "Project.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.WebAPI.dll"]