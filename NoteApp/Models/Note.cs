using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models
{
    internal class Note
    {
        public string Filename { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public Note()
        {
            Filename = $"{Path.GetRandomFileName}.notes.txt";
            Date = DateTime.Now;
            Text = "";
        }

        public void Save() => File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, Filename), Text);

        public void Delete() => File.Delete(Path.Combine(FileSystem.AppDataDirectory, Filename));

        public static Note Load(string filename)
        {
            filename = Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            return new()
            {
                Filename = Path.GetFileName(filename),
                Text = File.ReadAllText(filename),
                Date = File.GetCreationTime(filename)
            };
        }

        public static IEnumerable<Note> LoadAll()
        {
            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            return Directory.EnumerateFiles(appDataPath, "*.notes.txt")     // Select the file names from the directory
                .Select(f => Note.Load(Path.GetFileName(f)))                // Each file name is used to load a note
                .OrderByDescending(n => n.Date);                            // With the final collection of notes, order them by date
        }
    }
}
