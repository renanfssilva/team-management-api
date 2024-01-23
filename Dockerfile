FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TeamManagement.API/TeamManagement.API.csproj", "TeamManagement.API/"]
COPY ["TeamManagement.Application/TeamManagement.Application.csproj", "TeamManagement.Application/"]
COPY ["TeamManagement.Domain/TeamManagement.Domain.csproj", "TeamManagement.Domain/"]
COPY ["TeamManagement.Infrastructure/TeamManagement.Infrastructure.csproj", "TeamManagement.Infrastructure/"]
RUN dotnet restore "TeamManagement.API/TeamManagement.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TeamManagement.API/TeamManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeamManagement.API/TeamManagement.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamManagement.API.dll"]