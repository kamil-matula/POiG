using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Input;

namespace MiniTC.UserControls
{
    public partial class PanelTC : UserControl
    {

        public PanelTC()
        {
            InitializeComponent();
        }

        #region Własności zależne

        private static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.Register("DoubleClickCommand", typeof(ICommand), typeof(PanelTC),
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty CurrentPathProperty =
            DependencyProperty.Register("CurrentPath", typeof(string), typeof(PanelTC),
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty CurrentDriveProperty =
            DependencyProperty.Register("CurrentDrive", typeof(string), typeof(PanelTC),
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty AvailableDrivesProperty =
            DependencyProperty.Register("AvailableDrives", typeof(string[]), typeof(PanelTC),
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty DirectoryContentProperty =
            DependencyProperty.Register("DirectoryContent", typeof(List<string>), typeof(PanelTC),
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty SelectedDirectoryProperty =
            DependencyProperty.Register("SelectedDirectory", typeof(string), typeof(PanelTC),
                new FrameworkPropertyMetadata(null));

        #endregion

        #region Polecenia

        public ICommand DoubleClickCommand // polecenie podwójnego wciśnięcia myszki
        {
            get { return (ICommand)GetValue(DoubleClickCommandProperty); }
            set { SetValue(DoubleClickCommandProperty, value); }
        }

        #endregion

        #region Interfejs publiczny

        public string CurrentPath         // aktualna ścieżka
        {
            get { return (string)GetValue(CurrentPathProperty); }
            set { SetValue(CurrentPathProperty, value); }
        }

        public string CurrentDrive        // aktualny dysk
        {
            get { return (string)GetValue(CurrentDriveProperty); }
            set { SetValue(CurrentDriveProperty, value); }
        }

        public string[] AvailableDrives   // kolekcja dostępnych dysków
        {
            get { return (string[])GetValue(AvailableDrivesProperty); }
            set { SetValue(AvailableDrivesProperty, value); }
        }

        public List<string> DirectoryContent // kolekcja plików i folderów aktualnego folderu
        {
            get { return (List<string>)GetValue(DirectoryContentProperty); }
            set { SetValue(DirectoryContentProperty, value); }
        }

        public string SelectedDirectory    // zaznaczony folder
        {
            get { return (string)GetValue(SelectedDirectoryProperty); }
            set { SetValue(SelectedDirectoryProperty, value); }
        }

        #endregion
    }
}