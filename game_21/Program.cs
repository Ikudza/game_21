using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_21
{
    class Player
    {
        protected string name;
        public int score; // curent score

        protected int winPercent = 0; // (count win game / all count game) %
        public int win;
        public int draw;
        public int lose;
        

        public Player()
        {

        }

        public Player(string name)
        {
            this.name = name;
        }

        //update % winner game
        public void updateSessionResult()
        {
            winPercent = win / (win + draw + lose);
        }

        //get 1 card from all cards
        public virtual void get_cards(Stack<int>cards)
        {
            score += cards.Pop();
            Console.WriteLine(String.Format("Your count: {0}", score));
        }

        public void show_cards()
        {
            Console.WriteLine(String.Format("{0} points of cards: {1}", name, score));
        }

        public void winner()
        {
            win += 1;
            updateSessionResult();
            Console.WriteLine(String.Format("{0} victory!", name));
        }
        public void losser()
        {
            lose += 1;
            updateSessionResult();
            Console.WriteLine(String.Format("{0} loser", name));
        }
        public void gameDraw()
        {
            draw += 1;
            updateSessionResult();
            Console.WriteLine("Draw!");
        }

        // Report all game in current session
        public void joint_account()
        {
            Console.WriteLine(String.Format("Player: {0}", name));
            Console.WriteLine(String.Format("Game winner: {0}", win));
            Console.WriteLine(String.Format("Game draw: {0}", draw));
            Console.WriteLine(String.Format("Game losser: {0}", lose));
            Console.WriteLine(String.Format("Winner Percent: {0}", winPercent));
        }
    }
    
    class AI : Player
    {
        public AI(string name)
        {
            this.name = name;
        }

        public override void get_cards(Stack<int> cards)
        {
            score += cards.Pop();
        }

        // Strategy game comp for game
        public bool Strategy(Stack<int> cards)
        {
            if (score <= 15)
            {
                get_cards(cards);
                Console.WriteLine(String.Format("{0} get card", name));
                return true;
            } else
            {
                Console.WriteLine(String.Format("{0} pass", name));
                return false;
            }
        }

    }
    class Game
    {
        //array point all cards
        public int[] Cards = { 2, 3, 4, 6, 7, 8, 9, 10, 11,
                               2, 3, 4, 6, 7, 8, 9, 10, 11,
                               2, 3, 4, 6, 7, 8, 9, 10, 11,
                               2, 3, 4, 6, 7, 8, 9, 10, 11};
        private Player player;
        private AI ai;
        public Game(Player player, AI ai)
        {
            this.player = player;
            this.ai = ai;
        }

        //start game
        public void start()
        {
            //update score for new start Game
            player.score = 0;
            ai.score = 0;
            string playerAnswer;
            bool aiNeedCards;
            Random rand = new Random();
            var gameCards = new Stack<int>(Cards.OrderBy(item => rand.Next()));
            do
            {
                Console.Clear();
                player.get_cards(gameCards);
                if (player.score >= 21)
                {
                    break;
                }
                aiNeedCards = ai.Strategy(gameCards);
                Console.WriteLine("You need card?(y/n)");
                playerAnswer = Console.ReadLine();
            } while (playerAnswer.Equals("y"));
            do
            {
                aiNeedCards = ai.Strategy(gameCards);
            } while (aiNeedCards);
            end();
        }

        // calculation of result players
        public void end()
        {
            Console.Clear();
            player.show_cards();
            ai.show_cards();
            Console.WriteLine();
            if (player.score == ai.score)
            {
                player.gameDraw();
                ai.gameDraw();
            }
            else if (player.score == 21)
            {
                player.winner();
                ai.losser();
            }
            else if (ai.score == 21)
            {
                ai.winner();
                player.losser();
            }
            else if (ai.score > 21)
            {
                if (player.score > 21)
                {
                    player.losser();
                    ai.losser();
                }
                else
                {
                    player.winner();
                    ai.losser();
                }
            }
            else if (player.score > 21)
            {
                player.losser();
                ai.winner();
            }
            else if (player.score > ai.score)
            {
                player.winner();
                ai.losser();
            }
            else
            {
                player.losser();
                ai.winner();
            }
            result();
        }

        //report game result players
        public void result()
        {
            Console.WriteLine();
            player.joint_account();
            Console.WriteLine();
            ai.joint_account();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string answer = "y";
            Console.Write("Hello! Please input your name: ");
            string name = Console.ReadLine();
            Player player = new Player(name);
            AI ai = new AI("Toster");
            while (answer.Equals("y"))
            {
                Game game = new Game(player, ai);
                game.start();
                Console.WriteLine();
                Console.WriteLine("You want to play still?(y/n)");
                answer = Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
