using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vaja2
{
    public partial class Form1 : Form
    {
        public enum Igralec
        {
            /// znaki za igralce
            X, O
        }
        Igralec trenutniIgralec;

        List<Button> gumbi; ///ustvarimo array gumbov
        Random rand = new Random();
        int Igralec_Zmage = 0;
        int AI_Zmage = 0;
        public Form1()
        {
            InitializeComponent();
            resetGame(); // znova nalozi igro da lahko takoj igras
        }

        private void playerClick(object sender, EventArgs e)
        {
            var gumb = (Button)sender; /// uotovimo kaeri gumb je bil
            trenutniIgralec = Igralec.X; /// nastavi igralca na X
            gumb.Text = trenutniIgralec.ToString(); // spremenim text gumba na X
            gumb.Enabled = false; /// da ga nemores ponovno stisnet
            gumb.BackColor = System.Drawing.Color.Cyan; /// spremenim barvo gumba
            gumbi.Remove(gumb); /// odstanim iz array da ga tudi AI nemore stisnet
            Check(); /// preverimo ce je igralec zmagal
            AImoves.Start(); /// da zacne AI MORES ZBRISAT
        }

        private void restartGame(object sender, EventArgs e)
        {
            resetGame();
        }
        private void loadbuttons()
        {
            gumbi = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
        }
        private void resetGame()
        {
            // pogledamo za vsaki gumb ki je oznacen z play
            foreach (Control X in this.Controls) {
                if (X is Button && X.Tag == "Play") {
                    ((Button)X).Enabled = true; /// spremenim nazaj da jih lahko stisnem
                    ((Button)X).Text = "?"; /// nastavim text nazaj na vprasaj
                    ((Button)X).BackColor = default(Color); /// Barvo nazaj na default
                }
            }
            loadbuttons(); /// da jih dodam nazaj v array
        }
        private void Check()
        {
            /// preverjamo ce je kdo zmagal
            /// najprej za igralca
            if (button1.Text == "X" && button2.Text == "X" && button3.Text == "X"
                || button4.Text == "X" && button5.Text == "X" && button6.Text == "X"
                || button7.Text == "X" && button8.Text == "X" && button9.Text == "X"
                || button1.Text == "X" && button4.Text == "X" && button7.Text == "X"
                || button2.Text == "X" && button5.Text == "X" && button8.Text == "X"
                || button3.Text == "X" && button6.Text == "X" && button9.Text == "X"
                || button1.Text == "X" && button5.Text == "X" && button9.Text == "X"
                || button3.Text == "X" && button5.Text == "X" && button7.Text == "X")
            {
                // igralec je zmagal
                AImoves.Stop(); /// MORES POL ZBRISAT
                MessageBox.Show("Zmaga igralca");
                Igralec_Zmage++;
                label1.Text = "Igralec - " + Igralec_Zmage;
                resetGame();
            }
            else if (button1.Text == "O" && button2.Text == "O" && button3.Text == "O"
                || button4.Text == "O" && button5.Text == "O" && button6.Text == "O"
                || button7.Text == "O" && button8.Text == "O" && button9.Text == "O"
                || button1.Text == "O" && button4.Text == "O" && button7.Text == "O"
                || button2.Text == "O" && button5.Text == "O" && button8.Text == "O"
                || button3.Text == "O" && button6.Text == "O" && button9.Text == "O"
                || button1.Text == "O" && button5.Text == "O" && button9.Text == "O"
                || button3.Text == "O" && button5.Text == "O" && button7.Text == "O")
            {
                /// zmaga AI
                AImoves.Stop(); /// MORES POL ZBRISAT
                MessageBox.Show("Zmaga AI");
                AI_Zmage++;
                label2.Text = "AI - " + AI_Zmage;
                resetGame();
            }
        }

        private void AImove(object sender, EventArgs e)
        {
            if (gumbi.Count > 0) {
                int index = rand.Next(gumbi.Count);
                gumbi[index].Enabled = false;
                trenutniIgralec = Igralec.O;
                gumbi[index].Text = trenutniIgralec.ToString();
                gumbi[index].BackColor = System.Drawing.Color.DarkBlue;
                gumbi.RemoveAt(index);
                Check();
                AImoves.Stop();
            }
        }
    }
}
