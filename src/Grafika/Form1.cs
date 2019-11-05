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
        bool randomKdKsM = false;
        Bitmap sampleImage;
        Color backColor;
        Color lightColor;
        Color[,] sampleImageColor;
        Color[,] normalMapColor;
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
        bool triangleWeb;
        int m;
        (int, int, int) lightSource;
        double t = 1;
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
                g.PaintBrak(picture, wypelnienie, sampleImageColor, backColor, trybPracy, ks, kd, m, triangleWeb);
            }
            else
            {
                g.Paint(picture, wypelnienie, sampleImageColor, normalMapColor, backColor, trybPracy, ks, kd, m, rodzajMalowania, lightColor, opcjaWektoraN, lightSource, randomKdKsM, triangleWeb);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            isMoving = false;
            random = new Random();
            sampleImage = new Bitmap("picture.jpg");
            Bitmap normalMap = new Bitmap("normal.jpg");
            sampleImageColor = new Color[CONST.CONST.bitmapX + 1, CONST.CONST.bitmapY + 1];
            normalMapColor = new Color[CONST.CONST.bitmapX + 1, CONST.CONST.bitmapY + 1];
            for (int i=0;i<CONST.CONST.bitmapX + 1;i++)
            {
                for(int j=0;j<CONST.CONST.bitmapY + 1;j++)
                {
                    sampleImageColor[i, j] = sampleImage.GetPixel(i, j);
                    normalMapColor[i, j] = normalMap.GetPixel(i, j);
                }
            }
            textBox1.Text = CONST.CONST.trianglesX.ToString();
            textBox2.Text = CONST.CONST.trianglesY.ToString();
            textBox3.BackColor = Color.White;
            textBox4.BackColor = Color.White;
            lightColor = Color.White;
            backColor = Color.White;
            sizeX = CONST.CONST.trianglesX;
            sizeY = CONST.CONST.trianglesY;
            radioButton7.Checked = true;
            radioButton9.Checked = true;
            radioButton3.Checked = true;
            radioButton1.Checked = true;
            //checkbox2
            kd = 0.75;
            ks = 0.25;
            m = 30;
            trackBar1.Value = (int)(kd * 100);
            trackBar2.Value = (int)(ks * 100);
            trackBar3.Value = m;
            //   
            triangleWeb = true;
            lightSource = (CONST.CONST.bitmapX / 2, CONST.CONST.bitmapX / 2, 50);
            picture.InitializePicture(sizeX, sizeY);
            MyTimer.Interval = (20);
            MyTimer.Tick += new EventHandler(TimerFunction);
            MyTimer.Start();
        }
        private void TimerFunction(object sender, EventArgs e)
        {
            double newX = CONST.CONST.bitmapX / 2 * Math.Sin(t + 5 * Math.PI / 2) + CONST.CONST.bitmapX / 2;
            double newY = CONST.CONST.bitmapY / 2 * Math.Sin(4 * t) + CONST.CONST.bitmapY / 2;
            lightSource = ((int)newX, (int)newY, 50);
            t += 0.01;
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
                randomKdKsM = true;
                trackBar1.Enabled = false;
                trackBar2.Enabled = false;
                trackBar3.Enabled = false;
                foreach(var triangle in picture.Triangles)
                {
                    triangle.KS = random.NextDouble();
                    triangle.KD = random.NextDouble();
                    triangle.M = random.Next(1, 100);
                }
            }
            else
            {
                randomKdKsM = false;
                trackBar1.Enabled = true;
                trackBar2.Enabled = true;
                trackBar3.Enabled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                triangleWeb = false;
            }
            else
            {
                triangleWeb = true;
            }
        }
    }
}
