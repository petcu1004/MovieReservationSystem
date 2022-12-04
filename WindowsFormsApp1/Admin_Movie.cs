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
    public partial class Admin_Movie : Form
    {

        OleDbConnection conn;
        string connectionString;

        public Admin_Movie()
        {
            InitializeComponent();
            connectionString = "Provider=OraOLEDB.Oracle;Password=pigcoo1004; User ID=msm1004"; //oracle 서버 연결
            conn = new OleDbConnection(connectionString);

            try
            {
                conn.Open(); //데이터베이스 연결

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT movie_id, title, director, actor FROM movie WHERE flag='0'"; //member 테이블
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView1.ColumnCount = 4;

                //필드명 받아오는 반복문
                for (int i = 0; i < 4; i++)
                {
                    dataGridView1.Columns[i].Name = read.GetName(i);
                }

                //행 단위로 반복
                while (read.Read())
                {
                    object[] obj1 = new object[4]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 4; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView1.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                }
                read.Close();


                OleDbCommand cmd1 = new OleDbCommand();
                cmd1.CommandText = "select movie_id, title, director, actor from movie where flag='1'"; //member 테이블
                cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd1.Connection = conn;

                OleDbDataReader read1 = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView2.ColumnCount = 4;

                //필드명 받아오는 반복문
                for (int i = 0; i < 4; i++)
                {
                    dataGridView2.Columns[i].Name = read1.GetName(i);
                }

                //행 단위로 반복
                while (read1.Read())
                {
                    object[] obj1 = new object[4]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 4; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read1.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView2.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                }
                read1.Close();


                OleDbCommand cmd2 = new OleDbCommand();
                cmd2.CommandText = "select movie_id, title from movie where flag='1'"; //member 테이블
                cmd2.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd2.Connection = conn;

                OleDbDataReader read2 = cmd2.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView4.ColumnCount = 2;

                //필드명 받아오는 반복문
                for (int i = 0; i < 2; i++)
                {
                    dataGridView4.Columns[i].Name = read2.GetName(i);
                }

                //행 단위로 반복
                while (read2.Read())
                {
                    object[] obj1 = new object[2]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 2; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read2.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView4.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                }
                read2.Close();


                OleDbCommand cmd3 = new OleDbCommand();
                cmd3.CommandText = "select movie_id 영화번호, To_Char(time,'YYYY-MM-DD HH24:MI') 상영시간, theater_id 상영관번호 ,schedule_cost 요금 From schedule"; //member 테이블
                cmd3.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd3.Connection = conn;

                OleDbDataReader read3 = cmd3.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView3.ColumnCount = 4;

                //필드명 받아오는 반복문
                for (int i = 0; i < 4; i++)
                {
                    dataGridView3.Columns[i].Name = read3.GetName(i);
                }

                //행 단위로 반복
                while (read3.Read())
                {
                    object[] obj1 = new object[4]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 4; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read3.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView3.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                }
                read3.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }
            //dataGridView2.Hide();
/*            dataGridView1.Show();
            dataGridView2.Show();*/
            conn.Close();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            Movie_New frm = new Movie_New();
            this.Dispose();
            frm.ShowDialog();
            
        }

        private void button1_Click(object sender, EventArgs e) //검색 버튼
        {

            try
            {
                conn.Open(); //데이터베이스 연결

                OleDbCommand cmd = new OleDbCommand();

                cmd.CommandText = "select movie_id,title, director, actor from movie where flag='0' and lower(title) like '%" + textBox1.Text.ToString().ToLower() + "%'";

                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = 4;

                //필드명 받아오는 반복문
                for (int i = 0; i < 4; i++)
                {
                    dataGridView1.Columns[i].Name = read.GetName(i);

                }

                //행 단위로 반복
                while (read.Read())
                {
                    object[] obj1 = new object[4]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 4; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView1.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                }

                read.Close();


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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open(); //데이터베이스 연결

                OleDbCommand cmd = new OleDbCommand();

                cmd.CommandText = "select movie_id,title, director, actor from movie where flag='1' and lower(title) like '%" + textBox2.Text.ToString().ToLower() + "%'";

                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView2.Rows.Clear();
                dataGridView2.ColumnCount = 4;

                //필드명 받아오는 반복문
                for (int i = 1; i < 4; i++)
                {
                    dataGridView2.Columns[i].Name = read.GetName(i);

                }

                //행 단위로 반복
                while (read.Read())
                {
                    object[] obj1 = new object[4]; // 필드수만큼 오브젝트 배열

                    for (int i = 1; i < 4; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView2.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                }

                read.Close();


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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(); //영화제목
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            

            conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open(); //데이터베이스 연결
                OleDbCommand cmd = new OleDbCommand();

                cmd.CommandText = "UPDATE movie SET flag = '1' where title = '" + textBox3.Text + "'";


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


        private void updatedb()
        {
            conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open(); //데이터베이스 연결

                OleDbCommand cmd1 = new OleDbCommand();

                cmd1.CommandText = "select movie_id,title, director, actor from movie where flag='0' and lower(title) like '%" + textBox1.Text.ToString().ToLower() + "%'";

                cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd1.Connection = conn;

                OleDbDataReader read1 = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = 4;

                //필드명 받아오는 반복문
                for (int i = 0; i < 4; i++)
                {
                    dataGridView1.Columns[i].Name = read1.GetName(i);

                }

                //행 단위로 반복
                while (read1.Read())
                {
                    object[] obj1 = new object[4]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 4; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read1.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView1.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                }

                read1.Close();


                OleDbCommand cmd2 = new OleDbCommand();

                cmd2.CommandText = "select movie_id,title, director, actor from movie where flag='1' and lower(title) like '%" + textBox2.Text.ToString().ToLower() + "%'";

                cmd2.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd2.Connection = conn;

                OleDbDataReader read2 = cmd2.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView2.Rows.Clear();
                dataGridView2.ColumnCount = 4;

                //필드명 받아오는 반복문
                for (int i = 0; i < 4; i++)
                {
                    dataGridView2.Columns[i].Name = read2.GetName(i);

                }

                //행 단위로 반복
                while (read2.Read())
                {
                    object[] obj1 = new object[4]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 4; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read2.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView2.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                }

                read2.Close();










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

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString(); //영화제목

            conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open(); //데이터베이스 연결

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select movie_id 영화번호, To_Char(time,'YYYY-MM-DD HH24:MI') 상영시간, theater_id 상영관번호 , schedule_cost 요금 from schedule where movie_id = '" + textBox4.Text+"'"; //member 테이블
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select member_id, name from member where flag ='0' 결과
                dataGridView5.Rows.Clear();
                dataGridView5.ColumnCount = 4;

                //필드명 받아오는 반복문
                for (int i = 0; i < 4; i++)
                {
                    dataGridView5.Columns[i].Name = read.GetName(i);
                }

                //행 단위로 반복
                while (read.Read())
                {
                    object[] obj1 = new object[4]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 4; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView5.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                }
                read.Close();

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

        private void button3_Click(object sender, EventArgs e) //등록 버튼
        {
            if(!textBox4.Text.Equals("")&& !textBox6.Text.Equals(""))
            {
                DateTime dt = dateTimePicker2.Value;
                string str = string.Format("{0}-{1}-{2} {3}:{4}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
             


                conn = new OleDbConnection(connectionString);
                conn.Open(); //데이터베이스 연결
                try
                {

                    OleDbCommand cmd = new OleDbCommand();



                    cmd.CommandText = "INSERT INTO schedule VALUES('" + textBox4.Text + "','" + str + "','" + comboBox2.Text + "','" + textBox6.Text + "')";
                    

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery(); //쿼리문을 실행하고 영향받는 행의 수를 반환.
                    cmd.Parameters.Clear();

                    textBox4.Text = "";
                    comboBox2.Text = "";
                    textBox6.Text = "";

                    dataGridView5.Rows.Clear();
                    OleDbCommand cmd3 = new OleDbCommand();
                    cmd3.CommandText = "select movie_id 영화번호, To_Char(time,'YYYY-MM-DD HH24:MI') 상영시간, theater_id 상영관번호 ,schedule_cost 요금 From schedule"; //member 테이블
                    cmd3.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd3.Connection = conn;

                    OleDbDataReader read3 = cmd3.ExecuteReader(); //select member_id, name from member where flag ='0' 결과
                    dataGridView3.Rows.Clear();
                    dataGridView3.ColumnCount = 4;

                    //필드명 받아오는 반복문
                    for (int i = 0; i < 4; i++)
                    {
                        dataGridView3.Columns[i].Name = read3.GetName(i);
                    }

                    //행 단위로 반복
                    while (read3.Read())
                    {
                        object[] obj1 = new object[4]; // 필드수만큼 오브젝트 배열

                        for (int i = 0; i < 4; i++) // 필드 수만큼 반복
                        {
                            obj1[i] = new object();
                            obj1[i] = read3.GetValue(i); // 오브젝트배열에 데이터 저장
                        }

                        dataGridView3.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                    }
                    read3.Close();




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
            else
            {
                MessageBox.Show("위 항목을 전부 기입해 주세요.");
            }


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.Compare(comboBox2.Text, "1관", true) == 0)
            {
                textBox6.Text = "8000";
            }
            else if (string.Compare(comboBox2.Text, "2관", true) == 0)
            {
                textBox6.Text = "10000";
            }
            else if (string.Compare(comboBox2.Text, "3관", true) == 0)
            {
                textBox6.Text = "12000";
            }
        }
    }
}
