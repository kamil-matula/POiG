using System.Windows.Input;
using System.Windows;
using System.Collections.Generic;

namespace TeamMVVM.ViewModel
{
    using Model;
    using BaseClass;

    internal class Playing: ViewModelBase
    {
        private Team team = new Team();
        public List<int> Ages { get => team.GetAges; }                  // Lista lat
        public List<string> Players 
        { get => PlayerView.PlayerViewList(team.GetPlayers); }          // Lista graczy

        #region Interfejs publiczny
        public string CurrentFirstName { get; set; } = "Podaj imię";    // Zawartość pierwszego textboxa
        public string CurrentLastName { get; set; } = "Podaj nazwisko"; // Zawartość drugiego textboxa
        public int CurrentAge { get; set; } = 18;                       // Wybrana opcja w comboboxie
        public double CurrentWeight { get; set; } = 55.0;               // Ustawienie suwaka
        public int CurrentIndex { get; set; } = -1;                     // Zaznaczony piłkarz w listboxie
        #endregion

        #region Polecenia

        private ICommand addplayer = null;
        private ICommand removeplayer = null;
        private ICommand modifyplayer = null;
        private ICommand copyplayer = null;
        private ICommand saveteam = null;

        public ICommand AddPlayer
        {
            get
            {
                if (addplayer == null)
                {
                    addplayer = new RelayCommand(
                        arg =>
                        {
                            team.AddPlayerMethod(new Player(CurrentFirstName, CurrentLastName, CurrentAge, CurrentWeight));
                            onPropertyChanged(nameof(Players));
                        },
                        arg => (!string.IsNullOrEmpty(CurrentFirstName)) && (!string.IsNullOrEmpty(CurrentLastName))
                        && (CurrentFirstName != "Podaj imię") && (CurrentLastName != "Podaj nazwisko")
                        );
                }
                return addplayer;
            }
        }

        public ICommand RemovePlayer
        {
            get
            {
                if (removeplayer == null)
                {
                    removeplayer = new RelayCommand(
                        arg =>
                        {
                            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć?", "Usuwanie zawodnika", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                team.RemovePlayerMethod(CurrentIndex);
                                onPropertyChanged(nameof(Players));
                            }
                        },
                        arg => CurrentIndex != -1
                        );
                }
                return removeplayer;
            }
        }

        public ICommand ModifyPlayer
        {
            get
            {
                if (modifyplayer == null)
                {
                    modifyplayer = new RelayCommand(
                        arg =>
                        {
                            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz zmodyfikować?", "Modyfikowanie zawodnika", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                team.ModifyPlayerMethod(new Player(CurrentFirstName, CurrentLastName, CurrentAge, CurrentWeight), CurrentIndex);
                                onPropertyChanged(nameof(Players));
                            }
                        },
                        arg => CurrentIndex != -1 && (!string.IsNullOrEmpty(CurrentFirstName)) && (!string.IsNullOrEmpty(CurrentLastName))
                        && (CurrentFirstName != "Podaj imię") && (CurrentLastName != "Podaj nazwisko")
                        );
                }
                return modifyplayer;
            }
        }

        public ICommand CopyPlayer
        {
            get
            {
                if (copyplayer == null)
                {
                    copyplayer = new RelayCommand(
                        arg => { Player player = team.GetPlayers[CurrentIndex];
                            CurrentFirstName = player.FirstName; CurrentLastName = player.LastName;
                            CurrentAge = player.Age; CurrentWeight = player.Weight;
                            onPropertyChanged(nameof(CurrentFirstName), nameof(CurrentLastName),
                                nameof(CurrentAge), nameof(CurrentWeight));
                        },
                        arg => CurrentIndex != -1
                    );
                }
                return copyplayer;
            }
        }

        public ICommand SaveTeam
        {
            get
            {
                if (saveteam == null)
                    saveteam = new RelayCommand(arg => team.SaveTeam(@"LockerRoom.json"), arg => true);
                return saveteam;
            }
        }
        #endregion
    }
}