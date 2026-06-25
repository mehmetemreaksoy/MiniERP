# Learning Log

Bu dosya proje boyunca öğrenilen konuları ve yapılan işlemleri takip etmek için kullanılacaktır.

Amaç, sadece kod yazmak değil, yapılan işi anlamaktır.

Her önemli adımdan sonra bu dosyaya kısa not eklenecektir.

---

## Kayıt Formatı

Her yeni öğrenme notu şu formatta yazılacaktır:

### Tarih

Konu:

Yapılan işlem:

Değişen dosyalar:

Ne öğrendim?

Zorlandığım yer:

Sonraki adım:

---

## Örnek Kayıt

### 2026-06-24

Konu:

Proje dokümantasyonu hazırlığı

Yapılan işlem:

MiniERP projesi için başlangıç dokümantasyon dosyaları oluşturuldu.

Değişen dosyalar:

* project-overview.md
* ai-working-rules.md
* module-plan.md
* database-design.md
* learning-log.md

Ne öğrendim?

Bir projeye başlamadan önce proje amacı, modül planı, veritabanı tasarımı ve AI çalışma kurallarının belirlenmesi gerektiğini öğrendim.

Zorlandığım yer:

Projenin çok büyük başlamaması gerektiğini anlamak.

Sonraki adım:

ASP.NET Core MVC projesini oluşturmak.


### 2026-06-25

Konu:

Category CRUD modülü

Yapılan işlem:

MiniERP projesinde kategori yönetimi için listeleme, ekleme, güncelleme ve silme işlemleri yapıldı.

Değişen dosyalar:

- Models/Category.cs
- Data/AppDbContext.cs
- Controllers/CategoryController.cs
- Views/Category/Index.cshtml
- Views/Category/Create.cshtml
- Views/Category/Edit.cshtml
- Views/Category/Delete.cshtml
- appsettings.json
- Program.cs

Ne öğrendim?

ASP.NET Core MVC’de bir CRUD modülünün Model, Controller, View ve DbContext birlikte çalışarak oluşturulduğunu öğrendim.

Category modeli veritabanındaki Categories tablosunu temsil ediyor.

CategoryController kullanıcı isteklerini karşılıyor, AppDbContext üzerinden veritabanı işlemi yapıyor ve sonucu View dosyalarına gönderiyor.

View dosyaları kullanıcıya listeleme, ekleme, güncelleme ve silme ekranlarını gösteriyor.

Zorlandığım yer:

Port kullanım hatasının proje hatası olmadığını, uygulama zaten çalışırken aynı porttan tekrar başlatılmaya çalışıldığı için oluştuğunu öğrendim.

Sonraki adım:

Product modelini oluşturmak ve Product ile Category arasında ilişki kurmak.

### 2026-06-25

Konu:

Product CRUD modülü

Yapılan işlem:

MiniERP projesinde ürün yönetimi için listeleme, ekleme, güncelleme ve silme işlemleri oluşturuldu. Product ile Category arasında ilişki kuruldu.

Değişen dosyalar:

- Models/Product.cs
- Models/Category.cs
- Data/AppDbContext.cs
- Controllers/ProductController.cs
- Views/Product/Index.cshtml
- Views/Product/Create.cshtml
- Views/Product/Edit.cshtml
- Views/Product/Delete.cshtml

Ne öğrendim?

Product tablosunun Category tablosuna CategoryId ile bağlandığını öğrendim.

Ürün ekleme ve güncelleme ekranlarında kategori seçimi için dropdown kullanıldığını gördüm.

Index ekranında ürünün kategori bilgisinin gösterilebilmesi için ilişkili verinin de getirilmesi gerektiğini öğrendim.

Zorlandığım yer:

Product Edit ekranında Price alanında "The field Price must be a number." hatası görüldü. Bu hata büyük ihtimalle decimal sayıların Türkçe virgül ve HTML number input nokta formatı farkından kaynaklanıyor. Şimdilik ertelendi.

Sonraki adım:

Cari / müşteri yönetimi modülüne geçmek.

### 2026-06-25

Konu:

Customer / Cari CRUD modülü

Yapılan işlem:

MiniERP projesinde müşteri/cari yönetimi için listeleme, ekleme, güncelleme ve silme işlemleri oluşturuldu. Otomotiv sektörü için örnek müşteri verileri SQL insert komutlarıyla eklendi.

Değişen dosyalar:

- Models/Customer.cs
- Data/AppDbContext.cs
- Controllers/CustomerController.cs
- Views/Customer/Index.cshtml
- Views/Customer/Create.cshtml
- Views/Customer/Edit.cshtml
- Views/Customer/Delete.cshtml

Ne öğrendim?

Customer modeli veritabanındaki Customers tablosunu temsil ediyor.

CustomerController, müşteri verilerini listeleme, ekleme, düzenleme ve silme işlemlerini yönetiyor.

Bu modülde Product modülünden farklı olarak ilişki/dropdown yok. Bu yüzden CRUD yapısı daha sade.

SQL insert komutlarıyla veritabanına toplu test verisi eklenebileceğini öğrendim.

Zorlandığım yer:

Bu adımda büyük bir sorun yaşanmadı.

Sonraki adım:

Stok hareketleri modülüne geçmek.

### 2026-06-25

Konu:

StockMovement / Stok Hareketleri modülü

Yapılan işlem:

MiniERP projesinde ürünlere stok giriş ve stok çıkış işlemleri yapılabilecek stok hareketleri modülü oluşturuldu.

Değişen dosyalar:

- Models/StockMovement.cs
- Models/Product.cs
- Data/AppDbContext.cs
- Controllers/StockMovementController.cs
- Views/StockMovement/Index.cshtml
- Views/StockMovement/Create.cshtml
- Views/StockMovement/Delete.cshtml

Ne öğrendim?

StockMovement modeli ürünlerin stok giriş ve çıkış geçmişini tutuyor.

Product ile StockMovement arasında one-to-many ilişki kuruldu.

Stok hareketi eklenirken sadece StockMovement tablosuna kayıt atılmıyor, aynı zamanda Product.StockQuantity alanı da güncelleniyor.

MovementType "In" ise stok artıyor.

MovementType "Out" ise stok azalıyor.

Çıkış miktarı mevcut stoktan büyükse sistem hata veriyor ve stok eksiye düşmüyor.

Bu adımda basit CRUD’dan iş kuralı içeren bir yapıya geçildi.

Zorlandığım yer:

Hata mesajının frontend mi backend mi üretildiği karıştı. Mesajın büyük ihtimalle Controller tarafında ModelState.AddModelError ile üretildiği anlaşıldı.

Sonraki adım:

Satış yönetimi modülüne geçmek.

### 2026-06-25

Konu:

Sale / Satış Yönetimi modülü

Yapılan işlem:

MiniERP projesinde müşteri ve ürün seçilerek satış kaydı oluşturulabilecek satış modülü geliştirildi.

Değişen dosyalar:

- Models/Sale.cs
- Models/Customer.cs
- Models/Product.cs
- Data/AppDbContext.cs
- Controllers/SaleController.cs
- Views/Sale/Index.cshtml
- Views/Sale/Create.cshtml
- Views/Sale/Delete.cshtml

Ne öğrendim?

Sale modeli satış kayıtlarını temsil ediyor.

Sale ile Customer arasında ilişki kuruldu.

Sale ile Product arasında ilişki kuruldu.

Satış oluşturulurken UnitPrice kullanıcıdan alınmıyor, seçilen ürünün Price değerinden geliyor.

TotalPrice, Quantity * UnitPrice şeklinde backend tarafında hesaplanıyor.

Satış başarılı olursa Product.StockQuantity satış miktarı kadar azalıyor.

Stok yetersizse sistem hata veriyor, satış kaydı oluşturulmuyor ve stok değişmiyor.

Zorlandığım yer:

Satış silindiğinde stok geri alınmadı. Bunun sebebi, ilk sürümde Delete işleminin sadece satış kaydını silmesi olarak tasarlanmasıdır. Gerçek ERP sistemlerinde satış silmek yerine iptal/iade sistemi kullanmak daha doğru olur.

Sonraki adım:

Dashboard ekranı oluşturmak.

### 2026-06-25

Konu:

Dashboard UI modernizasyonu

Yapılan işlem:

Dashboard ekranı modern ERP/admin panel görünümüne çevrildi. Sol sidebar, üst header, modern kartlar ve tablo yapıları eklendi.

Ne öğrendim?

Layout dosyası değiştirilerek tüm uygulamanın genel görünümü etkilenebilir.

Dashboard sadece veri göstermek değil, aynı zamanda sistemin profesyonel görünümünü belirleyen ana ekrandır.

Zorlandığım yer:

İlk dashboard tasarımı çok sade ve default MVC görünümündeydi. Daha net tasarım promptu verilince Codex çok daha iyi sonuç üretti.

Sonraki adım:

Mevcut ekranları şimdilik koruyup, ileride toplu UI refactor aşamasında Türkçeleştirme, modern tablo yapısı ve modal edit/delete işlemlerini yapmak.