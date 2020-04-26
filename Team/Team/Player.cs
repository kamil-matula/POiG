// Kamil Matula, gr. D, 10.03.2020, "Drużyna"
namespace Team
{
    class Player
    {
        private int wiek; private string imie, nazwisko; private double waga;

        public Player(string imie, string nazwisko, int wiek, double waga) 
        {
            this.imie = imie; this.nazwisko = nazwisko; 
            this.wiek = wiek; this.waga = waga;
        }
        public Player(Player p) 
        {
            wiek = p.wiek;  imie = p.imie;
            nazwisko = p.nazwisko; waga = p.waga;
        }

        public override string ToString() 
        {
            return $"{imie} {nazwisko}, wiek: {wiek}, waga: {waga}";
        }

        public string Imie { get { return imie; } set { imie = value; } }
        public string Nazwisko { get { return nazwisko; } set { nazwisko = value; } }
        public int Wiek { get { return wiek; } set { wiek = value; } }
        public double Waga { get { return waga; } set { waga = value; } }
    }
}
