using System.Windows.Input;
using System.IO;

namespace MiniTC.ViewModel
{
    internal class MainVM : BaseVM
    {
        public MainVM()
        {
            Left = new PanelVM();
            Right = new PanelVM();
        }

        #region Własności
        private PanelVM _left;
        public PanelVM Left
        {
            get { return _left; }
            set { _left = value; onPropertyChanged(nameof(Left)); }
        }

        private PanelVM _right;
        public PanelVM Right
        {
            get { return _right; }
            set { _right = value; onPropertyChanged(nameof(Right)); }
        }
        #endregion

        #region Polecenia

        private ICommand _copyfile = null;
        public ICommand CopyFile
        {
            get
            {
                if (_copyfile == null)
                    _copyfile = new RelayCommand(arg => Copy(), arg => PreviewCopy());
                return _copyfile;
            }
        }
        #endregion

        #region Metody pomocnicze

        private void Copy()
        {
            string filePath, directoryPath, fileName;
            if (Left.SelectedDirectory != null)           // kopiowanie z lewego panelu do prawego
            {
                filePath = Left.CurrentPath + "\\" + Left.SelectedDirectory.Trim();
                directoryPath = Right.CurrentPath;
                fileName = Path.GetFileName(filePath);
            }
            else                                          // kopiowanie z prawego panelu do lewego
            {
                filePath = Right.CurrentPath + "\\" + Right.SelectedDirectory.Trim();
                directoryPath = Left.CurrentPath;
                fileName = Path.GetFileName(filePath);
            }
            File.Copy(filePath, directoryPath + "\\" + fileName);
            Left.CurrentPath = Left.CurrentPath;
            Right.CurrentPath = Right.CurrentPath;
        }

        private bool PreviewCopy()
        {
            // Brak możliwości kopiowania do tego samego katalogu co źródłowy:
            if (Left.CurrentPath == Right.CurrentPath) return false;

            // Brak możliwości kopiowania, gdy foldery zawierają pliki o takiej samej nazwie:
            if ((Right.SelectedDirectory != null && Left.DirectoryContent != null && Left.DirectoryContent.Contains(Right.SelectedDirectory))
                || (Left.SelectedDirectory != null && Right.DirectoryContent != null && Right.DirectoryContent.Contains(Left.SelectedDirectory)))
                return false;

            // Zaznaczono plik w lewym panelu i wybrano położenie w prawym panelu:
            if (Left.SelectedDirectory != null && !Left.SelectedDirectory.Contains("[D]") 
                && Left.SelectedDirectory != ".." && Right.CurrentPath != null) 
                return true;

            // Zaznaczono plik w prawym panelu i wybrano położenie w lewym panelu:
            if (Right.SelectedDirectory != null && !Right.SelectedDirectory.Contains("[D]")
                && Right.SelectedDirectory != ".." && Left.CurrentPath != null)
                    return true;

            return false;
        }
        #endregion
    }
}