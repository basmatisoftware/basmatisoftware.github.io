using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationNavigator
{
    public class Location
    {
        public int ID;
        public string Name;
        public string Description;
        public Item ItemRequiredToEnter { get; set; }
        public Quest QuestAvailableHere { get; set; }
        public Monster MonsterLivingHere { get; set; }
        public Location LocationToNorth;
        public Location LocationToEast;
        public Location LocationToSouth;
        public Location LocationToWest;

        public Location(int iD, string name, string description, Item itemRequiredToEnter = null, Quest questAvailableHere = null, Monster monsterLivingHere = null)
        {
            ID = iD;
            Name = name;
            Description = description;
            ItemRequiredToEnter = itemRequiredToEnter;
            QuestAvailableHere = questAvailableHere;
            MonsterLivingHere = monsterLivingHere;
        }

    }



}
