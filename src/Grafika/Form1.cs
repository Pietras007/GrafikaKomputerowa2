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
        public Wypelnienie wypelnienie;
        public RodzajMalowania rodzajMalowania;
        public OpcjaWektoraN opcjaWektoraN;
        Timer MyTimer = new Timer();
        bool isMoving;
        Vertice clickedPoint;
        double ks;
        double kd;
        int m;
        Vector vectorL = new Vector(0, 0, 1);
        Random random;
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
                g.Paint(picture, wypelnienie, sampleImage, backColor, trybPracy, ks, kd, m, rodzajMalowania, lightColor, opcjaWektoraN, vectorL);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            isMoving = false;
            random = new Random();
            sampleImage = new Bitmap("picture.jpg");
            textBox1.Text = CONST.CONST.trianglesX.ToString();
            textBox2.Text = CONST.CONST.trianglesY.ToString();
            textBox3.BackColor = Color.White;
            textBox4.BackColor = Color.White;
            lightColor = Color.White;
            sizeX = CONST.CONST.trianglesX;
            sizeY = CONST.CONST.trianglesY;
            radioButton7.Checked = true;
            radioButton9.Checked = true;
            radioButton3.Checked = true;
            radioButton1.Checked = true;
            checkBox2.Checked = true;
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
            if (int.TryParse(textBox1.Text, out x) && int.TryParse(textBox2.Text, out y))
            {
                sizeX = x;
                sizeY = y;
                picture.InitializePicture(sizeX, sizeY);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            colorDialog1.ShowDialog();
            textBox3.BackColor = colorDialog1.Color;
            backColor = textBox3.BackColor;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked)
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
            if (checkBox1.Checked)
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

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            rodzajMalowania = RodzajMalowania.Brak;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            rodzajMalowania = RodzajMalowania.Dokladne;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            rodzajMalowania = RodzajMalowania.Interpolowane;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            rodzajMalowania = RodzajMalowania.Hybrydowe;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            opcjaWektoraN = OpcjaWektoraN.Staly;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            opcjaWektoraN = OpcjaWektoraN.Tekstura;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            trybPracy = TrybPracy.SwiatloDaleko;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            trybPracy = TrybPracy.SwiatloWedrujace;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            wypelnienie = Wypelnienie.Tekstura;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            wypelnienie = Wypelnienie.JednolityKolor;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                kd = random.NextDouble();
                ks = random.NextDouble();
                m = random.Next(1, 100);
                trackBar1.Value = (int)(kd * 100);
                trackBar2.Value = (int)(ks * 100);
                trackBar3.Value = m;
                trackBar1.Enabled = false;
                trackBar2.Enabled = false;
                trackBar3.Enabled = false;
            }
            else
            {
                trackBar1.Enabled = true;
                trackBar2.Enabled = true;
                trackBar3.Enabled = true;
            }
        }
    }
}
