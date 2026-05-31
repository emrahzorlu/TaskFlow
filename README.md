# TaskFlow API

Ekip görev yönetimi için ASP.NET Core Web API projesi.

## Teknolojiler

- .NET 10, ASP.NET Core Web API
- Entity Framework Core + SQLite
- JWT Authentication
- AutoMapper, BCrypt

## Mimari

Controller → Service → Repository → DbContext

Generic Repository Pattern, Unit of Work, DTO Pattern, Soft Delete, Global Exception Middleware kullanıldı.

## Roller

- **Admin** — kullanıcı, proje ve görev yönetimi
- **Lead** — proje ve görev oluşturma, kullanıcı atama
- **Worker** — görev durumu güncelleme, yorum ekleme

## Çalıştırma

```bash
git clone https://github.com/emrahzorlu/TaskFlow.git
cd TaskFlow/TaskFlow
dotnet run
```

API: `http://localhost:5123`

Varsayılan admin: `admin@taskflow.com` / `Admin123!`
