using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using NoteApp.Models;
using NoteApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.ViewModels
{
    internal class NotesViewModel : IQueryAttributable
    {
        public ObservableCollection<NoteViewModel> AllNotes { get; }
        public ICommand NewCmd { get; }
        public ICommand SelectCmd { get; }

        public NotesViewModel()
        {
            AllNotes = new ObservableCollection<NoteViewModel>(Note.LoadAll().Select(n => new NoteViewModel(n)));
            NewCmd = new AsyncRelayCommand(NewNote);
            SelectCmd = new AsyncRelayCommand<NoteViewModel>(SelectNote);
        }

        private async Task NewNote()
        {
            await Shell.Current.GoToAsync(nameof(NotePage));
        }

        private async Task SelectNote(NoteViewModel note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(NotePage)}?load={note.Identifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                NoteViewModel matchedNote = AllNotes.Where(n => n.Identifier == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    AllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                NoteViewModel matchedNote = AllNotes.Where(n => n.Identifier == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
                }
                else // If note isn't found, it's new; add it.
                    AllNotes.Insert(0, new NoteViewModel(Note.Load(noteId)));

            }
        }
    }
}
