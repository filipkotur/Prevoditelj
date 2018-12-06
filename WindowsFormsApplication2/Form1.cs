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

namespace WindowsFormsApplication2
{   

    public partial class Form1 : Form
    {
        string tekst;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string originalni="" ;
            string prevedeni="" ;
            string link;
            string link1;
            string result ="";
            switch (comboBox1.SelectedItem.ToString().Trim())
            {
                case "engleski":
                    originalni = "english";
                    break;
                case "talijanski":
                    originalni= "italian";
                    break;
                case "francuski":
                    originalni = "french";
                    break;
                case "njemački":
                    originalni = "german";
                    break;
                case  "španjolski":
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
                    originalni = "polish";
                    break;
            }
            tekst = textBox1.Text;
            tekst= Regex.Replace(textBox1.Text, @"[\d-]", "");
            tekst = tekst.Replace(" ", "+");if (tekst.IndexOf("+") == 0) { tekst = tekst.Substring(1); }
            link1 = tekst;
            int n = 0; int i = 0;int kon=0;
            for ( i = 1; i < 10; i++)
            {
                 kon = link1.IndexOf("+");
                link1 = link1.Substring(kon+1);
                n = n + kon + 1;
            }
            tekst =tekst.Substring(0,n+1);
            link = "http://context.reverso.net/translation/" + originalni + "-" + prevedeni + "/" + tekst ;
            WebClient client = new WebClient();
            link1 = client.DownloadString(link);
            File.WriteAllText(@"C: \Users\Korisnik\Desktop\proba\localfile.txt", link1); 
            File.WriteAllText("localfile.txt", link1);
            string rezac = "\">";
            string rezac2 = "search\">"; 
             i = 0; n = 2;
            if (originalni == "english")
            {
                rezac2 = "lang"; i = 6; rezac = "="; n = 0;
            }
            if (link1.IndexOf("split wide-container") == -1)
            {
                
                result = "";
            }
            else
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
            textBox2.Text = result;
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
