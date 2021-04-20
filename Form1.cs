using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace рлс
{
    public partial class Form1 : Form
    {
        int currentRLS = 0;
        bool[] installedRLS = new bool[5];
        bool drawRLS = false;

        bool drawLA = false;
        int currentLA = 0;
        bool[] installedLA = new bool[5];

        bool isOnEarth = true;
        bool isOnAir = true;


        public Form1()
        {
            InitializeComponent();
            textBox1.BackColor = Color.FromArgb(225, 225, 225);
            richTextBox1.BackColor = Color.FromArgb(225, 225, 225);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            drawRLS = true;
            drawLA = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            label3.Text = (Convert.ToString(e.X) + " " + Convert.ToString(e.Y));
            heightCheck(e);
            Graphics g = pictureBox1.CreateGraphics();
            Image part;
            if(drawRLS)
            {
                if (isOnEarth)
                {
                    if (!installedRLS[0] && currentRLS == 1)
                    {
                        part = new Bitmap(Properties.Resources._1л117);
                        g.DrawImage(part, e.X, e.Y);
                        installedRLS[0] = true;

                    }
                    if (!installedRLS[1] && currentRLS == 2)
                    {
                        part = new Bitmap(Properties.Resources._35н6);
                        g.DrawImage(part, e.X, e.Y);
                        installedRLS[1] = true;
                    }
                    if (!installedRLS[2] && currentRLS == 3)
                    {
                        part = new Bitmap(Properties.Resources._39н6);
                        g.DrawImage(part, e.X, e.Y);
                        installedRLS[2] = true;
                    }
                    if (!installedRLS[3] && currentRLS == 4)
                    {
                        part = new Bitmap(Properties.Resources._55ж6);
                        g.DrawImage(part, e.X, e.Y);
                        installedRLS[3] = true;
                    }
                    if (!installedRLS[4] && currentRLS == 5)
                    {
                        part = new Bitmap(Properties.Resources._19ж6);
                        g.DrawImage(part, e.X, e.Y);
                        installedRLS[4] = true;
                    }
                }
            }
            else if(drawLA)
            {
                if (isOnAir)
                {
                    if (!installedLA[0] && currentLA == 1)
                    {
                        part = new Bitmap(Properties.Resources.su35g);

                        g.DrawImage(part, e.X, e.Y);
                        installedLA[0] = true;
                    }
                    if (!installedLA[1] && currentLA == 2)
                    {
                        part = new Bitmap(Properties.Resources.рлс_иконка_2);
                        g.DrawImage(part, e.X, e.Y);

                        installedLA[1] = true;
                    }
                    if (!installedLA[2] && currentLA == 3)
                    {
                        part = new Bitmap(Properties.Resources.yak_130_top);
                        g.DrawImage(part, e.X, e.Y);
                        installedLA[2] = true;
                    }
                    if (!installedLA[3] && currentLA == 4)
                    {
                        part = new Bitmap(Properties.Resources.рлс_иконка_4);
                        g.DrawImage(part, e.X, e.Y);
                        installedLA[3] = true;
                    }
                    if (!installedLA[4] && currentLA == 5)
                    {
                        part = new Bitmap(Properties.Resources.рлс_иконка_5);
                        g.DrawImage(part, e.X, e.Y);
                        installedLA[4] = true;
                    }
                }
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = (Convert.ToString(e.X) + " " + Convert.ToString(e.Y));
            Graphics g = pictureBox1.CreateGraphics();
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drawRLS = false;
            drawLA = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            drawLA = true;
            drawRLS = false;
        }
        void heightCheck(MouseEventArgs e)
        {
            if (e.Y > pictureBox1.Size.Height*0.53 && e.Y < pictureBox1.Size.Height)
            {
                isOnEarth = true;
            }
            else isOnEarth = false;
            if (e.Y > 0 && e.Y <= pictureBox1.Size.Height * 0.53)
            {
                isOnAir = true;
            }
            else isOnAir = false;
            if(drawRLS && !isOnEarth)
            {
                MessageBox.Show("РЛС нужно ставить на землю.","Ограничение");
            }
            if(drawLA && !isOnAir)
            {
                MessageBox.Show("ЛА нужно устанавливать на небо.","Ограничение");
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRLS = comboBox1.SelectedIndex;
            if (selectedRLS == 0)
            {
                currentRLS = 1;
                richTextBox1.Text = "text rls 1";
            }
            if (selectedRLS == 1)
            {
                currentRLS = 2;
                richTextBox1.Text = "text rls 2";
            }
            if (selectedRLS == 2)
            {
                currentRLS = 3;
                richTextBox1.Text = "text rls 3";
            }
            if (selectedRLS == 3)
            {
                currentRLS = 4;
                richTextBox1.Text = "text rls 4";
            }
            if (selectedRLS == 4)
            {
                currentRLS = 5;
                richTextBox1.Text = "text rls 5";
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedLA = comboBox2.SelectedIndex;
            if (selectedLA == 0)
            {
                currentLA = 1;
                richTextBox1.Text = "text la 1";
            }
            if (selectedLA == 1)
            {
                currentLA = 2;
                richTextBox1.Text = "text la 2";
            }
            if (selectedLA == 2)
            {
                currentLA = 3;
                richTextBox1.Text = "text la 3";
            }
            if (selectedLA == 3)
            {
                currentLA = 4;
                richTextBox1.Text = "text la 4";
            }
            if (selectedLA == 4)
            {
                currentLA = 5;
                richTextBox1.Text = "text la 51";
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Application.Restart();
            pictureBox1.Image = Properties.Resources.Безымянный2;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Image part = new Bitmap(Properties.Resources.mmfree_icon_sign_7033452);
            e.Graphics.DrawImage(part, (float)((pictureBox1.Size.Width * 0.5) - 32), (float)(pictureBox1.Size.Height * 0.53 + pictureBox1.Size.Height * 0.47 * 0.5 - 35));
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    public class RLS
    {
        double LocationX, LocationY;
        string type;

        double D;//длина?
        double Power;//излучаемая мощность
        double G0;//коэфф усил
        double Lambda;//длина волны
        double Sensivity;//чувствительность P пр мин
        double q;//параметр обнаружения
        double K;//результирующий коэффициент потерь 1.23 1.44 1.58
        double Potential;//приведенный потенциал
        double DeltaW;// эффективная полоса пропускания
        double LambdaRLS;// длина волны рлс
        double TimeS;//длительность импульсного сигнала на выходе приемника
        double TetaE;//ширина диграммы направленности антенны (ФАР) в плоскости Е, размерность которой определяет размерность ошибок
        double Kg;//0.6 ili 0.4
    }
    public class SKO
    {
        double SigmaD;//ошибка измерения дальности
        double SigmaV2;//ошибка в измерении скорости
        double SigmaE;//ошибка в измерении угловых координат
    }
    public class LA
    {
        double LocationX, LocationY;
        string type;

        double Surface;//эфф отраж поверхность цели
    }
}
    
