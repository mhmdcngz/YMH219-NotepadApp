namespace YMH219_NotepadApp
{
    public partial class MainForm : Form
    {
        private readonly NoteStorage storage = new NoteStorage();
        private List<Note> notes = new List<Note>();

        public MainForm()
        {
            InitializeComponent();

            Load += MainForm_Load;
            lstNotes.SelectedIndexChanged += LstNotes_SelectedIndexChanged;
            mnuExit.Click += MnuExit_Click;

            btnNew.Click += NewNote_Click;
            mnuNew.Click += NewNote_Click;
            btnSave.Click += SaveNote_Click;
            btnDelete.Click += DeleteNote_Click;
            mnuDelete.Click += DeleteNote_Click;
            btnExport.Click += ExportNote_Click;
            mnuExport.Click += ExportNote_Click;
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            LoadNotes();
            RefreshList();
        }

        private void LoadNotes()
        {
            try
            {
                notes = storage.Load();
                SetStatus(notes.Count + " not yüklendi.");
            }
            catch (InvalidDataException ex)
            {
                notes = new List<Note>();
                MessageBox.Show(ex.Message, "Bozuk dosya", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetStatus("Not dosyası bozuktu, boş listeyle başlandı.");
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                notes = new List<Note>();
                MessageBox.Show("Notlar yüklenemedi:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Notlar yüklenemedi.");
            }
        }

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

        private void NewNote_Click(object? sender, EventArgs e)
        {
            lstNotes.ClearSelected();
            ClearEditor();
            txtTitle.Focus();
            SetStatus("Yeni not: başlık ve içerik yazıp Kaydet'e basın.");
        }

        private void SaveNote_Click(object? sender, EventArgs e)
        {
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

            if (lstNotes.SelectedItem is Note selected)
            {
                selected.Update(title, content);
                note = selected;
                message = "Not güncellendi: " + title;
            }
            else
            {
                note = new Note(title, content);
                notes.Add(note);
                message = "Yeni not eklendi: " + title;
            }

            if (SaveNotes())
            {
                SetStatus(message);
            }

            RefreshList(note);
        }

        private void DeleteNote_Click(object? sender, EventArgs e)
        {
            if (lstNotes.SelectedItem is not Note note)
            {
                MessageBox.Show("Silmek için önce listeden bir not seçin.", "Not seçilmedi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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

        private void RefreshList(Note? noteToSelect = null)
        {
            lstNotes.BeginUpdate();
            lstNotes.Items.Clear();

            foreach (Note note in notes.OrderByDescending(n => n.UpdatedAt))
            {
                lstNotes.Items.Add(note);
            }

            lstNotes.EndUpdate();

            if (noteToSelect != null)
            {
                lstNotes.SelectedItem = noteToSelect;
            }
            else
            {
                ClearEditor();
            }
        }

        private void LstNotes_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstNotes.SelectedItem is Note note)
            {
                txtTitle.Text = note.Title;
                rtbContent.Text = note.Content;
                lblDates.Text = "Oluşturulma: " + note.CreatedAt.ToString("dd.MM.yyyy HH:mm")
                              + "     Son düzenleme: " + note.UpdatedAt.ToString("dd.MM.yyyy HH:mm");
            }
            else
            {
                ClearEditor();
            }
        }

        private void ClearEditor()
        {
            txtTitle.Clear();
            rtbContent.Clear();
            lblDates.Text = "";
        }

        private void SetStatus(string message)
        {
            lblStatus.Text = DateTime.Now.ToString("HH:mm:ss") + "  " + message;
        }

        private void MnuExit_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void ExportNote_Click(object? sender, EventArgs e)
        {
            if (lstNotes.SelectedItem is not Note note)
            {
                MessageBox.Show("Dışa aktarmak için önce listeden bir not seçin.", "Not seçilmedi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Notu .txt olarak dışa aktar";
            dialog.Filter = "Metin dosyası (*.txt)|*.txt";
            dialog.FileName = MakeSafeFileName(note.Title) + ".txt";

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

        private static string MakeSafeFileName(string title)
        {
            string safe = string.Join("_", title.Split(Path.GetInvalidFileNameChars()));
            return string.IsNullOrWhiteSpace(safe) ? "not" : safe;
        }
    }
}
