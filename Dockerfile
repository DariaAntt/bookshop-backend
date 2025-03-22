# Используем базовый образ Microsoft .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Копируем все файлы проекта
COPY . .

# Восстанавливаем зависимости и публикуем приложение
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Базовый образ для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Открываем порт
EXPOSE 80
EXPOSE 443

# Запускаем приложение
ENTRYPOINT ["dotnet", "backend.dll"]