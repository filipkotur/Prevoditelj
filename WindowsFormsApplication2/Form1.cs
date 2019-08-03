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
using RestSharp;
using RestSharp.Serialization.Json;
using Google.Apis.Util;

namespace WindowsFormsApplication2
{   

    public partial class Form1 : Form
    {
        
        public class Translation
        {
            public int code { get; set; }
            public string lang { get; set; }
            public List<string> text { get; set; }
            public string TranslatedText { get; set; }
            public string DetectedSourceLanguage { get; set; }
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
            tekst = textBox1.Text;
            switch (comboBox1.SelectedItem.ToString().Trim())
            {
                case "engleski":
                    if (comboBox3.SelectedItem.ToString() == "Reverso Context")
                        originalni = "english";
                    else
                        originalni = "en";
                    break;
                case "talijanski":
                    if (comboBox3.SelectedItem.ToString() == "Reverso Context")
                        originalni = "italian";
                    else
                        originalni = "it";
                    break;
                case "francuski":
                    if (comboBox3.SelectedItem.ToString() == "Reverso Context")
                        originalni = "french";
                    else
                        originalni = "fr";
                    break;
                case "njemački":
                    if (comboBox3.SelectedItem.ToString() == "Reverso Context")
                        originalni = "german";
                    else
                        originalni = "de";
                    break;
                case "španjolski":
                    if (comboBox3.SelectedItem.ToString() == "Reverso Context")
                        originalni = "spanish";
                    else
                        originalni = "es";
                    break;
                case "poljski":
                    if (comboBox3.SelectedItem.ToString() == "Reverso Context")
                        originalni = "polish";
                    else
                        originalni = "pl";
                    break;
            }
            switch (comboBox2.SelectedItem.ToString().Trim())
            {
                case "engleski":
                    if (comboBox3.SelectedItem.ToString() == "Reverso Context")
                        prevedeni = "english";
                    else
                        prevedeni = "en";
                    break;
                case "talijanski":
                    if (comboBox3.SelectedItem.ToString() == "Reverso Context")
                        prevedeni = "italian";
                    else
                        prevedeni = "it";
                    break;
                case "francuski":
                    if (comboBox3.SelectedItem.ToString() == "Reverso Context")
                        prevedeni = "french";
                    else
                        prevedeni = "fr";
                    break;
                case "njemački":
                    if (comboBox3.SelectedItem.ToString() == "Reverso Context")
                        prevedeni = "german";
                    else
                        prevedeni = "de";
                    break;
                case "španjolski":
                    if (comboBox3.SelectedItem.ToString() == "Reverso Context")
                        prevedeni = "spanish";
                    else
                        prevedeni = "es";
                    break;
                case "poljski":
                    if (comboBox3.SelectedItem.ToString() == "Reverso Context")
                        prevedeni = "polish";
                    else
                        prevedeni = "pl";
                    break;

            }
            if (comboBox2.SelectedItem.ToString() == comboBox1.SelectedItem.ToString())
            {
                tekst = textBox1.Text;
                textBox2.Text = tekst;
            }
            else if(comboBox3.SelectedItem.ToString() == "Yandex")
            {
               
                using (var wb = new WebClient())
                {
                    var reqData = new NameValueCollection();
                    reqData["text"] = tekst; // text to translate
                    reqData["lang"] = prevedeni; // target language
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
            else if (comboBox3.SelectedItem.ToString() == "Bing")
            {
                string strTranslatedText = null;
                
                prevedeni = prevedeni.Remove(prevedeni.Length - (prevedeni.Length - 2));
                try
                {
                    TranslatorService.LanguageServiceClient client = new TranslatorService.LanguageServiceClient();
                    client = new TranslatorService.LanguageServiceClient();
                    strTranslatedText = client.Translate("6CE9C85A41571C050C379F60DA173D286384E0F2", textBox1.Text, "", prevedeni);
                    textBox2.Text = strTranslatedText;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                
            }

            else if (comboBox3.SelectedItem.ToString() == "MyMemory")
            {
                
                tekst = Regex.Replace(textBox1.Text, @"[\d-]", "");
                tekst = tekst.Replace(" ", "%20");
                if (tekst.Length == tekst.LastIndexOf("%20")) { tekst.Remove(tekst.Length - 1); }
                if (tekst.IndexOf("%20") == 0) { tekst = tekst.Substring(1); }
                link1 = tekst;
                int n = 0; int i = 0; int kon = 0;
                for (i = 1; i < 10; i++)
                {
                    kon = link1.IndexOf("%20");
                    link1 = link1.Substring(kon + 1);
                    n = n + kon + 1;
                }
                link = "https://api.mymemory.translated.net/get?q=" + tekst + "&langpair=" + originalni + "|" + prevedeni;
                WebClient client = new WebClient();
                var link2 = client.DownloadData(link);
                link1 = Encoding.UTF8.GetString(link2);
                File.WriteAllText(@"C: \Users\filip\Desktop\proba\localfile.txt", link1);
                File.WriteAllText("localfile.txt", link1);
                int rezanje = link1.IndexOf("ext");
                link1 = link1.Substring(rezanje);
                 rezanje = link1.IndexOf(":");
                link1 = link1.Substring(rezanje);
                link1 = link1.Substring(2);
                int zadnji = link1.IndexOf("match") - 4;
                result = link1.Substring(1, zadnji);
                result = String.Join(" ", result.Split(' ').Reverse());
                textBox2.Text = result;
            }

            else{ 

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
            //if (comboBox1.Items.Contains("engleski")) { }
            //else 
            //{
            //    comboBox1.Items.Add("engleski");
            //}
            //if (comboBox1.SelectedItem.ToString() == "engleski" && comboBox2.SelectedItem.ToString() == "engleski")
            //{
            //    comboBox2.SelectedIndex = 0; comboBox2.Items.Remove("engleski");
            //}
            //else if (comboBox1.SelectedItem.ToString() == "engleski")
            //{
            //    comboBox2.Items.Remove("engleski");
            //}
            //else if (comboBox1.SelectedItem.ToString() != "engleski" )
            //{
            //    if (comboBox2.Items.Contains("engleski")) { }
            //    else
            //    {
            //        comboBox2.Items.Add("engleski");
            //    }
            //    comboBox2.SelectedIndex = 5;
            //}


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 5;
            comboBox3.SelectedIndex = 0;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        { 
            //if (comboBox2.Items.Contains("engleski")){ }
            //else
            //{
            //    comboBox2.Items.Add("engleski");
            //}
            //if ((comboBox2.SelectedItem.ToString() == "engleski" && comboBox1.SelectedItem.ToString() == "engleski") || (comboBox2.SelectedItem.ToString() == "engleski" && comboBox1.SelectedItem.ToString() != "engleski"))
            //{
            //    comboBox1.SelectedIndex = 0; comboBox1.Items.Remove("engleski");
            //}
            //if (comboBox2.SelectedItem.ToString()== "engleski")
            //{
            //    comboBox1.Items.Remove("engleski");
            //}
            //else if (comboBox2.SelectedItem.ToString() != "engleski")
            //{   
            //    if (comboBox1.Items.Contains("engleski")) { }
            //    else
            //    {
            //        comboBox1.Items.Add("engleski");
            //    }
            //    comboBox1.SelectedIndex = 5;
            //}
        }

        public void Button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(this); // Instantiate a Form3 object.
            f2.Show(); // Show Form3 and
            button2.Click -= Button2_Click;

        }
        public void aktivacija(object sender, EventArgs e) {
            button2.Click += Button2_Click;
        }
    }

}
