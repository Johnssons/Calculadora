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

            lblExpres.Text += "Infixa: " + txtVisor.Text + "\n";
        }

        private string converteInfixa (string infix)
        {
            PilhaLista<char> pil = new PilhaLista<char>();
            string ops = "()+_/^";
            foreach (char c in infix.ToCharArray())
            {
                if(ops.Contains(c))
                {
                    pil.Empilhar(c);
                }
            }
            
        }
    }
}
