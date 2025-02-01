# Identity API Authentication & Authorization with .NET 8+

Bu proje, .NET 8+ ile Identity kütüphanesini kullanarak API tabanlı kimlik doğrulama (authentication) ve yetkilendirme (authorization) işlemlerinin nasıl yapılacağını göstermek için hazırlandı. Projede, kullanıcı yönetimi ve token tabanlı doğrulama işlemleri yer almaktadır.

## ✨ Açıklama
.NET 8 ve Identity API kullanarak kimlik doğrulama ve yetkilendirme işlemlerinin nasıl yapılacağını anlamak için basit bir uygulama. Kullanıcı kaydı, giriş, şifre sıfırlama ve JWT tabanlı kimlik doğrulama gibi temel özellikleri içerir.

## 🚀 Özellikler
- ASP.NET Core Identity API kullanımı
- JWT Bearer Token ile kimlik doğrulama
- Kullanıcı işlemleri: kayıt, giriş, e-posta doğrulama, şifre sıfırlama
- İki faktörlü kimlik doğrulama (2FA) desteği
- Kullanıcı bilgilerini yönetme
- In-Memory veritabanı kullanımı (gerçek bir veritabanına taşınabilir)

---

## 🔧 Kurulum ve Çalıştırma
### 1️⃣ Bağımlılıkları Yükleyin
Proje dizinine giderek aşağıdaki komutu çalıştırın:
```sh
 dotnet restore
```

### 2️⃣ Uygulamayı Başlatın
```sh
 dotnet run
```

Uygulama varsayılan olarak `https://localhost:5001` adresinde çalışacaktır.

---

## 📌 API Uç Noktaları
### 🔑 Kimlik Doğrulama
| Yöntem | Endpoint | Açıklama |
|--------|---------|----------|
| `POST` | `/register` | Yeni kullanıcı kaydı oluşturur. |
| `POST` | `/login` | Kullanıcı giriş yapar ve JWT token alır. |
| `POST` | `/refresh` | JWT token yeniler. |
| `GET`  | `/confirmEmail` | E-posta doğrulamasını tamamlar. |
| `POST` | `/resendConfirmationEmail` | E-posta doğrulama kodunu yeniden gönderir. |
| `POST` | `/forgotPassword` | Şifre sıfırlama talebi oluşturur. |
| `POST` | `/resetPassword` | Şifre sıfırlama işlemi yapar. |
| `POST` | `/logout` | Oturumu kapatır. |

### 🔒 Kullanıcı Yönetimi (Yetkilendirme Gerektirir)
| Yöntem | Endpoint | Açıklama |
|--------|---------|----------|
| `POST` | `/manage/2fa` | İki faktörlü kimlik doğrulamasını aç/kapat. |
| `GET`  | `/manage/info` | Kullanıcı bilgilerini getirir. |
| `POST` | `/manage/info` | Kullanıcı bilgilerini günceller. |

---

## 🔑 Kimlik Doğrulama Mekanizması
Bu API, kimlik doğrulama için **Bearer Token** kullanır. Kullanıcı giriş yaptığında, API bir JWT token döner. Yetkilendirilmiş isteklerde bu token’ı **Authorization** başlığı ile göndermek gerekir:

```sh
Authorization: Bearer {JWT_TOKEN}
```

---

## 🏗 Teknolojiler
- .NET 8
- ASP.NET Core Identity
- Entity Framework Core (In-Memory DB)

---

## 📌 Katkıda Bulunma
Geri bildirim veya önerileriniz için **pull request** veya **issue** oluşturabilirsiniz.

📩 Sorularınız için bana ulaşabilirsiniz! 🎉

