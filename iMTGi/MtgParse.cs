using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace iMTGi
{
    public class MtgParse
    {
        StreamReader reader = File.OpenText("mtgfile.txt");
        string line;
        int row = 0; 
        int column = 0;
        int iterations = 0;
        string name = null;
        string newName = null;
        string cost = null;
        string type = null;
        string attdDef = null;
        string color = "L";
        //+Image cardImage = null;
        //List<string> text = new List<string>();
        string[] text = new string[10];
        public List<Card> cards = new List<Card>();
        public MtgParse() { }
        /*public void parse()
        {
            while ((line = reader.ReadLine()) != null)
            {
                //string[] lines = line.Split('\n');
                //int myInteger = int.Parse(lines[1]); // Here's your integer.
                // Now let's find the path.

                int blank;
                //foreach (string item in lines)
                //{
                if (row == 0)
                {
                    name = line;
                    row++;
                }
                else if (line == "Scheme")
                {
                    type = line;
                    row++;
                    row++;
                }
                else if (row == 1)
                {
                    cost = line;
                    row++;
                }
                else if (row == 2)
                {
                    type = line;
                    row++;
                }
                else if (line == "")
                {
                    //List<string> newText = text;
                    string[] newText = new string[10];
                    for (int i = 0; i != 10; i++)
                    {
                        if (text[i] != null)
                        {
                            newText[i] = String.Copy(text[i]);
                            if (i != 9)
                                if (text[i + 1] == null)
                                {
                                    block = String.Copy(text[i]);
                                    newText[i] = "";
                                }
                        }
                    }
                    Card card = new Card(name, cost, type, attdDef, newText, block);
                    cards.Add(card);
                    row = 0;
                    for (int i = 0; i != 10; i++)
                    {
                        text[i] = "";
                    }
                    name = null;
                    cost = null;
                    type = null;
                    attdDef = null;
                    column = 0;
                }
                else if (line != "" && int.TryParse(line[0].ToString(), out blank))
                {
                    attdDef = line;
                }
                else
                {
                    text[column] = line;
                    column++;
                }

                /if (line == "")
            {
                int bob = 5;
            }/
        }*/
        private string GetHtmlCode()
        {
            string url = "https://www.google.com/search?q=" + name + " mtg" + "&tbm=isch";
            //string url = "https://www.bing.com/images/search?q=" + name + " mtg" + "&FORM=HDRSC2";
            //string urlname = name.Replace(" ", "+");
            //string url = "http://www.dogpile.com/search/images?fcoid=417&fcop=topnav&fpid=27&q=" + urlname + "+mtg&ql=";
            string data = "";
            //try
            //{
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();

                using (Stream dataStream = response.GetResponseStream())
                {
                    if (dataStream == null)
                        return "";
                    using (var sr = new StreamReader(dataStream))
                    {
                        
                        data = sr.ReadToEnd();
                    }
                }

                return data;
            /*}
            catch (WebException ex)
            {
                using (var sr = new StreamReader(ex.Response.GetResponseStream()))
                    data = sr.ReadToEnd();
                return data;

            }*/
        }
        private List<string> GetUrls(string html)
        {
            var urls = new List<string>();
            int ndx = html.IndexOf("class=\"images_table\"", StringComparison.Ordinal);
            if (ndx == -1)
                ndx = 0;
            ndx = html.IndexOf("<img", ndx, StringComparison.Ordinal);

            while (ndx >= 0)
            {
                ndx = html.IndexOf("src=\"", ndx, StringComparison.Ordinal);
                ndx = ndx + 5;
                int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
                string url = html.Substring(ndx, ndx2 - ndx);
                urls.Add(url);
                ndx = html.IndexOf("<img", ndx, StringComparison.Ordinal);
            }
            return urls;
        }
        private byte[] GetImage(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return null;
                using (var sr = new BinaryReader(dataStream))
                {
                    byte[] bytes = sr.ReadBytes(100000);

                    return bytes;
                }
            }

            //return null;
        }
        /*
        public void parse(ProgressBar bar)
        {
            while ((line = reader.ReadLine()) != null)
            {
                //string[] lines = line.Split('\n');
                //int myInteger = int.Parse(lines[1]); // Here's your integer.
                // Now let's find the path.

                int blank;
                //foreach (string item in lines)
                //{
                if (line == "Akron Legionnaire")
                {
                    row = 0;
                }
                if (row == 0)
                {
                    name = line;
                    newName = String.Copy(name);
                    row++;
                }
                else if (line == "Scheme" || line == "Vanguard")
                {
                    type = line;
                    row++;
                    row++;
                }
                else if (row == 1)
                {
                    cost = line;
                    row++;
                }
                else if (row == 2)
                {
                    type = line;
                    row++;
                }
                else if (line == "")
                {
                    //List<string> newText = text;
                    string[] newText = new string[10];
                    for (int i = 0; i != 10; i++)
                    {
                        if (text[i] != null)
                        {
                            newText[i] = String.Copy(text[i]);
                            if (i != 9)
                                if (text[i + 1] == null)
                                {
                                    block = String.Copy(text[i]);
                                    newText[i] = "";
                                }
                        }
                    }
                    Card card = new Card(name, cost, type, attdDef, newText, block);
                    cards.Add(card);
                    row = 0;
                    for (int i = 0; i != 10; i++)
                    {
                        text[i] = "";
                    }
                    if (!File.Exists(name + ".png"))
                    {

                        string html = GetHtmlCode();
                        List<string> urls = GetUrls(html);
                        //var rnd = new Random();

                        //int randomUrl = rnd.Next(0, urls.Count - 1);

                        *string luckyUrl = urls[0];

                        byte[] image = GetImage(luckyUrl);
                        using (var ms = new MemoryStream(image))
                        {
                       
                            cardImage = BitmapSource.FromStream(ms);
                        }*
                        WebClient webClient = new WebClient();
                        name = name.Replace("//", "-");
                        webClient.DownloadFile(urls[0], name + ".png");
                    }
                    name = null;
                    cost = null;
                    type = null;
                    attdDef = null;
                    column = 0;
                    //bar.Value++;

                }
                else if (line != "" && int.TryParse(line[0].ToString(), out blank))
                {
                    attdDef = line;
                }
                else
                {
                    text[column] = line;
                    column++;
                }

                *if ()
                {
                    path = item;
                }*
                //}

                // At this point, `myInteger` and `path` contain the values we want
                // for the current line. We can then store those values or print them,
                // or anything else we like.
            }
            *if (line == "")
            {
                int bob = 5;
            }*
        }
        public void parse(BackgroundWorker bw)
        {
            while ((line = reader.ReadLine()) != null)
            {

                int blank;
                if (row == 0)
                {
                    name = line;
                    newName = String.Copy(name);
                    name = name.Replace("//", "-");
                    row++;
                }
                else if (line == "Scheme" || line == "Vanguard")
                {
                    type = line;
                    row++;
                    row++;
                }
                else if (row == 1)
                {
                    cost = line;
                    row++;
                }
                else if (row == 2)
                {
                    type = line;
                    row++;
                }
                else if (line == "")
                {
                    string[] newText = new string[10];
                    for (int i = 0; i != 10; i++)
                    {
                        if (text[i] != null)
                        {

                            if (i != 9)
                            {
                                if (text[i + 1] == null)
                                {
                                    block = String.Copy(text[i]);
                                    newText[i] = "";
                                }
                                else
                                    newText[i] = String.Copy(text[i]);
                            }
                        }
                    }
                    Card card = new Card(name, cost, type, attdDef, newText, block);
                    cards.Add(card);
                    row = 0;
                    for (int i = 0; i != 10; i++)
                    {
                        text[i] = "";
                    }
                    if (!File.Exists(name + ".png"))
                    {

                        string html = GetHtmlCode();
                        List<string> urls = GetUrls(html);
                        
                        WebClient webClient = new WebClient();
                        
                        if (newName == name)
                            webClient.DownloadFile(urls[0], name + ".png");
                        else if (newName != name)
                        { 
                            //int bob = 5; 
                        }
                    }
                    name = null;
                    cost = null;
                    type = null;
                    attdDef = null;
                    column = 0;

                    System.Threading.Thread.Sleep(5);
                    bw.ReportProgress(cards.Count());
                    //bar.Value++;

                }
                else if (line != "" && int.TryParse(line[0].ToString(), out blank))
                {
                    attdDef = line;
                }
                else
                {
                    text[column] = line;
                    column++;
                }

                *if ()
                {
                    path = item;
                }*
                //}

                // At this point, `myInteger` and `path` contain the values we want
                // for the current line. We can then store those values or print them,
                // or anything else we like.
            }
            *if (line == "")
            {
                int bob = 5;
            }*
        }
        */
        public void parse(BackgroundWorker bw, bool loadImages)
        {
            while ((line = reader.ReadLine()) != null)
            {

                int blank;
                if (row == 0)
                {
                    name = line;
                    if (name == "Abandoned Outpost")
                        name = line;
                    newName = String.Copy(name);
                    name = name.Replace("//", "-");
                    name = name.Replace("\\", "-");
                    name = name.Replace("\"", "");
                    name = name.Replace(":", "-");
                    name = name.Replace("?", "-");
                    row++;
                }
                else if (line == "CantCast" || line == "Scheme" || line == "Vanguard" || line == "Land" || line == "Legendary Land" || line == "Plane -- Dominaria" || line == "Conspiracy" || line == "Plane -- Ravnica" || line == "Plane -- Zendikar" || line == "Artifact Land" || line == "Flip" || line == "Snow Land" || line == "Ongoing Scheme" || line == "Legendary Snow Land" || line == "Land Creature -- Forest Dryad" || line == "Basic Land -- Forest" || line == "Basic Land -- Swamp" || line == "Basic Land -- Island" || line == "Basic Land -- Mountain" || line == "Basic Land -- Plains" || line == "Basic Snow Land -- Forest" || line == "Basic Snow Land -- Swamp" || line == "Basic Snow Land -- Island" || line == "Basic Snow Land -- Mountain" || line == "Basic Snow Land -- Plains" || line == "Land -- Desert" || line == "Land -- Gate" || line == "Land -- Swamp Mountain" || line == "Land -- Swamp Forest" || line == "Land -- Forest Island" || line == "Land -- Forest Plains" || line == "Land -- Mountain Forest" || line == "Land -- Mountain Plains" || line == "Land -- Plains Swamp" || line == "Land -- Plains Island" || line == "Land -- Island Mountain" || line == "Land -- Island Swamp" || line == "Land -- Locus" || line == "Land -- Lair" || line == "Land -- Urza's" || line == "Land -- Urza's Mine" || line == "Land -- Urza's Power-Plant" || line == "Land -- Urza's Tower" || line == "Plane -- Azgol" || line == "Plane -- Arkhos" || line == "Plane -- Alara" || line == "Plane -- Belenon" || line == "Plane -- Bolas's Meditation Realm" || line == "Plane -- Equilor" || line == "Plane -- Ergamon" || line == "Plane -- Fabacin" || line == "Plane -- Innistrad" || line == "Plane -- Iquatana" || line == "Plane -- Ir" || line == "Plane -- Shandalar" || line == "Plane -- Kephalai" || line == "Plane -- Kolbahan" || line == "Plane -- Karsus" || line == "Plane -- Kaldheim" || line == "Plane -- Kamigawa" || line == "Plane -- Kyneth" || line == "Plane -- Kinshala" || line == "Plane -- Luvion" || line == "Plane -- Lorwyn" || line == "Plane -- Mercadia" || line == "Plane -- Muraganda" || line == "Plane -- Moag" || line == "Plane -- Mirrodin" || line == "Plane -- New Phyrexia" || line == "Plane -- Ulgrotha" || line == "Plane -- Phyrexia" || line == "Plane -- Pyrulea" || line == "Plane -- Regatha" || line == "Plane -- Rath" || line == "Plane -- Rabiah" || line == "Plane -- Segovia" || line == "Plane -- Shandalar" || line == "Plane -- Serra's Realm" || line == "Plane -- Shadowmoor" || line == "Plane -- Wildfire" || line == "Plane -- Valla" || line == "Plane -- Vryn" || line == "Phenomenon" || line == "Land -- Plains" || line == "Land -- Forest" || line == "Land -- Swamp" || line == "Land -- Island" || line == "Land -- Mountain" || line == "Plane -- Mongseng") 
                {
                    type = line;
                    row++;
                    row++;
                    cost = "0";
                }
                else if (row == 1)
                {
                    int cmc = 0;

                    string[] digits = Regex.Split(line,  @"\D+");
                    foreach (string value in digits)
                    {
                        int number;
                        if (int.TryParse(value, out number))
                        {
                            cmc += number;
                        }
                    }
                    cost = line;
                    var charsToRemove = new string[] { "G", "B", "U", "R", "W" };
                    int length = line.Length;
                    foreach (var c in charsToRemove)
                    {
                        int lengthcheck = line.Length;
                        line = line.Replace(c, string.Empty);
                        if (lengthcheck != line.Length)
                            color += c;
                    }
                    if (color == null)
                        color = "L";
                    charsToRemove = new string[] { "(r/p)", "(w/p)", "(u/p)", "(b/p)", "(g/p)", "(w/u)", "(2/u)", "(r/w)", "(w/b)", "(b/g)", "(b/r)", "(g/u)", "(g/w)", "(u/r)", "(u/b)", "(r/g)", "(2/b)", "(2/r)", "(2/w)", "(2/u)", "(2/g)" };
                    int length2 = cost.Length;
                    foreach (var c in charsToRemove)
                    {
                        int lengthcheck = cost.Length;
                        switch(c)
                        {
                            case "(r/p)":
                                cost = cost.Replace(c, "(2LorR)");
                                if (lengthcheck != cost.Length)
                                    color += "R";
                                break;
                            case "(w/p)":
                                cost = cost.Replace(c, "(2LorW)");
                                if (lengthcheck != cost.Length)
                                    color += "W";
                                break;
                            case "(u/p)":
                                cost = cost.Replace(c, "(2LorU)");
                                if (lengthcheck != cost.Length)
                                    color += "U";
                                break;
                            case "(b/p)":
                                cost = cost.Replace(c, "(2LorB)");
                                if (lengthcheck != cost.Length)
                                    color += "B";
                                break;
                            case "(g/p)":
                                cost = cost.Replace(c, "(2LorG)");
                                if (lengthcheck != cost.Length)
                                    color += "G";
                                break;
                            case "(w/u)":
                                cost = cost.Replace(c, "(WorU)");
                                if (lengthcheck != cost.Length)
                                    color += "WU";
                                break;
                            case "(2/u)":
                                cost = cost.Replace(c, "(2orU)");
                                if (lengthcheck != cost.Length)
                                    color += "W";
                                break;
                            case "(r/w)":
                                cost = cost.Replace(c, "(RorW)");
                                if (lengthcheck != cost.Length)
                                    color += "WR";
                                break;
                            case "(w/b)":
                                cost = cost.Replace(c, "(WorB)");
                                if (lengthcheck != cost.Length)
                                    color += "WB";
                                break;
                            case "(b/g)":
                                cost = cost.Replace(c, "(BorG)");
                                if (lengthcheck != cost.Length)
                                    color += "BG";
                                break;
                            case "(b/r)":
                                cost = cost.Replace(c, "(BorR)");
                                if (lengthcheck != cost.Length)
                                    color += "BR";
                                break;
                            case "(g/u)":
                                cost = cost.Replace(c, "(GorU)");
                                if (lengthcheck != cost.Length)
                                    color += "GU";
                                break;
                            case "(g/w)":
                                cost = cost.Replace(c, "(GorW)");
                                if (lengthcheck != cost.Length)
                                    color += "GW";
                                break;
                            case "(u/r)":
                                cost = cost.Replace(c, "(UorR)");
                                if (lengthcheck != cost.Length)
                                    color += "UR";
                                break;
                            case "(u/b)":
                                cost = cost.Replace(c, "(UorB)");
                                if (lengthcheck != cost.Length)
                                    color += "UB";
                                break;
                            case "(r/g)":
                                cost = cost.Replace(c, "(RorG)");
                                if (lengthcheck != cost.Length)
                                    color += "RG";
                                break;
                            case "(2/b)":
                                cost = cost.Replace(c, "(2orB)");
                                if (lengthcheck != cost.Length)
                                    color += "B";
                                break;
                            case "(2/r)":
                                cost = cost.Replace(c, "(2orR)");
                                if (lengthcheck != cost.Length)
                                    color += "R";
                                break;
                            case "(2/w)":
                                cost = cost.Replace(c, "(2orW)");
                                if (lengthcheck != cost.Length)
                                    color += "W";
                                break;
                            case "(2/g)":
                                cost = cost.Replace(c, "(2orG)");
                                if (lengthcheck != cost.Length)
                                    color += "G";
                                break;
                        }
                    }
                    int addamount = 0;
                    foreach (var c in charsToRemove)
                    {
                        addamount += Regex.Matches(cost, c).Count;
                    }

                    cmc += length - line.Length;
                    cmc += addamount;
                    
                    row++;
                }
                else if (row == 2)
                {
                    type = line;
                    row++;
                }
                else if (line == "")
                {
                    string[] newText = new string[10];
                    string newBlock = "";
                    for (int i = 0; i != 10; i++)
                    {
                        if (text[i] != null && i != 9)
                        {
                            int b = i + 1;
                            if (text[b] == null || text[b] == "")
                            {
                                newBlock = String.Copy(text[i]);
                                //text[i] = "";
                            }
                            else
                            {
                                newText[i] = String.Copy(text[i]);
                                newBlock = String.Copy(text[i]);
                            }
                        }
                    }
                    Card card = new Card(name, cost, type, attdDef, newText, newBlock, color);
                    cards.Add(card);
                    row = 0;
                    for (int i = 0; i != 10; i++)
                    {
                        text[i] = "";
                    }
                    if (loadImages)
                    {
                        if (!File.Exists(name + ".png"))
                        {

                            string html = GetHtmlCode();
                            List<string> urls = GetUrls(html);

                            WebClient webClient = new WebClient();

                            if (newName == name)
                                webClient.DownloadFile(urls[0], name + ".png");
                            else if (newName != name)
                            { 
                            }
                        }
                    }
                    name = null;
                    cost = null;
                    type = null;
                    attdDef = null;
                    column = 0;
                    color = "L";
                    iterations++;
                    if (iterations >= 20)
                    {
                        System.Threading.Thread.Sleep(1);
                        iterations = 0;
                    }
                    bw.ReportProgress(cards.Count());
                    //bar.Value++;

                }
                else if (line != "" && (int.TryParse(line[0].ToString(), out blank) || line[0] == '*'))
                {
                    attdDef = line;
                }
                else
                {
                    text[column] = line;
                    column++;
                }
            }
        }
    }
}
