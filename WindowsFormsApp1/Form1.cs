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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int vek;
        public int Vek()
        {
            return vek;
        }       
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            StreamReader ctenar = new StreamReader("rodna_cisla.txt");
            double prumer = 0;
            int pocet = 0;
            bool prosinec = false;
            while (!ctenar.EndOfStream)
            {
                string[] pole = ctenar.ReadLine().Split(';');
                listBox1.Items.Add(pole[0] + ';' + pole[1] + ';' + pole[2]);
                string jmeno = pole[0];
                double znamka = double.Parse(pole[1]);
                string rodnecislo = pole[2];
                prumer += znamka;
                pocet++;
                //MessageBox.Show(rodnecislo[2].ToString() + rodnecislo[3].ToString());
                if ((((int)rodnecislo[2] == 1 && (int)rodnecislo[3] == 2) || ((int)rodnecislo[2] == 6 && (int)rodnecislo[3] == 2)) && prosinec == false)
                {
                    MessageBox.Show("První člověk, jenž je narozen v prosinci: " + jmeno);
                    prosinec = true;
                }
            }
            ctenar.Close();
            if(pocet != 0)
            {
                prumer /= pocet;
            }
            StreamWriter zapisovak = new StreamWriter("rodna_cisla.txt", true);
            zapisovak.WriteLine("Průměr všech známek: " + prumer);
            zapisovak.Close();
        }
    }
}
