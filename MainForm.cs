using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RLS_Computing
{
    public partial class MainForm : Form
    {
        int n,m;

        RLS[] masRLS;
        LA[] masLA;
        SKO[,] masSKO;

        int selectedRLS;//текущий выбранный РЛС
        bool[] installedRLS;//массив определяющий какие РЛС установлены
        bool drawRLS;//флаг о том что рисуем РЛС

        bool drawLA;//флаг о том что рисуем ЛА
        int selectedLA;//текущий выбранный ЛА
        bool[] installedLA;//массив определяющий какие ЛА уже установлены

        bool isOnEarth;//флаг о том что курсор на земле (реализация в heightcheck)
        bool isOnAir;//флаг о том что курсор на небе (реализация в heightcheck)


        public MainForm()
        {
            InitializeComponent();
            InitializeValues();
            textBox1.BackColor = Color.FromArgb(225, 225, 225);
            richTextBox1.BackColor = Color.FromArgb(225, 225, 225);
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
        }

        private void InitializeValues()
        {
            n = comboBox1.Items.Count;
            m = comboBox2.Items.Count;
            masRLS = new RLS[n];
            masLA = new LA[m];
            masSKO = new SKO[m, n];
            for (int i = 0; i < n; i++)
            {
                masRLS[i] = new RLS();
            }
            for (int j = 0; j < m; j++)
            {
                masLA[j] = new LA();
            }
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
                    if (!installedRLS[0] && selectedRLS == 0)
                    {
                        part = new Bitmap(Properties.Resources._22ж6);
                        g.DrawImage(part, e.X, e.Y);
                        installedRLS[0] = true;

                    }
                    if (!installedRLS[1] && selectedRLS == 1)
                    {
                        part = new Bitmap(Properties.Resources._55ж6);
                        g.DrawImage(part, e.X, e.Y);
                        installedRLS[1] = true;
                    }
                    if (!installedRLS[2] && selectedRLS == 2)
                    {
                        part = new Bitmap(Properties.Resources._35д6);
                        g.DrawImage(part, e.X, e.Y);
                        installedRLS[2] = true;
                    }
                    if (!installedRLS[3] && selectedRLS == 3)
                    {
                        part = new Bitmap(Properties.Resources._1л117м);
                        g.DrawImage(part, e.X, e.Y);
                        installedRLS[3] = true;
                    }
                    if (!installedRLS[4] && selectedRLS == 4)
                    {
                        part = new Bitmap(Properties.Resources._19ж6_1);
                        g.DrawImage(part, e.X, e.Y);
                        installedRLS[4] = true;
                    }
                }
            }
            else if(drawLA)
            {
                if (isOnAir)
                {
                    if (!installedLA[0] && selectedLA == 0)
                    {
                        part = new Bitmap(Properties.Resources.su35g);

                        g.DrawImage(part, e.X, e.Y);
                        installedLA[0] = true;
                    }
                    if (!installedLA[1] && selectedLA == 1)
                    {
                        part = new Bitmap(Properties.Resources.бпла);
                        g.DrawImage(part, e.X, e.Y);

                        installedLA[1] = true;
                    }
                    if (!installedLA[2] && selectedLA == 2)
                    {
                        part = new Bitmap(Properties.Resources.yak_130_top);
                        g.DrawImage(part, e.X, e.Y);
                        installedLA[2] = true;
                    }
                    if (!installedLA[3] && selectedLA == 3)
                    {
                        part = new Bitmap(Properties.Resources.рлс_иконка_4);
                        g.DrawImage(part, e.X, e.Y);
                        installedLA[3] = true;
                    }
                    if (!installedLA[4] && selectedLA == 4)
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
            int i = selectedRLS = comboBox1.SelectedIndex;
            switch(selectedRLS)
            {
                case 0:
                    masRLS[0] = new RLS()
                    {
                        type = comboBox1.SelectedItem.ToString(),
                        D = 1,
                        Power = 14,
                        G0 = 1,
                        Lambda = 1,//длина волны
                        Sensivity = 1,//чувствительность P пр мин
                        q = 1,//параметр обнаружения
                        K = 1,//результирующий коэффициент потерь 1.23 1.44 1.58
                        DeltaW = 1,// эффективная полоса пропускания
                        LambdaRLS = 1,// длина волны рлс
                        TimeS = 1,//длительность импульсного сигнала на выходе приемника
                        TetaE = 1,//ширина диграммы направленности антенны (ФАР) в плоскости Е, размерность которой определяет размерность ошибок
                        Kg = 1,
                    };
                    break;
                case 1:
                    masRLS[1] = new RLS()
                    {
                        type = comboBox1.SelectedItem.ToString(),
                        D = 1,
                        Power = 15.5,
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
                    break;
                case 2:
                    masRLS[1] = new RLS()
                    {
                        type = comboBox1.SelectedItem.ToString(),
                        D = 1,
                        Power = 3,
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
                    break;
                case 3:
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
                    break;
                case 4:
                    masRLS[1] = new RLS()
                    {
                        type = comboBox1.SelectedItem.ToString(),
                        D = 1,
                        Power = 3,
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
                    break;
            }

            masRLS[i].description = "Номер: " + 1 + "\n" +
                                        "Тип: " + masRLS[i].type + "\n" +
                                        "Длина: " + masRLS[i].D + "\n" +
                                        "Мощность: " + masRLS[i].Power + "\n" +
                                        "Коэф усиления: " + masRLS[i].G0 + "\n" +
                                        "Длина волны: " + masRLS[i].Lambda + "\n" +
                                        "Чувствительность: " + masRLS[i].Sensivity + "\n" +
                                        "Параметр обнаружения: " + masRLS[i].q + "\n" +
                                        "Результирующий коэф потерь: " + masRLS[i].K + "\n" +
                                        "Приведенный потенциал: " + masRLS[i].Potential + "\n" +
                                        "Эффектив полоса пропускания: " + masRLS[i].DeltaW + "\n" +
                                        "Длина волны РЛС: " + masRLS[i].LambdaRLS + "\n" +
                                        "Длительность импульсного сигнала: " + masRLS[i].TimeS + "\n" +
                                        "Ширина диаграммы направ антенны: " + masRLS[i].TetaE + "\n";
            richTextBox1.Text = masRLS[i].description;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedLA = comboBox2.SelectedIndex;
            if (selectedLA == 0)
            {
                richTextBox1.Text = "text la 1";
            }
            if (selectedLA == 1)
            {
                richTextBox1.Text = "text la 2";
            }
            if (selectedLA == 2)
            {
                richTextBox1.Text = "text la 3";
            }
            if (selectedLA == 3)
            {
                richTextBox1.Text = "text la 4";
            }
            if (selectedLA == 4)
            {
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
            HelpForm f2 = new HelpForm();
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    masSKO[i, j] = new SKO(masRLS[i],masLA[j]);
                }
            }
            CalculateForm f3 = new CalculateForm();
            f3.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
    public class RLS
    {
        public bool installed;
        public double LocationX, LocationY;
        public string type;
        public string description;
        public double D;//длина?
        public double Power;//излучаемая мощность
        public double G0;//коэфф усил
        public double Lambda;//длина волны
        public double Sensivity;//чувствительность P пр мин
        public double q;//параметр обнаружения
        public double K = 10;//результирующий коэффициент потерь 1.23 1.44 1.58
        public double Potential;//приведенный потенциал
        public double DeltaW;// эффективная полоса пропускания
        public double LambdaRLS;// длина волны рлс
        public double TimeS;//длительность импульсного сигнала на выходе приемника
        public double TetaE;//ширина диграммы направленности антенны (ФАР) в плоскости Е, размерность которой определяет размерность ошибок
        public double TetaB;//ширина диграммы направленности антенны (ФАР) в плоскости B
        public double Kg = 0.4;//0.6 ili 0.4
        public RLS()
        {
            CalculatePotential();
        }

        internal void CalculatePotential()
        {
            Potential = (Power * Math.Pow(G0, 2) * Math.Pow(Lambda, 2)) / (Math.Pow(Math.PI, 3) * Sensivity * K);
        }
    }
    public class SKO
    {
        public bool installed;
        RLS rls;
        LA la;
        public double SigmaD;//ошибка измерения дальности
        public double SigmaV2;//ошибка в измерении скорости
        public double SigmaE;//ошибка в измерении угловых координат
        public double SigmaB;
        public SKO(RLS rls, LA la)
        {
            SigmaD = 1;
            SigmaV2 = (rls.LambdaRLS/2)*((Math.Sqrt(3)*Math.Pow(rls.D,2))/Math.PI*rls.TimeS*Math.Sqrt(rls.Potential*la.Surface));
            SigmaE = 1;
        }
    }
    public class LA
    {
        public double LocationX, LocationY;
        public string type;

        public double Surface;//эфф отраж поверхность цели













        public double D;//длина?
        public double Power;//излучаемая мощность
        public double G0;//коэфф усил
        public double Lambda;//длина волны
        public double Sensivity;//чувствительность P пр мин
        public double q;//параметр обнаружения
        public double DeltaW;// эффективная полоса пропускания
        public double LambdaRLS;// длина волны рлс
        public double TimeS;//длительность импульсного сигнала на выходе приемника
        public double TetaE;//ширина диграммы направленности антенны (ФАР) в плоскости Е, размерность которой определяет размерность ошибок
        public double TetaB;//ширина диграммы направленности антенны (ФАР) в плоскости B
    }

}
    
