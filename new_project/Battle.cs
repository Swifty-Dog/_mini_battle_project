using System;
using System.Reflection.Metadata;
using System.Threading;

class Battle
{
    public Player Hero;
    public Weapon EquippedWeapon;
    public Monster CurrentMonster;

    public Battle(Player user)
    {
        Hero = user;
        EquippedWeapon = Hero.EquippedWeapon;
        CurrentMonster = Hero.CurrentLocation.MonsterLivingHere;
    }

    public void InBattle()
    {
        bool inBattle = true;
        bool playerWinner = false;
        bool monsterWinner = false;
        if (CurrentMonster == null)
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("There is nothing to attack in this area");
            inBattle = false;
        }
        else
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine($"A {CurrentMonster.SingleName} attacks!");
        }

        while (inBattle)
        {
            Console.WriteLine(
                $"\nYour health: {Hero.Health}\n{CurrentMonster.SingleName}'s health: {CurrentMonster.BattleHealth}");
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1: Attack\n2: Heal\n3: Leave\n4: Quit");
            string battleChoice = Console.ReadLine();
            Console.Clear();
            switch (battleChoice)
            {
                case "1":

                    var attackDamage = EquippedWeapon.AttackDamage();
                    var monsterDamage = CurrentMonster.Attack();
                    if (CurrentMonster.Evasion())
                    {
                        Thread.Sleep(500);
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine($"The {CurrentMonster.SingleName} avoided your attack.");
                        Thread.Sleep(200);
                    }
                    else if (EquippedWeapon.StunHit())
                    {
                        Thread.Sleep(500);
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine($"You swing your {EquippedWeapon.Name}");
                        Thread.Sleep(500);
                        Console.WriteLine($"You deal {Math.Round(attackDamage * Hero.AttackMultiplier, 2)} damage");
                        CurrentMonster.BattleHealth -= Math.Round(attackDamage * Hero.AttackMultiplier, 2);
                        if (CurrentMonster.BattleHealth < 0)
                        {
                            CurrentMonster.BattleHealth = 0;
                        }
                        Thread.Sleep(500);
                        Console.WriteLine(
                            $"The {CurrentMonster} is stunned by your attack, it can not attack this round");
                        Thread.Sleep(300);
                        Console.WriteLine(
                            $"The {CurrentMonster.SingleName} has {CurrentMonster.BattleHealth} health left");
                        Thread.Sleep(300);

                    }
                    else
                    {
                        Thread.Sleep(500);
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine($"You swing your {EquippedWeapon.Name}");
                        Thread.Sleep(500);
                        Console.WriteLine($"You deal {Math.Round(attackDamage * Hero.AttackMultiplier, 2)} damage");
                        Thread.Sleep(500);
                        CurrentMonster.BattleHealth -= Math.Round(attackDamage * Hero.AttackMultiplier, 2);
                        if (CurrentMonster.BattleHealth < 0)
                        {
                            CurrentMonster.BattleHealth = 0;
                        }
                        Thread.Sleep(300);
                    }

                    if (CurrentMonster.BattleHealth == 0)
                    {
                        CurrentMonster.BattleHealth = CurrentMonster.Health;
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine($"You have defeated the {CurrentMonster.SingleName}");
                        CurrentMonster.PlayerGetsExp(Hero);
                        Console.WriteLine($"You gained {CurrentMonster.ExperienceGained} XP");
                        Item item = CurrentMonster.DropItem();
                        Hero.GetItem(item);
                        Console.WriteLine($"You picked up the {item.ItemName()}");
                        inBattle = false;
                        Hero.LevelUp();
                        break;
                    }
                    
                    Console.WriteLine($"\nThe {CurrentMonster.SingleName} strikes back!");
                    Thread.Sleep(500);
                    Console.WriteLine($"It deals {monsterDamage} damage");
                    Thread.Sleep(500);
                    Hero.Health -= monsterDamage;

                    if (Hero.Health <= 0)
                    {
                        Hero.Health = 0;
                        Console.WriteLine("\nYou have died, you spawn back at your home");
                        CurrentMonster.TakesItem(Hero);
                        Thread.Sleep(1000);
                        Hero.CurrentLocation = World.LocationByID(1);
                        Hero.Health = Hero.MaxHealth;
                        inBattle = false;
                    }

                    continue;

                case "2":
                    
                    var monsterDamage2 = CurrentMonster.Attack();
                    if (Hero.Health == Hero.MaxHealth)
                    {
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine("You are already at full health, you can not heal.");
                        Console.WriteLine("-------------------------------------------------------------------");
                        continue;
                    }
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("You drink a healing potion");
                    Hero.Health += 5;

                    if (Hero.Health > Hero.MaxHealth)
                    {
                        Hero.Health = Hero.MaxHealth;
                    }
                    Thread.Sleep(500);
                    Console.WriteLine($"You restored 5 health");
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine($"The {CurrentMonster.SingleName} strikes back!");
                    Thread.Sleep(500);
                    Console.WriteLine($"It deals {monsterDamage2} damage");
                    Thread.Sleep(500);
                    Hero.Health -= monsterDamage2;

                    if (Hero.Health <= 0)
                    {
                        Hero.Health = 0;
                        Console.WriteLine("\nYou have died, you spawn back at your home");
                        CurrentMonster.TakesItem(Hero);
                        Thread.Sleep(1000);
                        Hero.CurrentLocation = World.LocationByID(1);
                        Hero.Health = Hero.MaxHealth;
                        inBattle = false;
                    }
                    continue;

                case "3":
                    Console.WriteLine("You decide to come back another time");
                    Thread.Sleep(300);
                    inBattle = false;
                    break;

                case "4":
                    Console.WriteLine("You have quit the game");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}