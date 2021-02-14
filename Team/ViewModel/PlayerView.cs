using System.Collections.Generic;

namespace TeamMVVM.ViewModel
{
    using BaseClass;
    using Model;
    class PlayerView : ViewModelBase
    {
        public static List<string> PlayerViewList(List<Player> playerList)
        {
            List<string> playerviewlist = new List<string>();
            for (int i = 0; i < playerList.Count; i++) playerviewlist.Add(playerList[i].ToString());
            return playerviewlist;
        }
    }
}