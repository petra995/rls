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
        int n,m;

        RLS[] masRLS;
        LA[] masLA;
        SKO[] masSKO;

        int currentRLS;//текущий выбранный РЛС
        bool[] installedRLS;//массив определяющий какие РЛС установлены
        bool drawRLS;//флаг о том что рисуем РЛС

        bool drawLA;//флаг о том что рисуем ЛА
        int currentLA;//текущий выбранный ЛА
        bool[] installedLA;//массив определяющий какие ЛА уже установлены

        bool isOnEarth;//флаг о том что курсор на земле (реализация в heightcheck)
        bool isOnAir;//флаг о том что курсор на небе (реализация в heightcheck)


        public Form1()
        {
            InitializeComponent();
            InitializeValues();
            textBox1.BackColor = Color.FromArgb(225, 225, 225);
            richTextBox1.BackColor = Color.FromArgb(225, 225, 225);

        }

        private void InitializeValues()
        {
            n = comboBox1.Items.Count;
            m = comboBox2.Items.Count;
            masRLS = new RLS[n];
            masLA = new LA[m];
            masSKO = new SKO[m * n];

            installedRLS = new bool[n];
            installedLA = new bool[m];
        }

        private void button1_Click(object sender, EventArgs e)//установить РЛС
        {
            drawRLS = true;
            drawLA = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)//тык по карте
        {
            label3.Text = (Convert.ToString(e.X) + " " + Convert.ToString(e.Y));
            HeightCheck(e);
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
                        part = new Bitmap(Properties.Resources.бпла);
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
        void HeightCheck(MouseEventArgs e)
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
                masRLS[0] = new RLS() 
                {
                    type = comboBox1.SelectedItem.ToString(),
                    
                    D = 1,
                    Power = 1,
                    G0 = 1,
                    Lambda = 1,//длина волны
                    Sensivity = 1,//чувствительность P пр мин
                    q = 1,//параметр обнаружения
                    K = 1,//результирующий коэффициент потерь 1.23 1.44 1.58
                    Potential = 1,//приведенный потенциал
                    DeltaW = 1,// эффективная полоса пропускания
                    LambdaRLS = 1,// длина волны рлс
                    TimeS = 1,//длительность импульсного сигнала на выходе приемника
                    TetaE = 1,//ширина диграммы направленности антенны (ФАР) в плоскости Е, размерность которой определяет размерность ошибок
                    Kg = 1

                };
                masRLS[0].description = "Номер: " + 1 + "\n" +
                                        "Тип: " + masRLS[0].type + "\n" +
                                        "Длина: " + masRLS[0].D + "\n" +
                                        "Мощность: " + masRLS[0].Power + "\n" +
                                        "Коэф усиления: " + masRLS[0].G0 + "\n" +
                                        "Длина волны: " + masRLS[0].Lambda + "\n" +
                                        "Чувствительность: " + masRLS[0].Sensivity + "\n" +
                                        "Параметр обнаружения: " + masRLS[0].q + "\n" +
                                        "Результирующий коэф потерь: " + masRLS[0].K + "\n" +
                                        "Приведенный потенциал: " + masRLS[0].Potential + "\n" +
                                        "Эффектив полоса пропускания: " + masRLS[0].DeltaW + "\n" +
                                        "Длина волны РЛС: " + masRLS[0].LambdaRLS + "\n" +
                                        "Длительность импульсного сигнала: " + masRLS[0].TimeS + "\n" +
                                        "Ширина диаграммы направ антенны: " + masRLS[0].TetaE + "\n" +
                                        "Еще чтото: " + masRLS[0].Kg + "\n";
                richTextBox1.Text = masRLS[0].description;
                //masRLS[10] = new RLS();
            }
            if (selectedRLS == 1)
            {
                currentRLS = 2;
                richTextBox1.Text = "text rls 2";
                masRLS[1] = new RLS()
                {
                    type = comboBox1.SelectedItem.ToString(),
                    D = 1,
                    Power = 1,
                    G0 = 1,
                    Lambda = 1,//длина волны
                    Sensivity = 1,//чувствительность P пр мин
                    q = 1,//параметр обнаружения
                    K = 1,//результирующий коэффициент потерь 1.23 1.44 1.58
                    Potential = 1,//приведенный потенциал
                    DeltaW = 1,// эффективная полоса пропускания
                    LambdaRLS = 1,// длина волны рлс
                    TimeS = 1,//длительность импульсного сигнала на выходе приемника
                    TetaE = 1,//ширина диграммы направленности антенны (ФАР) в плоскости Е, размерность которой определяет размерность ошибок
                    Kg = 1

                };
            }
            if (selectedRLS == 2)
            {
                currentRLS = 3;
                richTextBox1.Text = "text rls 3";
                masRLS[2] = new RLS()
                {
                    type = comboBox1.SelectedItem.ToString(),
                    D = 1,
                    Power = 1,
                    G0 = 1,
                    Lambda = 1,//длина волны
                    Sensivity = 1,//чувствительность P пр мин
                    q = 1,//параметр обнаружения
                    K = 1,//результирующий коэффициент потерь 1.23 1.44 1.58
                    Potential = 1,//приведенный потенциал
                    DeltaW = 1,// эффективная полоса пропускания
                    LambdaRLS = 1,// длина волны рлс
                    TimeS = 1,//длительность импульсного сигнала на выходе приемника
                    TetaE = 1,//ширина диграммы направленности антенны (ФАР) в плоскости Е, размерность которой определяет размерность ошибок
                    Kg = 1

                };
            }
            if (selectedRLS == 3)
            {
                currentRLS = 4;
                richTextBox1.Text = "text rls 4";
                masRLS[3] = new RLS()
                {
                    type = comboBox1.SelectedItem.ToString(),
                    D = 1,
                    Power = 1,
                    G0 = 1,
                    Lambda = 1,//длина волны
                    Sensivity = 1,//чувствительность P пр мин
                    q = 1,//параметр обнаружения
                    K = 1,//результирующий коэффициент потерь 1.23 1.44 1.58
                    Potential = 1,//приведенный потенциал
                    DeltaW = 1,// эффективная полоса пропускания
                    LambdaRLS = 1,// длина волны рлс
                    TimeS = 1,//длительность импульсного сигнала на выходе приемника
                    TetaE = 1,//ширина диграммы направленности антенны (ФАР) в плоскости Е, размерность которой определяет размерность ошибок
                    Kg = 1

                };
            }
            if (selectedRLS == 4)
            {
                currentRLS = 5;
                richTextBox1.Text = "text rls 5";
                masRLS[4] = new RLS()
                {
                    type = comboBox1.SelectedItem.ToString(),
                    D = 1,
                    Power = 1,
                    G0 = 1,
                    Lambda = 1,//длина волны
                    Sensivity = 1,//чувствительность P пр мин
                    q = 1,//параметр обнаружения
                    K = 1,//результирующий коэффициент потерь 1.23 1.44 1.58
                    Potential = 1,//приведенный потенциал
                    DeltaW = 1,// эффективная полоса пропускания
                    LambdaRLS = 1,// длина волны рлс
                    TimeS = 1,//длительность импульсного сигнала на выходе приемника
                    TetaE = 1,//ширина диграммы направленности антенны (ФАР) в плоскости Е, размерность которой определяет размерность ошибок
                    Kg = 1

                };
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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
    public class RLS
    {
        public double LocationX, LocationY;
        public string type;
        public string description;
        public double D;//длина?
        public double Power;//излучаемая мощность
        public double G0;//коэфф усил
        public double Lambda;//длина волны
        public double Sensivity;//чувствительность P пр мин
        public double q;//параметр обнаружения
        public double K;//результирующий коэффициент потерь 1.23 1.44 1.58
        public double Potential;//приведенный потенциал
        public double DeltaW;// эффективная полоса пропускания
        public double LambdaRLS;// длина волны рлс
        public double TimeS;//длительность импульсного сигнала на выходе приемника
        public double TetaE;//ширина диграммы направленности антенны (ФАР) в плоскости Е, размерность которой определяет размерность ошибок
        public double Kg;//0.6 ili 0.4
    }
    public class SKO
    {
        public double SigmaD;//ошибка измерения дальности
        public double SigmaV2;//ошибка в измерении скорости
        public double SigmaE;//ошибка в измерении угловых координат
    }
    public class LA
    {
        public double LocationX, LocationY;
        public string type;

        public double Surface;//эфф отраж поверхность цели
    }
}
    
