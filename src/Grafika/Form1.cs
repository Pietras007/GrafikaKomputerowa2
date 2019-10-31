using Grafika.Constans;
using Grafika.Extentions;
using Grafika.Helpers;
using Grafika.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika
{
    public partial class Form1 : Form
    {
        public Picture picture = new Picture();
        public int sizeX;
        public int sizeY;
        public TrybPracy trybPracy;
        List<object> checkedTrybPracy = new List<object>();
        public Wypelnienie wypelnienie;
        List<object> checkedWypelnienie = new List<object>();
        public RodzajMalowania rodzajMalowania;
        List<object> checkedRodzajMalowania = new List<object>();
        Timer MyTimer = new Timer();
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (SolidBrush whiteBrush = new SolidBrush(Color.White), blackBrush = new SolidBrush(Color.Black), orangeBrush = new SolidBrush(Color.Orange))
            {
                using (Pen blackPen = new Pen(Color.Black))
                {
                    g.FillRectangle(whiteBrush, 0, 0, CONST.bitmapX, CONST.bitmapY);
                    foreach (var triangle in picture.Triangles)
                    {
                        g.PaintTriangle(blackPen, triangle);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = CONST.trianglesX.ToString();
            textBox2.Text = CONST.trianglesY.ToString();
            sizeX = CONST.trianglesX;
            sizeY = CONST.trianglesY;
            trybPracy = TrybPracy.SwiatloDaleko;
            wypelnienie = Wypelnienie.Tekstura;
            rodzajMalowania = RodzajMalowania.Brak;
            picture.InitializePicture(sizeX, sizeY);
            MyTimer.Interval = (15);
            MyTimer.Tick += new EventHandler(TimerFunction);
            MyTimer.Start();
        }
        private void TimerFunction(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int x, y;
            if(int.TryParse(textBox1.Text, out x) && int.TryParse(textBox2.Text, out y))
            {
                sizeX = x;
                sizeY = y;
                picture.InitializePicture(sizeX, sizeY);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UnCheckAllTrybPracy();
            CheckTrybPracy(sender);
            trybPracy = TrybPracy.Przesuwanie;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UnCheckAllTrybPracy();
            CheckTrybPracy(sender);
            trybPracy = TrybPracy.SwiatloDaleko;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UnCheckAllTrybPracy();
            CheckTrybPracy(sender);
            trybPracy = TrybPracy.SwiatloWedrujace;
        }

        private void CheckTrybPracy(object sender)
        {
            if (!checkedTrybPracy.Contains(sender))
            {
                checkedTrybPracy.Add(sender);
            }
            Button butt = (Button)sender;
            butt.BackColor = Color.Cyan;
        }

        private void UnCheckAllTrybPracy()
        {
            foreach (var sender in checkedTrybPracy)
            {
                Button butt = (Button)sender;
                butt.BackColor = SystemColors.Control;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UnCheckAllWypelnienie();
            CheckWypelnienie(sender);
            wypelnienie = Wypelnienie.Tekstura;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UnCheckAllWypelnienie();
            CheckWypelnienie(sender);
            wypelnienie = Wypelnienie.WybierzKolor;
        }

        private void CheckWypelnienie(object sender)
        {
            if (!checkedWypelnienie.Contains(sender))
            {
                checkedWypelnienie.Add(sender);
            }
            Button butt = (Button)sender;
            butt.BackColor = Color.Cyan;
        }

        private void UnCheckAllWypelnienie()
        {
            foreach (var sender in checkedWypelnienie)
            {
                Button butt = (Button)sender;
                butt.BackColor = SystemColors.Control;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            UnCheckAllRodzajMalowania();
            CheckRodzajMalowania(sender);
            rodzajMalowania = RodzajMalowania.Brak;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UnCheckAllRodzajMalowania();
            CheckRodzajMalowania(sender);
            rodzajMalowania = RodzajMalowania.Dokladne;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UnCheckAllRodzajMalowania();
            CheckRodzajMalowania(sender);
            rodzajMalowania = RodzajMalowania.Hybrydowe;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UnCheckAllRodzajMalowania();
            CheckRodzajMalowania(sender);
            rodzajMalowania = RodzajMalowania.Interpolowane;
        }

        private void CheckRodzajMalowania(object sender)
        {
            if (!checkedRodzajMalowania.Contains(sender))
            {
                checkedRodzajMalowania.Add(sender);
            }
            Button butt = (Button)sender;
            butt.BackColor = Color.Cyan;
        }

        private void UnCheckAllRodzajMalowania()
        {
            foreach (var sender in checkedRodzajMalowania)
            {
                Button butt = (Button)sender;
                butt.BackColor = SystemColors.Control;
            }
        }
    }
}
