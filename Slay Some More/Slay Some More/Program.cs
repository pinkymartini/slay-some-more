using System.Collections.Generic;

namespace SlaySomeMore
{
    class Player
    {
        public int health;
        public int mana;
        public int swordDamage;
        public int magicDamage;
        public int healAmount;
        public int meditateLevel;
        public int poisonLevel;
        //public int actionPoints;
        public Player()
        {
          health = 100;
          mana = 100;
          swordDamage = 5;
          magicDamage = 10;
          healAmount = 5;
          meditateLevel = 10;
          poisonLevel = 5;    
        }
        public void SwordAttack( Monster monster)
        {
            monster.health -= swordDamage;
        }
        public void MagicStrike(Monster monster)
        {
            
                monster.health -= magicDamage;
           
        }
        public void HealingSpell()
        {
            health += healAmount;
        }
        public void PoisonSpell(Monster monster)
        {//for 3 turns 
            //monster.health -= poisonLevel;
            monster.isPoisoned = true;
        }
        public void Meditate()
        {
            mana += meditateLevel;
        }
        public void GainStat()
        {
            Console.WriteLine("Please select a skill to upgrade. ");
            Console.WriteLine(" (0) Sword damage (current) : " + swordDamage + " Sword damage after upgrade: " + (swordDamage + 5));
            Console.WriteLine(" (1) Magic damage (current) : " + magicDamage + " Magic damage after upgrade: " + (magicDamage + 5));
            Console.WriteLine(" (2) Healing power (current) : " + healAmount + " Healing power after upgrade: " + (healAmount + 5));
            Console.WriteLine(" (3) Poison damage (current) : " + poisonLevel + " Poison damage after upgrade: " + (poisonLevel + 5));
            Console.WriteLine(" (4) Meditate amount (current) : " + meditateLevel + " Meditate amount after upgrade: " + (meditateLevel + 5));
            
            ConsoleKeyInfo choice=Console.ReadKey();
            Console.Write("\b");
            int choiceIndex= int.Parse(choice.KeyChar.ToString());


            //int choice = Convert.ToInt32(Console.ReadLine());
            if(choiceIndex == 0)
            {
                swordDamage += 5;
            }
            else if(choiceIndex == 1)
            {
                magicDamage += 5;
            }
            else if (choiceIndex == 2)
            {
                healAmount += 5;
            }
            else if (choiceIndex == 3)
            {
                poisonLevel += 5;
            }
            else if (choiceIndex == 4)
            {
             
                meditateLevel += 5;
            }
            else
            {
                Console.WriteLine("Please select a valid skill index. ");
                GainStat();
            }
        }

    }
    class Monster
    {
        public int health;
        public int damage;
        public int magicResistance;
        public bool isPoisoned;

        public Monster()
        {
            health=15;
            Random random=new Random();
            damage=5;
            magicResistance=0;
            isPoisoned=false;
        }

        public Monster(int health, int damage,int magicResistance)
        {
            this.health=health;
            this.damage=damage;
            this.magicResistance=magicResistance;
            isPoisoned=false;
        }

        public void AttackPlayer(Player player)
        {
            Random random=new Random();
            int randomDamage = random.Next(0, 6);
            player.health -= randomDamage;

        }
        public void RecievePoisonDamage( Player player)
        {
            if (isPoisoned)
            {
                health -= player.poisonLevel;
            }
           
        }
    }
    class Program
    {
        enum Action { SwordAttack , MagicAttack, Heal, PoisonAttack, Meditate, Quit };
        static void Main(string[]args)
        {
                GameLoop();
        }

    public static void GameLoop()
        {
            Player player = new Player();
            bool isGameOver = false;
            ConsoleKeyInfo quitInput;
           int monsterCounter = 1;
            ConsoleKeyInfo spellChoiceIndex;
            ConsoleKeyInfo input;
            int targetIndex;
            int poisonTurn = 1;
            int monsterKilled = 0;

            List<Monster> monsters = new List<Monster>();
            for (int i = 0; i <monsterCounter; i++)
            {
                monsters.Add(new Monster());

            }
            string title = @"
  ██████  ██▓    ▄▄▄     ▓██   ██▓     ██████  ▒█████   ███▄ ▄███▓▓█████     ███▄ ▄███▓ ▒█████   ██▀███  ▓█████ 
▒██    ▒ ▓██▒   ▒████▄    ▒██  ██▒   ▒██    ▒ ▒██▒  ██▒▓██▒▀█▀ ██▒▓█   ▀    ▓██▒▀█▀ ██▒▒██▒  ██▒▓██ ▒ ██▒▓█   ▀ 
░ ▓██▄   ▒██░   ▒██  ▀█▄   ▒██ ██░   ░ ▓██▄   ▒██░  ██▒▓██    ▓██░▒███      ▓██    ▓██░▒██░  ██▒▓██ ░▄█ ▒▒███   
  ▒   ██▒▒██░   ░██▄▄▄▄██  ░ ▐██▓░     ▒   ██▒▒██   ██░▒██    ▒██ ▒▓█  ▄    ▒██    ▒██ ▒██   ██░▒██▀▀█▄  ▒▓█  ▄ 
▒██████▒▒░██████▒▓█   ▓██▒ ░ ██▒▓░   ▒██████▒▒░ ████▓▒░▒██▒   ░██▒░▒████▒   ▒██▒   ░██▒░ ████▓▒░░██▓ ▒██▒░▒████▒
▒ ▒▓▒ ▒ ░░ ▒░▓  ░▒▒   ▓▒█░  ██▒▒▒    ▒ ▒▓▒ ▒ ░░ ▒░▒░▒░ ░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░   ░  ░░ ▒░▒░▒░ ░ ▒▓ ░▒▓░░░ ▒░ ░
░ ░▒  ░ ░░ ░ ▒  ░ ▒   ▒▒ ░▓██ ░▒░    ░ ░▒  ░ ░  ░ ▒ ▒░ ░  ░      ░ ░ ░  ░   ░  ░      ░  ░ ▒ ▒░   ░▒ ░ ▒░ ░ ░  ░
░  ░  ░    ░ ░    ░   ▒   ▒ ▒ ░░     ░  ░  ░  ░ ░ ░ ▒  ░      ░      ░      ░      ░   ░ ░ ░ ▒    ░░   ░    ░   
      ░      ░  ░     ░  ░░ ░              ░      ░ ░         ░      ░  ░          ░       ░ ░     ░        ░  ░
                          ░ ░                                                                                   
";
            string op1 = @"                                            _                _              _               _   
 _ __  _ _  ___  ___ ___  __ _  _ _  _  _  | |__ ___  _  _  | |_  ___   ___| |_  __ _  _ _ | |_ 
| '_ \| '_|/ -_)(_-<(_-< / _` || ' \| || | | / // -_)| || | |  _|/ _ \ (_-<|  _|/ _` || '_||  _|
| .__/|_|  \___|/__//__/ \__,_||_||_|\_, | |_\_\\___| \_, |  \__|\___/ /__/ \__|\__,_||_|   \__|
|_|                                  |__/             |__/                                      ";
            
            
            string op2 =@"                                  _                      _  _   
 _ __  _ _  ___  ___ ___  __ _  | |_  ___   __ _  _  _ (_)| |_ 
| '_ \| '_|/ -_)(_-<(_-< / _` | |  _|/ _ \ / _` || || || ||  _|
| .__/|_|  \___|/__//__/ \__, |  \__|\___/ \__, | \_,_||_| \__|
|_|                         |_|               |_|              ";


            
            Console.WriteLine(title);
            Console.WriteLine(op1);
            Console.WriteLine(op2);
          
            quitInput = Console.ReadKey();

            if (quitInput.Key == ConsoleKey.Q)
            {
                isGameOver = true;
                Console.Clear();
            }
            else
            {
                Console.Clear();
                while (!isGameOver)
                {

                    bool levelCleared = true;
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        if(monsters[i].isPoisoned)
                        {
                            monsters[i].RecievePoisonDamage(player);
                            if(monsters[i].health<=0)
                            {
                                monsters.RemoveAt(i);
                            }
                        }
                    }
                    if (player.health <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("YOU DIED. ");
                        Console.WriteLine("YOU HAVE SLAYED TOAL OF "+ monsterKilled+ " MONSTERS");
                        isGameOver = true;
                        break;
                    }
                    else if (quitInput.Key == ConsoleKey.Q)
                    {   
                        isGameOver = true;
                        break;
                    }

                    for (int i = 0; i < monsters.Count; i++)
                    {
                        Console.Write("@" + '\t');
                    }
                    Console.WriteLine();
                    Print("You see " + monsters.Count + "  monster(s). What will you do?",15);
                    Print("You have " + player.mana + " mana.",15);
                    Print("(0) Sword Attack for " + player.swordDamage,10);
                    Print("(1) Magic Attack for " + player.magicDamage,10) ;
                    Print("(2) Heal for " + player.healAmount,10);
                    Print("(3) Poison Attack for "+ player.poisonLevel,10);
                    Print("(4) Meditate for " + player.meditateLevel,10);
                    Print("(5) Quit the game", 10);

                    spellChoiceIndex = Console.ReadKey();
                    Console.Clear();
                    if (spellChoiceIndex.Key==ConsoleKey.D0)
                    {
                       Console.WriteLine("Choose monster index to Sword Attack");
                       
                            for (int i = 0; i < monsters.Count; i++)
                            {
                                Console.WriteLine("Monster number " + i + " has " + monsters[i].health+ " health remaining");

                            }
                        input= Console.ReadKey();
                        targetIndex= int.Parse(input.KeyChar.ToString());
                        Console.Clear();
                        player.SwordAttack(monsters[targetIndex]);

                        if(monsters[targetIndex].health<=0)
                        {
                            monsters.RemoveAt(targetIndex);
                            monsterKilled++;
                        }

                        for (int i = 0; i < monsters.Count; i++)
                        {
                            Console.WriteLine("Monster number " + i + " now has " + monsters[i].health + " health remaining");

                        }

                    }
                    else if (spellChoiceIndex.Key == ConsoleKey.D1)
                    {
                        Print("Choose monster index to Magic Attack",15);

                        for (int i = 0; i < monsters.Count; i++)
                        {
                            Console.WriteLine("Monster number " + i + " has " + monsters[i].health + " health");

                        }

                        input = Console.ReadKey();
                        targetIndex = int.Parse(input.KeyChar.ToString());
                        Console.Clear();

                        if (player.mana<0)
                        {
                            Console.WriteLine("You dont have enough mana, attacking with a sword");
                            player.SwordAttack(monsters[targetIndex]);

                        }
                        else
                        {
                            player.MagicStrike(monsters[targetIndex]);
                            player.mana -= 10;
                        }

                        if (monsters[targetIndex].health <= 0)
                        {
                            monsters.RemoveAt(targetIndex);
                            monsterKilled++;

                        }
                        for (int i = 0; i < monsters.Count; i++)
                        {
                            Console.WriteLine("Monster number " + i + " now has " + monsters[i].health + " health remaining");

                        }

                    }
                    else if (spellChoiceIndex.Key == ConsoleKey.D2)
                    {
                        player.HealingSpell();
                        Console.WriteLine("You now have "+ player.health);
                        
                        for (int i = 0; i < monsters.Count; i++)
                        {
                            Console.WriteLine("Monster number " + i + " now has " + monsters[i].health);

                        }

                    }
                    else if (spellChoiceIndex.Key == ConsoleKey.D3)
                    {
                        Console.WriteLine("Choose monster index to Poison Attack (DAMAGE WILL BE APPLIED AT THE BEGINNING OF THE NEXT TURN)" );

                        for (int i = 0; i < monsters.Count; i++)
                        {
                            Console.WriteLine("Monster number " + i + " has " + monsters[i].health);

                        }
                        input = Console.ReadKey();
                        targetIndex = int.Parse(input.KeyChar.ToString());

                        player.PoisonSpell(monsters[targetIndex]);
                   
                        Console.Clear();
                        for (int i = 0; i < monsters.Count; i++)
                        {
                            Console.WriteLine("Monster number " + i + " now has " + monsters[i].health);

                        }

                    }
                    else if (spellChoiceIndex.Key == ConsoleKey.D4)
                    {
                        player.Meditate();

                        for (int i = 0; i < monsters.Count; i++)
                        {
                            Console.WriteLine("Monster number " + i + " now has " + monsters[i].health);

                        }

                    }
                    else if (spellChoiceIndex.Key == ConsoleKey.D5)
                    {
                        Print("Go.",100);
                        Print("TURN BACK!!!!!!", 200);
                        Console.WriteLine("YOU HAVE SLAYED TOAL OF " + monsterKilled + " MONSTERS");
                        isGameOver = true;
                        break;
                    }

                    for (int i = 0; i < monsters.Count; i++)
                    {
                        if(monsters[i].health>0)
                        {
                            levelCleared = false;
                        }
                    }
                    if (levelCleared)
                    {
                        Console.WriteLine("Gratz, you finished the level");
                        monsterCounter++;
                        monsters.Clear();
                        for (int i = 0; i < monsterCounter; i++)
                        {
                            monsters.Add(new Monster());

                        }
                        player.GainStat();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Monsters' turn. ");

                        for (int i = 0; i < monsters.Count; i++)
                        {
                            monsters[i].AttackPlayer(player);
                        }
                        Console.WriteLine("Your remaining health: " + player.health);
                    }

                }
            }

        }


        public static void Choices()
        {

        }

        public static void Print(string text,int speed)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);

            }
            Console.WriteLine();
        }

    }





}