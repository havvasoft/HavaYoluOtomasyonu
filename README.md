#  AeroSys - Hava Yolu Otomasyon Sistemi Proje Raporu

##  Problem Tanımı
Günümüzde hava yolu şirketlerinin operasyonları (uçuş planlamaları, bilet satışları, personel atamaları ve bagaj takibi) manuel veya birbirinden kopuk sistemler üzerinden yürütüldüğünde veri tutarsızlıkları, operasyonel yavaşlıklar ve güvenlik açıkları ortaya çıkmaktadır. Bu proje kapsamında; yöneticilerin, personellerin ve yolcuların tek bir merkezi sistem üzerinden, kendi yetkileri dahilinde işlem yapabileceği güvenli, hızlı ve tutarlı bir "Hava Yolu Otomasyon Sistemi" geliştirilmesi hedeflenmiştir.

##  Yapılan Araştırmalar
Proje geliştirilmeden önce karşılaşılabilecek veri bütünlüğü sorunlarını çözmek adına ilişkisel veritabanı mimarileri araştırılmıştır. 
* Verilerin tekrara düşmemesi ve 5N normalizasyon kurallarının sağlanması için Microsoft SQL Server üzerinde Primary Key ve Foreign Key kısıtlayıcıları kurgulanmıştır.
* Veritabanı performansını artırmak için Index kullanımı, sık tekrarlanan sorgular için View ve Stored Procedure yapıları, otomatik işlemler (loglama vb.) için Trigger (Tetikleyici) mekanizmaları araştırılıp projeye entegre edilmiştir.
* Kullanıcı arayüzü ve arka plan (backend) iletişiminin kopuk olmaması için ASP.NET Core MVC mimarisinin avantajları incelenmiş ve geliştirme bu yönde yapılmıştır.

##  Yazılım Mimarisi
Projemiz **ASP.NET Core MVC (Model-View-Controller)** mimarisi kullanılarak C# dili ile geliştirilmiştir:
* **Model (Veri Katmanı):** Veritabanı tablolarının (Uçuşlar, Biletler, Yolcular vb.) nesne yönelimli programlama (OOP) mantığıyla C# sınıflarına dönüştürüldüğü kısımdır. Veritabanı iletişimi ADO.NET / Entity Framework Core kullanılarak sağlanmıştır.
* **View (Sunum Katmanı):** Kullanıcıların sistemi kullandığı arayüzdür. HTML5, CSS3, Bootstrap 5 ve Razor Syntax kullanılarak dinamik ve responsive (duyarlı) sayfalar tasarlanmıştır.
* **Controller (İş Mantığı):** Model ile View arasındaki iletişimi sağlayan, yetkilendirme (Authorization) kontrollerinin yapıldığı ve C# kodlarının bulunduğu ana merkezdir.

## ⚙️ Genel Yapı
Sistem genel yapısı itibarıyla üç farklı rol üzerinden (Admin, Staff, Customer) çalışmaktadır:
1. **Admin (Yönetici):** Sisteme tam erişimi vardır. Uçak, havalimanı, personel ekleme ve uçuşları planlama yetkilerine sahiptir.
2. **Staff (Personel):** Kendi departmanına göre yolcu check-in işlemleri, bagaj yüklemeleri ve bilet iptal/onay süreçlerini yönetir.
3. **Customer (Yolcu):** Sisteme üye olarak aktif uçuşları görebilir, kendi biletlerini ve geçmiş uçuş kayıtlarını inceleyebilir.
Veritabanı altyapısında 11 adet birbiriyle ilişkili tablo kullanılmış, veri güvenliği gelişmiş SQL yapıları ile koruma altına alınmıştır.

##  Akış Şeması
<img width="801" height="1171" alt="AkısDiyagramıVTYSdrawio" src="https://github.com/user-attachments/assets/cca6a0ef-cf94-4891-9258-f214797615d5" />

## 📊 Veri Tabanı Diyagramı (ER)
<img width="898" height="816" alt="Screenshot 2026-06-09 131902_edited" src="https://github.com/user-attachments/assets/bfdb336e-0dc2-445f-9af7-fd8047abe006" />
<img width="1585" height="818" alt="Screenshot 2026-06-09 131952_edited" src="https://github.com/user-attachments/assets/4df60194-438e-499b-a5c0-eb435814e67f" />
<img width="1588" height="803" alt="Screenshot 2026-06-09 131933_edited" src="https://github.com/user-attachments/assets/aa6fdc41-dc70-4467-99e5-a0df79236aa1" />

## 📚 Referanslar
1. Microsoft ASP.NET Core Documentation (https://learn.microsoft.com/en-us/aspnet/core/)
2. Microsoft SQL Server (T-SQL) Referansları
3. W3Schools HTML/CSS/Bootstrap Eğitim Kaynakları
