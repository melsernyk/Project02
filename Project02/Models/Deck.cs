﻿using System.Collections.Generic;

namespace Project02.Models
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            Cards = new List<Card>();
        }
    }
}
