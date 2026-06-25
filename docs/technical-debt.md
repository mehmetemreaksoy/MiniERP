# Technical Debt / Eksikler ve İyileştirme Notları

Bu dosya MiniERP projesinde ileride düzeltilecek teknik borçları, eksikleri ve refactor notlarını takip etmek için kullanılır.

Amaç, mevcut çalışan sistemi bozmadan eksikleri kayıt altına almak ve uygun zamanda toplu şekilde düzeltmektir.

---

## 1. Product Edit Decimal Hatası

Product Edit ekranında Price alanında şu hata görülebiliyor:

"The field Price must be a number."

Muhtemel sebep Türkçe virgül formatı ile HTML number input formatının farklı çalışmasıdır.

İleride decimal culture/format ayarı düzenlenecek.

---

## 2. Decimal Precision Uyarıları

EF Core tarafında decimal alanlar için precision uyarısı alınıyor.

İlgili alanlar:

* Product.Price
* Sale.UnitPrice
* Sale.TotalPrice

İleride AppDbContext içinde decimal precision ayarı yapılacak.

Örnek hedef:

decimal(18,2)

---

## 3. Login Ekranı Layout Sorunu

Login ekranında sidebar görünüyor.

Bu doğru değil.

Login ekranı admin panel layoutundan bağımsız, ayrı ve sade bir giriş ekranı olmalıdır.

Hedef:

* Login ekranında sidebar görünmemeli
* Login ekranında admin navbar görünmemeli
* Login ekranı ortalanmış, modern ve sade bir tasarıma sahip olmalı
* Login sayfası ayrı bir layout kullanmalı veya layout kullanmamalı

---

## 4. Dashboard Yetki Sorunu

Şu an dashboard sayfasına login olmadan girilebiliyor.

Bu mantıksız çünkü dashboard da sistemin parçasıdır.

Hedef:

* Sisteme giriş için önce login zorunlu olmalı
* Login olmadan Dashboard dahil hiçbir ERP ekranına erişilmemeli
* Login başarılı olursa kullanıcı Dashboard ekranına yönlendirilmeli
* Logout sonrası kullanıcı Login ekranına yönlendirilmeli

---

## 5. Türkçeleştirme

Mevcut ekranlarda İngilizce metinler bulunuyor.

İleride tüm sistem Türkçeleştirilecek.

Örnek:

* Dashboard → Ana Panel
* Categories → Kategoriler
* Products → Ürünler
* Customers → Cariler / Müşteriler
* Stock Movements → Stok Hareketleri
* Sales → Satışlar
* Create → Ekle
* Edit → Güncelle
* Delete → Sil

Not:

Türkçeleştirme sadece kullanıcı arayüzü için yapılacaktır.

Controller, Model, Entity, Property, DbSet, Migration, tablo ve backend kod isimleri İngilizce kalacaktır.

Amaç, yazılım geliştirme tarafında sektör standardını korurken kullanıcıya Türkçe ve anlaşılır bir arayüz sunmaktır.

---

## 6. CRUD Ekranlarının Modernleştirilmesi

Dashboard tasarımı beğenildi.

Category, Product, Customer, StockMovement ve Sale ekranları da aynı modern admin panel tasarımına uyarlanacak.

Hedef:

* Modern tablo tasarımı
* Kart yapısı
* Tutarlı butonlar
* Daha iyi spacing
* Dashboard ile uyumlu görünüm

---

## 7. Edit ve Delete İşlemlerinin Modal Olması

Şu an Edit ve Delete işlemleri ayrı sayfalarda açılıyor.

İleride bu işlemler modal/popup üzerinden yapılacak.

Hedef:

* Edit modal içinde açılsın
* Delete onayı modal içinde gösterilsin
* Kullanıcı liste ekranından ayrılmadan işlem yapabilsin

---

## 8. Satış Silme ve Stok Geri Alma Mantığı

Şu an satış silindiğinde satış kaydı siliniyor fakat ürün stoğu geri alınmıyor.

Bu şimdilik bilinçli bırakıldı.

Gerçek ERP mantığında satış doğrudan silinmemeli, iptal/iade süreci olmalı.

İleride yapılabilecekler:

* Sale.Status alanı eklemek
* CancelSale işlemi oluşturmak
* İptal nedeni tutmak
* İptal edilen satışta stoğu geri almak
* Satışı tamamen silmek yerine pasif/iptal durumuna almak

---

## 9. Admin Login Sisteminin Geliştirilmesi

Şu an admin kullanıcı bilgileri sabit tutuluyor.

Mevcut bilgiler:

* Username: admin
* Password: Admin123

İleride bu yapı geliştirilecek.

Hedef:

* Kullanıcıları veritabanında tutmak
* Şifreyi düz metin saklamamak
* ASP.NET Identity veya güvenli authentication yapısına geçmek
* Rol bazlı yetkilendirme eklemek

---

## 10. UI Refactor Stratejisi

Mevcut çalışan ekranlar şimdilik bozulmayacak.

UI düzenlemeleri tek tek değil, planlı bir refactor aşamasında yapılacak.

Amaç Codex token/kredi kullanımını azaltmak ve çalışan sistemi gereksiz yere bozmamaktır.
