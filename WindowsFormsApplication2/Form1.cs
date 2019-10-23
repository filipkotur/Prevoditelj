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
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Collections;

namespace WindowsFormsApplication2
{   
    //internal class MyResources
    //{ }

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

        
        public string tekst;
        public Form1()
        {
           
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string originalni = "";
            string prevedeni = "";
            tekst = textBox1.Text;
            if (comboBox3.SelectedItem.ToString() == "MyMemory" || comboBox3.SelectedItem.ToString() == "Reverso Context" )
            {
                tekst = String.Join(" ", tekst.Split().Take(10).ToArray());
            }

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
            tekst = Regex.Replace(textBox1.Text, @"[\d-]", "");
            BazniServer prijevod;
            if (comboBox3.SelectedItem.ToString() != "Yandex" || comboBox3.SelectedItem.ToString() == "Bing")
            {
                if ((comboBox2.SelectedItem.ToString() == comboBox1.SelectedItem.ToString()) || tekst == null || string.IsNullOrWhiteSpace(tekst))
                {
                    tekst = textBox1.Text;
                    textBox2.Text = tekst;

                }
            }
            else
            {
                if (comboBox3.SelectedItem.ToString() == "Yandex")
                    prijevod = new Yandex();
                else if (comboBox3.SelectedItem.ToString() == "Bing")
                    prijevod = new Bing();
                else if (comboBox3.SelectedItem.ToString() == "MyMemory")
                    prijevod = new MyMemory(originalni);
                else
                    prijevod = new Context_Reverso(originalni);
                textBox2.Text = prijevod.ZahtjevIOdgovor(tekst, prevedeni);
            }
            
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //comboBox1.DisplayMember = "Name";
            //comboBox2.DisplayMember = "Name";

            ////Create the reader for your resx file
            //ResXResourceReader reader = new ResXResourceReader("C:\\Users\\filip\\source\\repos\\Prevoditelj\\WindowsFormsApplication2\\strings.resx");
            ////ResourceManager rm = new ResourceManager(typeof(MyResources));
            ////var prijevod = rm.GetString("ENG");
            ////Set property to use ResXDataNodes in object ([see MSDN][2])
            //reader.UseResXDataNodes = true;
            //IDictionaryEnumerator enumerator = reader.GetEnumerator();

            //while (enumerator.MoveNext())
            //{   //Fill the combobox with all key/value pairs
            //    comboBox1.Items.Add(enumerator.Value);
            //    comboBox2.Items.Add(enumerator.Value);
            //}
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 5;
            comboBox3.SelectedIndex = 0;


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        { 
            
        }

        public void Button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(this); 
            f2.Show(); // Show Form3 and
            button2.Click -= Button2_Click;

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

            int broj_znakova = textBox1.Text.Length;//yandex 1`000`000 caracters
            string myString = broj_znakova.ToString();//bing 5000 2`000`000 per hour
            string mymemory = textBox1.Text;
            Encoding u8 = Encoding.UTF8;
            int iBC = u8.GetByteCount(mymemory);
            if (comboBox3.SelectedItem.ToString() == "Yandex")//
            {
                var textBytes = Encoding.UTF8.GetBytes(textBox1.Text);
                var textByteCount = Encoding.UTF8.GetByteCount(textBox1.Text);
                var textCharCount = Encoding.UTF8.GetCharCount(textBytes);

                if (textCharCount != textByteCount && textByteCount >= 500)
                {
                    textBox1.Text = Encoding.UTF32.GetString(Encoding.UTF32.GetBytes(textBox1.Text), 0, 500);
                }
                else if (textBox1.Text.Length >= 240)
                {
                    textBox1.Text = textBox1.Text.Substring(0, 240);
                }
                textBox1.MaxLength = 240;
                label6.Text = myString + "/500 bytes";
            }
            else if (comboBox3.SelectedItem.ToString() == "Bing")
            {
                var textBytes = Encoding.UTF8.GetBytes(textBox1.Text);
                var textByteCount = Encoding.UTF8.GetByteCount(textBox1.Text);
                var textCharCount = Encoding.UTF8.GetCharCount(textBytes);

                if (textCharCount != textByteCount && textByteCount >= 500)
                {
                    textBox1.Text = Encoding.UTF32.GetString(Encoding.UTF32.GetBytes(textBox1.Text), 0, 500);
                }
                else if (textBox1.Text.Length >= 240)
                {
                    textBox1.Text = textBox1.Text.Substring(0, 240);
                }
                textBox1.MaxLength = 240;
                label6.Text = myString + "/500 bytes";
            }
            else if (comboBox3.SelectedItem.ToString() == "MyMemory")//
            {
                var textBytes = Encoding.UTF8.GetBytes(textBox1.Text);
                var textByteCount = Encoding.UTF8.GetByteCount(textBox1.Text);
                var textCharCount = Encoding.UTF8.GetCharCount(textBytes);

                if (textCharCount != textByteCount && textByteCount >= 500)
                {
                    textBox1.Text = Encoding.UTF32.GetString(Encoding.UTF32.GetBytes(textBox1.Text), 0, 500);
                }
                else if (textBox1.Text.Length >= 240)
                {
                    textBox1.Text = textBox1.Text.Substring(0, 240);
                }
                label6.Text = iBC + "/500 bytes";
            }

            else if (comboBox3.SelectedItem.ToString() == "Reverso Context")//
            {
                var textBytes = Encoding.UTF8.GetBytes(textBox1.Text);
                var textByteCount = Encoding.UTF8.GetByteCount(textBox1.Text);
                var textCharCount = Encoding.UTF8.GetCharCount(textBytes);

                if (textCharCount != textByteCount && textByteCount >= 500)
                {
                    textBox1.Text = Encoding.UTF32.GetString(Encoding.UTF32.GetBytes(textBox1.Text), 0, 500);
                }
                else if (textBox1.Text.Length >= 240)
                {
                    textBox1.Text = textBox1.Text.Substring(0, 240);
                }
                textBox1.MaxLength = 240;
                label6.Text = myString + "/500 bytes";
            }
        }
        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() == "Yandex" || comboBox3.SelectedItem.ToString() == "Bing")
            {
                comboBox1.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = true;
            }
        }

        
    }

}
