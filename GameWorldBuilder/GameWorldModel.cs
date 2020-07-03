using System.Collections.Generic;

namespace GameWorldBuilder
{
    // Publiczny interfejs z nazwą i metodą klonującą prototyp:
    public interface IPrototype
    {
        string Name { get; }
        IPrototype Clone();
    }



    // Klasa dziedzicząca po interfejsie reprezentujące postacie; 
    // zawiera takie właściwości jak nazwa, poziom czy punkty zdrowia i many:
    public class Character : IPrototype
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int HealthPoints { get; set; }
        public int ManaPoints { get; set; }
        public double HPExpander { get; set; } = 1.1; // edycja tego pola zmieni prędkość zwiększania zdrowia
        public double MPExpander { get; set; } = 1.1; // edycja tego pola zmieni prędkość zwiększania many

        public Character(string name, int level, int hp, int mp)
        {
            Name = name; Level = level; HealthPoints = hp; ManaPoints = mp;
        }

        // Funkcja ta pozwala zwiększyć/zmniejszyć poziom (a co za tym idzie ilość punktów zdrowia i many):
        public Character ChangeLevelTo(int level)
        {
            int tmp = Level, hp = HealthPoints, mp = ManaPoints;
            if (tmp < level)
            {
                while (tmp < level)
                {
                    tmp++; hp = (int)(hp * HPExpander);
                    mp = (int)(mp * MPExpander);
                }
            }
            else
            {
                while (tmp > level)
                {
                    tmp--; hp = (int)(hp / HPExpander);
                    mp = (int)(mp / MPExpander);
                }
            }
            return new Character(Name, tmp, hp, mp);
        }

        // Klonowanie postaci pozwala na istnienie wielu identycznych postaci, 
        // co jest czymś naturalnym w przypadku gier; ponadto można tę postać zmodyfikować 
        // dla własnych potrzeb nie tracąc przy okazji właściwości prototypu 
        // (jest więc w pewnym sensie możliwość cofnięcia zmian):
        public IPrototype Clone() => new Character(Name, Level, HealthPoints, ManaPoints);

        public override string ToString()
            => $"  {Name}, {Level} lvl, HP: {HealthPoints}/{HealthPoints}, MP: {ManaPoints}/{ManaPoints}";
    }



    // Klasa dziedzicząca po interfejsie reprezentujące lokacje; 
    // zawiera takie właściwości jak nazwa, sugerowany poziom czy listę postaci:
    public class Location : IPrototype
    {
        public string Name { get; set; }
        public int SuggestedMinimumLevel { get; set; }
        public int SuggestedMaximumLevel { get; set; }
        public List<Character> Characters { get; set; }

        public Location(string name, int minlevel, int maxlevel, List<Character> chars = null)
        {
            Name = name; SuggestedMinimumLevel = minlevel; SuggestedMaximumLevel = maxlevel; Characters = chars;
        }

        // Klonowanie lokacji pozwala na budowanie lokacji podobnych bez konieczności zaczynania od zera;
        // mogą więc powtarzać się postaci w niej występujące czy sugerowany poziom:
        public IPrototype Clone() => new Location(Name, SuggestedMinimumLevel, SuggestedMaximumLevel, Characters);

        public override string ToString()
        {
            var tmp = $" Location: {Name} ({SuggestedMinimumLevel}-{SuggestedMaximumLevel})\n";
            foreach (Character ch in Characters)
                tmp += ch.ToString() + "\n";
            return tmp;
        }
    }



    // Klasa przetrzymująca wzorcowy świat gry:
    public class GameManager
    {
        readonly List<IPrototype> prototypes = new List<IPrototype>(); // lista prototypów postaci i lokacji:
        public List<IPrototype> Prototypes => prototypes;

        // Możliwość dodania nowego prototypu, aby następnie móc go klonować:
        public void AddNewPrototype(IPrototype prot) { prototypes.Add(prot); }

        // Wyszukiwanie obiektu po nazwie znacznie ułatwia proces klonowania, 
        // gdyż użytkownik nie musi wiedzieć nic więcej poza nazwą obiektu, który chce dla siebie skopiować:
        public IPrototype Clone(string name) => prototypes.Find(i => i.Name == name);

        // Na potrzeby aplikacji w konstruktorze menedżera gry umieszczono kilka funkcji, 
        // które budują proponowany wzorcowy świat gry; po stworzeniu obiektu tej klasy można 
        // nadal dodawać prototypy czy klonować obiekty zawarte w przetrzymywanej liście, a następnie je modyfikować:
        public GameManager()
        {
            AddNewPrototype(new Character("Green Orc", 1, 100, 50));
            AddNewPrototype(new Character("Murlock", 1, 100, 20));
            AddNewPrototype(new Character("Blood Elf", 1, 100, 30));
            AddNewPrototype(new Character("Red Orc", 5, 500, 150)); // ta i poniższe postaci nie występują z mniejszym poziomem niż 5
            AddNewPrototype(new Character("Imp", 10, 600, 300));
            AddNewPrototype(new Character("Dark Elf", 8, 700, 300));
            AddNewPrototype(new Character("Shadow Dwarf", 12, 1000, 500));
            AddNewPrototype(new Location("The Great Orc's Canyon", 1, 10,
                new List<Character>() { (Character)Clone("Green Orc"), (Character)Clone("Red Orc") }
                ));
            AddNewPrototype(new Location("Ghostland", 5, 15,
                new List<Character>() { (Character)Clone("Dark Elf"), (Character)Clone("Shadow Dwarf") }
                ));
            AddNewPrototype(new Location("The River", 10, 20,
                new List<Character>() { ((Character)Clone("Blood Elf")).ChangeLevelTo(11),
                    ((Character)Clone("Murlock")).ChangeLevelTo(15), ((Character)Clone("Imp")).ChangeLevelTo(15) }
                ));
        }

        public override string ToString()
        {
            string tmp = "\n";
            foreach (IPrototype prot in prototypes)
            {
                try { tmp += ((Location)prot).ToString() + "\n"; }
                catch { }
            }
            return tmp;
        }
    }
}
