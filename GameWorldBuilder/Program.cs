// Kamil Matula, 30.06.2020

// Projekt GameWorldBuilder opiera się na działaniu wzorca projektowego Prototyp. 
// Pozwala na zapisanie pewnych wzorcowych obiektów typu Character i Location, 
// a następnie wykorzystanie ich do tworzenia nowych lokacji i postaci.
// Gdyby nie wzorzec Prototyp tworzenie obiektów zajmowałoby znacznie więcej czasu
// ze względu na konieczność budowania ich od zera.

using System;
using System.Collections.Generic;

namespace GameWorldBuilder
{
    class Program
    {
        static void Main()
        {
            bool menu = true; var gm = new GameManager(); var account = new Account();
            List<string> tmp = new List<string>(); int decision;

            foreach (IPrototype prot in gm.Prototypes)
            {
                try { account.Characters.Add((Character)prot); }
                catch { }
            }

            while (menu == true)
            {
                Console.Clear();
                Console.WriteLine("\n    *** WELCOME TO THE GAMEWORLDBUILDER! ***\n\n 1. Show default world." +
                    "\n 2. Show my world. \n 3. Add location. \n 4. Modify location. \n 5. Exit");
                Console.Write("\n Decision: ");
                switch (Console.ReadLine())
                {
                    case "1": // wyświetla świat budowany w konstruktorze klasy GameManager
                        Console.Clear();
                        Console.WriteLine(gm);
                        Console.ReadKey();
                        break;
                    case "2": // wyświetla świat budowany przez użytkownika - początkowo jest pusty
                        Console.Clear();
                        Console.WriteLine(account);
                        Console.ReadKey();
                        break;
                    case "3": // dodaje lokację z prototypowego świata
                        Console.Clear(); tmp.Clear();
                        Console.WriteLine("\n Choose location to clone:\n");
                        foreach (IPrototype prot in gm.Prototypes)
                        {
                            try { tmp.Add(((Location)prot).Name); }
                            catch { }
                        }
                        for (int i = 0; i < tmp.Count; i++) Console.WriteLine($" {i+1}. {tmp[i]}");

                        Console.Write("\n Decision: ");
                        try { decision = int.Parse(Console.ReadLine()); }
                        catch { break; }

                        // Prototyp lokacji jest klonowany do spisu lokacji użytkownika:
                        if (decision > tmp.Count || decision < 0) break;
                        else account.Locations.Add((Location)gm.Clone(tmp[decision - 1]));

                        break;
                    case "4": // modyfikuje lokację w świecie użytkownika
                        Console.Clear();
                        Console.WriteLine("\n Choose location to modify:\n");
                        for (int i = 0; i < account.Locations.Count; i++) Console.WriteLine($" {i + 1}.{account.Locations[i].ToString()}");

                        Console.Write("\n Decision: ");
                        try { decision = int.Parse(Console.ReadLine()); }
                        catch { break; }

                        // Prototyp lokacji jest modyfikowany przez użytkownika:
                        if (decision > account.Locations.Count || decision < 0) break;
                        else UserFunctions.ModifyLocation(account, decision);

                        break;
                    case "5":
                        menu = false;
                        break;
                    default:
                        break;
                }
            }
        } 
    }
}
