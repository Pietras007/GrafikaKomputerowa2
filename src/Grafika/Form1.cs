using Grafika.CONST;
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
        Bitmap sampleImage;
        Color backColor;
        Color lightColor;
        public int sizeX;
        public int sizeY;
        public TrybPracy trybPracy;
        List<object> checkedTrybPracy = new List<object>();
        public Wypelnienie wypelnienie;
        List<object> checkedWypelnienie = new List<object>();
        public RodzajMalowania rodzajMalowania;
        List<object> checkedRodzajMalowania = new List<object>();
        Timer MyTimer = new Timer();
        bool isMoving;
        Vertice clickedPoint;
        double ks;
        double kd;
        int m;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (rodzajMalowania == RodzajMalowania.Brak)
            {
                g.PaintBrak(picture, wypelnienie, sampleImage, backColor, trybPracy, ks, kd, m);
            }
            else
            {
                g.Paint(picture, wypelnienie, sampleImage, backColor, trybPracy, ks, kd, m, rodzajMalowania, lightColor);
            }
            //pictureBox1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            isMoving = false;
            sampleImage = new Bitmap("picture.jpg");
            textBox1.Text = CONST.CONST.trianglesX.ToString();
            textBox2.Text = CONST.CONST.trianglesY.ToString();
            textBox3.BackColor = Color.White;
            textBox4.BackColor = Color.White;
            lightColor = Color.White;
            sizeX = CONST.CONST.trianglesX;
            sizeY = CONST.CONST.trianglesY;
            trybPracy = TrybPracy.SwiatloDaleko;
            wypelnienie = Wypelnienie.Tekstura;
            rodzajMalowania = RodzajMalowania.Brak;
            picture.InitializePicture(sizeX, sizeY);
            MyTimer.Interval = (15);
            MyTimer.Tick += new EventHandler(TimerFunction);
            MyTimer.Start();
            //pictureBox1.Invalidate();
        }
        private void TimerFunction(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int x, y;
            if (int.TryParse(textBox1.Text, out x) && int.TryParse(textBox2.Text, out y))
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
                butt.BackColor = SystemColors.ControlLight;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UnCheckAllWypelnienie();
            CheckWypelnienie(sender);
            wypelnienie = Wypelnienie.Tekstura;
            textBox3.BackColor = Color.White;
            backColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UnCheckAllWypelnienie();
            CheckWypelnienie(sender);
            wypelnienie = Wypelnienie.WybierzKolor;
            colorDialog1.ShowDialog();
            textBox3.BackColor = colorDialog1.Color;
            backColor = textBox3.BackColor;
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
                butt.BackColor = SystemColors.ControlLight;
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
                butt.BackColor = SystemColors.ControlLight;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (trybPracy == TrybPracy.Przesuwanie)
            {
                if (isMoving)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        clickedPoint.MoveVerticeTo(new Vertice(e.X, e.Y));
                    }
                }

                Vertice p = picture.GetVertice(new Vertice(e.X, e.Y));
                if (p != null)
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand;
                }
                else
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (trybPracy == TrybPracy.Przesuwanie)
            {
                Vertice p = picture.GetVertice(new Vertice(e.X, e.Y));
                if (p != null)
                {
                    clickedPoint = p;
                    isMoving = true;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ks = trackBar1.Value / 100.0;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            kd = trackBar2.Value / 100.0;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            m = trackBar3.Value;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            textBox4.BackColor = colorDialog2.Color;
            lightColor = textBox4.BackColor;
        }
    }
}
