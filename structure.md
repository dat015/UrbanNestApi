# 1. Domain Layer (Class Library) - Trái tim hệ thống
dotnet new classlib -n UrbanNest.Services.Apartment.Domain

# 2. Application Layer (Class Library) - Logic nghiệp vụ
dotnet new classlib -n UrbanNest.Services.Apartment.Application

# 3. Infrastructure Layer (Class Library) - Giao tiếp DB, External
dotnet new classlib -n UrbanNest.Services.Apartment.Infrastructure

# 4. API Layer (Web API) - Cổng giao tiếp
dotnet new webapi -n UrbanNest.Services.Apartment.API

# Add tất cả project trong src vào solution
dotnet sln add src/**/*.

# Thiết lập Dependencies

# Application phụ thuộc Domain
dotnet add src/UrbanNest.Services.Apartment.Application/UrbanNest.Services.Apartment.Application.csproj reference src/UrbanNest.Services.Apartment.Domain/UrbanNest.Services.Apartment.Domain.csproj

# Infrastructure phụ thuộc Application (để implement interface) và Domain
dotnet add src/UrbanNest.Services.Apartment.Infrastructure/UrbanNest.Services.Apartment.Infrastructure.csproj reference src/UrbanNest.Services.Apartment.Application/UrbanNest.Services.Apartment.Application.csproj
dotnet add src/UrbanNest.Services.Apartment.Infrastructure/UrbanNest.Services.Apartment.Infrastructure.csproj reference src/UrbanNest.Services.Apartment.Domain/UrbanNest.Services.Apartment.Domain.csproj

# API phụ thuộc Application (để gọi service) và Infrastructure (để DI)
dotnet add src/UrbanNest.Services.Apartment.API/UrbanNest.Services.Apartment.API.csproj reference src/UrbanNest.Services.Apartment.Application/UrbanNest.Services.Apartment.Application.csproj
dotnet add src/UrbanNest.Services.Apartment.API/UrbanNest.Services.Apartment.API.csproj reference src/UrbanNest.Services.Apartment.Infrastructure/UrbanNest.Services.Apartment.Infrastructure.csproj

# Giải thích
Domain: Luật nghiệp vụ cốt lõi (entities, value objects, interface repository)

Application: Use cases, service xử lý nghiệp vụ, interface (cần Domain để làm việc)

Infrastructure: Cài đặt cụ thể (DB, Redis, Email…), implement interface từ Application & Domain

API: Web layer gọi Application và inject Infrastructure

# phụ thuộc (reference) nghĩa là dự án A có quyền dùng code/tài nguyên PUBLIC của dự án B

# Cấu trúc cơ bản
UrbanNest/
├── UrbanNest.sln
├── src/
│   ├── UrbanNest.Services.Apartment.API/            # [Presentation]
│   │   ├── Controllers/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── UrbanNest.Services.Apartment.Application/    # [Core Logic]
│   │   ├── Interfaces/
│   │   ├── DTOs/
│   │   └── Services/ (hoặc UseCases)
│   │
│   ├── UrbanNest.Services.Apartment.Domain/         # [Core Entities]
│   │   ├── Entities/
│   │   └── Enums/
│   │
│   └── UrbanNest.Services.Apartment.Infrastructure/ # [Database & External]
│       ├── Data/ (DbContext)
│       └── Repositories/

# Api Gatewat, Load Balancing, Reverse Proxy,... -> YARP (Development by Microsoft)
Trong Microservices, Frontend (Next.js) không được phép gọi trực tiếp tới các service con (như Apartment Service hay Billing Service). Frontend chỉ biết một địa chỉ duy nhất là Gateway. Gateway sẽ chịu trách nhiệm định tuyến (Routing) request đến đúng nơi.

Chúng ta sẽ sử dụng YARP (Yet Another Reverse Proxy) - thư viện mã nguồn mở hiệu năng cao do chính Microsoft phát triển.

# Cấu hình
src/UrbanNest.Gateway/appsettings.json
Routes: Bắt tất cả request bắt đầu bằng /api/apartments/.

Clusters: Chuyển tiếp request đó đến http://localhost:5001 (Nơi Apartment Service đang chạy).