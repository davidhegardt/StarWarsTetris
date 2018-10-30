using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Margin
    {
        public int left { get; set; }
        public int top { get; set; }        
        public int right { get; set; }
        public int bottom { get; set; }

        public Margin(int pLeft, int pTop, int pRight, int pBottom)
        {
            top = pTop;
            left = pLeft;
            right = pRight;
            bottom = pBottom;
        }

        public Margin()
        {

        }
    }
}
