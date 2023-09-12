using System.Linq;

class Quest
{
    public int ID;
    public string Name;
    public string Description;
    public Item? Item;
    public Weapon? Weapon;
    public Item ItemToCollect;
    public int AmountToCollect;

    public Quest(int id, string name, string description, Item? item, Weapon? weapon, Item? itemToCollect, int amountToCollect)
    {
        ID = id;
        Name = name;
        Description = description;
        Item = item;
        Weapon = weapon;
        ItemToCollect = itemToCollect;
        AmountToCollect = amountToCollect;
    }
}
