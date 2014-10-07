using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krig.ViewModel
{
    class StartUp
    {
        private ArrayList deck1, deck2;

        public StartUp()
        {
            generateDecks();
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

        private void shuffleDecks()
        {
            
        }
    }
}
