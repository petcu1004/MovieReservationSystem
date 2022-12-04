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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();

        }

        bool idcheck = false;

        OleDbConnection conn;
        string connectionString = "Provider=MSDAORA;Password=pigcoo1004;User ID=msm1004"; //oracle 서버 연결

        private void button1_Click(object sender, EventArgs e)
        {
            if (!idcheck)
            {
                MessageBox.Show("ID 중복확인을 해주세요");
                return;

            }
            if (!(textBox2.Text.ToString().Equals("")) && !(textBox4.Text.ToString().Equals("")) && !(textBox5.Text.ToString().Equals("")) && !(textBox3.Text.ToString().Equals("")))
            {
              
                conn = new OleDbConnection(connectionString);

                try
                {

                    string value = textBox2.Text;

                    // SHA256 해시 생성
                    SHA256 hash = new SHA256Managed();
                    byte[] bytes = hash.ComputeHash(Encoding.ASCII.GetBytes(value));

                    // 16진수 형태로 문자열 결합
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in bytes)
                        sb.AppendFormat("{0:x2}", b);

                    // 문자열 출력
                    String password = sb.ToString();



                    conn.Open(); //데이터베이스 연결
                    OleDbCommand cmd = new OleDbCommand();

                    cmd.CommandText = "INSERT INTO member VALUES('" + textBox1.Text + "','" + password + "','" + textBox3.Text + "','" + textBox4.Text + "',DEFAULT,'" + textBox5.Text + "','0')";  //member 테이블


                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    cmd.ExecuteNonQuery(); //쿼리문을 실행하고 영향받는 행의 수를 반환.
                    MessageBox.Show("가입이 완료되었습니다");
                    //updatedb();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); //에러 메세지 
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close(); //데이터베이스 연결 해제
                    }
                }

                Dispose(true);
            }
            else
            {
                MessageBox.Show("위 항목을 모두 기입하시길 바랍니다. ");
            }
           
        }

        private void button2_Click(object sender, EventArgs e) //중복확인 버튼
        {
            conn = new OleDbConnection(connectionString);

            try
            {
                conn.Open(); //데이터베이스 연결
                OleDbCommand cmd = new OleDbCommand();

                cmd.CommandText = "select * from member where member_id= '"+ textBox1.Text + "'";  //member 테이블
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select 회원ID from 회원 결과


                if (!(read.Read()))
                {
                    idcheck = true;
                    MessageBox.Show("사용가능한 ID입니다"); //에러 메세지 
                }
                else
                {
                    MessageBox.Show("중복 ID입니다"); //에러 메세지 
                }
                read.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            idcheck = false;
        }

    }
}

