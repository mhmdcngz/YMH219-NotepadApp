namespace YMH219_NotepadApp
{
    // Tek bir notu temsil eder: verisi ve notla ilgili küçük davranışlar (Update, ToString).
    public class Note
    {
        // Özellikler public ve { get; set; }: System.Text.Json sadece public özellikleri serileştirir,
        // dosyadan okurken değerleri geri yazabilmesi için de "set" gerekir.

        // Benzersiz kimlik. Başlıkları aynı olan iki not bile birbirinden ayırt edilebilir.
        public Guid Id { get; set; }

        // = "": Nullable açık olduğu için boş metinle başlatılır, null hatası oluşmaz.
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";

        // Oluşturulma tarihi sonradan değişmez; düzenlenme tarihi her Update()'te güncellenir.
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Parametresiz kurucu. JSON'dan okurken System.Text.Json önce bunu çağırır,
        // sonra dosyadaki Id ve tarihleri bu değerlerin üzerine yazar.
        public Note()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;

            // İkinci kez DateTime.Now yazılmaz; yoksa iki tarih arasında milisaniye farkı oluşabilirdi.
            UpdatedAt = CreatedAt;
        }

        // Yeni not eklerken kullanılır. ": this()": Önce parametresiz kurucu çalışır (Id ve tarihler atanır),
        // sonra başlık ve içerik yazılır. Böylece aynı kod iki kez yazılmaz.
        public Note(string title, string content) : this()
        {
            Title = title;
            Content = content;
        }

        // Düzenlemenin tek giriş noktası (kapsülleme): Form başlığı ve tarihi ayrı ayrı değiştirmez,
        // bu metodu çağırır; böylece düzenlenme tarihini güncellemek unutulamaz.
        public void Update(string title, string content)
        {
            Title = title;
            Content = content;

            UpdatedAt = DateTime.Now;
        }

        // "override": object sınıfındaki ToString() eziliyor (çok biçimlilik).
        // ListBox listedeki nesneleri ToString() ile yazar; ezilmeseydi "YMH219_NotepadApp.Note" yazardı.
        public override string ToString()
        {
            // Ternary operatörü (koşul ? doğruysa : yanlışsa): Başlık boşsa yer tutucu metin göster.
            return string.IsNullOrWhiteSpace(Title) ? "(Başlıksız Not)" : Title;
        }

    }
}