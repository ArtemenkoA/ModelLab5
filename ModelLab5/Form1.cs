using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ModelLab5
{
    public partial class Form1 : Form
    {
        Queue<int> queue_cli = new Queue<int>();
        Queue<int> queue12 = new Queue<int>();
        Queue<int> queue3 = new Queue<int>();

        int otkaz = 0, done = 0, number = 0, lids = 300;


        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void Client_Tick(object sender, EventArgs e)
        {
            Client.Interval = rnd.Next(80, 120);
            if (lids!=0)
            {
                queue_cli.Enqueue(rnd.Next());
                listBox1.Items.Add(lids);
            }
                lids--;
        }

        private void Operator1_Tick(object sender, EventArgs e)
        {
            Operator1.Interval = rnd.Next(150, 250);
            if (queue_cli.Count != 0)
            {
                queue_cli.Dequeue();
                queue12.Enqueue(rnd.Next());
                lids--;
                listBox1.Items.Add("Клиент принят оператором 1");
            }
            else if (queue_cli.Count > 1)
            {
                otkaz++;
                listBox1.Items.Add("Заявка пропущена");
                lids--;
            }
        }

        private void Operator2_Tick(object sender, EventArgs e)
        {
            Operator2.Interval = rnd.Next(300, 500);
            if (queue_cli.Count != 0)
            {
                queue_cli.Dequeue();
                queue12.Enqueue(rnd.Next());
                lids--;
                listBox1.Items.Add("Клиент принят оператором 2");
            }
            else if(queue_cli.Count > 1)
            {
                otkaz++;
                listBox1.Items.Add("Заявка пропущена");
                lids--;
            }
        }

        private void Operator3_Tick(object sender, EventArgs e)
        {
            Operator3.Interval = rnd.Next(200, 600);
            if (queue_cli.Count != 0)
            {
                queue_cli.Dequeue();
                queue3.Enqueue(rnd.Next());
                lids--;
                listBox1.Items.Add("Клиент принят оператором 3");
            }
            else if (queue_cli.Count > 1)
            {
                otkaz++;
                listBox1.Items.Add("Заявка пропущена");
                lids--;
            }
        }

        private void Comp1_Tick(object sender, EventArgs e)
        {
            Comp1.Interval = 150;
            if (queue12.Count != 0)
            {
                queue12.Dequeue();
                done++;
                listBox1.Items.Add("Заявка обработана компьютером 1");
            }
        }

        private void Comp2_Tick(object sender, EventArgs e)
        {
            Comp2.Interval = 300;
            if (queue3.Count != 0)
            {
                queue3.Dequeue();
                done++;
                listBox1.Items.Add("Заявка обработана компьютером 2");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client = new Timer();
            Operator1 = new Timer();
            Operator2 = new Timer();
            Operator3 = new Timer();
            Comp2 = new Timer();
            Comp1 = new Timer();
            Client.Tick += new EventHandler(Client_Tick);
            Operator1.Tick += new EventHandler(Operator1_Tick);
            Operator2.Tick += new EventHandler(Operator2_Tick);
            Operator3.Tick += new EventHandler(Operator3_Tick);
            Comp1.Tick += new EventHandler(Comp1_Tick);
            Comp2.Tick += new EventHandler(Comp2_Tick);
            Client.Start();
            Operator1.Start();
            Operator2.Start();
            Operator3.Start();
            Comp1.Start();
            Comp2.Start();
            
            Task.Run(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (lids != 0)
                {
                    int c = 0; c++;
                }
                sw.Stop();
                Client.Stop();
                Operator1.Stop();
                Operator2.Stop();
                Operator3.Stop();
                Comp1.Stop();
                Comp2.Stop();
                listBox1.Items.Add($"Количество отказов: {otkaz}");
                listBox1.Items.Add($"Количество обработанных заявок: {done}");
            });
           
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e){}
    }
}
