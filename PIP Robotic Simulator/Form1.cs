using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIP_Robotic_Simulator
{
    public partial class Form1 : Form
    {

        int Target_Size = 10;

        public Form1()
        {
            InitializeComponent();
        }
        
        Bitmap flag = new Bitmap(512, 512);
        
        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            Set_Robot_Location(0, 0, 0);
            CL_Global_Variables.Robot_Coordinates[1] = "000";
            CL_Global_Variables.Robot_Coordinates[2] = "256";
            CL_Global_Variables.Robot_Coordinates[3] = "256";
        }       

        

        private void Set_Robot_Location(int Button_State, int X_Coordinate, int Y_Coordinate)
        {
            Graphics Movement_Graphics = Graphics.FromImage(flag);
            Movement_Graphics.Clear(Color.White);


            Brush Forward_Brush = Brushes.LightGreen;
            Movement_Graphics.FillPie(Forward_Brush, 0,0, 512, 512, 20,140);

            Brush Reverse_Brush = Brushes.LightBlue;
            Movement_Graphics.FillPie(Reverse_Brush, 0, 0, 512, 512, 200, 140);


            Brush Turn_Brush = Brushes.LightCoral;
            Movement_Graphics.FillPie(Turn_Brush, 0, 0, 512, 512, 160, 40);
            Movement_Graphics.FillPie(Turn_Brush, 0, 0, 512, 512, 340, 40);

            Brush Noise_Brush = Brushes.LightGoldenrodYellow;
            Movement_Graphics.FillPie(Noise_Brush, 216, 216, 80, 80, 0, 360);

            Pen blackPen = new Pen(Color.Black, 3);
            Movement_Graphics.DrawEllipse(blackPen, X_Coordinate - (Target_Size / 2), Y_Coordinate - (Target_Size / 2), Target_Size, Target_Size);

            if (CL_Global_Variables.Current_Robot_Push==000)
            {
                Brush Test_brush = Brushes.Red;
                Movement_Graphics.FillEllipse(Test_brush, X_Coordinate - (Target_Size / 2), Y_Coordinate - (Target_Size / 2), Target_Size, Target_Size);
            }



            System.Drawing.Font Label_Font = new System.Drawing.Font("Arial", 16);
            Brush Text_Brush = Brushes.DarkSlateGray;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            Movement_Graphics.DrawString("[DRIVE SIMULATOR]", Label_Font, Text_Brush, 300, 480, drawFormat);
            Movement_Graphics.DrawString("FORWARD =>", Label_Font, Text_Brush, 10, 125, drawFormat);
            Movement_Graphics.DrawString("TURN =>", Label_Font, Text_Brush, 10, 250, drawFormat);
            Movement_Graphics.DrawString("REVERSE =>", Label_Font, Text_Brush, 10, 381, drawFormat);
            

            pictureBox1.Image = flag; 

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //label1.Text = Cursor.Position.X + ":" + Cursor.Position.Y;            
            //label2.Text = CL_Global_Variables.Received_Message;

            try
            {
                CL_Global_Variables.Robot_Coordinates = CL_Global_Variables.Received_Message.Split(':');
                CL_Global_Variables.Current_Robot_Push = Convert_To_Speed(CL_Global_Variables.Robot_Coordinates[5]);                
                CL_Global_Variables.Current_Robot_X_Coordinate = Convert_To_Speed(CL_Global_Variables.Robot_Coordinates[2]);
                CL_Global_Variables.Current_Robot_Y_Coordinate = Convert_To_Speed(CL_Global_Variables.Robot_Coordinates[3]); 
                Set_Robot_Location(CL_Global_Variables.Current_Robot_Push, CL_Global_Variables.Current_Robot_Y_Coordinate, CL_Global_Variables.Current_Robot_X_Coordinate);
            }
            catch { }
        }

        private int Convert_To_Speed(string Raw_Data)
        {
            int Speed = 0;            
            try
            {
                Speed = Convert.ToInt32(Raw_Data);                
            }
            catch
            {
            }
            return Speed;
        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            CL_TCPListener.Run_Listener();
        }
    }
}
