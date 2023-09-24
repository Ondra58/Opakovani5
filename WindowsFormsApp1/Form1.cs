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
using System.Net.Sockets;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void Vek(string[] pole, ref bool prosinec, out int vek, ref string mesic2)
        {
            string jmeno = pole[0];
            string rodnecislo = pole[2];
            int rok = 0;           
            if ((((int)rodnecislo[2] - 48 == 1 && (int)rodnecislo[3] - 48 == 2) || ((int)rodnecislo[2] - 48 == 6 && (int)rodnecislo[3] - 48 == 2)) && prosinec == false)
            {
                MessageBox.Show("První člověk, jenž je narozen v prosinci: " + jmeno);
                prosinec = true;
            }
            if (Convert.ToInt32(rodnecislo.Substring(0, 2)) > 23)
            {
                rok = Convert.ToInt32(rodnecislo.Substring(0, 2)) + 1900;
            }
            else
            {
                rok = Convert.ToInt32(rodnecislo.Substring(0, 2)) + 2000;
            }           
            int mesic = Convert.ToInt32(rodnecislo.Substring(2, 2));
            int den = Convert.ToInt32(rodnecislo.Substring(4, 2));
            DateTime datumanrozeni = new DateTime(rok, mesic, den);
            TimeSpan cc = new TimeSpan();
            cc = (DateTime.Now - datumanrozeni);
            vek = (int)cc.TotalDays / 365;
            switch(mesic)
            {
                case 1: { mesic2 = "leden"; break; }
                case 2: { mesic2 = "únor"; break; }
                case 3: { mesic2 = "březen"; break; }
                case 4: { mesic2 = "duben"; break; }
                case 5: { mesic2 = "květen"; break; }
                case 6: { mesic2 = "červen"; break; }
                case 7: { mesic2 = "červenec"; break; }
                case 8: { mesic2 = "srpen"; break; }
                case 9: { mesic2 = "září"; break; }
                case 10: { mesic2 = "říjen"; break; }
                case 11: { mesic2 = "listopad"; break; }
                case 12: { mesic2 = "prosinec"; break; }
            }
        }  
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            StreamReader ctenar = new StreamReader("rodna_cisla.txt");
            double prumer = 0;
            int pocet = 0;
            bool prosinec = false;
            int vek;
            int[] poleveku = new int[3];
            string mesic2 = "";
            while (!ctenar.EndOfStream)
            {
                string[] pole = ctenar.ReadLine().Split(';');
                listBox1.Items.Add(pole[0] + ';' + pole[1] + ';' + pole[2]);
                string jmeno = pole[0];
                double znamka = double.Parse(pole[1]);
                string rodnecislo = pole[2];
                Vek(pole, ref prosinec, out vek, ref mesic2);
                poleveku[pocet] = vek;                      
                prumer += znamka;
                pocet++;               
            }
            ctenar.Close();
            if(pocet != 0)
            {
                prumer /= pocet;
            }
            StreamWriter zapisovak = new StreamWriter("rodna_cisla.txt", true);
            zapisovak.WriteLine("\nPrůměr všech známek: " + prumer);
            int i = 0;
            foreach (string zaznam in listBox1.Items)
            {
                string novyzaznam = zaznam + ";" + poleveku[i];
                listBox2.Items.Add(novyzaznam);
                i++;
            }
            zapisovak.Close();

            SaveFileDialog ulozeni = new SaveFileDialog();
            ulozeni.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            string nazev = "";
            if (ulozeni.ShowDialog() == DialogResult.OK)
            {
                nazev = ulozeni.FileName;
            }
            StreamWriter zapisovak2 = new StreamWriter(nazev, true);
            foreach (string zaznam in listBox1.Items)
            {
                string[] pole = zaznam.Split(';');
                string jmeno = pole[0];
                double znamka = double.Parse(pole[1]);
                Vek(pole, ref prosinec, out vek, ref mesic2);
                if (znamka < 3)
                {
                    zapisovak2.WriteLine(jmeno+ ";" + znamka + ";" + mesic2);
                }
            }
        }
    }
}
