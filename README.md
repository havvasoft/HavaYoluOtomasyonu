✈️ AeroSys - Hava Yolu Otomasyon Sistemi
📌 Proje Özeti
Bu proje, uçuş seferlerinin, bilet satışlarının, personel ve yolcu kayıtlarının dijital ortamda yönetilmesini sağlayan web tabanlı bir otomasyon sistemidir.

Sistemde Admin (Yönetici), Staff (Personel) ve Customer (Müşteri) olmak üzere üç temel kullanıcı rolü ve yetki seviyesi bulunmaktadır.

Yöneticiler ve personeller; uçuşları, biletleri ve havalimanı verilerini merkezi bir Yönetim Paneli (Dashboard) üzerinden kontrol eder.

Müşteriler sisteme üye olarak aktif uçuşları görüntüleyebilir ve kendi adlarına bilet oluşturabilirler.

Sistem altyapısı, güvenliği sağlamak amacıyla rol tabanlı yetkilendirme (Role-Based Authorization) kilitleriyle korunmaktadır.

💻 Geliştirme Ortamı ve Teknolojiler
Programlama Dili: C#

Mimari / Framework: ASP.NET Core MVC

Veri Tabanı: Microsoft SQL Server

ORM (Nesne-İlişkisel Eşleme): Entity Framework Core

Ön Yüz (Front-End) Tasarımı: HTML5, CSS3, Bootstrap 5, Razor Web Syntax

Geliştirme Aracı (IDE): Visual Studio 2022

Versiyon Kontrol Sistemi: Git & GitHub

⚙️ Projenin Yüklenmesi ve Çalıştırılması
Adım 1: Projenin GitHub deposunu bilgisayarınıza klonlayın.

Adım 2: İndirilen proje klasörünü Visual Studio üzerinden açın.

Adım 3: Teslim edilen SQL betik (script) dosyasındaki komutları SQL Server Management Studio (SSMS) üzerinde çalıştırarak gerekli veri tabanını ve tabloları oluşturun.

Adım 4: Proje içerisindeki appsettings.json dosyasında yer alan ConnectionStrings (Bağlantı Dizesi) ayarlarını kendi yerel SQL sunucunuza göre güncelleyin.

Adım 5: Visual Studio üzerinden projeyi derleyin (Build) ve çalıştırın (F5).

Adım 6: Tüm yetkilere sahip yönetim panelini test etmek için aşağıdaki varsayılan bilgileriyle sisteme giriş yapabilirsiniz:

Yetki Türü: Admin

E-posta: admin@aerosys.com

Şifre: 123
