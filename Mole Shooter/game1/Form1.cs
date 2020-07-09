#define My_Debug
using game1.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game1
{
    public partial class Form1 : Form
    {
        int _gameframe = 0;
        int _splatTime = 0;
        bool splat = false;
        Random rnd = new Random();

        int _hits = 0;
        int _mis = 0;
        int _totalhits = 0;
        double _avrhits = 0;
#if My_Debug
        int _cursx = 0;
        int _cursy = 0;
#endif
        CMole _mole;
        CScoreFrame _scoreframe;
        CSing1 _sing;
        CSplat1 _splat;
        public Form1()
        {
            InitializeComponent();
            Bitmap b = new Bitmap(Resources.Site);
            this.Cursor = CustomCurser.CreateCursor(b, b.Height / 2, b.Width / 2);
            _mole = new CMole() { Left = 10, Top = 500 };
            _sing = new CSing1() { Left =720, Top = 10 };
            _scoreframe = new CScoreFrame () { Left = 10, Top = 10 };
            _splat = new CSplat1();
        }

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            if (_gameframe >= 10)
            {
                UpdateMole();
                _gameframe = 0;
            }
            if (splat)
            {
                if (_splatTime >= 4)
                {
                    splat = false;
                    _splatTime = 0;
                    UpdateMole();
                }
                _splatTime++;
            }
            _gameframe++;
            this.Refresh();
        }
        private void UpdateMole()
        {
            _mole.Update(

            rnd.Next(Resources.mole.Width, this.Width - Resources.mole.Width),
                rnd.Next(this.Height / 2, this.Height - Resources.mole.Height * 2)
                );
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            if (splat)
            {
                _splat.DrawImage(dc);
            }
            else
            {
                _mole.DrawImage(dc);
            }
       
            _sing.DrawImage(dc);
            _scoreframe.DrawImage(dc);

#if My_Debug
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis;
            Font _font = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);
     TextRenderer.DrawText(dc, "X=" + _cursx.ToString() + ":" + "Y=" + _cursy.ToString(), _font, new Rectangle(1, 1, 120, 30), SystemColors.ControlText, flags);
     
#endif
            TextFormatFlags flags1 = TextFormatFlags.Left;
            Font _font1 = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(e.Graphics, "Shots: " + _totalhits.ToString(), _font, new Rectangle(50, 42, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Hits: " + _hits.ToString(), _font, new Rectangle(50, 62, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Misses: " + _mis.ToString(), _font, new Rectangle(50, 82, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Avg: " + _avrhits.ToString("F0")+"%", _font, new Rectangle(50, 102, 120, 20), SystemColors.ControlText, flags);
            base.OnPaint(e);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
#if My_Debug
            _cursx = e.X;
            _cursy = e.Y;
#endif
            this.Refresh();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X > 850 && e.X < 963 && e.Y > 36 && e.Y < 100)//strat 
            {
                timerGameLoop.Start();
            }
           else if (e.X > 740 && e.X < 974 && e.Y > 115 && e.Y < 165)//stop 
            {
                timerGameLoop.Stop();
            }
           else if (e.X > 857 && e.X < 1031 && e.Y > 170 && e.Y < 236)//reset 
            {
                timerGameLoop.Stop();
                _hits = 0;
                _mis = 0;
                _avrhits = 0;
                _totalhits = 0;
            }
           else if (e.X > 819 && e.X < 966 && e.Y > 245 && e.Y < 288)//quit
            {
                this.Close();
            }
            else
            {
                if (_mole.Hit(e.X, e.Y))
                {
                    splat = true;
                    _splat.Left = _mole.Left - Resources.Splat.Width / 3;
                    _splat.Top = _mole.Top - Resources.Splat.Height / 3;
                    _hits++;
                }else
                {
                    _mis++;
                }
            
                _totalhits = _hits + _mis;
                _avrhits =( (double)_hits / (double)_totalhits) * 100.0;
            }
            FireGun();

        }
        private void FireGun()
        {
            SoundPlayer simpleSound = new SoundPlayer(Resources.Shotgun_2);
            simpleSound.Play();
        }


    }
}
