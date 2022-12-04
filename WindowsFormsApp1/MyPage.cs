using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class MyPage : Form
    {
        String ID;

        OleDbConnection conn;
        string connectionString = "Provider=MSDAORA;Password=pigcoo1004;User ID=msm1004"; //oracle 서버 연결
        public Move_Data_Zero Move_State_Logout;

        public MyPage()
        {
            InitializeComponent();
        }

        public void getid(string id)
        {
            ID = id;
            //MessageBox.Show(ID);
            textBox1.Text = ID;

            conn = new OleDbConnection(connectionString);

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select * from member where member_id ='" + ID + "'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select 회원ID from 회원 결과

                if (!(read.Read()))
                    MessageBox.Show("존재하지 않는 아이디입니다");//왜 이걸 쓰면 되고 안쓰면 안되는걸까..

                //MessageBox.Show(read.GetValue(2).ToString());
                //MessageBox.Show(read.GetValue(3).ToString());
                label9.Text = read.GetValue(0).ToString(); //아이디
                textBox1.Text = read.GetValue(1).ToString(); //비번
                textBox5.Text = read.GetValue(2).ToString(); //이름
                textBox2.Text = read.GetValue(3).ToString(); //전화번호
                label7.Text = read.GetValue(4).ToString(); //등급
                textBox4.Text = read.GetValue(5).ToString(); //카드

                read.Close();
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }
            

        }

        private void button1_Click(object sender, EventArgs e) //수정 버튼
        {

            conn = new OleDbConnection(connectionString);

            try
            {
                conn.Open();

                string value = textBox1.Text;

                // SHA256 해시 생성
                SHA256 hash = new SHA256Managed();
                byte[] bytes = hash.ComputeHash(Encoding.ASCII.GetBytes(value));

                // 16진수 형태로 문자열 결합
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.AppendFormat("{0:x2}", b);

                // 문자열 출력
                String password = sb.ToString();


                OleDbCommand cmd = new OleDbCommand();
                //cmd.CommandText = "select * from member where member_id ='" + ID + "'";
                //MessageBox.Show("Error: " + textBox1.Text);
                cmd.CommandText = "UPDATE member SET member_pw = '"+ password + "', name='"+ textBox5.Text+"',phone='"+ textBox2.Text+"',card='"+textBox4.Text+ "' where member_id = '" + ID + "'";

                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();


                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }
            Dispose(true);
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn = new OleDbConnection(connectionString);

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand();
                //cmd.CommandText = "select * from member where member_id ='" + ID + "'";
                //MessageBox.Show("Error: " + textBox1.Text);
                cmd.CommandText = "DELETE from member where member_id = '" + ID + "'";

                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();


                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }
            MessageBox.Show("회원탈퇴가 완료되었습니다.");
            this.Hide();
            Move_State_Logout();
            
            Dispose(true);
           /* Login frm = new Login();
            frm.ShowDialog();*/
        }
    }
}
