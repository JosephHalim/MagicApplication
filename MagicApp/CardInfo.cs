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
}
