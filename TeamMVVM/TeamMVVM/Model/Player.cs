namespace TeamMVVM.Model
{
    class Player
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public override string ToString() => $"{FirstName} {LastName}, wiek: {Age}, waga: {Weight} kg";

        public Player(string firstname, string lastname, int age, double weight)
        {
            FirstName = firstname; LastName = lastname; Age = age; Weight = weight;
        }
        public Player(Player p) { FirstName = p.FirstName; LastName = p.LastName; Age = p.Age; Weight = p.Weight; }

        public Player() { }
    }
}