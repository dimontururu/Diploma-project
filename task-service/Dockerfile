# Базовый образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Сначала копируем ВСЕ файлы проектов
COPY ["task-service/Presentation.csproj", "task-service/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]

# 2. Восстанавливаем зависимости
RUN dotnet restore "task-service/Presentation.csproj"

# 3. Копируем весь остальной код
COPY . .

# 4. Собираем проект
WORKDIR "/src/task-service"
RUN dotnet build "Presentation.csproj" -c Release -o /app/build

# Этап публикации
FROM build AS publish
RUN dotnet publish "Presentation.csproj" -c Release -o /app/publish

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]