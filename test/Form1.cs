using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("ban co muon thoat ","thong bao hoi",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(result==DialogResult.Yes)
            {
                Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
        string StrCon = @"Data Source=PHAM_TUAN_ANH\SQLEXPRESS;Initial Catalog=QLSP;Integrated Security=True";
        SqlConnection conn = null;
        private void moketnoi()
        {
            if(conn == null)
            {
                conn = new SqlConnection(StrCon);
            }
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

        }
        private void hienthidanhsach()
        {
            moketnoi();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from tblSanPham";
            cmd.Connection= conn;
            SqlDataReader reader = cmd.ExecuteReader();
            lsvdanhsach.Items.Clear();
            while(reader.Read())
            {
                string masp = reader.GetString(0);
                string tensp =reader.GetString(1);
                string kieudang = reader.GetString(2);
                string tinhtrang = reader.GetString(3);
                string slnhap = reader.GetInt32(4).ToString();
                string gianhap = reader.GetFloat(5).ToString();
                string hangsanxuat = reader.GetString(6);

                ListViewItem lvi = new ListViewItem(masp);
                lvi.SubItems.Add(tensp);
                lvi.SubItems.Add(kieudang);
                lvi.SubItems.Add(tinhtrang);
                lvi.SubItems.Add(slnhap);
                lvi.SubItems.Add(gianhap);
                lvi.SubItems.Add(hangsanxuat);

                lsvdanhsach.Items.Add(lvi);
                
            }
            reader.Close();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            grthongtinchitiet.Enabled = false;
            btnsua.Enabled = false;
            btnxoa.Enabled = false;

            hienthidanhsach();
            
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string masptk = txtmasptk.Text.Trim();
            string tensp = txttensptk.Text.Trim();
            if(masptk !=""&& tensp=="")
            {
                timkiemthemmasp(masptk);
            }
            else if (masptk =="" && tensp != "")
            {
                timkiemtheotensp(tensp);
            }
            else if (masptk =="" &&tensp=="")
            {
                MessageBox.Show("khong duoc de trong ma sp");
                txtmasptk.Focus();
            }
        }

        private void timkiemtheotensp(string tenspl)
        {
            moketnoi();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from tblSanPham where MaSpham like '%"+tenspl+"%'";
            cmd.Connection = conn;
            SqlDataReader reader = cmd.ExecuteReader();
            lsvdanhsach.Items.Clear();
            while (reader.Read())
            {
                string masp = reader.GetString(0);
                string tensp = reader.GetString(1);
                string kieudang = reader.GetString(2);
                string tinhtrang = reader.GetString(3);
                string slnhap = reader.GetInt32(4).ToString();
                string gianhap = reader.GetFloat(5).ToString();
                string hangsanxuat = reader.GetString(6);

                ListViewItem lvi = new ListViewItem(masp);
                lvi.SubItems.Add(tensp);
                lvi.SubItems.Add(kieudang);
                lvi.SubItems.Add(tinhtrang);
                lvi.SubItems.Add(slnhap);
                lvi.SubItems.Add(gianhap);
                lvi.SubItems.Add(hangsanxuat);

                lsvdanhsach.Items.Add(lvi);

            }
            reader.Close();
        }

        private void timkiemthemmasp(string masptk)
        {
            moketnoi();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from tblSanPham where MaSpham ='"+masptk+"'";
            cmd.Connection = conn;
            SqlDataReader reader = cmd.ExecuteReader();
            lsvdanhsach.Items.Clear();
            while (reader.Read())
            {
                string masp = reader.GetString(0);
                string tensp = reader.GetString(1);
                string kieudang = reader.GetString(2);
                string tinhtrang = reader.GetString(3);
                string slnhap = reader.GetInt32(4).ToString();
                string gianhap = reader.GetFloat(5).ToString();
                string hangsanxuat = reader.GetString(6);

                ListViewItem lvi = new ListViewItem(masp);
                lvi.SubItems.Add(tensp);
                lvi.SubItems.Add(kieudang);
                lvi.SubItems.Add(tinhtrang);
                lvi.SubItems.Add(slnhap);
                lvi.SubItems.Add(gianhap);
                lvi.SubItems.Add(hangsanxuat);

                lsvdanhsach.Items.Add(lvi);
                
            }
            reader.Close();
        }
        int chucnang = 0;
        

        private void btnthem_Click(object sender, EventArgs e)
        {
            chucnang = 1;

            grthongtinchitiet.Enabled = true;
        }

        private void themsp()
        {
            moketnoi();
            string masp = txtmasp.Text.Trim();
            string tensp = txttensp.Text.Trim();
            string kieudang = txtkieudang.Text.Trim();
            string tinhtrang = txttinhtrang.Text.Trim();
            string slnhap = Convert.ToInt32(txtsoluongnhap.Text.Trim()).ToString();
            string gianhap=Convert.ToDouble(txtgianhap.Text.Trim()).ToString();
            string hangsanxuat = txthangsanxuat.Text.Trim();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into tblSanPham values(N'" + masp + "', N'" + tensp + "',N'" + kieudang + "',N'" + tinhtrang + "','" + slnhap + "','" + gianhap + "',N'" + hangsanxuat + "')";
            cmd.Connection = conn;
            int kq = cmd.ExecuteNonQuery();
            
            if (kq > 0)
            {
                MessageBox.Show("them thanh cong ");
                hienthidanhsach();
                xoadulieu();
            }
        }

        private void xoadulieu()
        {
            txtmasp.Text = "";
            txttensp.Text = "";
            txtkieudang.Text = "";
            txttinhtrang.Text = "";
            txtsoluongnhap.Text = "";
            txtgianhap.Text = "";
            txthangsanxuat.Text = "";
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            chucnang = 2;
            grthongtinchitiet.Enabled = true;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if(chucnang == 1)
            {
                themsp();
            }
            else if (chucnang == 2) 
            {
                suasp();
            }
        }

        private void suasp()
        {
            moketnoi();
            
            string masp = txtmasp.Text.Trim();
            string tensp = txttensp.Text.Trim();
            string kieudang = txtkieudang.Text.Trim();
            string tinhtrang = txttinhtrang.Text.Trim();
            string slnhap = Convert.ToInt32(txtsoluongnhap.Text.Trim()).ToString();
            string gianhap = Convert.ToDouble(txtgianhap.Text.Trim()).ToString();
            string hangsanxuat = txthangsanxuat.Text.Trim();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update tblSanPham set TenSpham ='" + tensp + "',KieuDang='" + kieudang + "',TinhTrang='" + tinhtrang + "',SlNhap='" + slnhap + "',GiaNhap='" + gianhap + "',HangSanXuat='" + hangsanxuat + "', MaSpham ='" + masp + "'where MaSpham = '" + masp + "'";
            cmd.Connection = conn;
            int kq = cmd.ExecuteNonQuery();

            if (kq > 0)
            {
                MessageBox.Show("sua thanh cong");
                hienthidanhsach();
                xoadulieu();
                btnsua.Enabled = false;
                btnxoa.Enabled = false;
            }
        }

        private void lsvdanhsach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvdanhsach.Items.Count == 0) return;
            ListViewItem lvi = lsvdanhsach.SelectedItems[0];
            txtmasp.Text = lvi.SubItems[0].Text.Trim();
            txttensp.Text = lvi.SubItems[1].Text.Trim();
            txtkieudang.Text = lvi.SubItems[2].Text.Trim();
            txttinhtrang.Text = lvi.SubItems[3].Text.Trim();
            txtsoluongnhap.Text = Convert.ToInt32(lvi.SubItems[4].Text).ToString();
            txtgianhap.Text = Convert.ToDouble(lvi.SubItems[5].Text).ToString();
            txthangsanxuat.Text = lvi.SubItems[6].Text.Trim();
            btnsua.Enabled = true;
            btnxoa.Enabled = true;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            
            grthongtinchitiet.Enabled = true;
            xoaTTSP();
        }

        private void xoaTTSP()
        {
            DialogResult result = MessageBox.Show("ban co muon xoa khong ", "thong bao hoi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                moketnoi();

                string masp = txtmasp.Text.Trim();
                
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from tblSanPham where MaSpham ='"+masp+"'";
                cmd.Connection = conn;
                int kq = cmd.ExecuteNonQuery();

                if (kq > 0)
                {
                    MessageBox.Show("xoa thanh cong");
                    hienthidanhsach();
                    xoadulieu();
                    btnsua.Enabled = false;
                    btnxoa.Enabled = false;

                }
            }
        }
    }
}
