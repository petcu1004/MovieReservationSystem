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
    public delegate void Move_Data3(string data1, string data2, string data3, string data4, string data5, string data6, string data7);
    public partial class Reservation_List : Form
    {

        public Move_Data3 setReservationID;
        String ID;
        bool ischeck=false;
        OleDbConnection conn;
        string connectionString;

        public void getid(string id)
        {
            ID = id;
            //MessageBox.Show(ID);
            label4.Text = ID;
            label4.Hide();
            update();
        }

        public Reservation_List()
        {
            InitializeComponent();
            label8.Hide();

        }
        public void update() {

            connectionString = "Provider=OraOLEDB.Oracle;Password=pigcoo1004; User ID=msm1004"; //oracle 서버 연결
            conn = new OleDbConnection(connectionString);

            conn.Open(); //데이터베이스 연결
            try { 
            

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT movie_id 영화번호, To_Char(time,'YYYY-MM-DD HH24:MI') 상영시간, theater_id 상영관번호, row_num 행번호, column_num 위치번호, reserving_date 예매일시, reserved_seat_cost 금액 , reserved_id 예약번호 FROM reserved_seat where member_id='" + label4.Text + "'"; //member 테이블

                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = 8;

                //필드명 받아오는 반복문
                for (int i = 0; i < 8; i++)
                {
                    dataGridView1.Columns[i].Name = read.GetName(i);
                }

                //행 단위로 반복
                while (read.Read())
                {
                    object[] obj1 = new object[8]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 8; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView1.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                }
                read.Close();
                conn.Close();

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
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            label8.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

            



        }



        private void button3_Click(object sender, EventArgs e)
        {
            connectionString = "Provider=OraOLEDB.Oracle;Password=pigcoo1004; User ID=msm1004"; //oracle 서버 연결
            conn = new OleDbConnection(connectionString);

            conn.Open(); //데이터베이스 연결
            try
            {

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "delete from reserved_seat where movie_id='" + textBox1.Text + "'and time='" + textBox2.Text + 
                    "'and theater_id ='" + textBox3.Text + "'and row_num ='" + textBox4.Text + "'and column_num ='" + textBox5.Text + "'"; //member 테이블

                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();


                cmd.CommandText = "delete from theater_seat where time='" + textBox2.Text +
                    "'and theater_id ='" + textBox3.Text + "'and row_num ='" + textBox4.Text + "'and column_num ='" + textBox5.Text + "'"; //member 테이블

                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();


                // conn.Close();

                MessageBox.Show("예매가 취소되었습니다.");

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                conn.Close();
            }


            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }

            update();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(""))
            {
                Reservation frm = new Reservation();
                this.setReservationID += new Move_Data3(frm.getrid);
                this.setReservationID(label8.Text, textBox3.Text, textBox2.Text, textBox1.Text, textBox4.Text, textBox5.Text, ID);
                frm.up = new Move_Data_Zero(this.update);
                
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("예매 내역을 선택해주세요");
            }
            
        }
    }
}
