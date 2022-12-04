using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Admin_Page : Form
    {
        public Admin_Page()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e) //회원 관리
        {
            Admin_Member frm = new Admin_Member();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) //영화 관리
        {
            Admin_Movie frm = new Admin_Movie();  
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Movies_Sales frm = new Movies_Sales();
            frm.ShowDialog();
        }
    }
}
