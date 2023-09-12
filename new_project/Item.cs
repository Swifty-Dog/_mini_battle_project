class Item
{
    public int ID;

    public readonly string SingleName;
    public readonly string PluralName;
    public int Amount;

    public Item(int id, string singleName, string pluralName)
    {
        ID = id;
        SingleName = singleName;
        PluralName = pluralName;
        Amount = 1;

    }
    public string ItemName()
    {
        if (Amount > 1)
        {
            return PluralName;
        }
        return SingleName;
    }
}