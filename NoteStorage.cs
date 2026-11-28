using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace YMH219_NotepadApp
{
    // Notları dosyaya kaydetme ve dosyadan yükleme işinden sadece bu sınıf sorumlu.
    // Form dosya işlerini bilmez, sadece Load(), Save() ve ExportToText() çağırır (sorumlulukların ayrılması).
    public class NoteStorage
    {
        // JSON ayarları. "static readonly": Program boyunca tek kopya oluşturulur, her kayıtta yeniden oluşturulmaz.
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            // JSON'u tek satır yerine girintili, okunabilir şekilde yaz.
            WriteIndented = true,

            // Türkçe karakterler dosyada "\u015F" gibi kodlar yerine "ş" olarak yazılsın.
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        // Kayıt dosyasının tam yolu. Sadece "get" var: değeri yalnızca kurucuda atanabilir, sonra değiştirilemez.
        public string FilePath { get; }

        // Kurucu: Dosya yolunu hesaplar → C:\Users\<kullanıcı>\AppData\Roaming\YMH219-NotepadApp\notes.json
        public NoteStorage()
        {
            // AppData\Roaming: Uygulamaların kullanıcı verisini sakladığı standart klasör.
            // Program klasörü yerine burası seçildi; bin silinse de notlar kaybolmaz, yazma izni sorunu da olmaz.
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Path.Combine: Klasör adlarını "\" ile doğru şekilde birleştirir.
            string folder = Path.Combine(appData, "YMH219-NotepadApp");

            FilePath = Path.Combine(folder, "notes.json");
        }

        // Dosyadaki notları okuyup liste olarak döndürür.
        public List<Note> Load()
        {
            // Eksik dosya: İlk açılışta dosya henüz yoktur. Hata değil, boş listeyle başlanır.
            if (!File.Exists(FilePath))
            {
                return new List<Note>();
            }

            string json;

            // StreamReader: Dosyayı metin olarak okur.
            // "using": Bloktan çıkınca dosyayı kapatır (hata olsa bile), dosya kilitli kalmaz.
            using (StreamReader reader = new StreamReader(FilePath))
            {
                json = reader.ReadToEnd();
            }

            // Dosya var ama içi boşsa okunacak bir şey yok.
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Note>();
            }

            try
            {
                // Deserialize: JSON metnini List<Note> nesnesine çevirir.
                // "?": Sonuç null olabilir (örn. dosyada sadece "null" yazıyorsa).
                List<Note>? notes = JsonSerializer.Deserialize<List<Note>>(json, JsonOptions);

                // "??": Soldaki null ise sağdakini kullan.
                return notes ?? new List<Note>();
            }
            catch (JsonException ex)
            {
                // Bozuk dosya: Kullanıcının verisi tamamen kaybolmasın diye .bak olarak yedeklenir.
                string backupPath = FilePath + ".bak";

                File.Copy(FilePath, backupPath, overwrite: true);

                // Mesajı göstermek formun işi; burada anlaşılır bir hata fırlatıp forma bildiriyoruz.
                // "ex" asıl hatayı içinde taşır (inner exception).
                throw new InvalidDataException("Not dosyası bozuk olduğu için okunamadı. Bozuk dosya şuraya yedeklendi: \n" + backupPath, ex);
            }
        }

        // Verilen not listesini dosyaya yazar. Hata olursa (disk, izin) exception forma kadar çıkar.
        public void Save(List<Note> notes)
        {
            // "!": Derleyiciye "bu null olmaz" diyoruz, çünkü yolu kurucuda kendimiz oluşturduk.
            string folder = Path.GetDirectoryName(FilePath)!;

            // Klasör yoksa oluşturur, varsa hiçbir şey yapmaz.
            Directory.CreateDirectory(folder);

            // Serialize: Not listesini JSON metnine çevirir.
            string json = JsonSerializer.Serialize(notes, JsonOptions);

            // Önce geçici dosyaya yaz: Yazma yarıda kesilirse (çökme, elektrik) asıl notes.json bozulmaz.
            string tempPath = FilePath + ".tmp";

            // StreamWriter: Dosyaya metin yazar. Dosya varsa içeriğini silip baştan yazar.
            using (StreamWriter writer = new StreamWriter(tempPath))
            {
                writer.Write(json);
            }

            // Yazma başarıyla bittiyse geçici dosyayı asıl dosyanın yerine taşı.
            File.Move(tempPath, FilePath, overwrite: true);
        }

        // Tek bir notu düz metin (.txt) olarak verilen yola yazar: başlık, ayraç çizgisi, tarihler, içerik.
        public void ExportToText(Note note, string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(note.Title);

                // Başlığın altına başlık uzunluğunda (en az 10) "=" çizgisi.
                writer.WriteLine(new string('=', Math.Max(note.Title.Length, 10)));
                writer.WriteLine("Oluşturulma: " + note.CreatedAt.ToString("dd.MM.yyyy HH:mm"));
                writer.WriteLine("Son düzenleme: " + note.UpdatedAt.ToString("dd.MM.yyyy HH:mm"));
                writer.WriteLine();
                writer.Write(note.Content);
            }
        }
    }
}