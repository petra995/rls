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
            masSKO = new SKO[n, m];
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
                        description = "Диапазон волн - Сантиметровый-(13 см)\n" +
                                        "Пределы работы\n" +
                                        "по дальности, км 5…400\n" +
                                        "по азимуту, град. 0…360\n" +
                                        "по углу места, град. 0…30\n" +
                                        "по высоте, км 0…30\n" +
                                        "Дальность обнаружения\n" +
                                        "воздушной цели с ЭПР 1м2, км,\n" +
                                        "на высотах 100 м 37\n" +
                                        "500 м 75\n" +
                                        "1000 м 105\n" +
                                        "10000 м 310\n" +
                                        "Коэффициент подавления отражений от местных предметов, дБ 40\n" +
                                        "Период обзора пространства, с 10\n" +
                                        "Количество сопровождаемых целей 120\n" +
                                        "Точность измерения координат:\n" +
                                        "дальности, м 300\n" +
                                        "азимута, мин 15 (20 ПАП)\n" +
                                        "высоты, м 500 (800)\n" +
                                        "Разрешающая способность:\n" +
                                        "по дальности, м 400\n" +
                                        "по азимуту, град. 3,5\n" +
                                        "\n" +
                                        "Средняя излучаемая мощность, кВт 14",
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
                        description = "Диапазон волн - метровый\n" +
                                        "Пределы работы\n" +
                                        "по дальности, км 1200\n" +
                                        "по азимуту, град. 0…360\n" +
                                        "по углу места, град. 16\n" +
                                        "по высоте, км. 75\n" +
                                        "Дальность обнаружения воздушной цели с ЭПР 1м2, км,\n" +
                                        "на высотах 100 м 35\n" +
                                        "500 м 80\n" +
                                        "1000 м 110\n" +
                                        "4000 м 200\n" +
                                        "10000 м 300\n" +
                                        "Помехозащищенность: от пассивной помехи, пачек на 100м пути 1-2\n" +
                                        "от активной помехи, Вт/МГц 10\n" +
                                        "Период обзора пространства, с 10,20\n" +
                                        "Точность измерения координат:\n" +
                                        "дальности, м 400\n" +
                                        "азимута, град. 1\n" +
                                        "по углу места, град. 2\n" +
                                        "Разрешающая способность:\n" +
                                        "по дальности, м 1700\n" +
                                        "по азимуту, град. 1\n" +
                                        "Средняя излучаемая\n" +
                                        "мощность, кВт 14…17",
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
                    masRLS[2] = new RLS()
                    {
                        type = comboBox1.SelectedItem.ToString(),
                        description = "Диапазон волн - сантиметровый\n" +
                                        "Пределы работы\n" +
                                        "по дальности, км 05…200\n" +
                                        "по азимуту, град. 0…360\n" +
                                        "по углу места, град. –0,10…+30\n" +
                                        "по высоте, км 100\n" +
                                        "Дальность обнаружения\n" +
                                        "воздушной цели с ЭПР 1м2, км,\n" +
                                        "на высотах 50 м 40\n" +
                                        "100 м 46\n" +
                                        "500 м 86\n" +
                                        "1000 м 120\n" +
                                        "10000 м 200\n" +
                                        "Коэффициент подавления\n" +
                                        "отражений от местных\n" +
                                        "предметов, дБ 48\n" +
                                        "Период обзора пространства, с 5,10\n" +
                                        "Количество сопровождаемых\n" +
                                        "целей 127\n" +
                                        "Точность измерения координат:\n" +
                                        "дальности, м 250\n" +
                                        "азимута, мин. 20\n" +
                                        "высоты, м 400\n" +
                                        "Разрешающая способность:\n" +
                                        "по дальности, м 300\n" +
                                        "по азимуту, град. 4\n" +
                                        "по высоте, м 1500\n" +
                                        "\n" +
                                        "Средняя излучаемая\n" +
                                        "мощность, кВт 3\n",
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
                    };
                    break;
                case 3:
                    masRLS[3] = new RLS()
                    {
                        type = comboBox1.SelectedItem.ToString(),
                        description = "Диапазон рабочих частот, см 10\n" +
                                        "Максимальная дальность, км 350\n" +
                                        "Угол обзора в вертикальной плоскости, рад. 28\n" +
                                        "Точность определения координат (СКО):\n" +
                                        "дальности, м 125\n" +
                                        "азимута, угл. мин. 6\n" +
                                        "высоты, м 400\n" +
                                        "Разрешающая способность: 170\n" +
                                        "по дальности, м 125\n" +
                                        "по азимуту, град. 1\n" +
                                        "Коэффициент подавления отражений от местных предметов, дБ 45\n" +
                                        "Темп обновления информации, с 5 и 10\n" +
                                        "Количество сопровождаемых целей 200\n" +
                                        "Потребляемая мощность, кВт 50\n" +
                                        "Условия окружающей среды: трассы\n" +
                                        "рабочий диапазон температур, °С от – 40 до +50\n" +
                                        "относительная влажность, % до 100 (в тропическом исполнении)\n" +
                                        "скорость ветра, м/с до 25 без РПУ\n" +
                                        "Время свертывания и развертывания РЛС, ч 5",
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
                    };
                    break;
                case 4:
                    masRLS[4] = new RLS()
                    {
                        type = comboBox1.SelectedItem.ToString(),
                        description = "Диапазон волн - сантиметровый\n" +
                                        "Пределы работы\n" +
                                        "по дальности, км 05…150\n" +
                                        "по азимуту, град. 0…360\n" +
                                        "по углу места, град. –0,10…+30\n" +
                                        "по высоте, км 100\n" +
                                        "Дальность обнаружения\n" +
                                        "воздушной цели с ЭПР 1м2, км,\n" +
                                        "на высотах 50 м 30\n" +
                                        "100 м 45\n" +
                                        "500 м 80\n" +
                                        "1000 м 120\n" +
                                        "10000 м 145\n" +
                                        "Коэффициент подавления\n" +
                                        "отражений от местных\n" +
                                        "предметов, дБ 48\n" +
                                        "Период обзора пространства, с 5,10\n" +
                                        "Количество сопровождаемых\n" +
                                        "целей 127\n" +
                                        "Точность измерения координат:\n" +
                                        "дальности, м 100\n" +
                                        "азимута, мин. 15\n" +
                                        "высоты, м 400\n" +
                                        "Разрешающая способность:\n" +
                                        "по дальности, м 300\n" +
                                        "по азимуту, град. 4\n" +
                                        "по высоте, м 1500\n" +
                                        "Средняя излучаемая\n" +
                                        "мощность, кВт 3",
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
                    };
                    break;
            }
            richTextBox1.Text = masRLS[i].description;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = selectedLA = comboBox2.SelectedIndex;
            switch (selectedLA)
            {
                case 0:
                    masLA[0] = new LA()
                    {
                        type = comboBox2.SelectedItem.ToString(),
                        Surface = 1,
                    };
                    break;
                case 1:
                    masLA[1] = new LA()
                    {
                        type = comboBox2.SelectedItem.ToString(),
                        Surface = 1,
                    };
                    break;
                case 2:
                    masLA[2] = new LA()
                    {
                        type = comboBox2.SelectedItem.ToString(),
                        Surface = 1,
                    };
                    break;
                case 3:
                    masLA[3] = new LA()
                    {
                        type = comboBox2.SelectedItem.ToString(),
                        Surface = 1,
                    };
                    break;
                case 4:
                    masLA[4] = new LA()
                    {
                        type = comboBox2.SelectedItem.ToString(),
                        Surface = 1,
                    };
                    break;
            }
            masLA[i].description = "Номер: " + 1 + "\n" +
                                        "Тип: " + masLA[i].type + "\n" +
                                        "Эфф отражающая поверхность: " + masLA[i].Surface + "\n";
                                       
            richTextBox1.Text = masLA[i].description;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Application.Restart();
            pictureBox1.Image = Properties.Resources.Безымянный2;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Image part = new Bitmap(Properties.Resources.goal);
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
            f3.TransferValues(n,m,masSKO);
            f3.SetValues();
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
        public RLS rls;
        public LA la;
        public double SigmaD;//ошибка измерения дальности
        public double SigmaV2;//ошибка в измерении скорости
        public double SigmaE;//ошибка в измерении угловых координат
        public double SigmaB;
        public SKO(RLS rls, LA la)
        {
            this.rls = rls;
            this.la = la;
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
        public string description;
    }

}
    
