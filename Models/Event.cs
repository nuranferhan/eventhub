using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Models
{
    /// <summary>
    /// Etkinlik ana modeli. Her etkinlik bir organizatör tarafından oluşturulur
    /// ve katılımcı kontenjanına sahiptir.
    /// </summary>
    public class Event
    {
        // Birincil anahtar - otomatik artan
        [Key]
        public int Id { get; set; }

        // Etkinlik başlığı - zorunlu, maksimum 150 karakter
        [Required(ErrorMessage = "Etkinlik başlığı zorunludur.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Başlık 5 ile 150 karakter arasında olmalıdır.")]
        [Display(Name = "Etkinlik Başlığı")]
        public string Title { get; set; } = string.Empty;

        // Etkinlik detaylı açıklaması - zorunlu
        [Required(ErrorMessage = "Etkinlik açıklaması zorunludur.")]
        [StringLength(2000, MinimumLength = 20, ErrorMessage = "Açıklama en az 20 karakter olmalıdır.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        // Kısa özet - liste görünümlerinde kullanılır
        [StringLength(300, ErrorMessage = "Özet en fazla 300 karakter olmalıdır.")]
        [Display(Name = "Kısa Özet")]
        public string? Summary { get; set; }

        // Etkinlik tarihi ve saati - zorunlu
        [Required(ErrorMessage = "Etkinlik tarihi zorunludur.")]
        [Display(Name = "Tarih ve Saat")]
        public DateTime EventDate { get; set; }

        // Etkinlik bitiş tarihi
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? EndDate { get; set; }

        // Etkinlik konumu / adresi
        [Required(ErrorMessage = "Konum bilgisi zorunludur.")]
        [StringLength(300, ErrorMessage = "Konum en fazla 300 karakter olmalıdır.")]
        [Display(Name = "Konum")]
        public string Location { get; set; } = string.Empty;

        // Online etkinlik için URL (opsiyonel)
        [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
        [Display(Name = "Online Bağlantı")]
        public string? OnlineUrl { get; set; }

        // Maksimum katılımcı sayısı - 0 ise sınır yok
        [Range(0, 10000, ErrorMessage = "Kontenjan 0 ile 10000 arasında olmalıdır.")]
        [Display(Name = "Maksimum Katılımcı")]
        public int MaxCapacity { get; set; } = 0;

        // Etkinlik kategorisi (Workshop, Konferans, Seminer vb.)
        [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
        [Display(Name = "Kategori")]
        public EventCategory Category { get; set; }

        // Etkinlik durumu (Yayın, Taslak, İptal, Tamamlandı)
        [Display(Name = "Durum")]
        public EventStatus Status { get; set; } = EventStatus.Draft;

        // Etkinlik kapak görseli URL'i
        [Display(Name = "Kapak Görseli")]
        public string? CoverImageUrl { get; set; }

        // Ücretli mi, ücretsiz mi?
        [Display(Name = "Ücretli Etkinlik")]
        public bool IsPaid { get; set; } = false;

        // Ücret miktarı (ücretli ise)
        [Range(0, 99999, ErrorMessage = "Ücret geçerli bir değer olmalıdır.")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Ücret (TL)")]
        public decimal Price { get; set; } = 0;

        // Kayıt oluşturulma tarihi
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Son güncelleme tarihi
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Yabancı anahtar: Etkinliği oluşturan kullanıcı
        public string CreatedByUserId { get; set; } = string.Empty;

        // Navigasyon: Etkinliği oluşturan kullanıcı nesnesi
        [ForeignKey("CreatedByUserId")]
        public ApplicationUser? CreatedBy { get; set; }

        // Navigasyon: Bu etkinliğe yapılan kayıtlar koleksiyonu
        public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();

        // Hesaplanan özellik: Mevcut katılımcı sayısı
        [NotMapped]
        public int CurrentParticipantCount => Registrations.Count(r => r.Status == RegistrationStatus.Confirmed);

        // Hesaplanan özellik: Kontenjan dolu mu?
        [NotMapped]
        public bool IsFullyBooked => MaxCapacity > 0 && CurrentParticipantCount >= MaxCapacity;

        // Hesaplanan özellik: Kalan kontenjan sayısı
        [NotMapped]
        public int RemainingCapacity => MaxCapacity == 0 ? int.MaxValue : MaxCapacity - CurrentParticipantCount;
    }

    /// <summary>
    /// Etkinlik kategorileri enumerasyonu
    /// </summary>
    public enum EventCategory
    {
        [Display(Name = "Workshop")]
        Workshop = 1,

        [Display(Name = "Konferans")]
        Conference = 2,

        [Display(Name = "Seminer")]
        Seminar = 3,

        [Display(Name = "Networking")]
        Networking = 4,

        [Display(Name = "Hackathon")]
        Hackathon = 5,

        [Display(Name = "Eğitim")]
        Training = 6,

        [Display(Name = "Diğer")]
        Other = 7,

        [Display(Name = "Konser")]
        Concert = 8,

        [Display(Name = "Sinema")]
        Cinema = 9,

        [Display(Name = "Tiyatro")]
        Theater = 10,

        [Display(Name = "Spor")]
        Sports = 11,

        [Display(Name = "Festival")]
        Festival = 12,

        [Display(Name = "Sergi")]
        Exhibition = 13,

        [Display(Name = "Seminer Serisi")]
        SeminarSeries = 14
    }

    /// <summary>
    /// Etkinlik durum enumerasyonu
    /// </summary>
    public enum EventStatus
    {
        [Display(Name = "Taslak")]
        Draft = 0,

        [Display(Name = "Yayında")]
        Published = 1,

        [Display(Name = "İptal Edildi")]
        Cancelled = 2,

        [Display(Name = "Tamamlandı")]
        Completed = 3
    }
}
