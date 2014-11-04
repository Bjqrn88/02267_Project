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
            Console.WriteLine("Game time: " + (end - start)/1000);
        }


        private Boolean gameOver()
        {
            if (deck1.Count == 0 && swapPile1.Count == 0)
            {
                Console.WriteLine("Player 1 looses!");
                return true;
            }
            else if (deck2.Count == 0 && swapPile2.Count == 0)
            {
                Console.WriteLine("Player 2 looses!");
                return true;
            }
            else
            {
                return false;
            }
        }
        private void play()
        {
            //Setup swap piles
            swapPile1 = new ArrayList();
            swapPile2 = new ArrayList();
            warWinnings = new ArrayList();
            int turnCounter = 1;
            do
            {
                Console.WriteLine("Turn " + turnCounter + " begins");

                //Check if a player has run out of cards
                if (deck1.Count == 0)
                {
                    Console.WriteLine("Player 1 uses swap");
                    deck1 = shuffleDeck(swapPile1);
                    Console.WriteLine("Deck 1 size after swap: " + deck1.Count);
                }
                if (deck2.Count == 0)
                {
                    Console.WriteLine("Player 2 uses swap");
                    deck2 = shuffleDeck(swapPile2);
                    Console.WriteLine("Deck 2 size after swap: " + deck2.Count);
                }
                
                //Draw cards from decks.
                Console.WriteLine("Draw cards");

                Card player1Card = drawSingleCard(deck1);
                Console.WriteLine("Card 1: " + player1Card.Value);

                Card player2Card = drawSingleCard(deck2);
                Console.WriteLine("Card 2: " + player2Card.Value);

                //Declare war
                if (player1Card.Value == player2Card.Value)
                {
                    Console.WriteLine("War declared");
                    if (war())
                    {
                        moveCards(warWinnings, swapPile1);
                        swapPile1.Add(player1Card);
                        swapPile1.Add(player2Card);
                        warWinnings.Clear();
                        Console.WriteLine("Player 1 wins war!");
                    }
                    else
                    {
                        moveCards(warWinnings, swapPile2);
                        swapPile2.Add(player1Card);
                        swapPile2.Add(player2Card);
                        warWinnings.Clear();
                        Console.WriteLine("Player 2 wins war!");
                    }
                }
                //Player 1 wins
                else if (player1Card.Value > player2Card.Value)
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
                int cardsInPlay = deck1.Count + deck2.Count + swapPile1.Count + swapPile2.Count;
                if (cardsInPlay != 52)
                {
                    Console.WriteLine("CARD COUNT ERROR: " + cardsInPlay);
                    break;
                }
            } while (!gameOver());
            Console.WriteLine("Game ended");
        }
        /**
         * Handles war condition
         * Returns true if player1(Human) wins
        */
        private Boolean war()
        {
            //Check players have enough cards.
            int player1AdjustSize = 3;
            int player2AdjustSize = 3;
            if (deck1.Count < 4)
            {
                moveCards(deck1, swapPile1);
                deck1 = shuffleDeck(swapPile1);

                if (deck1.Count < 4)
                {
                    Console.WriteLine("Player 1 count exception");
                    player1AdjustSize = deck1.Count;
                }
            }
            if (deck2.Count < 4)
            {
                moveCards(deck2, swapPile2);
                deck2 = shuffleDeck(swapPile2);

                if (deck2.Count < 4)
                {
                    Console.WriteLine("Player 2 count exception");
                    player2AdjustSize = deck2.Count;
                }
            }
            ArrayList player1Cards = drawCards(deck1, player1AdjustSize);
            ArrayList player2Cards = drawCards(deck2, player2AdjustSize);

            //Add cards to war array
            moveCards(player1Cards, warWinnings);
            moveCards(player2Cards, warWinnings);

            Console.WriteLine("Player 1 has cards");
            for (int i = 0; i < player1Cards.Count; i++)
            {
                Console.WriteLine(player1Cards[i].ToString());
            }
            Console.WriteLine("Player 2 has cards");
            for (int i = 0; i < player2Cards.Count; i++)
            {
                Console.WriteLine(player2Cards[i].ToString());
            }

            //Select card
            int player1Pick = random.Next(0, player1AdjustSize-1);
            Card player1Card = (Card)player1Cards[player1Pick];
            Console.WriteLine("Player 1 picks: " + player1Card.ToString());

            int player2Pick = random.Next(0, player2AdjustSize-1);
            Card player2Card = (Card)player2Cards[player2Pick];
            Console.WriteLine("Player 2 picks: " + player2Card.ToString());

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

        private Card drawSingleCard(ArrayList deck)
        {
            Card card = (Card)deck[0];
            deck.RemoveAt(0);
            return card;
        }

        /**
         * Draws 3 cards from a deck. 
        */
        private ArrayList drawCards(ArrayList deck, int numberOfCards)
        {
            ArrayList cards = new ArrayList();
            for (int i = 0; i < numberOfCards; i++)
            {
                cards.Add(deck[0]);
                deck.RemoveAt(0);
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
                from.Remove(i);
            }
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
                    card = new Card(i, color, suit);
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

            while (deck.Count > 0)
            {
                int randomIndex = random.Next(0, deck.Count - 1);
                //Console.WriteLine("Random value is: " + randomIndex);

                shuffledDeck.Add(deck[randomIndex]);
                deck.RemoveAt(randomIndex);
            }
            Console.WriteLine("Deck size: " + shuffledDeck.Count);
            return shuffledDeck;
        }
    }
}