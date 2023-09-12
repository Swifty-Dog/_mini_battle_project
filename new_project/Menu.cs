using System.Numerics;
using System.Security.Cryptography.X509Certificates;

class Menu
{
    public Player User;
    public bool Playing;

    public Menu(Player user)
    {
        User = user;
        Playing = true;
    }
    public void GiveQuest()
    {
        if (User.CurrentQuest != User.CurrentLocation.QuestAvailableHere && User.CurrentQuest == null)
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine($"The {User.CurrentLocation.QuestGiver} has a quest for you.");
            while (true)
            {
                Console.WriteLine($"\nDo you want to accept the quest (y/n): {User.CurrentLocation.QuestAvailableHere.Name}");
                switch (Console.ReadLine().ToLower())
                {
                    case "y":
                        User.CurrentQuest = User.CurrentLocation.QuestAvailableHere;
                        break;
                    case "n":
                        break;
                    default:
                        continue;
                }
                break;
            }
        }
    }
    public void CompleteQuest()
    {
        if (User.CurrentLocation.QuestAvailableHere == User.CurrentQuest && User.CurrentQuest != null)
        {
            Quest quest = User.CurrentQuest;
            bool checking = true;
            while (checking)
            {
                {
                    bool completed = false;
                    foreach (Item item in User.Inventory)
                    {
                        if (item == quest.ItemToCollect && item.Amount >= quest.AmountToCollect)
                        {
                            Console.WriteLine("-------------------------------------------------------------------");
                            Console.WriteLine($"Congrats! You have collected enough {quest.ItemToCollect.ItemName()}.");                      
                            Console.WriteLine("Take this as a token of gratitude.");
                            if (quest.Item == null)
                            {
                                Console.WriteLine("-------------------------------------------------------------------");
                                Console.WriteLine($"You have received: {quest.Weapon.Name}");
                                User.EquippedWeapon = quest.Weapon;
                                completed = true;
                                User.CurrentQuest = null;
                                World.Quests.Remove(quest);
                                User.CurrentLocation.QuestAvailableHere = null;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("-------------------------------------------------------------------");
                                Console.WriteLine($"*you have received: {quest.Item.SingleName}.");
                                User.GetItem(quest.Item);
                                completed = true;
                                User.CurrentQuest = null;
                                World.Quests.Remove(quest);
                                User.CurrentLocation.QuestAvailableHere = null;
                                break;
                            }
                        }
                    }

                    if (completed)
                    {
                        while (User.Inventory.Contains(quest.ItemToCollect))
                        {
                            User.RemoveItem(quest.ItemToCollect);
                        }
                    }

                    else
                    {
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine($"This is not enough. Return when you have {quest.AmountToCollect} {quest.ItemToCollect.ItemName()}");
                    }
                    checking = false;
                }
            }
        }
    }
    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("What would you like to do (enter a number)?\n1: See game stats\n2: Move\n3: Fight\n4: Map\n5: Quit");
            string choice;

            choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    Console.WriteLine(User.Info());
                    break;
                case "2":
                    User.Move();
                    break;
                case "3":
                    Battle currentBattle = new(User);
                    currentBattle.InBattle();
                    break;
                case "4":
                    Console.WriteLine(User.CurrentLocation.Map());
                    break;
                case "5":
                    Playing = false;
                    break;
                default:
                    Console.WriteLine("Invalid Choice\n");
                    continue;
            }
            break;
        }
    }
}
