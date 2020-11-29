using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Canoe.Models
{

    public class AccessLevel
    {
        public int AccessLevelID { get; set; }
        public string AccessLevelDescription { get; set; }
    }

    public class Guild
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int LeaderCD { get; set; }
    }

    public class Cards
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Boolean CommanderCapable { get; set; }
        public string Rarity { get; set; }
    }

    public class CardView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double ConvertedManaCost { get; set; }
        public Boolean CommanderCapable { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public int RarityCD { get; set; }
        public string Rarity { get; set; }
        public string ManaCost { get; set; }

    }
    public class CardViewComplete
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double ConvertedManaCost { get; set; }
        public Boolean CommanderCapable { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public string ManaCost { get; set; }
        public string Rarity { get; set; }
        public string Tribes { get; set; }
        public string Classes { get; set; }
        public string Colors { get; set; }

    }


    public class Classes
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class ClassesEdit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int pageNumber { get; set; }
    }


    public class Colors
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class ColorsEdit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int pageNumber { get; set; }
    }

    public class PlayerDeck
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public string Commander { get; set; }
        public int PlayerCD { get; set; }

    }
    public class DeckFormats
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class DeckFormatsEdit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int pageNumber { get; set; }
    }
    public class MatchTypes
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class MatchTypesEdit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int pageNumber { get; set; }
    }

    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
       
        public DateTime? DateOfBirth { get; set; }
        public int? GuildCD { get; set; }
    }

    public class PlayerEdit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GuildCD { get; set; }
        public int pageNumber { get; set; }
    }

    public class PlayerCollection
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Boolean CommanderCapable { get; set; }
        public string Rarity { get; set; }
        public int CardCD { get; set; }
        public int? PlayerCD { get; set; }
        public string Deck { get; set; }
    }

    public class Rarity
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class RarityEdit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int pageNumber { get; set; }
    }

    public class Sets
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class SetsEdit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int pageNumber { get; set; }
    }


    public class Tribes
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class TribesEdit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int pageNumber { get; set; }
    }
}
