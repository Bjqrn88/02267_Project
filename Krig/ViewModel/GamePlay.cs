using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krig.ViewModel
{
    class GamePlay
    {
        private ArrayList deck1, deck2, swapPile1, swapPile2, warWinnings;
        private Random random = new Random();

        public GamePlay()
        {
            generateDecks();
            Console.WriteLine("Deck size: " + deck1.Count);
            deck1 = shuffleDeck(deck1);
            deck2 = shuffleDeck(deck2);
            long start = Environment.TickCount;
            play();
            long end = Environment.TickCount;
            Console.WriteLine("Game time: " + (end - start));
        }
        //Generates cards and creates a black and a red deck.
        private void generateDecks()
        {
            deck1 = new ArrayList();
            deck2 = new ArrayList();
            //Generate cards.
            Card card;
            //Loop though suits
            for (int s = 0; s < 4; s++)
            {
                String suit = ""; //Init value, should be changed by code.
                int color = 2; //Init value, should be changed by code.
                switch (s)
                {
                    case 0:
                        suit = "spades";
                        color = 0;
                        break;
                    case 1:
                        suit = "clubs";
                        color = 0;
                        break;
                    case 2:
                        suit = "hearts";
                        color = 1;
                        break;
                    case 3:
                        suit = "diamonds";
                        color = 1;
                        break;
                    default:
                        Console.WriteLine("We got problems");
                        break;
                }
                //Loop though values
                for (int i = 1; i <= 13; i++)
                {
                    card = new Card(i,color,suit);
                    //Console.WriteLine(card.ToString());
                    if (color == 0)
                    {
                        deck1.Add(card);
                    }
                    else
                    {
                        deck2.Add(card);
                    }
                }
            }
        }

        private ArrayList shuffleDeck(ArrayList deck)
        {
            ArrayList shuffledDeck = new ArrayList();

            while(deck.Count > 0)
            {
                int randomIndex = random.Next(0, deck.Count-1);
                //Console.WriteLine("Random value is: " + randomIndex);
                
                shuffledDeck.Add(deck[randomIndex]);
                deck.RemoveAt(randomIndex);
            }           
            Console.WriteLine("Deck size: " + shuffledDeck.Count);
            return shuffledDeck;
        }

        private Boolean gameOver()
        {
            if (deck1.Count == 0)
            {
                Console.WriteLine("Player 1 looses!");
                return true;
            }
            else if(deck2.Count == 0) 
            {
                Console.WriteLine("Player 2 looses!");
                return true;
            }
            else {
                return false;
            }
        }
        private void play() {
            //Setup swap piles
            swapPile1 = new ArrayList();
            swapPile2 = new ArrayList();
            warWinnings = new ArrayList();
            int turnCounter = 1;
            do {
                Console.WriteLine("Turn " + turnCounter + " begins");
                //Draw cards from decks.
                Console.WriteLine("Draw cards");
                Card player1Card = (Card) deck1[0];
                Console.WriteLine("Card 1: "+player1Card.Value);
                deck1.RemoveAt(0);
                Card player2Card = (Card) deck2[0];
                Console.WriteLine("Card 2: " + player2Card.Value);
                deck2.RemoveAt(0);

                //Check if a player has run out of cards
                if (deck1.Count == 0)
                {
                    Console.WriteLine("Player 1 uses swap");
                    deck1 = shuffleDeck(swapPile1);
                    Console.WriteLine("Deck 1 size after swap: " + deck2.Count);
                }
                if (deck2.Count == 0)
                {
                    Console.WriteLine("Player 2 uses swap");
                    deck2 = shuffleDeck(swapPile2);
                    Console.WriteLine("Deck 2 size after swap: "+deck2.Count);
                }

                //Declare war
                if (player1Card.Value == player2Card.Value)
                {
                    Console.WriteLine("War declared");
                    if (war())
                    {
                        moveCards(warWinnings, swapPile1);
                        swapPile1.Add(player1Card);
                        swapPile1.Add(player2Card);
                    }
                    else
                    {
                        moveCards(warWinnings, swapPile2);
                        swapPile2.Add(player1Card);
                        swapPile2.Add(player2Card);
                    }
                }
                //Player 1 wins
                else if(player1Card.Value > player2Card.Value)
                {
                    Console.WriteLine("Player 1 wins round");
                    swapPile1.Add(player1Card);
                    swapPile1.Add(player2Card);
                }
                //Player 2 wins
                else
                {
                    Console.WriteLine("Player 2 wins round");
                    swapPile2.Add(player1Card);
                    swapPile2.Add(player2Card);
                }
                Console.WriteLine("Deck sizes: deck1: " + deck1.Count + " deck2: " + deck2.Count);
                Console.WriteLine("Swap sizes: swap1: " + swapPile1.Count + " swap2: " + swapPile2.Count);
                turnCounter++;
            } while(!gameOver());
            Console.WriteLine("Game ended");
        }
        /**
         * Handles war condition
         * Returns true if player1(Human) wins
        */
        private Boolean war()
        {
            ArrayList player1Cards = drawCards(deck1);
            ArrayList player2Cards = drawCards(deck2);
            
            //Add cards to war array
            moveCards(player1Cards, warWinnings);
            moveCards(player2Cards, warWinnings);

            //Select card
            int player1Pick = random.Next(0, 2);
            Card player1Card = (Card) player1Cards[player1Pick];

            int player2Pick = random.Next(0, 2);
            Card player2Card = (Card)player2Cards[player2Pick];

            //Player 1 wins
            if (player1Card.Value > player2Card.Value)
            {
                return true;
            }
            //Player 2 wins
            else if (player2Card.Value > player1Card.Value)
            {
                return false;
            }
            else
            {
                //Recursive call.
                return war();
            }

        }

        /**
         * Draws 3 cards from a deck. 
        */
        private ArrayList drawCards(ArrayList deck)
        {
            ArrayList cards = new ArrayList();
            for (int i = 0; i < 3; i++)
            {
                cards.Add(deck[i]);
                deck.Remove(i);
            }
            return cards;
        }
        /**
         * Moves cards from one array to another.
        */
        private void moveCards(ArrayList from, ArrayList to) 
        {
            for (int i = 0; i < from.Count; i++)
            {
                Card c = (Card)from[i];
                to.Add(c);
                //to.Add((Card)from[i]);
            }
        }
    }

}
