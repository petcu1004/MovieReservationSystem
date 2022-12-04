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

    public partial class Reservation : Form
    {
        OleDbConnection conn;
        string connectionString;

        Image image = null;
        Image thumnail_img = null;
        int member_cnt=1;
        int select_cnt;
        bool s1 = false, s2 = false, s3 = false, s4 = false, s5 = false,rl=false;
        public Move_Data_Zero up;
        string r;
        string c ;

        public Reservation()
        {
            InitializeComponent();
            connectionString = "Provider=OraOLEDB.Oracle;Password=pigcoo1004; User ID=msm1004"; //oracle 서버 연결
            conn = new OleDbConnection(connectionString);

            comboBox1.Text = "1";


            label29.Dispose();
            label53.Dispose();
            


            //패널 숨기기
            panel2.Hide();//Step2
            panel4.Hide();//Step3
            panel8.Hide();//Step4
            panel9.Hide();//Step5


            //다음 버튼 숨기기
            button7.Hide(); //Step2
            button8.Hide(); //Step3
            button11.Hide();//Step4
            button12.Hide();//Step5

            //이전버튼 숨기기
            button13.Hide(); //Step2 -> Step1
            button14.Hide();//Step3 -> Step2
            button15.Hide();//Step4 -> Step3
            button16.Hide();//Step5 -> Step4

            label55.Hide();


            try
            {
                conn.Open(); //데이터베이스 연결

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select movie_id, title, director, actor from movie"; //step1의 영화 리스트
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }
            //dataGridView2.Hide();
            dataGridView1.Show();

            panel2.Hide();


        }


        private void button3_Click(object sender, EventArgs e) //검색 버튼
        {
            try
            {
                conn.Open(); //데이터베이스 연결

                OleDbCommand cmd = new OleDbCommand();

                cmd.CommandText = "select movie_id,title, director, actor from movie where lower(title) like '%" + textBox1.Text.ToString().ToLower() + "%'"; //step1의 영화 리스트

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



        private void button1_Click(object sender, EventArgs e) //다음 버튼 (Step1 -> Step2)
        {
            if (s1)
            {
                //버튼을 누르면 다음 그리드로 넘어가도록 show랑 hide 사용

                //step 화면 가림
                dataGridView1.Hide();
                panel1.Hide();
                label11.Hide();
                textBox1.Hide();
                button3.Hide();
                button1.Hide(); //step1->step2 다음 버튼 가림
                button2.Hide();

                panel2.Show();
                button7.Show(); //step2->step3 다음 버튼 보여줌(step2버튼)
                button13.Show();


                updatedb();
            }
            else
            {
                MessageBox.Show("영화를 선택해주세요"); //에러 메세지 
            }
     
            
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


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            conn = new OleDbConnection(connectionString);
            pictureBox1.Image = null;


            //클릭하면 해당 영화 폼 나오게끔하기
            label16.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); //영화번호
            label13.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(); //영화제목 title
            label14.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(); //감독
            label15.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(); //주연 배우

            try
            {
                conn.Open(); //데이터베이스 연결
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select poster from movie where flag='1' and  title = '" + label13.Text + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select  결과

                if (read.Read())
                {

                    if (read.GetValue(0).ToString() != "")  //이미지가 없으면
                    {
                        image = ByteArrayToImage((byte[])read.GetValue(0));
                        Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                        thumnail_img = image.GetThumbnailImage(400, 400, callback, new IntPtr()); //썸네일 만들기
                        pictureBox1.Image = thumnail_img;
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                //cmd.ExecuteNonQuery();
                read.Close();


                OleDbCommand cmd1 = new OleDbCommand();
                cmd1.CommandText = "select flag from movie where title = '" + label13.Text + "'";
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = conn;
                //cmd1.ExecuteNonQuery();


                OleDbDataReader read1 = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                //행 단위로 반복
                while (read1.Read())
                {
                    label55.Text = read1.GetValue(0).ToString();

                }
                if (string.Compare(label55.Text, "0", true) == 0)
                    MessageBox.Show("상영 예정작입니다. 예약하실 수 없습니다.");
                else
                    s1 = true;

                read1.Close();


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

        private void button4_Click(object sender, EventArgs e) //1관
        {
            label20.Text = button4.Text;
            updatedb();
            //dataGridView2.Rows.Clear();
        }

        private void button5_Click(object sender, EventArgs e) //2관
        {
            label20.Text = button5.Text;
            updatedb();
            //dataGridView2.Rows.Clear();
        }

        private void button6_Click(object sender, EventArgs e) //3관
        {
            label20.Text = button6.Text; //3관이 쓰여짐
            updatedb();
            //dataGridView2.Rows.Clear();
        }

        private void updatedb() //상영날짜와 시간 선택
        {

            conn = new OleDbConnection(connectionString);
            conn.Open(); //데이터베이스 연결
            try
            {
                //conn.Open(); //데이터베이스 연결
                OleDbCommand cmd1 = new OleDbCommand();
                cmd1.CommandText = "select To_Char(time,'YYYY-MM-DD HH24:MI') time From schedule where movie_id='" + label16.Text+ "'and theater_id='" + label20.Text+"'"; //member 테이블
                cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd1.Connection = conn;

                OleDbDataReader read1 = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                dataGridView2.ColumnCount = 1;
                dataGridView2.Rows.Clear();

                //필드명 받아오는 반복문
                for (int i = 0; i < 1; i++)
                {
                    dataGridView2.Columns[i].Name = read1.GetName(i);

                }

                //행 단위로 반복
                while (read1.Read())
                {
                    object[] obj1 = new object[1]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 1; i++) // 필드 수만큼 반복
                    {
                        obj1[i] = new object();
                        obj1[i] = read1.GetValue(i); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView2.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                }
                

                read1.Close();
                //cmd1.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            label21.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (!label21.Equals("날짜"))
                s2 = true;
        }

        private void button7_Click(object sender, EventArgs e) //Step2-> Step3
        {
            //예약된 좌석 표시하는 중 --------------------------------------------------------------------------

            conn = new OleDbConnection(connectionString);
            conn.Open(); //데이터베이스 연결

            string a = "";
            string b = "";

          
            string[,] arr = new string[60,2];
            int reserv_i = 0;
            try
            {
                //conn.Open(); //데이터베이스 연결
                OleDbCommand cmd1 = new OleDbCommand();
                cmd1.CommandText = "select row_num, column_num from reserved_seat where theater_id='" + label20.Text + "' and time ='" + label21.Text + "'"; //member 테이블
                cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd1.Connection = conn;
                OleDbDataReader read = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                
                while (read.Read())
                {
                    a = read.GetValue(0).ToString();
                    b = read.GetValue(1).ToString();
                    //MessageBox.Show(a + b);

                    arr[reserv_i, 0] = a;
                    arr[reserv_i, 1] = b;
                    char t1= a.ToCharArray()[0];
                    int tt1 = (int)t1 - 65;
                    char t2 = b.ToCharArray()[0];
                    int tt2 = (int)t2 - 49;
                    // MessageBox.Show(tt1.ToString()+ tt2.ToString());

                    reserv_i++;
                }
               
           
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }




            if (s2)
            {
                panel2.Hide(); //Step2 화면 숨기기
                button7.Hide(); //Step2->step3 버튼 숨기기 (step2 버튼)
                button13.Hide();
                panel4.Show(); //Step3 화면 보여주기
                button8.Show();//Step3->step4 버튼 보여주기 (step3 버튼)
                button14.Show();

                panel5.Hide(); //1관 좌석모형
                panel6.Hide(); //2관 좌석모형
                panel7.Hide(); //3관 좌석모형

                if (label20.Text == button4.Text) //1관을 선택했을 경우
                {
                    panel5.Show();
                    panel6.Hide();

                    //해당하는 panel을 보여주기
                    conn = new OleDbConnection(connectionString);
                    conn.Open(); //데이터베이스 연결
                    try
                    {




                        //conn.Open(); //데이터베이스 연결
                        OleDbCommand cmd1 = new OleDbCommand();
                        cmd1.CommandText = "select seat1 s1, seat2 s2, seat3 s3 from seat"; //member 테이블
                        cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd1.Connection = conn;

                        OleDbDataReader read1 = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                        dataGridView3.ColumnCount = 3;
                        //dataGridView3.Rows.Clear();

                        //필드명 받아오는 반복문
                        for (int i = 0; i < 3; i++)
                        {
                            dataGridView3.Columns[i].Name = read1.GetName(i);

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

                            dataGridView3.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                        }
                        read1.Close();
                        //cmd1.ExecuteNonQuery();



                        OleDbCommand cmd2 = new OleDbCommand();
                        cmd2.CommandText = "select seat4 s4, seat5 s5, seat6 s6 from seat"; //member 테이블
                        cmd2.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd2.Connection = conn;

                        OleDbDataReader read2 = cmd2.ExecuteReader(); //select member_id, name from member where flag ='1' 결과

                        dataGridView4.ColumnCount = 3;

                        for (int i = 0; i < 3; i++)
                        {
                            dataGridView4.Columns[i].Name = read2.GetName(i);
                        }
                        dataGridView4.Rows.Clear();
                        //행 단위로 반복
                        while (read2.Read())
                        {

                            object[] obj2 = new object[3]; // 필드수만큼 오브젝트 배열

                            for (int i = 0; i < 3; i++) // 필드 수만큼 반복
                            {
                                obj2[i] = new object();
                                obj2[i] = read2.GetValue(i); // 오브젝트배열에 데이터 저장
                            }

                            dataGridView4.Rows.Add(obj2); //데이터그리드뷰에 오브젝트 배열 추가
                        }

                        read2.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message); //에러 메세지 
                    }



                }

                else if (label20.Text == button5.Text) //2관을 선택했을 경우
                {
                    panel5.Hide();
                    panel6.Show();
                    panel7.Hide();

                    conn = new OleDbConnection(connectionString);
                    conn.Open(); //데이터베이스 연결
                    try
                    {
                        //conn.Open(); //데이터베이스 연결
                        OleDbCommand cmd1 = new OleDbCommand();
                        cmd1.CommandText = "select seat1 s1, seat2 s2 from seat"; //member 테이블
                        cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd1.Connection = conn;

                        OleDbDataReader read1 = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                        dataGridView5.ColumnCount = 2;
                        //dataGridView3.Rows.Clear();

                        //필드명 받아오는 반복문
                        for (int i = 0; i < 2; i++)
                        {
                            dataGridView5.Columns[i].Name = read1.GetName(i);

                        }

                        //행 단위로 반복
                        while (read1.Read())
                        {
                            object[] obj1 = new object[2]; // 필드수만큼 오브젝트 배열

                            for (int i = 0; i < 2; i++) // 필드 수만큼 반복
                            {
                                obj1[i] = new object();
                                obj1[i] = read1.GetValue(i); // 오브젝트배열에 데이터 저장
                            }

                            dataGridView5.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                        }
                        read1.Close();
                        //cmd1.ExecuteNonQuery();


                        OleDbCommand cmd2 = new OleDbCommand();
                        cmd2.CommandText = "select seat3 s3, seat4 s4 from seat"; //member 테이블
                        cmd2.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd2.Connection = conn;

                        OleDbDataReader read2 = cmd2.ExecuteReader(); //select member_id, name from member where flag ='1' 결과

                        dataGridView6.ColumnCount = 2;

                        for (int i = 0; i < 2; i++)
                        {
                            dataGridView6.Columns[i].Name = read2.GetName(i);
                        }
                        dataGridView6.Rows.Clear();
                        //행 단위로 반복
                        while (read2.Read())
                        {

                            object[] obj2 = new object[2]; // 필드수만큼 오브젝트 배열

                            for (int i = 0; i < 2; i++) // 필드 수만큼 반복
                            {
                                obj2[i] = new object();
                                obj2[i] = read2.GetValue(i); // 오브젝트배열에 데이터 저장
                            }

                            dataGridView6.Rows.Add(obj2); //데이터그리드뷰에 오브젝트 배열 추가
                        }

                        read2.Close();


                        OleDbCommand cmd3 = new OleDbCommand();
                        cmd3.CommandText = "select seat5 s5, seat6 s6 from seat"; //member 테이블
                        cmd3.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd3.Connection = conn;

                        OleDbDataReader read3 = cmd3.ExecuteReader(); //select member_id, name from member where flag ='1' 결과

                        dataGridView7.ColumnCount = 2;

                        for (int i = 0; i < 2; i++)
                        {
                            dataGridView7.Columns[i].Name = read3.GetName(i);
                        }
                        dataGridView7.Rows.Clear();
                        //행 단위로 반복
                        while (read3.Read())
                        {

                            object[] obj3 = new object[2]; // 필드수만큼 오브젝트 배열

                            for (int i = 0; i < 2; i++) // 필드 수만큼 반복
                            {
                                obj3[i] = new object();
                                obj3[i] = read3.GetValue(i); // 오브젝트배열에 데이터 저장
                            }

                            dataGridView7.Rows.Add(obj3); //데이터그리드뷰에 오브젝트 배열 추가
                        }

                        read3.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message); //에러 메세지 
                    }


                }

                else if (label20.Text == button6.Text) //3관을 선택했을 경우
                {
                    panel5.Hide();
                    panel6.Hide();
                    panel7.Show();

                    conn = new OleDbConnection(connectionString);
                    conn.Open(); //데이터베이스 연결
                    try
                    {
                        //conn.Open(); //데이터베이스 연결
                        OleDbCommand cmd1 = new OleDbCommand();
                        cmd1.CommandText = "select seat1 s1, seat2 s2, seat3 s3 from seat_"; //member 테이블
                        cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd1.Connection = conn;

                        OleDbDataReader read1 = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                        dataGridView8.ColumnCount = 3;
                        dataGridView8.Rows.Clear();
                        
                        //필드명 받아오는 반복문
                        for (int i = 0; i < 3; i++)
                        {
                            dataGridView8.Columns[i].Name = read1.GetName(i);

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

                            dataGridView8.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                        }
                        read1.Close();
                        //cmd1.ExecuteNonQuery();


                        OleDbCommand cmd2 = new OleDbCommand();
                        cmd2.CommandText = "select seat4 s4, seat5 s5, seat6 s6 from seat_"; //member 테이블
                        cmd2.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd2.Connection = conn;

                        OleDbDataReader read2 = cmd2.ExecuteReader(); //select member_id, name from member where flag ='1' 결과

                        dataGridView9.ColumnCount = 3;

                        for (int i = 0; i < 3; i++)
                        {
                            dataGridView9.Columns[i].Name = read2.GetName(i);
                        }
                        dataGridView9.Rows.Clear();
                        //행 단위로 반복
                        while (read2.Read())
                        {

                            object[] obj2 = new object[3]; // 필드수만큼 오브젝트 배열

                            for (int i = 0; i < 3; i++) // 필드 수만큼 반복
                            {
                                obj2[i] = new object();
                                obj2[i] = read2.GetValue(i); // 오브젝트배열에 데이터 저장
                            }

                            dataGridView9.Rows.Add(obj2); //데이터그리드뷰에 오브젝트 배열 추가
                        }



                        read2.Close();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message); //에러 메세지 
                    }




                }

                dataGridView3.CurrentCell = null;
                dataGridView4.CurrentCell = null;
                dataGridView5.CurrentCell = null;
                dataGridView6.CurrentCell = null;
                dataGridView7.CurrentCell = null;
                dataGridView8.CurrentCell = null;
                dataGridView9.CurrentCell = null;

                for (int j = 0; j < reserv_i; j++)
                {

                    char t1 = arr[j, 0].ToCharArray()[0];
                    int tt1 = (int)t1 - 65;
                    char t2 = arr[j, 1].ToCharArray()[0];
                    int tt2 = (int)t2 - 49;

                    if (label20.Text == button4.Text)
                    {
                        if (tt2 > 2)
                        {
                            dataGridView4.Rows[tt1].Cells[tt2 - 3].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }
                        else
                        {
                            dataGridView3.Rows[tt1].Cells[tt2].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }

                    }
                    else if (label20.Text == button5.Text)
                    {
                        if (tt2 > 3)
                        {
                            dataGridView7.Rows[tt1].Cells[tt2 - 4].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }
                        else if(tt2 > 1)
                        {
                            dataGridView6.Rows[tt1].Cells[tt2 - 2].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }
                        else
                        {
                            dataGridView5.Rows[tt1].Cells[tt2].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }
                    }
                    else if (label20.Text == button6.Text)
                    {
                        if (tt2 > 2)
                        {
                            dataGridView9.Rows[tt1].Cells[tt2 - 3].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }
                        else
                        {
                            dataGridView8.Rows[tt1].Cells[tt2].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }
                    }
                }


                //dataGridView6.Rows[0].Cells[0].Style.BackColor = Color.FromArgb(240, 255, 240);
                //button2.Hide();
            }
            else
            {
                MessageBox.Show("상영관과 시간을 선택해 주세요."); //에러 메세지 
            }
        }

        
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e) //1관 A
        {
            string text;
            string row;
            string column;

            char sp = ' ';
            bool t = dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor.ToString().Equals("Color [Empty]");
            if ((select_cnt < member_cnt) && t)
            { 
                string seat;
                seat = dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                
                if (!label23.Text.Contains(seat))
                {
                    label23.Text = label23.Text + Environment.NewLine + seat;
                    select_cnt++;
                }
                else
                {
                    MessageBox.Show("선택하신 좌석입니다. 다른 좌석을 선택하십시오");
                }
            }
            else if (!t)
            {
                MessageBox.Show("이미 예약된 자리 입니다.");
            }
            else
            {
                MessageBox.Show("해당 인원만큼 모두 선택하셨습니다.");
            }
         

        }

        private void button9_Click(object sender, EventArgs e) //선택버튼
        {
            label23.Text = "";
            member_cnt = int.Parse(comboBox1.Text);
            select_cnt = 0;
            MessageBox.Show(member_cnt+"명의 좌석을 선택하시오");

        }

            private void button10_Click(object sender, EventArgs e)
        {
            label23.Text = "";
            select_cnt = 0;
            panel5.Show();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e) //1관 B
        {
            bool t = dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor.ToString().Equals("Color [Empty]");
            if ((select_cnt < member_cnt) && t)
            {
                string seat;
                seat = dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                
                if (!label23.Text.Contains(seat))
                {
                    label23.Text = label23.Text + Environment.NewLine + seat;
                    select_cnt++;
                }     
                else
                {
                    MessageBox.Show("선택하신 좌석입니다. 다른 좌석을 선택하십시오");
                }

            }
            else if (!t)
            {
                MessageBox.Show("이미 예약된 자리 입니다.");
            }
            else
            {
                MessageBox.Show("해당 인원만큼 모두 선택하셨습니다.");
            }
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e) //2관 A
        {
            bool t = dataGridView5.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor.ToString().Equals("Color [Empty]");
            if ((select_cnt < member_cnt) && t)
            {
                string seat;
                seat = dataGridView5.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                
                if (!label23.Text.Contains(seat))
                {
                    label23.Text = label23.Text + Environment.NewLine + seat;
                    select_cnt++;
                }
                else
                {
                    MessageBox.Show("선택하신 좌석입니다. 다른 좌석을 선택하십시오");
                }

            }
            else if (!t)
            {
                MessageBox.Show("이미 예약된 자리 입니다.");
            }
            else
            {
                MessageBox.Show("해당 인원만큼 모두 선택하셨습니다.");
            }
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)//2관 B
        {
            bool t = dataGridView6.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor.ToString().Equals("Color [Empty]");
            if ((select_cnt < member_cnt) && t)
            {
                string seat;
                seat = dataGridView6.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                
                if (!label23.Text.Contains(seat))
                {
                    label23.Text = label23.Text + Environment.NewLine + seat;
                    select_cnt++;
                }
                else
                {
                    MessageBox.Show("선택하신 좌석입니다. 다른 좌석을 선택하십시오");
                }

            }
            else if (!t)
            {
                MessageBox.Show("이미 예약된 자리 입니다.");
            }
            else
            {
                MessageBox.Show("해당 인원만큼 모두 선택하셨습니다.");
            }
        }

        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)//2관 C
        {
            bool t = dataGridView7.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor.ToString().Equals("Color [Empty]");
            if ((select_cnt < member_cnt) && t)
            {
                string seat;
                seat = dataGridView7.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                
                if (!label23.Text.Contains(seat))
                {
                    label23.Text = label23.Text + Environment.NewLine + seat;
                    select_cnt++;
                }
                else
                {
                    MessageBox.Show("선택하신 좌석입니다. 다른 좌석을 선택하십시오");
                }

            }
            else if (!t)
            {
                MessageBox.Show("이미 예약된 자리 입니다.");
            }
            else
            {
                MessageBox.Show("해당 인원만큼 모두 선택하셨습니다.");
            }
        }

        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e) //3관 A
        {
            bool t = dataGridView8.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor.ToString().Equals("Color [Empty]");
            if ((select_cnt < member_cnt) && t)
            {
                string seat;
                seat = dataGridView8.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                
                if (!label23.Text.Contains(seat))
                {
                    //좌석(A 4)을 구분하여 행번호와 열번호에 넣어야함..

                    label23.Text = label23.Text + Environment.NewLine + seat;
                    select_cnt++;
                }
                else
                {
                    MessageBox.Show("선택하신 좌석입니다. 다른 좌석을 선택하십시오");
                }
            }
            else if (!t)
            {
                MessageBox.Show("이미 예약된 자리 입니다.");
            }
            else
            {
                MessageBox.Show("해당 인원만큼 모두 선택하셨습니다.");
            }
        }

        private void dataGridView9_CellClick(object sender, DataGridViewCellEventArgs e) //3관 B
        {
            bool t = dataGridView9.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor.ToString().Equals("Color [Empty]");
            if ((select_cnt < member_cnt) && t)
            {
                string seat;
                seat = dataGridView9.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                
                if (!label23.Text.Contains(seat))
                {
                    label23.Text = label23.Text + Environment.NewLine + seat;
                    select_cnt++;
                }
                else
                {
                    MessageBox.Show("선택하신 좌석입니다. 다른 좌석을 선택하십시오");
                }
            }
            else if (!t)
            {
                MessageBox.Show("이미 예약된 자리 입니다.");
            }
            else
            {
                MessageBox.Show("해당 인원만큼 모두 선택하셨습니다.");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(member_cnt==select_cnt)
            { 
                panel4.Hide(); //Step3 화면 숨기기
                button14.Hide();
                button8.Hide(); //Step3->step4 버튼 숨기기 (step3 버튼)
                panel8.Show(); //Step4 화면 보여주기
                button11.Show(); //Step4->step5 버튼 보여주기 (step4 버튼)
                button15.Show();

                //button2.Hide();

                conn = new OleDbConnection(connectionString);
                pictureBox2.Image = thumnail_img;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                label24.Text = label13.Text; //영화제목

                label25.Text = label20.Text; //상영관
                label26.Text = label21.Text; //일시
                label27.Text = comboBox1.Text; //인원
                label28.Text = label23.Text; //좌석번호

                label29.Text = label25.Text + label21.Text + label16.Text; //예약번호

             
            }
            else
            {
                MessageBox.Show("해당 인원 수 만큼 좌석을 선택하지 않았습니다."); //에러 메세지 
            }


        }

        string ID;
        string rID;
        public void getid(string id)
        {
            ID = id; //ID가 member_id 의미함
            //MessageBox.Show(ID);
            //test.Text = ID;
        }

        public void getrid(string rid1, string rid2, string rid3, string rid4, string rid5, string rid6, string rid7) //예약번호, 상영관 번호, 일시, 영화번호, 행, 열
        {

            button9.Dispose();
            button14.Dispose();
            pictureBox2.Dispose();
            rID = rid1; //ID가 member_id 의미함
            label20.Text = rid2;
            label21.Text = rid3;
            label16.Text = rid4;
            label25.Text = rid2;
            r = rid5;
            c = rid6;
            rl = true;
            ID = rid7;
            s2 = true;
            dataGridView1.Hide();
            panel1.Hide();
            label11.Hide();
            textBox1.Hide();
            button3.Hide();
            button1.Hide(); //step1->step2 다음 버튼 가림
            button2.Hide();

            panel2.Show();
            button7.Show(); //step2->step3 다음 버튼 보여줌(step2버튼)
            button13.Show();

            conn = new OleDbConnection(connectionString);
            conn.Open(); //데이터베이스 연결

            string a = "";
            string b = "";


            string[,] arr = new string[60, 2];
            int reserv_i = 0;
            try
            {
                dataGridView3.CurrentCell = null;
                dataGridView4.CurrentCell = null;
                dataGridView5.CurrentCell = null;
                dataGridView6.CurrentCell = null;
                dataGridView7.CurrentCell = null;
                dataGridView8.CurrentCell = null;
                dataGridView9.CurrentCell = null;

                //conn.Open(); //데이터베이스 연결
                OleDbCommand cmd1 = new OleDbCommand();
                cmd1.CommandText = "select row_num, column_num from reserved_seat where theater_id='" + label20.Text + "' and time ='" + label21.Text + "'"; //member 테이블
                cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd1.Connection = conn;
                OleDbDataReader read = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과


                while (read.Read())
                {
                    a = read.GetValue(0).ToString();
                    b = read.GetValue(1).ToString();
                    //MessageBox.Show(a + b);

                    arr[reserv_i, 0] = a;
                    arr[reserv_i, 1] = b;
                    char t1 = a.ToCharArray()[0];
                    int tt1 = (int)t1 - 65;
                    char t2 = b.ToCharArray()[0];
                    int tt2 = (int)t2 - 49;
                    // MessageBox.Show(tt1.ToString()+ tt2.ToString());

                    reserv_i++;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }

          
            if (s2)
            {
        
                panel2.Hide(); //Step2 화면 숨기기
                button7.Hide(); //Step2->step3 버튼 숨기기 (step2 버튼)
                button13.Hide();
                panel4.Show(); //Step3 화면 보여주기
                button8.Show();//Step3->step4 버튼 보여주기 (step3 버튼)
                button14.Show();

                panel5.Hide(); //1관 좌석모형
                panel6.Hide(); //2관 좌석모형
                panel7.Hide(); //3관 좌석모형

                if (label20.Text == button4.Text) //1관을 선택했을 경우
                {
                  
                    panel5.Show();
                    panel6.Hide();

                    //해당하는 panel을 보여주기
                    conn = new OleDbConnection(connectionString);
                    conn.Open(); //데이터베이스 연결
                    try
                    {




                        //conn.Open(); //데이터베이스 연결
                        OleDbCommand cmd1 = new OleDbCommand();
                        cmd1.CommandText = "select seat1, seat2, seat3 from seat"; //member 테이블
                        cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd1.Connection = conn;

                        OleDbDataReader read1 = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                        dataGridView3.ColumnCount = 3;
                        //dataGridView3.Rows.Clear();

                        //필드명 받아오는 반복문
                        for (int i = 0; i < 3; i++)
                        {
                            dataGridView3.Columns[i].Name = read1.GetName(i);

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

                            dataGridView3.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                        }
                        read1.Close();
                        //cmd1.ExecuteNonQuery();



                        OleDbCommand cmd2 = new OleDbCommand();
                        cmd2.CommandText = "select seat4, seat5, seat6 from seat"; //member 테이블
                        cmd2.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd2.Connection = conn;

                        OleDbDataReader read2 = cmd2.ExecuteReader(); //select member_id, name from member where flag ='1' 결과

                        dataGridView4.ColumnCount = 3;

                        for (int i = 0; i < 3; i++)
                        {
                            dataGridView4.Columns[i].Name = read2.GetName(i);
                        }
                        dataGridView4.Rows.Clear();
                        //행 단위로 반복
                        while (read2.Read())
                        {

                            object[] obj2 = new object[3]; // 필드수만큼 오브젝트 배열

                            for (int i = 0; i < 3; i++) // 필드 수만큼 반복
                            {
                                obj2[i] = new object();
                                obj2[i] = read2.GetValue(i); // 오브젝트배열에 데이터 저장
                            }

                            dataGridView4.Rows.Add(obj2); //데이터그리드뷰에 오브젝트 배열 추가
                        }

                        read2.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message); //에러 메세지 
                    }



                }

                else if (label20.Text == button5.Text) //2관을 선택했을 경우
                {
                    panel5.Hide();
                    panel6.Show();
                    panel7.Hide();

                    conn = new OleDbConnection(connectionString);
                    conn.Open(); //데이터베이스 연결
                    try
                    {
                        //conn.Open(); //데이터베이스 연결
                        OleDbCommand cmd1 = new OleDbCommand();
                        cmd1.CommandText = "select seat1, seat2 from seat"; //member 테이블
                        cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd1.Connection = conn;

                        OleDbDataReader read1 = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                        dataGridView5.ColumnCount = 2;
                        //dataGridView3.Rows.Clear();

                        //필드명 받아오는 반복문
                        for (int i = 0; i < 2; i++)
                        {
                            dataGridView5.Columns[i].Name = read1.GetName(i);

                        }

                        //행 단위로 반복
                        while (read1.Read())
                        {
                            object[] obj1 = new object[2]; // 필드수만큼 오브젝트 배열

                            for (int i = 0; i < 2; i++) // 필드 수만큼 반복
                            {
                                obj1[i] = new object();
                                obj1[i] = read1.GetValue(i); // 오브젝트배열에 데이터 저장
                            }

                            dataGridView5.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                        }
                        read1.Close();
                        //cmd1.ExecuteNonQuery();


                        OleDbCommand cmd2 = new OleDbCommand();
                        cmd2.CommandText = "select seat3, seat4 from seat"; //member 테이블
                        cmd2.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd2.Connection = conn;

                        OleDbDataReader read2 = cmd2.ExecuteReader(); //select member_id, name from member where flag ='1' 결과

                        dataGridView6.ColumnCount = 2;

                        for (int i = 0; i < 2; i++)
                        {
                            dataGridView6.Columns[i].Name = read2.GetName(i);
                        }
                        dataGridView6.Rows.Clear();
                        //행 단위로 반복
                        while (read2.Read())
                        {

                            object[] obj2 = new object[2]; // 필드수만큼 오브젝트 배열

                            for (int i = 0; i < 2; i++) // 필드 수만큼 반복
                            {
                                obj2[i] = new object();
                                obj2[i] = read2.GetValue(i); // 오브젝트배열에 데이터 저장
                            }

                            dataGridView6.Rows.Add(obj2); //데이터그리드뷰에 오브젝트 배열 추가
                        }

                        read2.Close();


                        OleDbCommand cmd3 = new OleDbCommand();
                        cmd3.CommandText = "select seat5, seat6 from seat"; //member 테이블
                        cmd3.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd3.Connection = conn;

                        OleDbDataReader read3 = cmd3.ExecuteReader(); //select member_id, name from member where flag ='1' 결과

                        dataGridView7.ColumnCount = 2;

                        for (int i = 0; i < 2; i++)
                        {
                            dataGridView7.Columns[i].Name = read3.GetName(i);
                        }
                        dataGridView7.Rows.Clear();
                        //행 단위로 반복
                        while (read3.Read())
                        {

                            object[] obj3 = new object[2]; // 필드수만큼 오브젝트 배열

                            for (int i = 0; i < 2; i++) // 필드 수만큼 반복
                            {
                                obj3[i] = new object();
                                obj3[i] = read3.GetValue(i); // 오브젝트배열에 데이터 저장
                            }

                            dataGridView7.Rows.Add(obj3); //데이터그리드뷰에 오브젝트 배열 추가
                        }

                        read3.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message); //에러 메세지 
                    }


                }

                else if (label20.Text == button6.Text) //3관을 선택했을 경우
                {
                    panel5.Hide();
                    panel6.Hide();
                    panel7.Show();

                    conn = new OleDbConnection(connectionString);
                    conn.Open(); //데이터베이스 연결
                    try
                    {
                        //conn.Open(); //데이터베이스 연결
                        OleDbCommand cmd1 = new OleDbCommand();
                        cmd1.CommandText = "select seat1, seat2, seat3 from seat_"; //member 테이블
                        cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd1.Connection = conn;

                        OleDbDataReader read1 = cmd1.ExecuteReader(); //select member_id, name from member where flag ='0' 결과

                        dataGridView8.ColumnCount = 3;
                        dataGridView8.Rows.Clear();

                        //필드명 받아오는 반복문
                        for (int i = 0; i < 3; i++)
                        {
                            dataGridView8.Columns[i].Name = read1.GetName(i);

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

                            dataGridView8.Rows.Add(obj1); //데이터그리드뷰에 오브젝트 배열 추가
                        }
                        read1.Close();
                        //cmd1.ExecuteNonQuery();


                        OleDbCommand cmd2 = new OleDbCommand();
                        cmd2.CommandText = "select seat4, seat5, seat6 from seat_"; //member 테이블
                        cmd2.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd2.Connection = conn;

                        OleDbDataReader read2 = cmd2.ExecuteReader(); //select member_id, name from member where flag ='1' 결과

                        dataGridView9.ColumnCount = 3;

                        for (int i = 0; i < 3; i++)
                        {
                            dataGridView9.Columns[i].Name = read2.GetName(i);
                        }
                        dataGridView9.Rows.Clear();
                        //행 단위로 반복
                        while (read2.Read())
                        {

                            object[] obj2 = new object[3]; // 필드수만큼 오브젝트 배열

                            for (int i = 0; i < 3; i++) // 필드 수만큼 반복
                            {
                                obj2[i] = new object();
                                obj2[i] = read2.GetValue(i); // 오브젝트배열에 데이터 저장
                            }

                            dataGridView9.Rows.Add(obj2); //데이터그리드뷰에 오브젝트 배열 추가
                        }



                        read2.Close();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message); //에러 메세지 
                    }




                }

                dataGridView3.CurrentCell = null;
                dataGridView4.CurrentCell = null;
                dataGridView5.CurrentCell = null;
                dataGridView6.CurrentCell = null;
                dataGridView7.CurrentCell = null;
                dataGridView8.CurrentCell = null;
                dataGridView9.CurrentCell = null;

                for (int j = 0; j < reserv_i; j++)
                {

                    char t1 = arr[j, 0].ToCharArray()[0];
                    int tt1 = (int)t1 - 65;
                    char t2 = arr[j, 1].ToCharArray()[0];
                    int tt2 = (int)t2 - 49;

                    if (label20.Text == button4.Text)
                    {
                        if (tt2 > 2)
                        {
                            dataGridView4.Rows[tt1].Cells[tt2 - 3].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }
                        else
                        {
                            dataGridView3.Rows[tt1].Cells[tt2].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }

                    }
                    else if (label20.Text == button5.Text)
                    {
                        if (tt2 > 3)
                        {
                            dataGridView7.Rows[tt1].Cells[tt2 - 4].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }
                        else if (tt2 > 1)
                        {
                            dataGridView6.Rows[tt1].Cells[tt2 - 2].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }
                        else
                        {
                            dataGridView5.Rows[tt1].Cells[tt2].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }
                    }
                    else if (label20.Text == button6.Text)
                    {
                        if (tt2 > 2)
                        {
                            dataGridView9.Rows[tt1].Cells[tt2 - 3].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }
                        else
                        {
                            dataGridView8.Rows[tt1].Cells[tt2].Style.BackColor = Color.FromArgb(0, 255, 0);
                        }
                    }
                }


                //dataGridView6.Rows[0].Cells[0].Style.BackColor = Color.FromArgb(240, 255, 240);
                //button2.Hide();
            }
            else
            {
                MessageBox.Show("상영관과 시간을 선택해 주세요."); //에러 메세지 
            }
            //MessageBox.Show(ID);
            //test.Text = ID;


        }

        private void button11_Click(object sender, EventArgs e) //Step4화면을 보여주고 사라짐 step5화면 등장하게 하는 버튼
        {
            panel8.Hide(); //Step4 화면 숨기기
            button15.Hide();
            button11.Hide(); //Step3->step4 버튼 숨기기 (step4 버튼)
            panel9.Show(); //Step5 화면 보여주기
            button16.Show();
            button12.Show(); //Step4->step5 버튼 보여주기 (step5 버튼)

            //button2.Hide();

            string text = label28.Text;
            char sp = ' ';
            string[] spstring = text.Split(sp);

            
            conn = new OleDbConnection(connectionString);
            conn.Open(); //데이터베이스 연결

            //결제금액을 가져오기
            try //보여주기 화면을 위해서 select함.
            {
                OleDbCommand cmd1 = new OleDbCommand();

                cmd1.CommandText = "select schedule_cost from schedule where movie_id ='" + label16.Text + "' and theater_id='" + label25.Text + "'"; //member 테이블


                cmd1.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd1.Connection = conn;
                OleDbDataReader read = cmd1.ExecuteReader();


                while (read.Read())
                {
                    //MessageBox.Show(read.GetValue(0).ToString());
                    //int cost = int.Parse(read.GetValue(0).ToString());
                    label47.Text = read.GetValue(0).ToString();
                }
                read.Close();


                int cost = Convert.ToInt32(label47.Text);
                //MessageBox.Show(cost.ToString());
                int people = int.Parse(label27.Text);
                int total = cost * people;
                //MessageBox.Show(total.ToString());

                label46.Text = total.ToString();



                //회원의 등급 가져오기 -> 그러려면 회원의 id를 가져와야함!
                OleDbCommand cmd2 = new OleDbCommand();

                cmd2.CommandText = "select grade from member where member_id ='" + ID + "'"; //member 테이블


                cmd2.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd2.Connection = conn;
                OleDbDataReader read2 = cmd2.ExecuteReader();


                while (read2.Read())
                {
                    label45.Text = read2.GetValue(0).ToString();
                }

                read2.Close();


                //회원 등급에 따른 할인율 가져오기
                OleDbCommand cmd3 = new OleDbCommand();

                cmd3.CommandText = "select discount_rate from discount_rate where grade ='" + label45.Text + "'"; //member 테이블


                cmd3.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd3.Connection = conn;
                OleDbDataReader read3 = cmd3.ExecuteReader();


                while (read3.Read())
                {
                    label44.Text = read3.GetValue(0).ToString();
                }

                read3.Close();



                int rate = Convert.ToInt32(label44.Text); //등급에 따른 할인율
                double discount_total = total * ((100 - rate) * 0.01);
                //MessageBox.Show(total.ToString());

                label42.Text = discount_total.ToString(); //남은 결제 금액

                int a = Convert.ToInt32(label46.Text);
                int b = Convert.ToInt32(label42.Text);
                int r = a - b;


                label43.Text = r.ToString(); //할인 금액

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

        private void button12_Click(object sender, EventArgs e)
        {
            if (rl)
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    conn.Open();
                    cmd.CommandText = "delete from reserved_seat where reserved_id = '" + rID + "'";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();

                   
                    cmd.CommandText = "delete from theater_seat where theater_id = '" + label20.Text + "' and row_num='"+r+"'and column_num='"+c+"'and time='"+label21.Text+"'";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    up();



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); //에러 메세지 
                }


            }
            if (s5 )
            {
                string text = label28.Text;
                char sp = ' ';
                string[] spstring = text.Split(sp);
                conn = new OleDbConnection(connectionString);
                conn.Open();

                for (int i = 0; i < spstring.Length - 1; i += 2)
                {
                    try
                    {
                        OleDbCommand cmd = new OleDbCommand();

                        int c = Convert.ToInt32(label47.Text); //1인당 금액
                        int ra = Convert.ToInt32(label44.Text); //등급에 따른 할인율
                        double discnt_total = c * ((100 - ra) * 0.01); //1인당 들어가는 할인이 적용된 금액
                        label53.Text = discnt_total.ToString();
                        label53.Hide();
                        //MessageBox.Show(label53.Text);
                        //MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                        //MessageBox.Show(label29.Text);
                        cmd.CommandText = "insert into theater_seat values('" + label25.Text + "','" + spstring[i].Trim() + "','" + spstring[i + 1].Trim() + "','" + label21.Text + "')"; //member 테이블

                        //MessageBox.Show(spstring[i]+spstring[i+1]);

                        cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "INSERT INTO reserved_seat VALUES('" + label16.Text + "','" + label26.Text + "','" + label25.Text + "','" + spstring[i].Trim() + "','" +
                            spstring[i + 1].Trim() + "','" + ID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" + label53.Text + "','" + label29.Text + "'||'R'||to_char(R_N.nextval, 'FM000'))";

                        cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                        //OleDbDataReader read2 = cmd.ExecuteReader();

                        up();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message); //에러 메세지 
                    }


                }
                conn.Close();



                MessageBox.Show("결제되었습니다.");
                this.Close();
            }
            else
            {
                MessageBox.Show("결제 수단을 선택하세요.");
            }
           
        }

        private void button2_Click(object sender, EventArgs e) //step1의 이전버튼
        {
            MessageBox.Show("첫번째 창입니다");
        }

        private void button13_Click(object sender, EventArgs e) //step2 화면 - 이전버튼
        {
            //Step1화면이 보여야 함 
            
            //패널
            dataGridView1.Show();
            panel1.Show();
            label11.Show();
            textBox1.Show();
            button3.Show();
            button1.Show();


            
            panel2.Hide();//Step2
            panel4.Hide();//Step3
            panel8.Hide();//Step4
            panel9.Hide();//Step5


            //다음 버튼 
            button1.Show();//Step1
            button7.Hide(); //Step2
            button8.Hide(); //Step3
            button11.Hide();//Step4
            button12.Hide();//Step5

            //이전 버튼 
            button2.Show();//Step1
            button13.Hide(); //Step2 -> Step1
            button14.Hide(); //Step3 -> Step2
            button15.Hide();//Step4 -> Step3
            button16.Hide();//Step5 -> Step4
            
        }

    

        private void button14_Click(object sender, EventArgs e) //step3 화면- 이전버튼
        {
            //Step2가 보여야함.

            dataGridView1.Hide();
            panel1.Hide();
            label11.Hide();
            textBox1.Hide();
            button3.Hide();
            button1.Hide();
            button2.Hide();


            //패널
            panel2.Show();//Step2
            panel4.Hide();//Step3
            panel8.Hide();//Step4
            panel9.Hide();//Step5


            //다음 버튼 
            button7.Show(); //Step2
            button8.Hide(); //Step3
            button11.Hide();//Step4
            button12.Hide();//Step5

            //이전 버튼 
            button2.Hide();//Step1
            button13.Show(); //Step2 -> Step1
            button14.Hide(); //Step3 -> Step2
            button15.Hide();//Step4 -> Step3
            button16.Hide();//Step5 -> Step4
        }

        private void button15_Click(object sender, EventArgs e) //step4 화면의 이전버튼
        {
            dataGridView1.Hide();
            panel1.Hide();
            label11.Hide();
            textBox1.Hide();
            button3.Hide();
            button1.Hide();
            button2.Hide();


            //Step3가 보여야함.
            //패널
            panel2.Hide();//Step2
            panel4.Show();//Step3
            panel8.Hide();//Step4
            panel9.Hide();//Step5


            //다음 버튼 
            button7.Hide(); //Step2
            button8.Show(); //Step3
            button11.Hide();//Step4
            button12.Hide();//Step5

            //이전 버튼 
            button2.Hide();//Step1
            button13.Hide(); //Step2 -> Step1
            button14.Show(); //Step3 -> Step2
            button15.Hide();//Step4 -> Step3
            button16.Hide();//Step5 -> Step4
        }

        private void button16_Click(object sender, EventArgs e) //step5 화면의 이전버튼
        {
            dataGridView1.Hide();
            panel1.Hide();
            label11.Hide();
            textBox1.Hide();
            button3.Hide();
            button1.Hide();
            button2.Hide();

            //Step4가 보여야함.
            //패널
            panel2.Hide();//Step2
            panel4.Hide();//Step3
            panel8.Show();//Step4
            panel9.Hide();//Step5


            //다음 버튼 
            button7.Hide(); //Step2
            button8.Hide(); //Step3
            button11.Show();//Step4
            button12.Hide();//Step5

            //이전 버튼 
            button2.Hide();//Step1
            button13.Hide(); //Step2 -> Step1
            button14.Hide(); //Step3 -> Step2
            button15.Show();//Step4 -> Step3
            button16.Hide();//Step5 -> Step4
        }

        private void button17_Click(object sender, EventArgs e)
        {
            conn = new OleDbConnection(connectionString);

            if (string.Compare(comboBox2.Text, "카드", true) == 0)
            {
                try
                {
                    conn.Open(); //데이터베이스 연결
                    OleDbCommand cmd = new OleDbCommand();

                    cmd.CommandText = "SELECT card from member where member_id = '" + ID + "'";

                    //textBox1.Text = cmd.CommandText;
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;
                    OleDbDataReader read = cmd.ExecuteReader();


                    while (read.Read())
                    {
                        textBox2.Text = read.GetValue(0).ToString();
                        s5 = true;
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
            else if(string.Compare(comboBox2.Text, "현금", true) == 0)
            {
                textBox2.Text = label42.Text;
                s5 = true;

            }

        }

    }
}
