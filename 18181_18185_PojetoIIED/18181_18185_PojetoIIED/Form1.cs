using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _18181_18185_PojetoIIED
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExp_Click(object sender, EventArgs e)
        {
            lblExpres.Text = "";
            Button clicado = (Button)sender;
            txtVisor.Text += clicado.Text;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblExpres.Text = "";
            txtVisor.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblExpres.Text = "";
        }

        private void btnIgual_Click(object sender, EventArgs e)
        {
            bool encerraVal = false;
            double[] vet = new double[26];
            foreach(char c in txtVisor.Text.ToCharArray())
            {

            }
            lblExpres.Text += "Infixa: " + txtVisor.Text + "\n";
        }

        private string converteInfixa(string infix)
        {
            string posfix = "";
            PilhaLista<char> pil = new PilhaLista<char>();
            string ops = "()^/*+-";
            // a+b^(c + 4/2)*(6-8)
            foreach (char c in infix.ToCharArray())
            {
                if (ops.Contains(c))
                {
                    if (pil.EstaVazia())
                        pil.Empilhar(c);
                    else
                    {
                        
                        if(haPrecedencia(pil.OTopo(),c))
                        {
                            if ( c == ')')
                            {
                                while (!pil.EstaVazia() && pil.OTopo() != '(')
                                {
                                    posfix += pil.Desempilhar();
                                }
                            }
                            else
                            {
                                posfix += pil.Desempilhar();
                                pil.Empilhar(c);
                            }
                        }
                        else
                        {
                            pil.Empilhar(c);
                        }
                    }
                }
                else
                    posfix += c;
            }
            return posfix;
            
        }

        private bool haPrecedencia(char c, char v)
        {
            if (c == v)
                return true;
            if (c == '(')
                return true;
            if (c == '^' && v != '(')
                return true;
            if ((c == '/' || c == '*') && v != '(' && v != '^')
                return true;
            return false;
        
        }
    }
}
