# Current Codex Handoff

## 1. Projenin amacı

MiniERP, ASP.NET Core MVC ile geliştirilen eğitim amaçlı ama kurumsal ERP mantığına doğru büyütülen bir projedir. Amaç; stok, satış, satın alma, cari, tedarikçi, fatura, yetkilendirme ve audit log gibi temel ERP süreçlerini sade ve anlaşılır MVC yapısıyla geliştirmektir.

## 2. Kullanılan teknoloji stack

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Razor Views
- Bootstrap
- Git / GitHub

## 3. Aktif branch ve Git durumu

- Aktif branch: `main`
- Son kontrolde çalışma ağacı temiz değildi.
- Görülen değişiklikler ağırlıklı olarak `SalesInvoice` modülü, `AppDbContext`, `Customer`, `Sale`, `_Layout` ve ilgili migration dosyaları üzerindeydi.
- Kullanıcı değişiklikleri geri alınmayacak. Migration sadece açıkça istendiğinde alınacak veya değiştirilecek.

## 4. Projedeki ana modüller

- Dashboard / Ana Panel
- Identity login/logout ve rol sistemi
- Category
- Product
- Customer
- StockMovement
- Sale
- AuditLog
- Supplier
- Purchase
- SalesInvoice

## 5. Tamamlanan modüller

- Category CRUD
- Product CRUD, soft delete, pasif ürünler ve restore
- Customer CRUD
- StockMovement create/delete ve stok giriş/çıkış mantığı
- Sale create, cancel ve audit log entegrasyonu
- AuditLog altyapısı, servis, admin listeleme, filtreleme ve pagination
- Supplier CRUD, soft delete, pasif tedarikçiler ve restore
- Purchase create ve cancel
- SalesInvoice ilk versiyon: aktif satıştan fatura oluşturma
- Modern sidebar, kart/tablo tabanlı liste ekranları ve Bootstrap modal create/edit/delete akışı

## 6. Identity ve rol sistemi durumu

`AppDbContext`, `IdentityDbContext` kullanıyor. `Program.cs` içinde Identity, authentication, authorization, session, `IHttpContextAccessor` ve `IAuditLogService` kayıtları var.

Roller:

- `Admin`
- `Manager`
- `SalesUser`
- `WarehouseUser`
- `Viewer`

Controller yetkileri:

- `HomeController`: `Admin`, `Manager`, `Viewer`
- `CategoryController`: `Admin`
- `ProductController`: `Admin`, `WarehouseUser`
- `CustomerController`: `Admin`, `SalesUser`
- `StockMovementController`: `Admin`, `WarehouseUser`
- `SaleController`: `Admin`, `SalesUser`
- `SupplierController`: `Admin`
- `PurchaseController`: `Admin`
- `SalesInvoiceController`: `Admin`, `SalesUser`
- `AuditLogController`: `Admin`

## 7. Mevcut test kullanıcıları

Her ortamda:

- `admin / Admin123!` - `Admin`

Sadece Development ortamında:

- `manager / Test123!` - `Manager`
- `sales / Test123!` - `SalesUser`
- `warehouse / Test123!` - `WarehouseUser`
- `viewer / Test123!` - `Viewer`

## 8. AuditLog sistemi durumu

- `AuditLog` modeli ve `AuditLogs` DbSet'i var.
- `IAuditLogService` ve `AuditLogService` merkezi log yazıyor.
- Kullanıcı adı `HttpContext.User.Identity.Name` üzerinden alınıyor; kullanıcı yoksa `System` yazılıyor.
- IP adresi `HttpContext.Connection.RemoteIpAddress` üzerinden alınıyor.
- CRUD/cancel/restore/create invoice işlemlerinde audit log kullanılmaya başlandı.
- `AuditLogController` sadece Admin rolüne açık.
- AuditLog ekranında `searchText`, `userName`, `actionType`, `entityName`, `startDate`, `endDate` filtreleri ve 20 kayıtlık pagination var.
- Not: Filtre parametresi `action` değil `actionType` olmalı; `action` MVC route değeriyle çakışır.

## 9. Soft delete kullanılan yerler

- `Product`: `IsDeleted`, `DeletedDate`; Index sadece aktif ürünleri gösterir. Delete fiziksel silmez, pasife alır. Passive ve Restore ekranları var.
- `Supplier`: `IsDeleted`, `DeletedDate`; Index sadece aktif tedarikçileri gösterir. Delete fiziksel silmez, pasife alır. Passive ve Restore ekranları var.
- Diğer modellerde şimdilik soft delete yok.

## 10. Satış ve satış iptal mantığı

- Sale oluşturulurken sadece aktif ürünler dropdown'da listelenir.
- Sale oluşturulurken ürün stoğu satış miktarı kadar düşer.
- `Sale.Status` yeni satışta `Active` olur.
- Cancel işlemi satış zaten `Cancelled` ise tekrar iptal etmez.
- Cancel işlemi `Status = "Cancelled"`, `CancelledDate = DateTime.Now`, `CancelReason = ...` yapar ve ürün stoğunu satış miktarı kadar geri ekler.
- Sale Index'ten silme erişimi kaldırıldı; iptal sistemi kullanılmalı.
- Delete action/view kalmış olabilir; stok davranışı değiştirilmeden bırakıldı.

## 11. Satın alma ve satın alma iptal mantığı

- Purchase sadece Admin rolüne açık.
- Create işleminde aktif tedarikçiler ve aktif ürünler seçilir.
- `TotalPrice = Quantity * UnitPrice`.
- Yeni Purchase `Status = "Active"` olur.
- Create ürün stoğunu satın alma miktarı kadar artırır.
- Cancel işlemi zaten `Cancelled` olan kaydı tekrar iptal etmez.
- Cancel sırasında ürün yoksa hata verir.
- Ürün stoğu satın alma miktarından düşükse iptal edilmez ve Türkçe hata gösterilir.
- Cancel başarılıysa ürün stoğu miktar kadar düşer; `Status`, `CancelledDate`, `CancelReason` güncellenir.

## 12. Satış faturası modülü durumu

- `SalesInvoice` modeli var.
- `SalesInvoices` DbSet'i var.
- `Sale` modelinde `SalesInvoice?` navigation property var.
- `Customer` modelinde `ICollection<SalesInvoice>` var.
- Controller `Admin` ve `SalesUser` rollerine açık.
- Sadece `Active` ve henüz faturalanmamış satışlardan fatura oluşturulur.
- Fatura numarası örnek formatla üretilir: `INV-{year}-{0000}`.
- `SubTotal = Sale.TotalPrice`, `VatRate = 20`, `VatAmount = SubTotal * 0.20`, `GrandTotal = SubTotal + VatAmount`, `Status = "Issued"`.
- `SalesInvoice -> Sale` ve `SalesInvoice -> Customer` ilişkilerinde cascade delete kapalıdır: `DeleteBehavior.NoAction`.
- Not: Fatura iptal süreci henüz yok. Fatura numarası üretimi count tabanlı olduğu için ileride concurrency açısından güçlendirilebilir.

## 13. Tedarikçi modülü durumu

- `Supplier` modeli var.
- Alanlar: `Name`, `ContactPerson`, `Email`, `Phone`, `Address`, `TaxNumber`, `IsDeleted`, `CreatedDate`, `DeletedDate`.
- Supplier sadece Admin rolüne açık.
- Index aktif tedarikçileri listeler.
- Create/Edit/Delete Bootstrap modal yapısıyla çalışır.
- Delete fiziksel silmez; pasife alır.
- Passive ekranı pasif tedarikçileri `DeletedDate` descending listeler.
- Restore tedarikçiyi yeniden aktifleştirir.
- Purchase için `Supplier.Purchases` navigation property var.

## 14. Mevcut önemli iş kuralları

- Backend isimleri İngilizce kalacak: controller, model, entity, property, DbSet, tablo, migration, route.
- UI metinleri Türkçe olacak.
- Product price/culture decimal fix korunacak.
- Sale create stok düşürme kuralı bozulmayacak.
- Sale cancel stok geri ekleme kuralı bozulmayacak.
- Purchase create stok artırma kuralı bozulmayacak.
- Purchase cancel stok geri düşme ve yetersiz stok kontrolü bozulmayacak.
- StockMovement create `In` için stok artırır, `Out` için yeterli stok varsa düşer.
- StockMovement delete stoğu geri almaz; bu davranış onay olmadan değiştirilmez.
- Product ve Supplier soft delete davranışı korunacak.

## 15. UI kuralları

- Modern, sade, profesyonel admin panel çizgisi korunacak.
- Sidebar ve üst header bozulmayacak.
- Liste ekranlarında kart/tablo görünümü korunacak.
- Üst aksiyon butonları mümkünse sağ üstte hizalı olacak.
- Layout zaten sayfa başlığı gösterdiği için içerikte büyük tekrar başlıklar eklenmeyecek.
- Create/Edit/Delete işlemleri mümkün oldukça Bootstrap modal ile yapılacak.
- Gereksiz animasyon, karmaşık JavaScript ve büyük UI refactor yapılmayacak.

## 16. Dikkat edilmesi gereken teknik borçlar

- Bazı eski docs dosyalarında encoding kaynaklı bozuk Türkçe karakterler var.
- Çalışma ağacında SalesInvoice ve migration tarafında commitlenmemiş değişiklikler olabilir; başlamadan `git status --short` kontrol edilmeli.
- Migration dosyalarına dokunmak veya yeni migration almak sadece açık istekle yapılmalı.
- AuditLog entity filtre seçenekleri yeni modüllerle uyumlu mu kontrol edilebilir.
- Modal validation UX bazı ekranlarda sınırlı olabilir; invalid POST sonrası modal otomatik açılmayabilir.
- SalesInvoice cancel/void süreci henüz yok.
- Fatura numarası üretimi ileride daha güvenli hale getirilebilir.
- Login ekranı/layout ve eski docs notları güncel gerçek durumla zamanla sadeleştirilebilir.

## 17. Asla yapılmaması gerekenler

- Clean Architecture ekleme.
- CQRS ekleme.
- MediatR ekleme.
- Repository Pattern ekleme.
- Gereksiz büyük refactor yapma.
- Backend isimlerini Türkçeleştirme.
- Migration alma denmeden migration oluşturma.
- Kullanıcının değişikliklerini geri alma.
- Controller/model/view dosyalarını görev dışı değiştirme.
- Mevcut stok, satış, satın alma, soft delete, Identity veya role authorization davranışlarını onaysız değiştirme.

## 18. Sıradaki önerilen geliştirme adımları

Küçük adımlarla ilerlenmeli:

1. SalesInvoice migration/database durumunu açık istek gelirse netleştir.
2. SalesInvoice ekranını test et; aktif ve faturalanmamış satış seçimi doğru mu kontrol et.
3. AuditLog filtre entity seçeneklerini Supplier, Purchase, SalesInvoice ve Cancel/Restore aksiyonlarıyla güncelle.
4. SalesInvoice iptal/void sürecini tasarla.
5. Teknik borç dokümanlarını güncel gerçek duruma göre sadeleştir.

## 19. Yeni Codex session çalışma kuralları

- Her işlemden önce bu dosya okunacak.
- Yeni sessionlarda önce sadece `docs/current-codex-handoff.md` okunacak.
- Sonra yalnızca görevle ilgili model/controller/view dosyalarına bakılacak.
- Token/context tasarrufu için tüm proje her seferinde taranmayacak.
- Görev küçük parçalara bölünecek.
- Sadece gerekli dosyalar değiştirilecek.
- Kod yazmadan önce mevcut dosya davranışı kısa kontrol edilecek.
- İş bitince değişen dosyalar ve test komutu kısa yazılacak.
- Build gerekirse: `dotnet build src/MiniERP.Web/MiniERP.Web.csproj`
- Normal build dosya kilidine takılırsa alternatif output kullanılabilir: `dotnet build src/MiniERP.Web/MiniERP.Web.csproj -o artifacts/build-check`
