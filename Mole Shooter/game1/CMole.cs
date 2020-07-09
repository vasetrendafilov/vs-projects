using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using game1.Properties;

namespace game1
{
    class CMole : CImageBase
    {
        private Rectangle _moleHotSpot = new Rectangle();

        public CMole() : base(Resources.mole)
        {
            _moleHotSpot.X = Left + 35;
            _moleHotSpot.Y = Top - 1;
            _moleHotSpot.Width = 40;
            _moleHotSpot.Height =50;
        }
        public void Update(int X, int Y)
        {
            Left = X;
            Top = Y;
            _moleHotSpot.X = Left + 25;
            _moleHotSpot.Y = Top - 1;
        }
        public bool Hit(int X, int Y)
        {
            Rectangle c = new Rectangle(X, Y, 1, 1);
            if (_moleHotSpot.Contains(c))
            {
                return true;
            }
            return false;
        }
    }
}