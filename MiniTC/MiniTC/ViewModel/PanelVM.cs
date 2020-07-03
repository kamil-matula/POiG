using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace MiniTC.ViewModel
{
    internal class PanelVM : BaseVM
    {
        public PanelVM()
        {
            GetDrivesMethod();
        }

        #region Własności
        private string[] _availabledrives;       // kolekcja dostępnych dysków
        private string _currentdrive;            // aktualny dysk
        private string _currentpath;             // aktualna ścieżka
        private string _selecteddirectory;       // zaznaczony folder
        private List<string> _directorycontent;  // kolekcja aktualnego folderu

        public string CurrentPath
        {
            get { return _currentpath; }
            set 
            {
                try
                {
                    _currentpath = value;
                    onPropertyChanged(nameof(CurrentPath));
                    UpdateListBox();
                }
                catch { }

            }
        }

        public string CurrentDrive
        {
            get { return _currentdrive; }
            set 
            { 
                _currentdrive = value; 
                onPropertyChanged(nameof(CurrentDrive)); 
                UpdatePath(); 
            }
        }

        public string[] AvailableDrives
        {
            get { return _availabledrives; }
            set 
            { 
                _availabledrives = value;       
                onPropertyChanged(nameof(AvailableDrives)); 
            }
        }

        public List<string> DirectoryContent
        {
            get { return _directorycontent; }
            set 
            { 
                _directorycontent = value; 
                onPropertyChanged(nameof(DirectoryContent)); 
            }
        }

        public string SelectedDirectory
        {
            get { return _selecteddirectory; }
            set 
            { 
                _selecteddirectory = value; 
                onPropertyChanged(nameof(SelectedDirectory));
            }
        }
        #endregion

        #region Polecenia

        private ICommand _changedirectory = null;
        public ICommand ChangeDirectory
        {
            get
            {
                if (_changedirectory == null)
                {
                    _changedirectory = new RelayCommand(
                        arg => 
                        {
                            if (_selecteddirectory == "..") CurrentPath = Directory.GetParent(CurrentPath).FullName;
                            else
                            {
                                if (CurrentPath.EndsWith("\\")) // gdy jest w dysku lokalnym nie dodajemy slasha
                                    CurrentPath += _selecteddirectory.Replace("[D] ", "");
                                else
                                    CurrentPath += "\\" + _selecteddirectory.Replace("[D] ", "");
                            } 
                        },
                        arg => PreviewEntry());
                }
                return _changedirectory;
            }
        }
        #endregion

        #region Metody pomocnicze

        private bool PreviewEntry()
        {
            if (_selecteddirectory == "..") return true; // wyjście z folderu

            if (_selecteddirectory != null && _selecteddirectory.Contains("[D]")) return true; // wejście do folderu

            return false; // brak wejścia do plików
        }

        private void GetDrivesMethod()
        {
            AvailableDrives = Directory.GetLogicalDrives();
        }

        private void UpdatePath()
        {
            CurrentPath = _currentdrive;
        }

        private void UpdateListBox()
        {
            List<string> Content = new List<string>();
            try
            {
                string[] files = Directory.GetFiles(CurrentPath);
                string[] directories = Directory.GetDirectories(CurrentPath);
                DirectoryInfo parentFile = Directory.GetParent(CurrentPath);

                // Na liście pojawią się tylko pliki i foldery nieukryte, a wrócić można wciskając ".."
                if (parentFile != null) Content.Add("..");
                foreach (string dir in directories) 
                    if (!(new DirectoryInfo(dir).Attributes.HasFlag(FileAttributes.Hidden))) 
                        Content.Add("[D] " + Path.GetFileName(dir));
                foreach (string fil in files)
                    if (!(new FileInfo(fil).Attributes.HasFlag(FileAttributes.Hidden)))
                        Content.Add("      " + Path.GetFileName(fil));
            }
            catch { }
            DirectoryContent = Content;
        }

        #endregion
    }
}