# Module Plan

## Genel Yaklaşım

MiniERP projesi küçük adımlarla geliştirilecektir.

Amaç, tek seferde büyük bir ERP sistemi yapmak değildir. Amaç, çalışan ve anlaşılır bir ERP çekirdeği oluşturup zamanla büyütmektir.

Her modül önce temel haliyle yapılacak, sonra geliştirilecektir.

## Geliştirme Sırası

### 1. Proje Kurulumu

Amaç:

* ASP.NET Core MVC projesi oluşturmak
* SQL Server bağlantısını yapmak
* Entity Framework Core kurmak
* AppDbContext oluşturmak
* İlk migration almak

Bu aşamada henüz büyük modül geliştirilmeyecektir.

---

### 2. Kategori Yönetimi

Amaç:

Ürünleri sınıflandırmak için kategori yapısı oluşturmak.

Örnek kategoriler:

* Elektronik
* Gıda
* Ofis Malzemeleri
* Hammadde

Yapılacak işlemler:

* Category modeli
* Category tablosu
* Category CRUD işlemleri
* Listeleme
* Ekleme
* Güncelleme
* Silme

---

### 3. Ürün Yönetimi

Amaç:

Sistemde satılan veya stokta takip edilen ürünleri yönetmek.

Yapılacak işlemler:

* Product modeli
* Product - Category ilişkisi
* Ürün listeleme
* Ürün ekleme
* Ürün güncelleme
* Ürün silme
* Ürün stok miktarı takibi

---

### 4. Cari / Müşteri Yönetimi

Amaç:

Müşteri veya firma bilgilerini sistemde tutmak.

Yapılacak işlemler:

* Customer modeli
* Müşteri listeleme
* Müşteri ekleme
* Müşteri güncelleme
* Müşteri silme

İleride müşteri ve tedarikçi ayrımı yapılabilir.

---

### 5. Stok Hareketleri

Amaç:

Ürünlerin stok giriş ve çıkışlarını takip etmek.

Stok hareket türleri:

* Giriş
* Çıkış

Yapılacak işlemler:

* StockMovement modeli
* Ürün seçimi
* Hareket türü seçimi
* Miktar girişi
* Stok miktarını güncelleme
* Stok hareket geçmişi

---

### 6. Satış Yönetimi

Amaç:

Müşteriye ürün satışı kaydetmek.

Yapılacak işlemler:

* Sale modeli
* Customer seçimi
* Product seçimi
* Miktar girişi
* Toplam tutar hesaplama
* Satış sonrası stok düşme

İlk sürümde satış işlemi basit tutulacaktır.

---

### 7. Dashboard

Amaç:

Sistemin genel durumunu tek ekranda göstermek.

Gösterilecek bilgiler:

* Toplam ürün sayısı
* Toplam kategori sayısı
* Toplam müşteri sayısı
* Kritik stoktaki ürünler
* Son stok hareketleri
* Son satışlar

---

### 8. Login ve Kullanıcı Yönetimi

Amaç:

Sisteme yetkisiz erişimi engellemek.

Yapılacak işlemler:

* Login
* Logout
* Admin kullanıcı
* Rol yapısı

Bu modül ilk aşamada sona bırakılabilir. Önce temel ERP mantığı kurulacaktır.

## İleride Eklenebilecek Modüller

* Tedarikçi yönetimi
* Satın alma yönetimi
* Fatura yönetimi
* Depo yönetimi
* Gelişmiş raporlama
* Rol bazlı yetkilendirme
* Loglama
* Audit trail
* API katmanı
* Çok kullanıcılı yapı
