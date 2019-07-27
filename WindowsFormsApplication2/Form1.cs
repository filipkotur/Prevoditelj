using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Translation.V2;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Collections.Specialized;


namespace WindowsFormsApplication2
{   

    public partial class Form1 : Form
    {
        public class Translation
        {
            public int code { get; set; }
            public string lang { get; set; }
            public List<string> text { get; set; }
        }
        string tekst;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string originalni = "";
            string prevedeni = "";
            string link;
            string link1;
            string result = "";
            switch (comboBox1.SelectedItem.ToString().Trim())
            {
                case "engleski":
                    originalni = "english";
                    break;
                case "talijanski":
                    originalni = "italian";
                    break;
                case "francuski":
                    originalni = "french";
                    break;
                case "njemački":
                    originalni = "german";
                    break;
                case "španjolski":
                    originalni = "spanish";
                    break;
                case "poljski":
                    originalni = "polish";
                    break;
            }
            switch (comboBox2.SelectedItem.ToString().Trim())
            {
                case "engleski":
                    prevedeni = "english";
                    break;
                case "talijanski":
                    prevedeni = "italian";
                    break;
                case "francuski":
                    prevedeni = "french";
                    break;
                case "njemački":
                    prevedeni = "german";
                    break;
                case "španjolski":
                    prevedeni = "spanish";
                    break;
                case "poljski":
                    prevedeni = "polish";
                    break;

            }
            tekst = textBox1.Text;
            if (comboBox3.SelectedItem.ToString() == "Yandex")
            {

                using (var wb = new WebClient())
                {
                    var reqData = new NameValueCollection();
                    reqData["text"] = tekst; // text to translate
                    reqData["lang"] = "tr"; // target language
                    reqData["key"] = "trnsl.1.1.20190727T115039Z.2a06d4a1411c87fc.399cbc233d5bdbba636feba9f4975b23ff145a9d";

                    try
                    {
                        var response = wb.UploadValues("https://translate.yandex.net/api/v1.5/tr.json/translate", "POST", reqData);
                        string responseInString = Encoding.UTF8.GetString(response);

                        var rootObject = JsonConvert.DeserializeObject<Translation>(responseInString);
                        textBox2.Text = rootObject.text[0]; 
                    }
                    catch (Exception ex)
                    {
                        textBox2.Text= ex.Message;
                        throw;
                    }

                }

            }
            else { 

            tekst = Regex.Replace(textBox1.Text, @"[\d-]", "");
            tekst = tekst.Replace(" ", "+");
            if (tekst.Length == tekst.LastIndexOf("+")) { tekst.Remove(tekst.Length - 1); }
            if (tekst.IndexOf("+") == 0) { tekst = tekst.Substring(1); }
            link1 = tekst;
            int n = 0; int i = 0; int kon = 0;
            for (i = 1; i < 10; i++)
            {
                kon = link1.IndexOf("+");
                link1 = link1.Substring(kon + 1);
                n = n + kon + 1;
            }
            link = "http://context.reverso.net/translation/" + originalni + "-" + prevedeni + "/" + tekst;
            WebClient client = new WebClient();
            var link2 = client.DownloadData(link);
            link1 = Encoding.UTF8.GetString(link2);
            File.WriteAllText(@"C: \Users\filip\Desktop\proba\localfile.txt", link1);
            File.WriteAllText("localfile.txt", link1);
            string rezac = "\">";
            string rezac2 = "search\">";
            i = 0; n = 2;
            if (originalni == "english")
            {
                rezac2 = "lang"; i = 6; rezac = "="; n = 0;
            }
            if (link1.IndexOf("split wide-container") != -1)
            {
                while (link1.IndexOf("split wide-container") != -1)
                {
                    int rezanje = link1.IndexOf("split wide-container");
                    link1 = link1.Substring(rezanje);
                    rezanje = link1.IndexOf(rezac2);
                    link1 = link1.Substring(rezanje);
                    int prvi = link1.IndexOf(rezac) + i;
                    int zadnji = link1.IndexOf("</a>");
                    result = result + " " + link1.Substring(prvi + n, zadnji - prvi - n);
                }
            }
            else if (link1.IndexOf("title=\"Other\">") != -1)
            {
                rezac = "</div>\">";
                i = 7;
                rezac2 = "examples with alignment)";
                while (link1.IndexOf("title=\"Other\">") != -1)
                {
                    int rezanje = link1.IndexOf("title=\"Other\">");
                    link1 = link1.Substring(rezanje);
                    rezanje = link1.IndexOf(rezac2);
                    link1 = link1.Substring(rezanje);
                    int prvi = link1.IndexOf(rezac) + i;
                    int zadnji = link1.IndexOf("</a>");
                    result = result + " " + link1.Substring(prvi + n, zadnji - prvi - n);
                    while (result.IndexOf(" ") == 0) { result = result.Substring(1); }
                }
            }
            else //(link1.IndexOf("class=\"wide-container\">") != -1)
            {
                i = 1;
                int rezanje = link1.IndexOf("class=\"wide-container\">");
                link1 = link1.Substring(rezanje);
                rezanje = link1.IndexOf("class='translation'");
                link1 = link1.Substring(rezanje);
                rezanje = link1.IndexOf("on'");
                link1 = link1.Substring(rezanje);
                int prvi = link1.IndexOf(">") + i;
                int zadnji = link1.IndexOf("<");
                result = link1.Substring(prvi, zadnji - prvi);
            }




            textBox2.Text = result;
        }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Items.Contains("engleski")) { }
            else 
            {
                comboBox1.Items.Add("engleski");
            }
            if (comboBox1.SelectedItem.ToString() == "engleski" && comboBox2.SelectedItem.ToString() == "engleski")
            {
                comboBox2.SelectedIndex = 0; comboBox2.Items.Remove("engleski");
            }
            else if (comboBox1.SelectedItem.ToString() == "engleski")
            {
                comboBox2.Items.Remove("engleski");
            }
            else if (comboBox1.SelectedItem.ToString() != "engleski" )
            {
                if (comboBox2.Items.Contains("engleski")) { }
                else
                {
                    comboBox2.Items.Add("engleski");
                }
                comboBox2.SelectedIndex = 5;
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 5;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (comboBox2.Items.Contains("engleski")){ }
            else
            {
                comboBox2.Items.Add("engleski");
            }
            if ((comboBox2.SelectedItem.ToString() == "engleski" && comboBox1.SelectedItem.ToString() == "engleski") || (comboBox2.SelectedItem.ToString() == "engleski" && comboBox1.SelectedItem.ToString() != "engleski"))
            {
                comboBox1.SelectedIndex = 0; comboBox1.Items.Remove("engleski");
            }
            if (comboBox2.SelectedItem.ToString()== "engleski")
            {
                comboBox1.Items.Remove("engleski");
            }
            else if (comboBox2.SelectedItem.ToString() != "engleski")
            {   
                if (comboBox1.Items.Contains("engleski")) { }
                else
                {
                    comboBox1.Items.Add("engleski");
                }
                comboBox1.SelectedIndex = 5;
            }
        }
    }
}
