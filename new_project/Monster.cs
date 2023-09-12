class Monster
{
    public int ID;
    public string SingleName;
    public string PluralName;
    public int Damage;
    public double Health;
    public double BattleHealth;
    public int EvasionChance;
    public int ExperienceGained;
    public List<Item> Loot;
    public int CritChance;
    Random Rnd = new Random();

    public Monster(int id, string singleName, string pluralName, int damage, int health, int evasionChance, int critChance, int experienceGained)
    {
        ID = id;
        SingleName = singleName;
        PluralName = pluralName;
        Damage = damage;
        Health = health;
        BattleHealth = Health;
        EvasionChance = evasionChance;
        CritChance = critChance;
        ExperienceGained = experienceGained;
        Loot = new();
        Rnd = new();
    }

    public int Attack()
    {
        int attackDamage = Damage;
        if (CriticalHit())
        {
            attackDamage *= 2;
        }
        return attackDamage;
    }

    public bool Evasion()
    {
        return Rnd.Next(101) > (100 - EvasionChance);
    }
    public void Reset()
    {
        BattleHealth = Health;
    }
    public Item DropItem()
    { 
        int range = Loot.Count();
        int num = Rnd.Next(range);
        return Loot[num];
    }

    public void PlayerGetsExp(Player player)
    {
            player.Experience = player.Experience + ExperienceGained;
    }


    public void PlayerGetsGold(Player player)
    {
        if (Health == 0)
        {
            player.Gold = player.Gold + 100;
        }
    }

    public void TakesItem(Player player)
    {
        
        int range = player.Inventory.Count();
        int num = Rnd.Next(range);
        Item lostItem = player.Inventory[num];
        if (lostItem != World.ItemByID(7))
        {
            Console.WriteLine($"You lost a {lostItem.SingleName}");
             player.RemoveItem(lostItem);
        }
        else
        {
            Console.WriteLine("Lucky you. You didn't lose anything.");
        }
    }
    
    public bool CriticalHit() => Rnd.Next(101) > (100 - CritChance);
}