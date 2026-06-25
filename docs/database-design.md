# Database Design

## Genel Yaklaşım

Bu dosya MiniERP projesinin başlangıç veritabanı tasarımını açıklar.

Veritabanı tasarımı ilk aşamada sade tutulacaktır.

Amaç, ERP mantığını öğrenmek ve temel ilişkileri doğru kurmaktır.

## Ana Tablolar

İlk sürümde kullanılacak temel tablolar:

* Categories
* Products
* Customers
* StockMovements
* Sales

---

## Categories Tablosu

Ürün kategorilerini tutar.

Alanlar:

* Id
* Name
* Description
* CreatedDate

İlişki:

Bir kategorinin birden fazla ürünü olabilir.

Category - Product ilişkisi:

One-to-Many

---

## Products Tablosu

Ürün bilgilerini tutar.

Alanlar:

* Id
* Name
* Description
* Price
* StockQuantity
* CriticalStockLevel
* CategoryId
* CreatedDate

İlişki:

Bir ürün bir kategoriye bağlıdır.

Product - Category ilişkisi:

Many-to-One

---

## Customers Tablosu

Müşteri veya cari bilgilerini tutar.

Alanlar:

* Id
* Name
* Email
* Phone
* Address
* CreatedDate

İlk sürümde müşteri ve tedarikçi ayrımı yapılmayacaktır.

İleride CustomerType alanı eklenebilir.

---

## StockMovements Tablosu

Stok giriş ve çıkış işlemlerini tutar.

Alanlar:

* Id
* ProductId
* MovementType
* Quantity
* Description
* MovementDate

MovementType değerleri:

* In
* Out

İlişki:

Bir ürünün birden fazla stok hareketi olabilir.

Product - StockMovement ilişkisi:

One-to-Many

---

## Sales Tablosu

Satış işlemlerini tutar.

Alanlar:

* Id
* CustomerId
* ProductId
* Quantity
* UnitPrice
* TotalPrice
* SaleDate

İlişkiler:

Bir satış bir müşteriye bağlıdır.

Bir satış bir ürüne bağlıdır.

Customer - Sale ilişkisi:

One-to-Many

Product - Sale ilişkisi:

One-to-Many

---

## Başlangıç İlişki Özeti

Category 1 - N Product

Product 1 - N StockMovement

Customer 1 - N Sale

Product 1 - N Sale

---

## Notlar

İlk sürümde veritabanı sade tutulacaktır.

Gereksiz tablolar eklenmeyecektir.

Önce temel iş akışı çalışır hale getirilecektir.

Daha sonra ihtiyaç oldukça yeni alanlar ve tablolar eklenecektir.
