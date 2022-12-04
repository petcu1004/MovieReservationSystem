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
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Movie_New : Form
    {
        OleDbConnection conn;
        string connectionString = "Provider=OraOLEDB.Oracle;Password=pigcoo1004;User ID=msm1004"; //oracle 서버 연결

        Image image = null;
        Image thumnail_img = null;

        public Movie_New()
        {
            InitializeComponent();
        }

        public bool ThumbnailCallback()
        {
            return true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        //Image Image1=null;



        private void button1_Click(object sender, EventArgs e)
        {
            

            OpenFileDialog dialog = new OpenFileDialog(); //이미지를 선택하기 위한 다이얼로그를 생성합니다.
            dialog.InitialDirectory = @"D:\"; //다이얼로그를 열었을 때 보여줄 초기 위치를 설정합니다.

            //다이얼로그의 결과값에 따라 처리해준다.
            if (dialog.ShowDialog() == DialogResult.OK)  //선택한 이미지의 값을 image_file 변수에 대입한다.
            {
                string image_file = string.Empty; //선택한 이미지를 담기위한 지역변수를 생성합니다
                image_file = dialog.FileName;
                textBox3.Text = image_file;
                image = Bitmap.FromFile(image_file);
                Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);//썸네일 만들기
                thumnail_img = image.GetThumbnailImage(400, 400, callback, new IntPtr());
                pictureBox1.Image = thumnail_img; //컨트롤에 선택한 이미지를 넣는다.

                //pictureBox1.Image = Bitmap.FromFile(image_file); //컨트롤에 선택한 이미지를 넣는다.
                //Image1 = Bitmap.FromFile(image_file);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; //PictureBox 컨트롤에 선택한 이미지를 넣습니다.
            }
            else if (dialog.ShowDialog() == DialogResult.Cancel) //해당 이벤트를 종료합니다
            {
                return;
            }

            
        }

        /*        private byte[] imageToByteArray(Image img)
                {
                    ImageConverter imageConverter = new ImageConverter();
                    byte[] b = (byte[])imageConverter.ConvertTo(img, typeof(byte[]));
                    return b;
                }*/



        private void button2_Click(object sender, EventArgs e) //저장 버튼
        {

            conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open(); //데이터베이스 연결
                OleDbCommand cmd = new OleDbCommand();
                if (image == null)
                {
                    cmd.CommandText = "INSERT INTO movie VALUES('M'||to_char(M_N.nextval,'FM000'),'" + textBox2.Text + "','" +
                        textBox4.Text + "','" + textBox5.Text + "', NULL, '0')";

                }
                else
                {
                    cmd.CommandText = "INSERT INTO movie VALUES('M'||to_char(M_N.nextval,'FM000'),'" + textBox2.Text + "','" +
                        textBox4.Text + "','" + textBox5.Text + "', :image_var, '0')";


                    byte[] bytes = imageToByteArray(image);
                    OleDbParameter para = new OleDbParameter();
                    para.OleDbType = OleDbType.LongVarBinary;
                    para.ParameterName = ":image_var";
                    para.Direction = ParameterDirection.Input;
                    para.SourceColumn = "product_image";
                    para.Size = bytes.Length;
                    para.Value = bytes;
                    cmd.Parameters.Add(para);
                }

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery(); //쿼리문을 실행하고 영향받는 행의 수를 반환.
                cmd.Parameters.Clear();
                conn.Close();
                textBox2.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                image = null;
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
            Admin_Movie frm = new Admin_Movie();

            frm.ShowDialog();

        }
        private byte[] imageToByteArray(Image img)
        {
            ImageConverter imageConverter = new ImageConverter();
            byte[] b = (byte[])imageConverter.ConvertTo(img, typeof(byte[]));
            return b;
        }
    }
}
