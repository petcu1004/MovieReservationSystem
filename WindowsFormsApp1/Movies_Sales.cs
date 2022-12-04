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
    public partial class Movies_Sales : Form
    {
        OleDbConnection conn;
        string connectionString;
        public Movies_Sales()
        {
            InitializeComponent();

            connectionString = "Provider=OraOLEDB.Oracle;Password=pigcoo1004; User ID=msm1004"; //oracle 서버 연결
            conn = new OleDbConnection(connectionString);


            try
            {

                conn.Open(); //데이터베이스 연결

                OleDbCommand cmd = new OleDbCommand();
                //cmd.CommandText = "select movie_id 영화번호 ,count(*) 예매횟수 from reserved_seat group by movie_id order by count(*) desc"; //step1의 영화 리스트
                cmd.CommandText = "select movie_id 영화번호, title 제목 from movie order by movie_id asc";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;


                OleDbDataReader read = cmd.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = 2;


                //필드명 받아오는 반복문
                for (int i = 0; i < 2; i++)
                {
                    dataGridView1.Columns[i].Name = read.GetName(i);
                    //dt.Columns[i].ColumnName = read.GetName(i);

                }

                //행 단위로 반복
                while (read.Read())
                {
                    object[] obj1 = new object[2]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 2; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView1.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                                                  //dt.Rows.Add(obj1);

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
            string temp = dataGridView1.Rows[0].Cells[0].Value.ToString(); ;
            if (!temp.Equals("")) 
            {

                label2.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                label3.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();

                int total = 0;
                try
                {

                    conn.Open(); //데이터베이스 연결

                    OleDbCommand cmd = new OleDbCommand();
                    //cmd.CommandText = "select movie_id 영화번호 ,count(*) 예매횟수 from reserved_seat group by movie_id order by count(*) desc"; //step1의 영화 리스트
                    cmd.CommandText = "select reserved_seat_cost from reserved_seat where movie_id= '" + label2.Text + "'";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;


                    OleDbDataReader read = cmd.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                    //행 단위로 반복
                    while (read.Read())
                    {
                        total += Int32.Parse(read.GetValue(0).ToString());

                    }

                    read.Close();

                    label5.Text = total.ToString();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            label2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            label3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            int total=0;
            try
            {

                conn.Open(); //데이터베이스 연결

                OleDbCommand cmd = new OleDbCommand();
                //cmd.CommandText = "select movie_id 영화번호 ,count(*) 예매횟수 from reserved_seat group by movie_id order by count(*) desc"; //step1의 영화 리스트
                cmd.CommandText = "select reserved_seat_cost from reserved_seat where movie_id= '" +label2.Text +"'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;


                OleDbDataReader read = cmd.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                //행 단위로 반복
                while (read.Read())
                {
                    total+= Int32.Parse(read.GetValue(0).ToString());

                }

                read.Close();

                label5.Text = total.ToString();
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
