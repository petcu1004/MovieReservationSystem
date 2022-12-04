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

namespace WindowsFormsApp1
{
    public partial class Admin_Member : Form
    {

        OleDbConnection conn;
        string connectionString;
        

        public Admin_Member()
        {
            InitializeComponent();
            dataGridView1.Rows.Clear();
            connectionString = "Provider=MSDAORA;Password=pigcoo1004; User ID=msm1004"; //oracle 서버 연결

            //연결 스트링에 대한 정보 
            //Oracle - MSDAORA 
            //dataGridView1.Rows.Clear();
            conn = new OleDbConnection(connectionString);
            conn.Open(); //데이터베이스 연결
            updatedb();
        }

        private void Admin_Member_Load(object sender, EventArgs e)
        {


        }

        private void tabPage1_Click(object sender, EventArgs e)
        {


        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }



        private void updatedb()
        {

            try
            {
                //conn.Open(); //데이터베이스 연결
                OleDbCommand cmd1 = new OleDbCommand();
                cmd1.CommandText = "select member_id, name, phone from member where flag ='0' and  member_id!='admin'"; //member 테이블
                cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd1.Connection = conn;

                OleDbDataReader read1 = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView1.ColumnCount = 3;

                //필드명 받아오는 반복문
                for (int i = 0; i < 3; i++)
                {
                    dataGridView1.Columns[i].Name = read1.GetName(i);
                    
                }

                //행 단위로 반복
                while (read1.Read())
                {
                    object[] obj1 = new object[3]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 3; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read1.GetValue(i); // 오브젝트배열에 데이터 저장
                    }
                    
                    dataGridView1.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                }

                read1.Close();

                


                OleDbCommand cmd2 = new OleDbCommand();
                cmd2.CommandText = "select member_id, name, phone from member where flag ='1' and  member_id!='admin'"; //member 테이블
                cmd2.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd2.Connection = conn;

                OleDbDataReader read2 = cmd2.ExecuteReader(); //select member_id, name from member where flag ='1' 결과

                dataGridView2.ColumnCount = 3;

                for (int i = 0; i < 3; i++)
                {
                    dataGridView2.Columns[i].Name = read2.GetName(i);
                }
                dataGridView2.Rows.Clear();
                //행 단위로 반복
                while (read2.Read())
                {
                    
                    object[] obj2 = new object[3]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 3; i++) // 필드 수만큼 반복
                    {
                        obj2[i] = new object();
                        obj2[i] = read2.GetValue(i); // 오브젝트배열에 데이터 저장
                    }
                    
                    dataGridView2.Rows.Add(obj2); //데이터그리드뷰에 오브젝트 배열 추가
                }

                read2.Close();


                OleDbCommand cmd3 = new OleDbCommand();
                cmd3.CommandText = "select member_id, name, phone, grade, card from member where flag ='1' and  member_id!='admin'"; //member 테이블
                cmd3.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd3.Connection = conn;

                OleDbDataReader read3 = cmd3.ExecuteReader(); //select member_id, name from member where flag ='1' 결과

                dataGridView3.ColumnCount = 5;

                for (int i = 0; i < 5; i++)
                {
                    dataGridView3.Columns[i].Name = read3.GetName(i);
                }
                dataGridView3.Rows.Clear();
                //행 단위로 반복
                while (read3.Read())
                {

                    object[] obj3 = new object[5]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 5; i++) // 필드 수만큼 반복
                    {
                        obj3[i] = new object();
                        obj3[i] = read3.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView3.Rows.Add(obj3); //데이터그리드뷰에 오브젝트 배열 추가
                }

                read3.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e) //승인 버튼 누름
        {

            dataGridView1.Rows.Clear();

            conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open(); //데이터베이스 연결
                OleDbCommand cmd = new OleDbCommand();
                
                cmd.CommandText = "UPDATE member SET flag = '1' where member_id = '" + textBox1.Text + "'" ;


                //textBox1.Text = cmd.CommandText;
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                cmd.ExecuteNonQuery(); //쿼리문을 실행하고 영향받는 행의 수를 반환.
                updatedb();


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




        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox6.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox7.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox8.Text = dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();

            conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open(); //데이터베이스 연결
                OleDbCommand cmd = new OleDbCommand();

                cmd.CommandText = "UPDATE member SET grade = '"+comboBox1.Text+"' where member_id = '" + textBox5.Text + "'";


                //textBox1.Text = cmd.CommandText;
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                cmd.ExecuteNonQuery(); //쿼리문을 실행하고 영향받는 행의 수를 반환.
                updatedb();


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
        }
    }
}
