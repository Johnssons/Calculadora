using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _18181_18185_PojetoIIED
{
    public partial class Form1 : Form
    {
        private string[] ops = new string[] { "+", "-", "/", "*", "^", "(", ")" };
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
            txtResultado.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblExpres.Text = "";
        }

        private void btnIgual_Click(object sender, EventArgs e)
        {
            FilaLista<string> infixa = new FilaLista<string>();
            FilaLista<string> posfixa = new FilaLista<string>();
            PilhaLista<string> ops = new PilhaLista<string>();
            string expressao = txtVisor.Text;

            for (int i = 0; i < expressao.Length; i++)
            {
                string elemento = "";

                if (!IsOp(expressao[i].ToString()))
                {
                    elemento = "";
                    int inicial = i;
                    while (inicial + elemento.Length < expressao.Length && (!IsOp(expressao[inicial + elemento.Length].ToString()) || expressao[inicial + elemento.Length] == '.'))
                        elemento += expressao[inicial + elemento.Length];
                    i = inicial + elemento.Length - 1;
                    posfixa.Enfileirar(elemento);
                }
                else
                {
                    elemento = expressao[i] + "";
                    while (!ops.EstaVazia() && TemPrecedencia(ops.OTopo()[0], elemento[0]))
                    {
                        char op = ops.OTopo()[0];
                        if (op == '(')
                            break;
                        else
                        {
                            posfixa.Enfileirar(op + "");
                            ops.Desempilhar();
                        }
                    }

                    if (elemento != ")")
                        ops.Empilhar(elemento);
                    else
                        ops.Desempilhar();
                }
                if (elemento != "(" && elemento != ")")
                    infixa.Enfileirar(elemento);
            }
            while (!ops.EstaVazia())
            {
                string op = ops.Desempilhar();
                if (op != "(" && op != ")")
                {
                    posfixa.Enfileirar(op);
                }
            }
            escreverSeq(infixa, posfixa);
            txtResultado.Text = CalcularResultado(posfixa).ToString();
        }
        private void escreverSeq(FilaLista<string> inf, FilaLista<string> pos)
        {
           
                char letra = 'A';
                string[] vet = pos.ToArray();
            lblExpres.Text += "Posfixa: ";
            for (int i = 0; i < vet.Length; i++)
                {
                    if (IsOp(vet[i]))
                    {
                    lblExpres.Text += vet[i];
                    }
                    else
                    lblExpres.Text += letra++;
                }
            lblExpres.Text += "\n" + "Infixa: "; ;
            letra = 'A';
            vet = inf.ToArray();
            for (int i = 0; i < vet.Length; i++)
            {
                if (IsOp(vet[i]))
                {
                    lblExpres.Text += vet[i];
                }
                else
                    lblExpres.Text += letra++;
            }

        }
        private double CalcularResultado(FilaLista<string> expre)
        {
            PilhaLista<double> valores = new PilhaLista<double>();
            double v1 = 0, v2 = 0, result = 0;
            string[] vet = expre.ToArray();

            for (int c = 0; c < vet.Length; c++)
            {
                if (!IsOp(vet[c]))
                    valores.Empilhar(double.Parse(vet[c].Replace('.', ',')));
                else
                {
                    v1 = valores.Desempilhar();
                    v2 = valores.Desempilhar();
                    switch (vet[c])
                    {
                        case "+": result = v2 + v1; break;
                        case "-": result = v2 - v1; break;
                        case "*": result = v2 * v1; break;
                        case "/":
                            if (v1 == 0)
                                throw new DivideByZeroException("Divisão por 0");
                            result = v2 / v1; break;
                        case "^": result = Math.Pow(v2, v1); break;
                    }
                    valores.Empilhar(result);
                }
            }

            return valores.Desempilhar();
        }

        private bool IsOp (string c)
        {
            return ops.Contains(c);
        }

        private bool TemPrecedencia(char topo, char operacao)
        {
            switch (topo)
            {
                case '+':
                case '-':
                    if (operacao == '+' || operacao == '-' || operacao == ')')
                        return true; break;

                case '*':
                case '/':
                case '^':
                    if (operacao == '+' || operacao == '-' || operacao == '*' || operacao == '/' || operacao == ')')
                        return true; break;

                case '(':
                    if (operacao == ')')
                        return true;
                    break;

            }
            return false;
        }
    }
        
}
