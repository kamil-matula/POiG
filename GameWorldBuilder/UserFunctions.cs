using System;
using System.Collections.Generic;

namespace GameWorldBuilder
{
    public class Account //prosta klasa zawierająca listy lokacji i postaci
    {
        public List<Character> Characters = new List<Character>();
        public List<Location> Locations = new List<Location>();

        public override string ToString()
        {
            string tmp = "\n";
            foreach (Location loc in Locations)
                tmp += loc.ToString() + "\n";
            return tmp;
        }
    }

    public class UserFunctions
    {
        public static void ModifyLocation(Account account, int decision)
        {
            bool modifyingmenu = true; int readnumber;
            while (modifyingmenu == true)
            {
                Console.Clear();
                Console.WriteLine(account.Locations[decision - 1].ToString() + "\n");
                Console.WriteLine("\n Choose stat to modify:\n 1. Name" +
                    "\n 2. Suggested Minimum Level \n 3. Suggested Maximum Level \n 4. Characters \n 5. I've finished editing.");
                Console.Write("\n Decision: ");
                switch (Console.ReadLine())
                {
                    case "1": // zmiana nazwy lokacji
                        Console.Clear();
                        Console.WriteLine(" Old name: " + account.Locations[decision - 1].Name);
                        Console.Write(" New name: ");
                        account.Locations[decision - 1].Name = Console.ReadLine();
                        break;
                    case "2": // zmiana proponowanego minimalnego poziomu w lokacji
                        Console.Clear();
                        Console.WriteLine(" Old suggested minimum level: " + account.Locations[decision - 1].SuggestedMinimumLevel);
                        Console.Write(" New suggested minimum level: ");
                        try { readnumber = int.Parse(Console.ReadLine()); }
                        catch { break; }
                        if (readnumber < 1 || readnumber > 100) break;
                        account.Locations[decision - 1].SuggestedMinimumLevel = readnumber;
                        break;
                    case "3": // zmiana proponowanego maksymalnego poziomu w lokacji
                        Console.Clear();
                        Console.WriteLine(" Old suggested maximum level: " + account.Locations[decision - 1].SuggestedMaximumLevel);
                        Console.Write(" New suggested maixmum level: ");
                        try { readnumber = int.Parse(Console.ReadLine()); }
                        catch { break; }
                        if (readnumber < 1 || readnumber > 100) break;
                        account.Locations[decision - 1].SuggestedMaximumLevel = readnumber;
                        break;
                    case "4": // zmiana postaci występujących w lokacji
                        Console.Clear();
                        Console.WriteLine("\n Do you want to add default (1), modify existing (2) or delete existing (3) character?\n");
                        Console.Write("\n Decision: ");
                        try { readnumber = int.Parse(Console.ReadLine()); }
                        catch { break; }
                        if (readnumber == 1) AddCharacter(account, decision);
                        if (readnumber == 2) ModifyCharacter(account, decision);
                        if (readnumber == 3) DeleteCharacter(account, decision);
                        break;
                    case "5":
                        modifyingmenu = false;
                        break;
                }
            }
        }

        private static void AddCharacter(Account account, int decision) // dodanie postaci do lokacji
        {
            Console.WriteLine("\n Choose character to clone:"); int i = 1, tmp;
            foreach (Character ch in account.Characters)
                Console.WriteLine($" {i++}.{ch.ToString()}");
            Console.Write("\n Decision: ");
            try { tmp = int.Parse(Console.ReadLine()); }
            catch { return; }
            if (tmp < 0 || tmp > account.Characters.Count) return;
            account.Locations[decision - 1].Characters.Add((Character)account.Characters[tmp - 1].Clone());
        }

        private static void ModifyCharacter(Account account, int decision) // modyfikacja postaci w lokacji
        {
            Console.WriteLine("\n Choose character to modify:"); int i = 1, tmp;
            foreach (Character ch in account.Locations[decision - 1].Characters)
                Console.WriteLine($" {i++}.{ch.ToString()}");
            Console.Write("\n Decision: ");
            try { tmp = int.Parse(Console.ReadLine()); }
            catch { return; }
            if (tmp < 0 || tmp > account.Locations[decision - 1].Characters.Count) return;

            bool modifyingcharmenu = true; int readnumber; double readexpander;
            Character chosencharacter = account.Locations[decision - 1].Characters[tmp - 1];
            while (modifyingcharmenu == true)
            {
                Console.Clear();
                Console.WriteLine(chosencharacter.ToString() + "\n");
                Console.WriteLine("\n Choose stat to modify:\n 1. Name" +
                    "\n 2. Level \n 3. Health Points \n 4. Mana Points \n 5. Health Points Expander " +
                    "\n 6. Mana Points Expander \n 7. I've finished editing.");
                Console.Write("\n Decision: ");
                switch (Console.ReadLine())
                {
                    case "1": // zmiana nazwy
                        Console.Clear();
                        Console.WriteLine(" Old name: " + chosencharacter.Name);
                        Console.Write(" New name: ");
                        chosencharacter.Name = Console.ReadLine();
                        break;
                    case "2": // zmiana poziomu
                        Console.Clear();
                        Console.WriteLine(" Old level: " + chosencharacter.Level);
                        Console.Write(" New level: ");
                        try { readnumber = int.Parse(Console.ReadLine()); }
                        catch { break; }
                        if (readnumber < 1 || readnumber > 100) break;
                        chosencharacter = chosencharacter.ChangeLevelTo(readnumber);
                        break;
                    case "3": // zmiana wartości punktów zdrowia
                        Console.Clear();
                        Console.WriteLine(" Old number of health points: " + chosencharacter.HealthPoints);
                        Console.Write(" New number of health points: ");
                        try { readnumber = int.Parse(Console.ReadLine()); }
                        catch { break; }
                        if (readnumber < 1 || readnumber > 10000) break;
                        chosencharacter.HealthPoints = readnumber;
                        break;
                    case "4": // zmiana wartości punktów many
                        Console.Clear();
                        Console.WriteLine(" Old number of mana points: " + chosencharacter.ManaPoints);
                        Console.Write(" New number of mana points: ");
                        try { readnumber = int.Parse(Console.ReadLine()); }
                        catch { break; }
                        if (readnumber < 1 || readnumber > 10000) break;
                        chosencharacter.ManaPoints = readnumber;
                        break;
                    case "5": // zmiana wartości mnożnika punktów zdrowia
                        Console.Clear();
                        Console.WriteLine(" Old health points expander: " + chosencharacter.HPExpander);
                        Console.Write(" New health points expander: ");
                        try { readexpander = double.Parse(Console.ReadLine()); }
                        catch { break; }
                        if (readexpander < 0.1 || readexpander > 10) break;
                        chosencharacter.HPExpander = readexpander;
                        break;
                    case "6": // zmiana wartości mnożnika punktów many
                        Console.Clear();
                        Console.WriteLine(" Old mana points expander: " + chosencharacter.MPExpander);
                        Console.Write(" New mana points expander: ");
                        try { readexpander = double.Parse(Console.ReadLine()); }
                        catch { break; }
                        if (readexpander < 0.1 || readexpander > 10) break;
                        chosencharacter.MPExpander = readexpander;
                        break;
                    case "7": // koniec modyfikowania
                        modifyingcharmenu = false;
                        account.Characters.Add((Character)chosencharacter.Clone()); // nowy prototyp jest zapisywany w liście postaci...
                        account.Locations[decision - 1].Characters[tmp - 1] = chosencharacter; // ...a w lokalizacji podmieniany
                        break;
                }
            }
        }

        private static void DeleteCharacter(Account account, int decision) // usuwanie postaci z lokalizacji
        {
            Console.WriteLine("\n Choose character to clone:"); int i = 1, tmp;
            foreach (Character ch in account.Locations[decision-1].Characters)
                Console.WriteLine($" {i++}.{ch.ToString()}");
            Console.Write("\n Decision: ");
            try { tmp = int.Parse(Console.ReadLine()); }
            catch { return; }
            if (tmp < 0 || tmp > account.Locations[decision - 1].Characters.Count) return;
            account.Locations[decision - 1].Characters.RemoveAt(tmp - 1);
        }
    }
}
