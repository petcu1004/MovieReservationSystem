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
    public partial class Popular_Movie_Rank : Form
    {
        OleDbConnection conn;
        string connectionString;

        Image image = null;
        Image thumnail_img = null;


        public Popular_Movie_Rank()
        {
            InitializeComponent();
            connectionString = "Provider=OraOLEDB.Oracle;Password=pigcoo1004; User ID=msm1004"; //oracle 서버 연결
            conn = new OleDbConnection(connectionString);

            int count = 0;
            try
            {

                conn.Open(); //데이터베이스 연결

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select movie_id 영화번호 ,count(*) 예매횟수 from reserved_seat group by movie_id order by count(*) desc"; //step1의 영화 리스트
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;


                OleDbDataReader read = cmd.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = 2;

                DataTable dt = new DataTable("test");


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
                    count++;
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


            label11.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
            label8.Text = dataGridView1.Rows[0].Cells[1].Value.ToString(); //예매 숫자

            label12.Text = dataGridView1.Rows[1].Cells[0].Value.ToString();
            label9.Text = dataGridView1.Rows[1].Cells[1].Value.ToString();

            label13.Text = dataGridView1.Rows[2].Cells[0].Value.ToString();
            label10.Text = dataGridView1.Rows[2].Cells[1].Value.ToString();
            update();

        }


        private Image ByteArrayToImage(byte[] bytes)
        {
            ImageConverter imageConverter = new ImageConverter();
            Image img = (Image)imageConverter.ConvertFrom(bytes);
            return img;
        }

        public bool ThumbnailCallback()
        {
            return true;
        }


        private void update()
        {
            pictureBox1.Image = null;

            conn.Open();
            OleDbCommand cmd1 = new OleDbCommand();
            cmd1.CommandText = "select title from movie where movie_id= '" + label11.Text + "'"; //step1의 영화 리스트
            cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
            cmd1.Connection = conn;

            OleDbDataReader read1 = cmd1.ExecuteReader();

            while (read1.Read())
            {
                label5.Text = read1.GetValue(0).ToString();

            }
            read1.Close();
            //cmd1.ExecuteNonQuery();

            OleDbCommand cmd2 = new OleDbCommand();
            cmd2.CommandText = "select title from movie where movie_id= '" + label12.Text + "'"; //step1의 영화 리스트
            cmd2.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
            cmd2.Connection = conn;

            OleDbDataReader read2 = cmd2.ExecuteReader();

            while (read2.Read())
            {
                label6.Text = read2.GetValue(0).ToString();

            }
            read2.Close();

            OleDbCommand cmd3 = new OleDbCommand();

            cmd3.CommandText = "select title from movie where movie_id= '" + label13.Text + "'"; //step1의 영화 리스트
            cmd3.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
            cmd3.Connection = conn;

            OleDbDataReader read3 = cmd3.ExecuteReader();

            while (read3.Read())
            {
                label7.Text = read3.GetValue(0).ToString();

            }
            read3.Close();
           



            OleDbCommand cmd4 = new OleDbCommand();
            cmd4.CommandText = "select poster from movie where  movie_id = '" + label11.Text + "'";
            cmd4.CommandType = CommandType.Text;
            cmd4.Connection = conn;

            OleDbDataReader read4 = cmd4.ExecuteReader(); //select  결과

            if (read4.Read())
            {

                if (read4.GetValue(0).ToString() != "")  //이미지가 없으면
                {
                    image = ByteArrayToImage((byte[])read4.GetValue(0));
                    Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                    thumnail_img = image.GetThumbnailImage(400, 400, callback, new IntPtr()); //썸네일 만들기
                    pictureBox1.Image = thumnail_img;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            
            read4.Close();

            OleDbCommand cmd5 = new OleDbCommand();
            cmd5.CommandText = "select poster from movie where  movie_id = '" + label12.Text + "'";
            cmd5.CommandType = CommandType.Text;
            cmd5.Connection = conn;

            OleDbDataReader read5 = cmd5.ExecuteReader(); //select  결과

            if (read5.Read())
            {

                if (read5.GetValue(0).ToString() != "")  //이미지가 없으면
                {
                    image = ByteArrayToImage((byte[])read5.GetValue(0));
                    Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                    thumnail_img = image.GetThumbnailImage(400, 400, callback, new IntPtr()); //썸네일 만들기
                    pictureBox2.Image = thumnail_img;
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }

            read5.Close();

            OleDbCommand cmd6 = new OleDbCommand();
            cmd6.CommandText = "select poster from movie where  movie_id = '" + label13.Text + "'";
            cmd6.CommandType = CommandType.Text;
            cmd6.Connection = conn;

            OleDbDataReader read6 = cmd6.ExecuteReader(); //select  결과

            if (read6.Read())
            {

                if (read6.GetValue(0).ToString() != "")  //이미지가 없으면
                {
                    image = ByteArrayToImage((byte[])read6.GetValue(0));
                    Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                    thumnail_img = image.GetThumbnailImage(400, 400, callback, new IntPtr()); //썸네일 만들기
                    pictureBox3.Image = thumnail_img;
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }

            read6.Close();




            conn.Close();

        }

    }
}
