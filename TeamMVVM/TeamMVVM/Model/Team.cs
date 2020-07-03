using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TeamMVVM.Model
{
    class Team
    {
        List<int> agesList = ages(); List<Player> playersList = LoadTeam(@"LockerRoom.json");
        public void AddPlayerMethod(Player player) { playersList.Add(player); }
        public void RemovePlayerMethod(int number) { playersList.RemoveAt(number); }
        public void ModifyPlayerMethod(Player player, int number) { playersList[number] = player; }
        public List<int> GetAges { get => agesList; }
        public List<Player> GetPlayers { get => playersList; }
        private static List<int> ages()
        {
            List<int> tmp = new List<int>();
            for (int i = 18; i <= 50; i++) tmp.Add(i);
            return tmp;
        }

        private static List<Player> LoadTeam(string FileName)
        {
            List<Player> PlayersList = new List<Player>();
            if(File.Exists(FileName))
                PlayersList = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(FileName));
            return PlayersList;
        }

        public void SaveTeam(string FileName)
        {
            string Json = JsonConvert.SerializeObject(playersList);
            File.WriteAllText(FileName, Json);
        }
    }
}