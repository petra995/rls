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
    public partial class CalculateForm : Form
    {
        int n, m;
        SKO[,] masSKO;
        public CalculateForm()
        {
            InitializeComponent();
            
        }

        public void TransferValues(int n, int m, SKO[,] masSKO)
        {
            this.n = n;
            this.m = m;
            this.masSKO = masSKO;
        }

        public void SetValues()
        {
            bool flag1 = false, flag2 = false;
            for (int i = 0; i < n; i++)
            {
                if (masSKO[i, 0].rls.installed)
                {
                    flag1 = true;
                }
            }
            for (int i = 0; i < m; i++)
            {
                if (masSKO[0, i].la.installed)
                {
                    flag2 = true;
                }
            }
            if (!flag1 && !flag2)
            {
                MessageBox.Show("Не установлено ни одного рлс и ла", "Предупреждение");
            }
            else if (!flag1)
            {
                MessageBox.Show("Не установлено ни одного рлс", "Предупреждение");
            }
            else if (!flag2)
            {
                MessageBox.Show("Не установлено ни одного ла", "Предупреждение");
            }
            else
            {
                dataGridView1.Rows.Add();
                for (int i = 0; i < n; i++)
                {
                    dataGridView1[0, i * m].Value = masSKO[i, 0].rls.type;
                    for (int j = 0; j < m; j++)
                    {
                        //if (masSKO[i,j].la.installed)
                        dataGridView1.Rows.Add();
                        dataGridView1[1, j + i * m].Value = masSKO[i, j].la.type;
                        dataGridView1[2, j + i * m].Value = masSKO[i, j].SigmaD;
                        //masSKO[100, 100].SigmaB = 1;
                        //dataGridView1[j, 3].Value = masSKO[i, j].SigmaV2;
                        //dataGridView1[j, 4].Value = masSKO[i, j].SigmaE;
                        //dataGridView1[j, 5].Value = masSKO[i, j].SigmaB;
                    }
                }
            }
            
        }
    }
}
