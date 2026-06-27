# Project State

MiniERP, ASP.NET Core MVC ile geliştirilen eğitim amaçlı ERP projesidir.

## Mevcut Durum

Proje klasik MVC mimarisiyle geliştiriliyor.

Backend isimlendirmeleri İngilizce kalacaktır.
Kullanıcı arayüzü Türkçe olacaktır.

## Tamamlanan Modüller

- Category CRUD
- Product CRUD
- Customer CRUD
- StockMovement
- Sale
- Dashboard
- Session based Admin Login
- Modern sidebar layout
- Modern dashboard
- Modern index/list screens

## Önemli Kurallar

- Clean Architecture kullanılmayacak.
- Repository Pattern eklenmeyecek.
- Async şimdilik kullanılmayacak.
- Controller, Model, DbSet, tablo isimleri İngilizce kalacak.
- Kullanıcının gördüğü metinler Türkçe olacak.
- Mevcut çalışan iş kuralları bozulmayacak.
- Codex sadece görevle ilgili dosyaları inceleyecek.
- Gereksiz refactor yapılmayacak.

## Bilinen Eksikler

- Edit/Delete işlemleri modal popup olacak.
- Create/Edit/Delete ekranları modernleştirilecek.
- Tüm UI metinleri tamamen Türkçeleştirilecek.
- Sale delete yerine ileride satış iptal/iade mantığı kurulacak.
- Admin login ileride Identity veya güvenli kullanıcı sistemiyle değiştirilecek.

## Tasarım Çizgisi

Dashboard tasarımı beğenildi.
Yeni ekranlar dashboard ve sidebar tasarımıyla uyumlu olmalıdır.

Modern, sade, profesyonel ERP/admin panel görünümü korunmalıdır.