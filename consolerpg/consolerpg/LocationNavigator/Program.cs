using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LocationNavigator
{
    class Program
    {

        //Create a player....
        private static Player _player = new Player("Bertha",10,10,20,0,1);
        
       

        static void Main(string[] args)
        {
            
            GameEngine.Initialize();
            

            //Start at HOME, with a sword
            _player.MoveTo( World.LocationByID(World.LOCATION_ID_HOME));
            InventoryItem sword = new InventoryItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD),1);
            _player.Inventory.Add(sword);
            


            while (true)
            {
                Console.Write("\n> ");
                string userInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    continue;
                }
                string cleanedInput = userInput.ToLower();

                if (cleanedInput == "exit")
                {
                    break;
                }
                ParseInput(cleanedInput);

            }//Keyboard input
         
        }//Main

        public static void ParseInput(string input)
        {
            if (input.Contains("help"))
            {
                Console.WriteLine("Help will come later. Sorry.");
            }
            else if (input.Contains("look"))
            {
                DisplayCurrentLocation();
            }

            else if (input.Contains("north") || input=="n")
            {
                _player.MoveNorth();
            }

            else if (input.Contains("south") || input =="s")
            {
                _player.MoveSouth();
            }

            else if (input.Contains("east") || input == "e")
            {
                _player.MoveEast();
            }

            else if (input.Contains("west") || input == "w")
            {
                _player.MoveWest();
            }
      
            else if(input == "inventory" || input == "i")
            {
                foreach(InventoryItem inventoryItem in _player.Inventory)
                {
                    Console.WriteLine("{0}: {1}", inventoryItem.Details.Name, inventoryItem.Quantity);
                }
            }

            else if (input == "stats")
            {
                Console.WriteLine("Current hit points: \t{0}", _player.CurrentHitPoints);
                Console.WriteLine("Maximum hit points: \t{0}", _player.MaximumHitPoints);
                Console.WriteLine("Experience points: \t{0}", _player.ExperiencePoints);
                Console.WriteLine("Level: \t\t\t{0}", _player.Level);
                Console.WriteLine("Gold: \t\t\t{0}", _player.Gold);
            }
            else if(input == "quests")
            {
                if (_player.Quests.Count == 0)
                {
                    Console.WriteLine("You do not have any quests.");
                } else
                {
                    foreach (PlayerQuest playerQuest in _player.Quests)
                    {
                        Console.WriteLine("{0}: {1}", playerQuest.Details.Name,
                            playerQuest.IsCompleted ? "Completed" : "Incomplete");
                    }
                }
            } else if (input.Contains("attack") || input == "a")
            {
                if(_player.CurrentLocation.MonsterLivingHere == null)
                {
                    Console.WriteLine("There is nothing here to attack.");
                } else
                {
                    if(_player.CurrentWeapon == null)
                    {
                        Console.WriteLine("You do are not equipped with a weapon.");
                    }  else
                    {
                        _player.UseWeapon(_player.CurrentWeapon);
                    }
                }
            } else if(input.StartsWith("equip "))
            {
                _player.UpdateWeapons();
                string inputWeaponName = input.Substring(6).Trim();
                if (string.IsNullOrEmpty(inputWeaponName))
                {
                    Console.WriteLine("You must enter the name of the weapon to equip.");
                } else
                {
                    Weapon weaponToEquip = _player.Weapons.SingleOrDefault(x => x.Name.ToLower() == inputWeaponName || x.NamePlural.ToLower() == inputWeaponName);
                    
                   
                    if (weaponToEquip == null)
                    {
                        Console.WriteLine("You do not have the weapon: {0}", inputWeaponName);
                    } else
                    {
                        _player.CurrentWeapon = weaponToEquip;
                        Console.WriteLine("You equip your {0}", _player.CurrentWeapon.Name);
                    }
                }
            } else if (input == "weapons")
            {
                _player.UpdateWeapons();
                Console.WriteLine("List of Weapons:");
                foreach (Weapon w in _player.Weapons)
                {
                    Console.WriteLine("\t" + w.Name);
                }
            }
        
        
            

          

            else
            {
                System.Console.WriteLine("I don't understand. Sorry.");
            }

        } //ParseInput

        private static void DisplayCurrentLocation() {
            Console.WriteLine("You are at: {0}", _player.CurrentLocation.Name);

            if(_player.CurrentLocation.Description != "")
            {
                Console.WriteLine("\t"+ _player.CurrentLocation.Description);
            }
        }


    }
}
