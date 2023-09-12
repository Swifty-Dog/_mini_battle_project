class Location
{
    public readonly int ID;
    public readonly string Name;
    public readonly string MapIcon;
    public readonly string Description;
    public readonly string? QuestGiver;
    public Location?  LocationToNorth;
    public Location? LocationToEast;
    public Location? LocationToSouth;
    public Location? LocationToWest;
    public Quest? QuestAvailableHere;
    public Monster? MonsterLivingHere;

    public Location(int id, string name, string mapIcon, string description, string? questGiver)
    {
        ID = id;
        Name = name;
        MapIcon = mapIcon;
        Description = description;
        QuestGiver = questGiver;
        LocationToNorth = null;
        LocationToEast = null;
        LocationToSouth = null;
        LocationToWest = null;
    }
    public string Compass()
    {
        string compass = "";
        if (LocationToNorth != null)
        {
            compass += "    N\n    |\n";
        }
        if (LocationToEast != null && LocationToWest != null)
        {
            compass += "W---|---E";
        }
        else if (LocationToWest != null)
        {
            compass += "W---|";
        }
        else if (LocationToEast != null)
        {
            compass += "    |---E";
        }
        else
        {
            compass += "    |";
        }
        if (LocationToSouth != null)
        {
            compass += "\n    |\n    S";
        }
        compass += "\n";
        return compass;
    }
    public string Map()
    {
        Console.WriteLine("-------------------------------------------------------------------");
        string map = "    P\n    |\n    A\n    |\nV-F-T-G-B-S\n    |\n    H";
        map += $"\nCurrent Location: {Name} ({MapIcon})";
        return map;
    }
}