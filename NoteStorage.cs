using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace YMH219_NotepadApp
{
    public class NoteStorage
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,

            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        public string FilePath { get; }

        public NoteStorage()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string folder = Path.Combine(appData, "YMH219-NotepadApp");

            FilePath = Path.Combine(folder, "notes.json");
        }

        public List<Note> Load()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Note>();
            }

            string json;

            using (StreamReader reader = new StreamReader(FilePath))
            {
                json = reader.ReadToEnd();
            }

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Note>();
            }

            try
            {
                List<Note>? notes = JsonSerializer.Deserialize<List<Note>>(json, JsonOptions);

                return notes ?? new List<Note>();
            }
            catch (JsonException ex)
            {
                string backupPath = FilePath + ".bak";

                File.Copy(FilePath, backupPath, overwrite: true);

                throw new InvalidDataException("Not dosyası bozuk olduğu için okunamadı. Bozuk dosya şuraya yedeklendi: \n" + backupPath, ex);
            }
        }

        public void Save(List<Note> notes)
        {
            string folder = Path.GetDirectoryName(FilePath)!;

            Directory.CreateDirectory(folder);

            string json = JsonSerializer.Serialize(notes, JsonOptions);

            string tempPath = FilePath + ".tmp";

            using (StreamWriter writer = new StreamWriter(tempPath))
            {
                writer.Write(json);
            }

            File.Move(tempPath, FilePath, overwrite: true);
        }
    }
}