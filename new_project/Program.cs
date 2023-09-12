class Program
{
    static void Main()
    {
        Player player = new(30, World.Weapons[0], World.Locations[0]);
        Menu menu = new(player);
        while (menu.Playing)
        {   
            player.AdventurersPassCheck();
            if (World.Quests.Count == 0)
            {
                menu.Playing = false;
                Console.WriteLine("You Won.");
            }
            menu.CompleteQuest();
            menu.GiveQuest();
            menu.MainMenu();
        }
    }
}
