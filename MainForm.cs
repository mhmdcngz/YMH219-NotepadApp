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

        private void txtOlarakDışaAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
