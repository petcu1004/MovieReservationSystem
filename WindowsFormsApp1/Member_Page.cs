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

    public delegate void Move_Data(string data);
    public delegate void Move_Data_Zero();

    public partial class Member_Page : Form
    {
        public Move_Data_Zero Move_State_Logout;
        public Move_Data setMemberID;
        string ID;
        public Member_Page()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reservation frm = new Reservation();
            this.setMemberID += new Move_Data(frm.getid);
            setMemberID(label2.Text);
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyPage frm = new MyPage();
            this.setMemberID += new Move_Data(frm.getid);
            frm.Move_State_Logout += new Move_Data_Zero(this.PuchLogoutState);
            setMemberID(label2.Text);
            frm.ShowDialog();
        }


        public void PuchLogoutState()
        {
            this.Hide();
            Login frm = new Login();
            frm.ShowDialog();
            
        }
        public void getID(string id)
        {
            ID = id;
            /*MessageBox.Show(ID);*/
            label2.Text = ID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reservation_List frm = new Reservation_List();
            this.setMemberID += new Move_Data(frm.getid);
            setMemberID(label2.Text);
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Popular_Movie_Rank frm = new Popular_Movie_Rank();
            frm.ShowDialog();
        }
    }
}
