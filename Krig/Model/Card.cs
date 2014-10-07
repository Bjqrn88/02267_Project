using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krig.ViewModel
{
    class Card
    {
        private int value;
        private int color; //0 for black, 1 for red
        private String suit;

        public Card(int value, int color, String suit)
        {
            this.value = value;
            this.color = color;
            this.suit = suit;
        }

        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        public int Color
        {
            get
            {
                return color;
            }
            set
            {
                this.color = value;
            }
        }

        public String Suit
        {
            get
            {
                return suit;
            }
            set
            {
                this.suit = value;
            }
        }
        public override String ToString()
        {
            String s = "The card is: " + value + " of " + suit;
            return s;
        }
        
    }
}
