using System.ComponentModel.DataAnnotations;
using EventHub.Models;
using Microsoft.AspNetCore.Http;

namespace EventHub.ViewModels
{
    // ============================================================
    // KIMLIK DOGRULAMA VIEWMODEL'LARI
    // ============================================================

    /// <summary>
    /// Kullanici kayit formu icin ViewModel.
    /// Data Annotations ile form validasyonu saglanir.
    /// </summary>
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad Soyad zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Ad Soyad 3 ile 100 karakter arasında olmalıdır.")]
        [Display(Name = "Kullanıcı Adı")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Şifre en az 8 karakter olmalıdır.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmemektedir.")]
        [Display(Name = "Şifre Tekrarı")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Biyografi en fazla 500 karakter olmalıdır.")]
        [Display(Name = "Hakkımda")]
        public string? Bio { get; set; }
    }

    /// <summary>
    /// Kullanici giris formu icin ViewModel
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; } = false;
    }

    // ============================================================
    // ETKINLIK VIEWMODEL'LARI
    // ============================================================

    /// <summary>
    /// Etkinlik listesi icin ozet bilgileri tasir
    /// </summary>
    public class EventListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public EventCategory Category { get; set; }
        public EventStatus Status { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentParticipantCount { get; set; }
        public bool IsFullyBooked { get; set; }
        public bool IsPaid { get; set; }
        public decimal Price { get; set; }
        public string? CoverImageUrl { get; set; }
        public string OrganizerName { get; set; } = string.Empty;

        // Kullanicinin bu etkinlige kayitli olup olmadigini gosterir
        public bool IsRegistered { get; set; } = false;
        public RegistrationStatus? UserRegistrationStatus { get; set; }
        public int? RegistrationId { get; set; }
        public string? CheckInCode { get; set; }
        public string? CheckInUrl { get; set; }
        public string? CheckInQrCodeBase64 { get; set; }
        public bool IsWaitlisted => UserRegistrationStatus == RegistrationStatus.Pending;
    }

    /// <summary>
    /// Etkinlik detay sayfasi icin tam bilgileri tasir
    /// </summary>
    public class EventDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? OnlineUrl { get; set; }
        public bool CanViewOnlineUrl { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentParticipantCount { get; set; }
        public int RemainingCapacity { get; set; }
        public bool IsFullyBooked { get; set; }
        public EventCategory Category { get; set; }
        public EventStatus Status { get; set; }
        public bool IsPaid { get; set; }
        public decimal Price { get; set; }
        public string? CoverImageUrl { get; set; }
        public string OrganizerName { get; set; } = string.Empty;
        public string OrganizerEmail { get; set; } = string.Empty;
        public string? OrganizerBio { get; set; }
        public DateTime CreatedAt { get; set; }

        // Mevcut kullanicinin kayit durumu
        public bool IsRegistered { get; set; } = false;
        public RegistrationStatus? RegistrationStatus { get; set; }
        public int? RegistrationId { get; set; }
        public string? CheckInCode { get; set; }
        public string? CheckInUrl { get; set; }
        public string? CheckInQrCodeBase64 { get; set; }
        public bool IsOfflineEvent { get; set; }
        public bool AttendanceConfirmed { get; set; }
        public string? PaymentFullName { get; set; }
        public decimal? PaidAmount { get; set; }
        public DateTime? PaymentCompletedAt { get; set; }
        public string? PaymentReference { get; set; }
        public DateTime? RefundRequestedAt { get; set; }

        // Katilimci listesi (yalnizca Admin/Organizator gorebilir)
        public List<ParticipantViewModel> Participants { get; set; } = new();
    }

    public class EventPaymentViewModel
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur.")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "Ad Soyad 3 ile 120 karakter arasında olmalıdır.")]
        [Display(Name = "Ad Soyad")]
        public string PaymentFullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kart numarası zorunludur.")]
        [RegularExpression(@"^[0-9\s]{16,23}$", ErrorMessage = "Geçerli bir kart numarası giriniz.")]
        [Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Son kullanma tarihi zorunludur.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/[0-9]{2}$", ErrorMessage = "AA/YY formatında giriniz.")]
        [Display(Name = "Son Kullanma Tarihi")]
        public string ExpiryDate { get; set; } = string.Empty;

        [Required(ErrorMessage = "CVV zorunludur.")]
        [RegularExpression(@"^[0-9]{3,4}$", ErrorMessage = "Geçerli bir CVV giriniz.")]
        [Display(Name = "CVV")]
        public string Cvv { get; set; } = string.Empty;
    }

    public class EventRefundViewModel
    {
        public int RegistrationId { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public decimal RefundAmount { get; set; }
        public string? PaymentFullName { get; set; }
        public string? PaymentReference { get; set; }

        [StringLength(500, ErrorMessage = "İade notu en fazla 500 karakter olmalıdır.")]
        [Display(Name = "İade Notu")]
        public string? RefundNote { get; set; }
    }

    /// <summary>
    /// Etkinlik olusturma / duzenleme formu icin ViewModel
    /// </summary>
    public class EventFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Etkinlik başlığı zorunludur.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Başlık 5 ile 150 karakter arasında olmalıdır.")]
        [Display(Name = "Etkinlik Başlığı")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(2000, MinimumLength = 20, ErrorMessage = "Açıklama en az 20 karakter olmalıdır.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "Özet en fazla 300 karakter olmalıdır.")]
        [Display(Name = "Kısa Özet")]
        public string? Summary { get; set; }

        [Required(ErrorMessage = "Etkinlik tarihi zorunludur.")]
        [Display(Name = "Başlangıç Tarihi ve Saati")]
        public DateTime EventDate { get; set; } = DateTime.Now.AddDays(7);

        [Display(Name = "Bitiş Tarihi ve Saati")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Konum zorunludur.")]
        [StringLength(300, ErrorMessage = "Konum en fazla 300 karakter olmalıdır.")]
        [Display(Name = "Konum / Adres")]
        public string Location { get; set; } = string.Empty;

        [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
        [Display(Name = "Online Bağlantı URL")]
        public string? OnlineUrl { get; set; }

        [Range(0, 10000, ErrorMessage = "Kontenjan 0 ile 10000 arasında olmalıdır.")]
        [Display(Name = "Maksimum Katılımcı (0 = sınır yok)")]
        public int MaxCapacity { get; set; } = 0;

        [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
        [Display(Name = "Kategori")]
        public EventCategory Category { get; set; }

        [Display(Name = "Durum")]
        public EventStatus Status { get; set; } = EventStatus.Draft;

        [Display(Name = "Kapak Görseli URL")]
        public string? CoverImageUrl { get; set; }

        [Display(Name = "Kapak Görselini Yükle")]
        public IFormFile? CoverImageFile { get; set; }

        [Display(Name = "Ücretli Etkinlik")]
        public bool IsPaid { get; set; } = false;

        [Range(0, 99999, ErrorMessage = "Geçerli bir ücret giriniz.")]
        [Display(Name = "Ücret (TL)")]
        public decimal Price { get; set; } = 0;
    }

    /// <summary>
    /// Katilimci listesi icin ViewModel
    /// </summary>
    public class ParticipantViewModel
    {
        public int RegistrationId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime RegisteredAt { get; set; }
        public RegistrationStatus Status { get; set; }
        public bool AttendanceConfirmed { get; set; }
        public string? Notes { get; set; }
    }

    // ============================================================
    // ANA SAYFA VIEWMODEL'LARI
    // ============================================================

    /// <summary>
    /// Ana sayfa icin istatistik ve ozet bilgileri tasir
    /// </summary>
    public class HomeViewModel
    {
        // Onsoz etkinlikler (yakin tarihli, yayinda olanlar)
        public List<EventListItemViewModel> FeaturedEvents { get; set; } = new();

        // Genel istatistikler
        public int TotalEvents { get; set; }
        public int TotalUsers { get; set; }
        public int TotalRegistrations { get; set; }
        public int UpcomingEventsCount { get; set; }
    }

    // ============================================================
    // PROFIL VIEWMODEL'LARI
    // ============================================================

    /// <summary>
    /// Kullanici profil sayfasi icin ViewModel
    /// </summary>
    public class UserProfileViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        // Kullanicinin katildigi etkinlikler
        public List<EventListItemViewModel> RegisteredEvents { get; set; } = new();

        // Toplam istatistikler
        public int TotalRegistrations { get; set; }
        public int UpcomingRegistrations { get; set; }
        public int PastRegistrations { get; set; }
    }

    /// <summary>
    /// Profil sayfasi icin kullanim alanlarini tek modelde birlestiren ViewModel.
    /// </summary>
    public class ProfilePageViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public int TotalRegistrations { get; set; }
        public int UpcomingRegistrations { get; set; }
        public int PastRegistrations { get; set; }
        public bool IsEditPanelOpen { get; set; }
        public bool IsPasswordPanelOpen { get; set; }

        public ProfileEditViewModel Edit { get; set; } = new();
        public ChangePasswordViewModel Password { get; set; } = new();
    }

    /// <summary>
    /// Uygulama ici bildirimlerin listelenmesi icin ViewModel.
    /// </summary>
    public class UserNotificationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public bool RequiresAction { get; set; }
        public string? EventTitle { get; set; }
        public int? EventId { get; set; }
        public int? RegistrationId { get; set; }
        public NotificationType Type { get; set; }
    }

    /// <summary>
    /// Profil bilgilerini guncellemek icin ViewModel.
    /// </summary>
    public class ProfileEditViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Kullanıcı adı 3 ile 100 karakter arasında olmalıdır.")]
        [Display(Name = "Kullanıcı Adı")]
        public string FullName { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Biyografi en fazla 500 karakter olmalıdır.")]
        [Display(Name = "Biyografi")]
        public string? Bio { get; set; }

        public string? ProfileImageUrl { get; set; }

        [Display(Name = "Profil Görselini Değiştir")]
        public IFormFile? ProfileImageFile { get; set; }
    }

    /// <summary>
    /// Sifre degistirme formu icin ViewModel.
    /// </summary>
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Mevcut şifre zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mevcut Şifre")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yeni şifre zorunludur.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Yeni şifre en az 8 karakter olmalıdır.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
            ErrorMessage = "Yeni şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir.")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmemektedir.")]
        [Display(Name = "Yeni Şifre Tekrarı")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    // ============================================================
    // ADMIN VIEWMODEL'LARI
    // ============================================================

    /// <summary>
    /// Admin panel anasayfa istatistikleri icin ViewModel
    /// </summary>
    public class AdminDashboardViewModel
    {
        public int TotalEvents { get; set; }
        public int PublishedEvents { get; set; }
        public int DraftEvents { get; set; }
        public int TotalUsers { get; set; }
        public int TotalRegistrations { get; set; }
        public int TodayRegistrations { get; set; }

        // Son eklenen etkinlikler
        public List<EventListItemViewModel> RecentEvents { get; set; } = new();

        // Kategoriye gore etkinlik dagilimi
        public Dictionary<string, int> EventsByCategory { get; set; } = new();
    }
}
