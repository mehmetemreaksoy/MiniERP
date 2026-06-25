# MiniERP - Project Overview

## Proje Amacı

MiniERP, küçük ve orta ölçekli işletmeler için geliştirilecek web tabanlı bir ERP sistemidir.

Bu projenin ana amacı sadece çalışan bir ERP yapmak değildir. Aynı zamanda yapay zeka destekli yazılım geliştirme sürecini öğrenmek, Codex ile kontrollü ve anlaşılır şekilde proje geliştirmektir.

## Eğitim Amacı

Bu proje bir öğrenme projesidir.

Geliştirme sürecinde amaç:

- ASP.NET Core MVC mimarisini daha iyi öğrenmek
- Entity Framework Core kullanımını pekiştirmek
- SQL Server ile veritabanı ilişkilerini anlamak
- CRUD işlemlerini gerçek iş senaryoları üzerinden öğrenmek
- Codex ile proje geliştirme disiplinini öğrenmek
- AI tarafından yazılan kodu okuyup anlayabilmek
- Her adımı küçük parçalara bölerek ilerlemek

## Kullanılacak Teknolojiler

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap
- Git / GitHub
- VS Code
- Codex CLI
- Codex IDE Extension

## İlk Sürüm Hedefi

İlk sürümde büyük ve karmaşık bir ERP sistemi yapılmayacaktır.

İlk hedef, çalışan ve anlaşılır bir ERP çekirdeği oluşturmaktır.

İlk sürümde olacak temel özellikler:

- Admin panel
- Kategori yönetimi
- Ürün yönetimi
- Cari / müşteri yönetimi
- Stok giriş ve çıkış işlemleri
- Satış kaydı
- Basit dashboard
- Login / kullanıcı yönetimi

## Mimari Yaklaşım

Proje başlangıçta klasik ASP.NET Core MVC mimarisiyle geliştirilecektir.

Başlangıçta Clean Architecture, CQRS, MediatR, ayrı Domain/Application/Infrastructure katmanları kullanılmayacaktır.

Proje klasör yapısı sade tutulacaktır:

- Controllers
- Models
- Views
- Data
- ViewModels
- Services
- wwwroot

## Uzun Vadeli Hedef

Proje ilerledikçe daha kurumsal yapıya taşınabilir.

İleride eklenebilecek konular:

- Rol bazlı yetkilendirme
- Depo yönetimi
- Fatura yönetimi
- Satın alma yönetimi
- Raporlama
- API katmanı
- Çok kullanıcılı yapı
- Loglama
- Audit trail
- Gelişmiş dashboard