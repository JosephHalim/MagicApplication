using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
   
    public class CardInfo
    {
        public string layout { get;  set; }
        public string name { get;  set; }
        public string manaCost { get;  set; }
        public string cmc { get;  set; }
        public List<string> colors { get;  set; }
        public string type { get;  set; }
        public List<string> subtypes { get;  set; }
        public string text { get;  set; }
        public string power { get;  set; }
        public string toughness { get;  set; }
        public string imagename { get;  set; }
        public List<string> printings { get;  set; }
        public List<Legalities> legalities { get;  set; }
        public List<string> colorIdentity { get;  set; }
    }
    /*public class Colors
    {
        public string ColorValues { get; set; }
    }
    public class Subtypes
    {
        public string SubtypesValues { get; set; }
    }
    public class Printings
    {
        public string PrintingsValues { get; set; }
    }*/
    public class Legalities
    {
        public string format { get; set; }
        public string legality { get; set; }
    }/*
    public class ColorIdentity
    {
        public string ColorIdentityValues { get; set; }
    }*/
        public class Card
        {
            public string artist { get; set; }
            public int cmc { get; set; }
            public List<string> colorIdentity { get; set; }
            public List<string> colors { get; set; }
            public string flavor { get; set; }
            public string id { get; set; }
            public string imageName { get; set; }
            public string layout { get; set; }
            public string manaCost { get; set; }
            public string mciNumber { get; set; }
            public int multiverseid { get; set; }
            public string name { get; set; }
            public string power { get; set; }
            public string rarity { get; set; }
            public List<string> subtypes { get; set; }
            public string text { get; set; }
            public string toughness { get; set; }
            public string type { get; set; }
            public List<string> types { get; set; }
            public bool? reserved { get; set; }
            public List<string> supertypes { get; set; }
            public List<int?> variations { get; set; }
            public bool? starter { get; set; }
        }

        public class Sets
        {
            public string name { get; set; }
            public string code { get; set; }
            public string gathererCode { get; set; }
            public string magicCardsInfoCode { get; set; }
            public string releaseDate { get; set; }
            public string border { get; set; }
            public string type { get; set; }
            public List<string> booster { get; set; }
            public string mkm_name { get; set; }
            public int mkm_id { get; set; }
            public List<Card> cards { get; set; }
        }
}
