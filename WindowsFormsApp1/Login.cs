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
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public delegate void Move_Data_One(string data);
    public partial class Login : Form
    {
        public Move_Data_One setMemberID;
        public Login()
        {
            InitializeComponent();
        }
        OleDbConnection conn;
        string connectionString = "Provider=MSDAORA;Password=pigcoo1004;User ID=msm1004"; //oracle 서버 연결


        private void register_btn_click(object sender, EventArgs e)
        {
            //MessageBox.Show("회원가입 is Clicked!");
            Register frm = new Register();
            frm.ShowDialog();


        }

        private void login_btn_click(object sender, EventArgs e)
        {
            //MessageBox.Show("로그인 is Clicked!");
            conn = new OleDbConnection(connectionString);

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select * from member where member_id ='" + textBox1.Text +"' and flag='1'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select 회원ID from 회원 결과


                if (!(read.Read()))
                    label_Error.Text = "존재하지 않는 아이디입니다";
                
                else //보안
                {
                    string input_value = textBox2.Text;

                    // SHA256 해시 생성
                    SHA256 hash1 = new SHA256Managed();
                    byte[] bytes1 = hash1.ComputeHash(Encoding.ASCII.GetBytes(input_value));

                    // 16진수 형태로 문자열 결합
                    StringBuilder sb1 = new StringBuilder();
                    foreach (byte b1 in bytes1)
                        sb1.AppendFormat("{0:x2}", b1);

                    // 입력값의 해시결과
                
                    String hash_value = sb1.ToString();

                    if (read.GetValue(1).ToString() != hash_value)
                    {
                        label_Error.Text = "비밀번호가 일치하지 않습니다";
                    }
                    else
                    {
                        label_Error.Text = read.GetValue(2).ToString() + "님 환영합니다";
                        if (textBox1.Text == "admin")
                        {
                            MessageBox.Show("관리자 계정을 들어옴!");
                            this.Visible = false;
                            Admin_Page frm1 = new Admin_Page();
                            frm1.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(textBox1.Text + " 님 계정을 들어옴!");
                            this.Visible = false;

                            Member_Page frm1 = new Member_Page();
                            this.setMemberID += new Move_Data_One(frm1.getID);
                            setMemberID(textBox1.Text);
                            frm1.ShowDialog();
                        }
                    }
                }
                
                read.Close();
                conn.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }


        }

    }
}
