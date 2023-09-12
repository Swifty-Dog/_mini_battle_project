using System.ComponentModel.Design;
using System.Numerics;

class Player
{
    public Weapon EquippedWeapon;
    public double Health;
    public Location CurrentLocation;
    public Quest? CurrentQuest;
    public List<Item> Inventory;
    public int PlayerLevel;
    public double AttackMultiplier;
    public double MaxHealth;
    public int Experience;
    public int Gold;
    public int Block;


    public Player(double health, Weapon equippedWeapon, Location currentLocation)
    {
        MaxHealth = health;
        EquippedWeapon = equippedWeapon;
        CurrentLocation = currentLocation;
        CurrentQuest = null;
        Inventory = new();
        PlayerLevel = 1;
        AttackMultiplier = 1;
        Health = MaxHealth;
        Experience = 0;
        Gold = 0;
        Block = 0;

    }
    public void Move()
    {
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"Where would you like to go?\nYou are at: {CurrentLocation.Name}. " +
                          $"From here you can go:\n{CurrentLocation.Compass()}");
        string direction = Console.ReadLine().ToUpper();
        switch (direction)
        {
            case "N":
                if (CurrentLocation.LocationToNorth != null)
                {
                    CurrentLocation = CurrentLocation.LocationToNorth;
                }
                else
                {
                    Console.WriteLine("\nYou are unable to move North.");
                }
                break;
            case "E":
                if (CurrentLocation.LocationToEast != null)
                {
                    CurrentLocation = CurrentLocation.LocationToEast;
                }
                else
                {
                    Console.WriteLine("\nYou are unable to move East.");
                }
                break;
            case "S":
                if (CurrentLocation.LocationToSouth != null)
                {
                    if (CurrentLocation.Name == "Town square" && Health != MaxHealth) 
                    {
                        Health = MaxHealth;
                        Console.WriteLine($"\nYou've had a good sleep. Your health got regenerated to {Health}.");
                    }
                    CurrentLocation = CurrentLocation.LocationToSouth;
                }
                else
                {
                    Console.WriteLine("\nYou are unable to move South.");
                }
                break;
            case "W":
                if (CurrentLocation.LocationToWest != null)
                {
                    CurrentLocation = CurrentLocation.LocationToWest;
                }
                else
                {
                    Console.WriteLine("\nYou are unable to move West.");
                }
                break;
            default:
                Console.WriteLine($"\n{direction} is not a valid direction");
                break;
        }
    }
    
    public string Info()
    {
        // Prints player level, the health, current quest and equipped weapon(+ stats)
        Console.WriteLine("-------------------------------------------------------------------");
        string playerInfo = "";
        playerInfo += $"Level: {PlayerLevel}";
        playerInfo += $"\nXP: {Experience}";
        playerInfo += $"\nHealth: {Health}/{MaxHealth}\n";
        

        // If there is no quest, the "current quest" will show "none"

        if (CurrentQuest != null)
        {
            playerInfo += $"Current quest: {CurrentQuest.Name}\n    {CurrentQuest.Description}\n    Collect: {CurrentQuest.AmountToCollect} {CurrentQuest.ItemToCollect.PluralName}\n";
        }
        else
        {
            playerInfo += "Current quest: None\n";
        }
        playerInfo += $"Weapon: {EquippedWeapon.Name}\n        Damage: {Math.Round(EquippedWeapon.Damage * AttackMultiplier, 2)}\n        Crit chance: {EquippedWeapon.CritChance}\n" +
            $"Inventory:\n";
        foreach(Item item in Inventory)
        {
            playerInfo += $"{item.Amount} x {item.ItemName()}\n";
        }
        return playerInfo;  
    }
    
    public void GetItem(Item item)
    {
        if (Inventory.Contains(item))
        {
            item.Amount++;
        }
        else
        {
            Inventory.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        if (Inventory.Contains(item) && item.Amount > 1)
        {
            item.Amount--;
        }
        else if (Inventory.Contains(item))
        {
            Inventory.Remove(item);
        }
    }


    public void LevelUp()
    {
        if (Experience % 100 == 0)
        {
            PlayerLevel++;
            AttackMultiplier += 0.15;
            MaxHealth += 2;
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine($"You Leveled up!\nCurrent level: {PlayerLevel}\n");
        }
    }
    
    public void AdventurersPassCheck()
    {
        if (CurrentLocation.ID == World.LOCATION_ID_GUARD_POST && Block == 0)
        {
            if (Inventory.Contains(World.ItemByID(7)))
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("It seems you have proven yourself and obtained the adventurers pass\n You may continue to the bridge");
                Block = 1;
            }
            else
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("\nYou must obtain the adventurers pass to reach the bridge");
                World.LocationByID(3).LocationToEast = null;
            }
        }
    }
}