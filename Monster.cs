public class Monster
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int CurrentHitPoints { get; set; }
    public int MaximumHitPoints { get; set; }
    public int MaximumDamage {  get; set; }

    public Monster(int ID, string Name, int CurrentHitPoints, int MaximumHitPoints, int MaximumDamage)
    {
        this.ID = ID;
        this.Name = Name;
        this.CurrentHitPoints = CurrentHitPoints;
        this.MaximumHitPoints = MaximumHitPoints;
        this.MaximumDamage = MaximumDamage;
    }

    public void Fight(Player player, Monster monster, string questType)
    {
        bool inCombat = true;

        while (inCombat && player.HitPoints > 0 && monster.CurrentHitPoints > 0)
        {
            Console.WriteLine("Choose an action: (1) Attack (2) Defend (3) Use Potion (4) Flee");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    player.Attack(monster);
                    break;
                case "2":
                    player.Defend();
                    break;
                case "3":
                    player.UsePotion();
                    break;
                case "4":
                    if (questType == "Side Quest")
                    {
                        Console.WriteLine($"You fled from the {monster.Name}. The quest is canceled.");
                        inCombat = false;
                    }
                    else
                    {
                        Console.WriteLine("You cannot flee from a main quest!");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            if (monster.CurrentHitPoints > 0)
            {
                monster.Attack(player);
                Console.WriteLine($"{monster.Name} has {monster.CurrentHitPoints} HP left.");
            }

            if (player.HitPoints <= 0)
            {
                Console.WriteLine("You were defeated!");
                inCombat = false;
            }
            else if (monster.CurrentHitPoints <= 0)
            {
                Console.WriteLine($"You defeated {monster.Name}!");
                inCombat = false;
            }
        }
    }

}

// Sogaand - 09/11/2024

// UPDATE LOG:
// --Can now choose one of four actions; Attack, Defend, Use Potion or Flee
// --Fleeing is only possible on a SIDE QUEST
// --Exits combat after player or monster dies


// TO DO: 
// --*IMPLEMENT PLAYER's Attack, Defend and UsePotion method
// --*IMPLEMENT MONSTERS's Attack (and maybe Defend?) method
// --Probably add player HitPoints as well after monster attacks (line 56/57)
