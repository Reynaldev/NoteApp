﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models
{
    internal class AllNotes
    {
        public  ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        public AllNotes() => LoadNotes();

        public void LoadNotes()
        {
            Notes.Clear();

            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            IEnumerable<Note> notes = Directory
                .EnumerateFiles(appDataPath, "*.notes.txt")     // Select the file names from directory
                .Select(filename => new Note()                  // Each file name is used to create a new note
                { 
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                })
                .OrderBy(note => note.Date);                    // With the final collection of notes, order them by date

            // Add each note into the ObservableCollection
            foreach (Note note in notes)
                Notes.Add(note);
        }
    }
}
