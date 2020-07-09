using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_2
{
    public partial class Form1 : Form
    {
        int y;
        bool right, left, up, down;
        Random rnd = new Random();


        private void timer1_Tick(object sender, EventArgs e)
        {
            //player move
            if(right == true) { player.Left += 5;}
            if(left == true) { player.Left -= 5; }
            if(up == true) { player.Top -= 5; }
            if(down == true) { player.Top += 5; }
            //player sudir box1
            if (box2.Top  <= player.Bottom && box2.Left <= player.Right && box2.Right >= player.Left && player.Top <= box2.Bottom)
            {
                down = false;
            }
            if (box2.Bottom  >= player.Top && box2.Left <= player.Right && box2.Right >= player.Left && player.Bottom >= box2.Top)
            {
                up = false;
            }
            if (box2.Right  >= player.Left && box2.Top <= player.Bottom && box2.Bottom >= player.Top && player.Right >= box2.Left)
            {
                left = false;
            }
            if (box2.Left  <= player.Right && box2.Top <= player.Bottom && box2.Bottom >= player.Top && player.Left <= box2.Right)
            {
                right = false;
            }
            box2.Left -= 1;
            if (box2.Right <= 0) { box2.Dispose(); }
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics dc = panel1.CreateGraphics();
            Brush r = new SolidBrush(Color.Red);
            Pen red = new Pen(r, 8);
            dc.FillRectangle(r,y, 150, 25, 25);

        }

        public Form1()
        {
            InitializeComponent();
            box2.Top = rnd.Next(0,348);
            box2.Left = rnd.Next(150,336);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) { right = true; }
            if (e.KeyCode == Keys.Left) { left = true; }
            if(e.KeyCode == Keys.Up) { up = true; }
            if(e.KeyCode == Keys.Down) { down = true; }
            if(e.KeyCode == Keys.Escape) { this.Close(); }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) { right =false; }
            if (e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.Up) { up = false; }
            if (e.KeyCode == Keys.Down) { down = false; }
        }


      
    }
}
