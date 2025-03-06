# Sử dụng hình ảnh ASP.NET runtime làm base để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

# Sử dụng hình ảnh SDK của .NET để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy file dự án vào container và chạy restore
COPY backend/ASPNET_API.csproj .
RUN dotnet restore "ASPNET_API.csproj"

# Copy toàn bộ mã nguồn vào container
COPY backend/ .

# Build ứng dụng với cấu hình Release
RUN dotnet build "ASPNET_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish ứng dụng (tối ưu cho production)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ASPNET_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Tạo image cuối cùng từ base và copy ứng dụng đã publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASPNET_API.dll"]
