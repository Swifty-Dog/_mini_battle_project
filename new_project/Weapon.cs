class Weapon
{
    public int ID;
    public string Name;
    public int CritChance;
    public int StunChance;
    public int Damage;
    Random Rnd;

    public Weapon(int id, string name, int critChance, int stunChance, int damage)
    {
        ID = id;
       Name = name;
        CritChance = critChance;
        StunChance = stunChance;
        Damage = damage;
        Rnd = new();
    }

    public bool CriticalHit() => Rnd.Next(101) > (100 - CritChance);

    public bool StunHit() => Rnd.Next(101) > (100 - StunChance);


    public int AttackDamage()
    {
        int attackDamage = Damage;
        if (CriticalHit())
        {
            attackDamage *= 2;
        }
        return attackDamage;
    }
}
