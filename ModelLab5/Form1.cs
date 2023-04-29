using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelLab5
{
    public partial class Form1 : Form
    {
        Queue<int> queue_cli = new Queue<int>();
        Queue<int> queue12 = new Queue<int>();
        Queue<int> queue3 = new Queue<int>();

        double otkaz = 0;
        int lids = 300;

        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void Client_Tick(object sender, EventArgs e)
        {
            Client.Interval = rnd.Next(8, 12);
            if (lids>=1)
            {
                queue_cli.Enqueue(rnd.Next());
                listBox1.Items.Add(lids);
            }
                lids--;
        }

        private void Operator1_Tick(object sender, EventArgs e)
        {
            Operator1.Interval = rnd.Next(15, 25);
            if (queue_cli.Count != 0 && queue_cli.Count < 2)
            {
                queue_cli.Dequeue();
                queue12.Enqueue(rnd.Next());
            }
             if (queue_cli.Count > 1)
            {
                otkaz++;
                listBox1.Items.Add("Заявка пропущена");
                queue_cli.Clear() ;
            }
        }

        private void Operator2_Tick(object sender, EventArgs e)
        {
            Operator2.Interval = rnd.Next(30, 50);
            if (queue_cli.Count != 0 && queue_cli.Count < 2)
            {
                queue_cli.Dequeue();
                queue12.Enqueue(rnd.Next());
            }
            if(queue_cli.Count > 1)
            {
                otkaz++;
                listBox1.Items.Add("Заявка пропущена");
                queue_cli.Clear() ;
            }
        }

        private void Operator3_Tick(object sender, EventArgs e)
        {
            Operator3.Interval = rnd.Next(20, 60);
            if (queue_cli.Count != 0 && queue_cli.Count < 2)
            {
                queue_cli.Dequeue();
                queue3.Enqueue(rnd.Next());
            }
            if (queue_cli.Count > 1)
            {
                otkaz++;
                listBox1.Items.Add("Заявка пропущена");
                queue_cli.Clear();
            }
        }

        private void Comp1_Tick(object sender, EventArgs e)
        {
            Comp1.Interval = 15;
            if (queue12.Count != 0)
            {
                queue12.Dequeue();
            }
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox2.Items.Add($"Количество отказов: {otkaz}");
            double res = otkaz / 300;
            listBox3.Items.Add("Вероятность отказа p =  " + res);
        }

        private void Comp2_Tick(object sender, EventArgs e)
        {
            Comp2.Interval = 30;
            if (queue3.Count != 0)
            {
                queue3.Dequeue();
            }
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox2.Items.Add($"Количество отказов: {otkaz}");
            double res = otkaz / 300;
            listBox3.Items.Add("Вероятность отказа p =  " + res);
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
                while (lids > 0)
                {
                    int c = 0; c++;
                }
                    Client.Stop();
                    Operator1.Stop();
                    Operator2.Stop();
                    Operator3.Stop();
                    Comp1.Stop();
                    Comp2.Stop();
            });
        }

        private void Form1_Load(object sender, EventArgs e){}
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e){}
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e){}
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e){}
    }
}