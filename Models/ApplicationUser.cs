using Microsoft.AspNetCore.Identity;

namespace EventHub.Models
{
    /// <summary>
    /// ASP.NET Core Identity üzerine genişletilmiş uygulama kullanıcısı.
    /// Standart Identity alanlarının yanı sıra ek profil bilgilerini barındırır.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        // Kullanıcının tam adı - zorunlu alan
        public string FullName { get; set; } = string.Empty;

        // Kullanıcının biyografi / tanıtım yazısı
        public string? Bio { get; set; }

        // Profil fotoğrafı URL'i (opsiyonel)
        public string? ProfileImageUrl { get; set; }

        // Hesap oluşturulma tarihi
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigasyon: Bu kullanıcının katıldığı etkinlik kayıtları
        public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();

        // Navigasyon: Bu kullanıcının oluşturduğu etkinlikler (Admin/Organizatör için)
        public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();
    }
}
