namespace YMH219_NotepadApp
{
    // Ana pencere. "partial": Sınıfın diğer yarısı (kontrollerin oluşturulması) MainForm.Designer.cs'te.
    // ": Form": Windows Forms'un Form sınıfından türetildi (kalıtım).
    public partial class MainForm : Form
    {
        // Dosya işlerini yapan nesne. Form dosyayla doğrudan uğraşmaz, her şeyi bu nesneye yaptırır.
        private readonly NoteStorage storage = new NoteStorage();

        // Tüm notlar uygulama çalışırken bellekte bu listede tutulur. Dosya sadece kalıcı saklama için.
        private List<Note> notes = new List<Note>();

        public MainForm()
        {
            // Designer'daki kontrolleri oluşturur. Kontrollere erişmeden önce çağrılmalı.
            InitializeComponent();

            // Olaylara abone ol ("+=": bu olay olunca şu metodu çağır).
            // Hepsi burada toplandığı için hangi kontrolün ne yaptığı tek yerden görülür.
            Load += MainForm_Load;
            lstNotes.SelectedIndexChanged += LstNotes_SelectedIndexChanged;
            mnuExit.Click += MnuExit_Click;

            // Menü ve buton aynı işi yapıyorsa aynı metoda bağlanır, kod tekrarlanmaz.
            btnNew.Click += NewNote_Click;
            mnuNew.Click += NewNote_Click;
            btnSave.Click += SaveNote_Click;
            btnDelete.Click += DeleteNote_Click;
            mnuDelete.Click += DeleteNote_Click;
            btnExport.Click += ExportNote_Click;
            mnuExport.Click += ExportNote_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        // Form açılırken bir kez çalışır: notları dosyadan yükleyip listeyi doldurur.
        private void MainForm_Load(object? sender, EventArgs e)
        {
            LoadNotes();
            RefreshList();
        }

        // Notları dosyadan okur. Hata olursa uygulama çökmez, kullanıcıya mesaj gösterilir
        // ve boş listeyle devam edilir.
        private void LoadNotes()
        {
            try
            {
                notes = storage.Load();
                SetStatus(notes.Count + " not yüklendi.");
            }
            catch (InvalidDataException ex)
            {
                // Bozuk dosya: NoteStorage .bak yedeğini aldı ve mesajı hazırladı, biz sadece gösteriyoruz.
                notes = new List<Note>();
                MessageBox.Show(ex.Message, "Bozuk dosya", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetStatus("Not dosyası bozuktu, boş listeyle başlandı.");
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                // "when": Sadece dosya/izin hatalarını yakala. Beklenmeyen hatalar gizlenmesin.
                notes = new List<Note>();
                MessageBox.Show("Notlar yüklenemedi:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Notlar yüklenemedi.");
            }
        }

        // Tüm notları dosyaya yazar. Başarılıysa true, hata olursa mesaj gösterip false döndürür.
        // Ekleme, düzenleme ve silmeden sonra çağrılır (otomatik kaydetme).
        private bool SaveNotes()
        {
            try
            {
                storage.Save(notes);
                return true;
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("Notlar kaydedilemedi:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Kaydetme başarısız oldu.");
                return false;
            }
        }

        // Not, aranan metni başlığında veya içeriğinde içeriyor mu?
        // CurrentCultureIgnoreCase: Büyük/küçük harfe bakmaz, Türkçe kurallarına uyar ("ödev" = "Ödev").
        // "static": Formun hiçbir alanını kullanmadığı için nesneye bağlı olması gerekmiyor.
        private static bool MatchesFilter(Note note, string filter)
        {
            return note.Title.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                || note.Content.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
        }

        // Arama kutusuna her harf yazıldığında listeyi filtreleyerek yeniden doldurur.
        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            RefreshList();

            if (txtSearch.Text.Trim() == "")
            {
                SetStatus("Tüm notlar gösteriliyor (" + notes.Count + ").");
            }
            else
            {
                SetStatus(lstNotes.Items.Count + " not bulundu.");
            }
        }

        // Yeni not: Seçimi kaldırıp düzenleme alanını boşaltır. Not, Kaydet'e basınca oluşturulur.
        private void NewNote_Click(object? sender, EventArgs e)
        {
            // Arama temizlenir ki eklenecek not filtre yüzünden listede gizli kalmasın.
            txtSearch.Clear();
            lstNotes.ClearSelected();
            ClearEditor();
            txtTitle.Focus();
            SetStatus("Yeni not: başlık ve içerik yazıp Kaydet'e basın.");
        }

        // Kaydet: Listede not seçiliyse onu günceller, seçili değilse yeni not ekler.
        private void SaveNote_Click(object? sender, EventArgs e)
        {
            // Trim(): Baştaki ve sondaki boşlukları siler, sadece boşluktan oluşan başlık da boş sayılır.
            string title = txtTitle.Text.Trim();
            string content = rtbContent.Text;

            if (title == "")
            {
                MessageBox.Show("Lütfen notun başlığını yazın.", "Başlık boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTitle.Focus();
                return;
            }

            Note note;
            string message;

            // "is Note selected": Seçili öğe bir Note ise onu "selected" değişkenine al (pattern matching).
            if (lstNotes.SelectedItem is Note selected)
            {
                // Düzenleme: Update() düzenlenme tarihini de günceller.
                selected.Update(title, content);
                note = selected;
                message = "Not güncellendi: " + title;
            }
            else
            {
                // Ekleme: Yeni nesne oluşturulur, Id ve tarihleri kurucuda atanır.
                note = new Note(title, content);
                notes.Add(note);
                message = "Yeni not eklendi: " + title;
            }

            if (SaveNotes())
            {
                SetStatus(message);
            }

            // Listeyi yenile ve kaydedilen notu seçili bırak.
            RefreshList(note);
        }

        // Sil: Seçili notu onay aldıktan sonra listeden ve dosyadan siler.
        private void DeleteNote_Click(object? sender, EventArgs e)
        {
            // "is not Note": Seçili bir not yoksa uyar ve çık.
            if (lstNotes.SelectedItem is not Note note)
            {
                MessageBox.Show("Silmek için önce listeden bir not seçin.", "Not seçilmedi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Silme geri alınamadığı için kullanıcıya Evet/Hayır sorulur.
            DialogResult answer = MessageBox.Show(
                "\"" + note.Title + "\" notu silinsin mi?", "Notu sil",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            notes.Remove(note);

            if (SaveNotes())
            {
                SetStatus("Not silindi: " + note.Title);
            }

            RefreshList();
        }

        // ListBox'ı "notes" listesine göre baştan doldurur (arama filtresini de uygular).
        // noteToSelect: İsteğe bağlı parametre. Verilirse liste dolduktan sonra o not seçilir.
        private void RefreshList(Note? noteToSelect = null)
        {
            // BeginUpdate/EndUpdate: Liste her öğede değil, en sonda bir kez çizilir. Titremeyi önler.
            lstNotes.BeginUpdate();
            lstNotes.Items.Clear();

            string filter = txtSearch.Text.Trim();

            // LINQ ile son düzenlenen not en üstte olacak şekilde sırala.
            // "n => n.UpdatedAt" bir lambda ifadesi: "her not için UpdatedAt değerine göre".
            foreach (Note note in notes.OrderByDescending(n => n.UpdatedAt))
            {
                if (filter == "" || MatchesFilter(note, filter))
                {
                    // Listeye metin değil Note nesnesinin kendisi eklenir.
                    // Ekranda Note.ToString() yani başlık görünür.
                    lstNotes.Items.Add(note);
                }
            }

            lstNotes.EndUpdate();

            // Not filtre yüzünden listede yoksa seçilemez; o zaman düzenleme alanı boşaltılır.
            if (noteToSelect != null && lstNotes.Items.Contains(noteToSelect))
            {
                // Bu atama SelectedIndexChanged olayını tetikler, not sağ tarafta gösterilir.
                lstNotes.SelectedItem = noteToSelect;
            }
            else
            {
                ClearEditor();
            }
        }

        // Listede seçim değişince seçilen notun başlığını, içeriğini ve tarihlerini sağ tarafta gösterir.
        private void LstNotes_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstNotes.SelectedItem is Note note)
            {
                txtTitle.Text = note.Title;
                rtbContent.Text = note.Content;

                // "dd.MM.yyyy HH:mm": Tarihi gün.ay.yıl saat:dakika biçiminde yazar.
                lblDates.Text = "Oluşturulma: " + note.CreatedAt.ToString("dd.MM.yyyy HH:mm")
                              + "     Son düzenleme: " + note.UpdatedAt.ToString("dd.MM.yyyy HH:mm");
            }
            else
            {
                ClearEditor();
            }
        }

        // Sağ taraftaki düzenleme alanını boşaltır. Birçok yerde gerektiği için ayrı metot.
        private void ClearEditor()
        {
            txtTitle.Clear();
            rtbContent.Clear();
            lblDates.Text = "";
        }

        // Durum çubuğuna saatle birlikte mesaj yazar (kullanıcıya geri bildirim).
        private void SetStatus(string message)
        {
            lblStatus.Text = DateTime.Now.ToString("HH:mm:ss") + "  " + message;
        }

        // Dosya → Çıkış: Ana form kapanınca uygulama da kapanır.
        private void MnuExit_Click(object? sender, EventArgs e)
        {
            Close();
        }

        // Seçili notu kullanıcının seçtiği yere .txt dosyası olarak kaydeder.
        private void ExportNote_Click(object? sender, EventArgs e)
        {
            if (lstNotes.SelectedItem is not Note note)
            {
                MessageBox.Show("Dışa aktarmak için önce listeden bir not seçin.", "Not seçilmedi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // "using": Metot bitince pencere nesnesi bellekten temizlenir (Dispose).
            using SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Notu .txt olarak dışa aktar";
            dialog.Filter = "Metin dosyası (*.txt)|*.txt";
            dialog.FileName = MakeSafeFileName(note.Title) + ".txt";

            // Kullanıcı İptal'e basarsa hiçbir şey yapma.
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                storage.ExportToText(note, dialog.FileName);
                SetStatus("Not dışa aktarıldı: " + dialog.FileName);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("Dosya yazılamadı:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Dışa aktarma başarısız oldu.");
            }
        }

        // Başlıktaki dosya adında kullanılamayan karakterleri (\ / : * ? " < > |) "_" ile değiştirir.
        // Başlık tamamen boş kalırsa "not" adını kullanır.
        private static string MakeSafeFileName(string title)
        {
            string safe = string.Join("_", title.Split(Path.GetInvalidFileNameChars()));
            return string.IsNullOrWhiteSpace(safe) ? "not" : safe;
        }
    }
}
