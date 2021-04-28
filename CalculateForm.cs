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
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = masSKO[i, j].rls.type;
                    dataGridView1[1, j+i].Value = masSKO[i, j].la.type;
                    dataGridView1[2,j+i].Value = masSKO[i, j].SigmaD;
                    //masSKO[100, 100].SigmaB = 1;
                    //dataGridView1[j, 3].Value = masSKO[i, j].SigmaV2;
                    //dataGridView1[j, 4].Value = masSKO[i, j].SigmaE;
                    //dataGridView1[j, 5].Value = masSKO[i, j].SigmaB;
                }
            }
        }
    }
}
