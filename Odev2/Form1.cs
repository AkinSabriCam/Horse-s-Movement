using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odev2
{
    public partial class Form1 : Form
    {
        int X, Y, A, B, deger = 0, Puan = 1;
        public Form1()
        {
            InitializeComponent();
        }
        public void Temizle()
        {
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    OyunPanel.Rows[j].Cells[i].Style.BackColor = Color.White;
                }
            }
        }
        public bool SayiKontrol(int x, int y)
        {  // karelerde sayı olup olmadığının kontrolü
            string s;
            int n;
            bool Sayi = false;
            try
            {
                s = OyunPanel.Rows[y].Cells[x].Value.ToString();
                if (!string.IsNullOrEmpty(s))
                   Sayi = int.TryParse(s, out n);
            }
            catch
            {
                s = null;
                Sayi = true;
            }

            return Sayi;
        }
        public void  Islem(int x, int y)
        { 
           //L şeklinde karelerin belirlenme işlemi yapılmakda  
            int[] skorKontrol = new int[8];
            int sayac = 0;

            
            Temizle();
            if (y + 1 <= Y && x - 2 >= 0)
            {
                if (!SayiKontrol(x - 2, y + 1))
                {
                    OyunPanel.Rows[y + 1].Cells[x - 2].Style.BackColor = Color.LightPink;
                    skorKontrol[0] = 1;
                }
            }
            if (y + 1 <= Y && x + 2 <= X)
            {
                if (!SayiKontrol(x + 2, y + 1))
                {
                    OyunPanel.Rows[y + 1].Cells[x + 2].Style.BackColor = Color.LightPink;
                    skorKontrol[1] = 1;
                }
            }
            if (y - 1 >= 0 && x + 2 <= X)
            {
                if (!SayiKontrol(x + 2, y - 1))
                {
                    OyunPanel.Rows[y - 1].Cells[x + 2].Style.BackColor = Color.LightPink;
                    skorKontrol[2] = 1;
                }
            }
            if (y - 1 >= 0 && x - 2 >= 0)
            {
                if (!SayiKontrol(x - 2, y - 1))
                {
                    OyunPanel.Rows[y - 1].Cells[x - 2].Style.BackColor = Color.LightPink;
                    skorKontrol[3] = 1;
                }
            }
            if (y - 2 >= 0 && x + 1 <= X)
            {
                if (!SayiKontrol(x + 1, y - 2))
                {
                    OyunPanel.Rows[y - 2].Cells[x + 1].Style.BackColor = Color.LightPink;
                    skorKontrol[4] = 1;
                }
            }
            if (y - 2 >= 0 && x - 1 >= 0)
            {
                if (!SayiKontrol(x - 1, y - 2))
                {
                    OyunPanel.Rows[y - 2].Cells[x - 1].Style.BackColor = Color.LightPink;
                    skorKontrol[5] = 1;
                }
            }
            if (y + 2 <= Y && x - 1 >= 0)
            {
                if (!SayiKontrol(x - 1, y + 2))
                {
                    OyunPanel.Rows[y + 2].Cells[x - 1].Style.BackColor = Color.LightPink;
                    skorKontrol[6] = 1;
                }
            }
            if (y + 2 <= Y && x + 1 <= X)
            {
                if (!SayiKontrol(x + 1, y + 2))
                {
                    OyunPanel.Rows[y + 2].Cells[x + 1].Style.BackColor = Color.LightPink;
                    skorKontrol[7] = 1;
                }
            }

            //dizi elemanlarının hepsi sıfır ise oyun biter skoru ekrana yazdır...
            for (int i = 0; i < 8; i++)
            {
                if (skorKontrol[i] == 0)
                    sayac++;
            }
            if (sayac >= 8)
            {
                label1.Text +=(Puan - 1).ToString();
                if (Puan - 1 == (X * Y))
                 MessageBox.Show("Tebrikler Kazandınız!!");

                MessageBox.Show("Oyun Bitti..");
                Application.Restart();


            }
        }




        private void OyunPanel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnYeniden_Click(object sender, EventArgs e)
        {

           
            MessageBox.Show("Yeni Oyun Yükleniyor..");
            Application.Restart();

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            X = 6;
            Y = 6;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            X = 5;
            Y = 5;
        }

        private void OyunPanel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            A = OyunPanel.CurrentCellAddress.X;
            B = OyunPanel.CurrentCellAddress.Y;
            if (deger == 0) 
            {
                OyunPanel.Rows[B].Cells[A].Value = Puan;
                Puan++;
                Islem(A, B);
                deger++;
            }
            if (OyunPanel.Rows[B].Cells[A].Style.BackColor == Color.LightPink && !SayiKontrol(A, B))
            {
                OyunPanel.Rows[B].Cells[A].Value = Puan;
                Puan++;
                Islem(A, B);
            }

        }



        private void btnbasla_Click(object sender, EventArgs e)
        {
            DataTable panel = new DataTable();
            OyunPanel.Visible = true;

            if (X == 0)
            {
                MessageBox.Show("Oyun Boyutunu Belirleyiniz");
                Application.Restart();
            }
            else
            {
                for (int i = 0; i < X; i++)
                    panel.Columns.Add();
                for (int j = 0; j < Y; j++)
                    panel.Rows.Add();

                OyunPanel.DataSource = panel;
                foreach (DataGridViewRow row in OyunPanel.Rows)
                {
                    row.Height = 50;
                }
                foreach (DataGridViewColumn col in OyunPanel.Columns)
                {
                    col.Width = 50;
                    OyunPanel.ColumnHeadersVisible = false;
                    OyunPanel.RowHeadersVisible = false;
                    OyunPanel.DefaultCellStyle.SelectionBackColor = OyunPanel.DefaultCellStyle.BackColor;
                }
                OyunPanel.ColumnHeadersVisible = false;
                OyunPanel.RowHeadersVisible = false;
                OyunPanel.AutoSize = true;

                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                radioButton5.Visible = false;
                btnbasla.Visible= false;
                btnYeniden.Visible = true;
                btnYeniden.Enabled = true;
                if (X > 8 && Y > 8)
                    this.WindowState = FormWindowState.Maximized;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            X = 7;
            Y = 7;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            X = 8;
            Y = 8;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            X = 9;
            Y = 9;
        }
    }
}
