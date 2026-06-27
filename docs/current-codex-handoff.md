# Current Codex Handoff

## 1. Projenin Amacı

MiniERP, ASP.NET Core MVC ile geliştirilen eğitim amaçlı bir ERP projesidir. Amaç önce sade, çalışan ve öğrenilebilir bir ERP çekirdeği oluşturmak; sonra sistemi kurumsal seviyeye taşımaktır.

## 2. Teknoloji Stack

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Bootstrap
- Razor Views
- Git / GitHub

## 3. Git Branch Bilgisi

Aktif branch:

```text
feature/identity-migration
```

## 4. Tamamlanan Modüller

- Dashboard / Ana Panel
- Category CRUD
- Product CRUD
- Customer / Cari CRUD
- StockMovement işlemleri
- Sale işlemleri
- Modern sidebar layout
- Modern dashboard
- Modern liste ekranları
- Login ekranı
- ASP.NET Identity altyapısı
- Role bazlı controller yetkilendirme

## 5. Identity Tarafında Yapılanlar

- `AppDbContext`, `IdentityDbContext` kullanacak hale getirildi.
- `Program.cs` içinde Identity servisleri eklendi.
- `UseAuthentication()` middleware'i eklendi.
- `AccountController` login/logout işlemleri `SignInManager<IdentityUser>` ile çalışıyor.
- `IdentitySeedData` ile roller ve kullanıcılar seed ediliyor.
- `AccessDenied` action ve view eklendi.

Not: Identity tabloları için migration/database update yapılmış olmalıdır.

## 6. Mevcut Roller

- `Admin`
- `Manager`
- `SalesUser`
- `WarehouseUser`
- `Viewer`

## 7. Test Kullanıcıları

Seed edilen kullanıcılar:

- `admin / Admin123!` - `Admin`

Development ortamında ayrıca:

- `manager / Test123!` - `Manager`
- `sales / Test123!` - `SalesUser`
- `warehouse / Test123!` - `WarehouseUser`
- `viewer / Test123!` - `Viewer`

Production ortamında development test kullanıcıları seed edilmez.

## 8. Controller Yetkilendirme Planı

- `HomeController`: `Admin`, `Manager`, `Viewer`
- `CategoryController`: `Admin`
- `ProductController`: `Admin`, `WarehouseUser`
- `CustomerController`: `Admin`, `SalesUser`
- `StockMovementController`: `Admin`, `WarehouseUser`
- `SaleController`: `Admin`, `SalesUser`

`AccountController` login/logout için açık kalmalıdır.

## 9. UI Kuralları

- Kullanıcının gördüğü metinler Türkçe olmalı.
- Backend isimleri İngilizce kalmalı.
- Modern admin panel çizgisi korunmalı.
- Sidebar + beyaz kart + temiz tablo tasarımı kullanılmalı.
- Dashboard tasarım dili yeni ekranlarda referans alınmalı.
- Gereksiz animasyon ve karmaşık JavaScript eklenmemeli.

## 10. Dikkat Edilmesi Gereken Teknik Borçlar

- Bazı dokümanlarda eski encoding kaynaklı bozuk Türkçe karakterler var.
- Sale delete şu an stoğu geri almıyor; ileride satış iptal/iade mantığı kurulmalı.
- StockMovement delete de stoğu geri almıyor; bu şimdilik bilinçli bırakıldı.
- Create/Edit/Delete ekranlarının tamamı modernleştirilmeye devam edilebilir.
- Edit/Delete modal refactor tüm ekranlarda kontrol edilerek iyileştirilmeli.
- Yetki dışı erişimlerin `/Account/AccessDenied` sayfasına düzgün düştüğü test edilmeli.
- Identity migration ve seed sırası yeni ortamda özellikle kontrol edilmeli.

## 11. Asla Yapılmaması Gerekenler

- Clean Architecture, CQRS, MediatR veya Repository Pattern ekleme.
- Büyük mimari refactor yapma.
- Controller, Model, Entity, DbSet, tablo veya route isimlerini Türkçeleştirme.
- Mevcut çalışan iş kurallarını gereksiz yere değiştirme.
- Migration alma denmeden migration oluşturma.
- UI değişikliği istenmedikçe mevcut tasarımı bozma.
- Satış silme veya stok hareketi silme davranışını onay almadan değiştirme.

## 12. Sıradaki Önerilen Adım

Önerilen sıradaki küçük adım:

```text
Identity migration durumunu kontrol et, database update yapıldı mı doğrula ve role bazlı erişimleri test et.
```

Sonrasında:

- `AccessDenied` yönlendirmesini doğrula.
- Test kullanıcılarıyla her rolün erişim sınırını kontrol et.
- Teknik borç dosyasını güncel gerçek durumla sadeleştir.

## 13. Test Edilen Son Durum

Role bazlı yetkilendirme test edildi.

Test kullanıcıları:

* `admin / Admin123!`
* `manager / Test123!`
* `sales / Test123!`
* `warehouse / Test123!`
* `viewer / Test123!`

Doğrulanan sonuçlar:

* Kullanıcılar başarıyla login olabiliyor.
* Rollerine uygun ekranlara erişebiliyorlar.
* Yetkisi olmayan ekranlara girmeye çalışınca `/Account/AccessDenied` sayfasına yönleniyorlar.
* AccessDenied sayfası çalışıyor.
* Identity login/logout akışı sorunsuz çalışıyor.

Sıradaki önerilen adım:

```text
Identity migration branch’ini main branch’e merge etmeden önce son build ve git kontrolü yapmak.
```
