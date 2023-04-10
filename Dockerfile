#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Project/Project.Web/Project.Web.csproj", "Project/Project.Web/"]
COPY ["Project/Project.Domain/Project.Domain.csproj", "Project/Project.Domain/"]
COPY ["Project/Project.Repository/Project.Repository.csproj", "Project/Project.Repository/"]
COPY ["Project/Project.Service/Project.Service.csproj", "Project/Project.Service/"]
RUN dotnet restore "Project/Project.Web/Project.Web.csproj"
COPY . .
WORKDIR "/src/Project/Project.Web"
RUN dotnet build "Project.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.Web.dll"]