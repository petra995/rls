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
        RLS[] masRLS;
        LA[] masLA;
        public CalculateForm()
        {
            InitializeComponent();
            
        }
        public void TransferMas(LA[] masLA, RLS[] masRLS)
        {
            this.masLA = masLA;
            this.masRLS = masRLS;
        }
        public double SigmaV2(RLS rls, LA la)
        {
            return (rls.LambdaRLS / 2) * ((Math.Sqrt(3) * Math.Pow(rls.D, 2)) / Math.PI * rls.TimeS * Math.Sqrt(rls.Potential * la.Surface));
        }
        public double SigmaD(RLS rls, LA la)
        {
            return (rls.LambdaRLS / 2) * ((Math.Sqrt(3) * Math.Pow(rls.D,2))/(rls.DeltaW * Math.Sqrt(rls.Potential * la.Surface)));
        }
        public double SigmaE(RLS rls, LA la)
        {
            return (rls.Kg * rls.TetaE * Math.Pow(rls.D,2)) / (Math.Sqrt(rls.Potential * la.Surface));
        }
        public double SigmaB(RLS rls, LA la)
        {
            return (rls.Kg * rls.TetaB * Math.Pow(rls.D, 2)) / (Math.Sqrt(rls.Potential * la.Surface));
        }
        public double SigmaMain(RLS rls, LA la)
        {
            return Math.Sqrt(Math.Pow(SigmaD(rls,la),2) + Math.Pow(SigmaE(rls, la) * rls.D, 2) + Math.Pow(SigmaB(rls, la) * rls.D, 2));
        }
        public void SetValues()
        {
            bool flag1 = false, flag2 = false;
            for (int i = 0; i < masRLS.Length; i++)
            {
                if (masRLS[i].installed)
                {
                    flag1 = true;
                }
            }
            for (int i = 0; i < masLA.Length; i++)
            {
                if (masLA[i].installed)
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
                //dataGridView1.Rows.Add();
                for (int i = 0; i < masRLS.Length; i++)
                {
                    dataGridView1[0, i * masLA.Length].Value = masRLS[i].type;
                    for (int j = 0; j < masLA.Length; j++)
                    {

                        dataGridView1.Rows.Add();
                        dataGridView1[1, j + i * masLA.Length].Value = masLA[j].type;
                        if(masRLS[i].installed && masLA[j].installed)
                        {
                            dataGridView1[2, j + i * masLA.Length].Value = SigmaD(masRLS[i], masLA[j]);
                            dataGridView1[3, j + i * masLA.Length].Value = SigmaV2(masRLS[i], masLA[j]);
                            dataGridView1[4, j + i * masLA.Length].Value = SigmaE(masRLS[i], masLA[j]);
                            dataGridView1[5, j + i * masLA.Length].Value = SigmaMain(masRLS[i], masLA[j]);
                        }
                    }
                }
            }
            
        }
    }
}
