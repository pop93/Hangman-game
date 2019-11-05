using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
using System.Xml;
using System.Diagnostics;

namespace IgraVjesala
{
    public partial class frmIgraVjesala : Form
    {
        private Bitmap[] hangImages = { IgraVjesala.Properties.Resources.hang1, IgraVjesala.Properties.Resources.hang2, IgraVjesala.Properties.Resources.hang3, IgraVjesala.Properties.Resources.hang4,
        IgraVjesala.Properties.Resources.hang5,IgraVjesala.Properties.Resources.hang6,IgraVjesala.Properties.Resources.hang7};

        private int wrongGuesses = 0;

        private string current = "";
        private string copyCurrent = "";

        private string[] words;

        public object Utilities { get; private set; }

        public frmIgraVjesala()
        {
            InitializeComponent();
        }

        private void loadWords()  //tu imam sve riječi ali ja hoću samo jednu riječ 
        {
            char[] delimiterChars = { ',' };
            string[] readText = File.ReadAllLines("drzave.csv");
            words = new string[readText.Length];
            int index = 0;

            foreach (string s in readText)
            {
                string[] line = s.Split(delimiterChars);
                words[index++] = line[1];


            }




        }


        private void setupWordChoise()  
        {
            wrongGuesses = 0;
            hangImage.Image = hangImages[wrongGuesses];
            int guessIndex = (new Random()).Next(words.Length); 
            current = words[guessIndex];


            copyCurrent = "";
            for (int index = 0; index < current.Length; index++)
            {
                copyCurrent += "_";

            }
            displayCopy();



        }

        private void displayCopy()
        {
            lblShowWord.Text = "";
            for (int index = 0; index < copyCurrent.Length; index++)
            {
                lblShowWord.Text += copyCurrent.Substring(index, 1);
                lblShowWord.Text += " ";
            }
        }


        private void updateCopy(char guess)
        {

        }


        private void testiranje()
        {
            int score = 0;
            string sName;
            switch (wrongGuesses)
            {

                case 0:
                    score = 1000;
                    lblResult.Text = "Pobjedili ste! Osvojeno:1000 bodova";
                    break;

                case 1:
                    score = 800;
                    lblResult.Text = "Pobjedili ste! Osvojeno:800 bodova";
                    break;
                case 2:
                    score = 600;
                    lblResult.Text = "Pobjedili ste! Osvojeno:600 bodova";
                    break;
                case 3:
                    score = 500;
                    lblResult.Text = "Pobjedili ste! Osvojeno:500 bodova";
                    break;
                case 4:
                    score = 400;
                    lblResult.Text = "Pobjedili ste! Osvojeno:400 boda";
                    break;
                case 5:
                    score = 200;
                    lblResult.Text = "Pobjedili ste! Osvojeno:200 boda";
                    break;
                case 6:
                    score = 100;
                    lblResult.Text = "Pobjedili ste! Osvojeno:100 boda";
                    break;

            }





            sName = Microsoft.VisualBasic.Interaction.InputBox("Upišite svoje ime:", "Ime?", "");


            while (sName == "")
            {
                MessageBox.Show("Molimo upišite ime.");
                sName = Microsoft.VisualBasic.Interaction.InputBox("Upišite svoje ime:", "ime?", "");
            }


            New_Score(score,sName);



        }
        private void New_Score(int score, string Sname)
        {
            string filename = "scores.txt";
            List<string> scoreList;
            if (File.Exists(filename))
                scoreList = File.ReadAllLines(filename).ToList();
            else
                scoreList = new List<string>();
            scoreList.Add(Sname +":"+ " " + score.ToString());
            var sortedScoreList = scoreList.OrderByDescending(ss => int.Parse(ss.Substring(ss.LastIndexOf(" ") + 1)));
            File.WriteAllLines(filename, sortedScoreList.ToArray());


        }




        private void cmdA_Click(object sender, EventArgs e)
        {
            Button choice = sender as Button;
            choice.Enabled = false;
            if (current.Contains(choice.Text))
            {
                char[] temp = copyCurrent.ToCharArray();
                char[] find = current.ToCharArray();
                char guessChar = choice.Text.ElementAt(0);

                for (int index = 0; index < find.Length; index++)
                {
                    if (find[index] == guessChar)
                    {
                        temp[index] = guessChar;
                    }
                }
                copyCurrent = new string(temp);
                displayCopy();
            }
            else
            {


                wrongGuesses++;
            }
            if (wrongGuesses < 7)
            {

                hangImage.Image = hangImages[wrongGuesses];
            }
            else
            {
                lblResult.Text = "Izgubili ste!";
            }
            string name;
            if (copyCurrent.Equals(current))

            {


                testiranje();

                Console.WriteLine("upišite svoje ime");
                name = Console.ReadLine();




            }
        }

        private void frmIgraVjesala_Load(object sender, EventArgs e)
        {
            loadWords();
            setupWordChoise();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            setupWordChoise();
            lblResult.Text = " ";
            Controls.Clear();
            InitializeComponent();
            setupWordChoise();


        }

        private async void button28_Click(object sender, EventArgs e)
        {
            

            try
            {
                using (StreamReader sr = new StreamReader("scores.txt"))
                {
                    String line = await sr.ReadToEndAsync();
                    richTextBox1.Text = line;
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Text = "Could not read the file";
            }
            
        }
    }
}
    