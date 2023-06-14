using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NoteApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.ViewModels
{
    internal class NoteViewModel : ObservableObject, IQueryAttributable
    {
        private Note _note;
        public string Text 
        {
            get => _note.Text;
            set 
            { 
                if (_note.Text != value)
                {
                    _note.Text = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime Date => _note.Date;
        public string Identifier => _note.Filename;

        public ICommand SaveCmd { get; private set; }
        public ICommand DeleteCmd { get; private set; }

        public NoteViewModel()
        {
            _note = new Note();
            SaveCmd = new AsyncRelayCommand(Save);
            DeleteCmd = new AsyncRelayCommand(Delete);
        }

        public NoteViewModel(Note note)
        {
            _note = note;
            SaveCmd = new AsyncRelayCommand(Save);
            DeleteCmd = new AsyncRelayCommand(Delete);
        }

        private async Task Save()
        {
            _note.Date = DateTime.Now;
            _note.Save();
            await Shell.Current.GoToAsync($"..?saved={_note.Filename}");
        }

        private async Task Delete()
        {
            _note.Delete();
            await Shell.Current.GoToAsync($"..?deleted={_note.Filename}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _note = Note.Load(query["load"].ToString());
                RefreshProperties();
            }
        }

        public void Reload()
        {
            _note = Note.Load(_note.Filename);
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Date));
        }
    }
}
