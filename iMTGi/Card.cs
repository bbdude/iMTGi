using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMTGi
{
    public class Card
    {
        public string name = null;
        string cost = null;
        string type = null;
        string attdDef = null;
        string block = null;
        string[] text = new string[10];
        string chunkText;
        string color = "";
        int id = 0;
        //List<string> text = new List<string>();

        public Card()
        { }
        public Card(string nName, string nCost, string nType, string nAttDef, string[] nText,string nBlock,string nColor)
        {
            block = nBlock;
            name = nName;
            cost = nCost;
            type = nType;
            attdDef = nAttDef;
            text = nText;
            color = nColor;
        }
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set {name = value ;} }
        public string Cost { get { return cost; } set { cost = value; } }
        public string Type { get { return type; } set { type = value; } }
        public string[] Text { get { return text; } set { text = value; } }
        public string ChunkText { get { return chunkText; } set { chunkText = value; } }
        public string Block { get { return block; } set { block = value; } }
        public string Color { get { return color; } set { color = value; } }
        
    }
}
