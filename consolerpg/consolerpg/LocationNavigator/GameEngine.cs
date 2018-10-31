using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationNavigator
{
    public static class GameEngine
    {
        //Change made at SISC front computer
        public static string Version = "0.0.5";
        public static string Concepts = "Locations Menu";
        public static Monster _currentMonster;
        
        public static void Initialize()
        {
            Console.WriteLine("Initializing Game Engine " + Version);
            Console.WriteLine("Welcome to the world of " + World.WorldName); //this initializes the World.
            Console.WriteLine("");
            //Remove these four lines later... just for testing
            //World.ListLocations();
            //World.ListItems();
            //World.ListQuests();
            //World.ListMonsters();
            

        }

        public static void QuestProcessor(Player _player, Location newLocation) {
            string questMessage;
        // Does the location have a quest?
            if (newLocation.QuestAvailableHere != null)
            {
                // See if the player already has the quest, and if they've completed it
                bool playerAlreadyHasQuest = _player.HasThisQuest(newLocation.QuestAvailableHere);
                bool playerAlreadyCompletedQuest = _player.CompletedThisQuest(newLocation.QuestAvailableHere);

                // See if the player already has the quest
                if (playerAlreadyHasQuest)
                {
                    // If the player has not completed the quest yet
                    if (!playerAlreadyCompletedQuest)
                    {
                        // See if the player has all the items needed to complete the quest
                        bool playerHasAllItemsToCompleteQuest = _player.HasAllQuestCompletionItems(newLocation.QuestAvailableHere);

                        // The player has all items required to complete the quest
                        if (playerHasAllItemsToCompleteQuest)
                        {
                            // Display message
                            questMessage= Environment.NewLine;
                            questMessage += "You complete the '" + newLocation.QuestAvailableHere.Name + "' quest." + Environment.NewLine;

                            // Remove quest items from inventory
                            _player.RemoveQuestCompletionItems(newLocation.QuestAvailableHere);

                            // Give quest rewards
                            questMessage += "You receive: " + Environment.NewLine;
                            questMessage += newLocation.QuestAvailableHere.RewardExperiencePoints.ToString() + " experience points" + Environment.NewLine;
                            questMessage += newLocation.QuestAvailableHere.RewardGold.ToString() + " gold" + Environment.NewLine;
                            questMessage += newLocation.QuestAvailableHere.RewardItem.Name + Environment.NewLine;
                            questMessage += Environment.NewLine;
                            Console.WriteLine(questMessage);

                            _player.ExperiencePoints += newLocation.QuestAvailableHere.RewardExperiencePoints;
                            _player.Gold += newLocation.QuestAvailableHere.RewardGold;

                            // Add the reward item to the player's inventory
                            _player.AddItemToInventory(newLocation.QuestAvailableHere.RewardItem);

                            // Mark the quest as completed
                            _player.MarkQuestCompleted(newLocation.QuestAvailableHere);
                        }
}
                }
                else
                {
                    // The player does not already have the quest

                    // Display the messages
                    questMessage = "You receive the " + newLocation.QuestAvailableHere.Name + " quest." + Environment.NewLine;
                    questMessage += newLocation.QuestAvailableHere.Description + Environment.NewLine;
                    questMessage += "To complete it, return with:" + Environment.NewLine;
                    foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                    {
                        if (qci.Quantity == 1)
                        {
                            questMessage += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;
                        }
                        else
                        {
                            questMessage += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                        }
                    }
                    questMessage += Environment.NewLine;
                    Console.WriteLine(questMessage);

                    // Add the quest to the player's quest list
                    _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere,false));
                }
            }
            
        } //QuestProcessor


        public static void MonsterProcessor(Player _player, Location newLocation)
        {
            
            string monsterMessage = "";
            // Does the location have a monster?
            if (newLocation.MonsterLivingHere != null)
            {
                monsterMessage += "You see a " + newLocation.MonsterLivingHere.Name + Environment.NewLine;
                Console.WriteLine(monsterMessage);
                // Make a new monster, using the values from the standard monster in the World.Monster list
                Monster standardMonster = World.MonsterByID(newLocation.MonsterLivingHere.ID);

                _currentMonster = new Monster(standardMonster.ID, standardMonster.Name, standardMonster.MaximumDamage,
                    standardMonster.RewardExperiencePoints, standardMonster.RewardGold, standardMonster.CurrentHitPoints, standardMonster.MaximumHitPoints);

                foreach (LootItem lootItem in standardMonster.LootTable)
                {
                    _currentMonster.LootTable.Add(lootItem);
                }

                
            }
            else
            {
                _currentMonster = null;

               
            }


        }//MonsterProcessor
       

    }
}
