using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris
{
    public interface Block
    {
        string Blocktype { get; set; }
        List<int> rows { get; set; }
        List<int> cols { get; set; }
        List<int> nextRows { get; set; }
        List<int> nextCols { get; set; }
        Brush color { get; }
        void RotateType(int rotationCount, List<int> currRow, List<int> currCol);

    }
}
