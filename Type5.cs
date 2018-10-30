using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris
{
    class Type5 : Block
    {
        public string blockType = "Type5";
        /*
        *      X 
        *      XX
        *       X
        */
        public List<int> rowValues;
        public List<int> colValues;
        public List<int> colValuesNext;
        public List<int> rowValuesNext;

        public Type5()
        {
            rowValues = new List<int>();
            colValues = new List<int>();
            colValues.Add(5); colValues.Add(5); colValues.Add(6); colValues.Add(6);
            rowValues.Add(1); rowValues.Add(2); rowValues.Add(2); rowValues.Add(3);
            setupNext();
        }

        private void rotateLeft()
        {
            rows[0] = rows[0] + 1; cols[0] = cols[0] - 1;
            rows[2] = rows[2] - 1; cols[2] = cols[2] - 1;
            rows[3] = rows[3] - 2; cols[3] = cols[3];
        }

        private void normalRotate()
        {
            rows[0] = rows[0] - 1; cols[0] = cols[0] + 1;
            rows[2] = rows[2] + 1; cols[2] = cols[2] + 1;
            rows[3] = rows[3] + 2; cols[3] = cols[3];
        }

        public string Blocktype
        {
            get { return blockType; }
            set { blockType = value; }
        }

        public List<int> rows
        {
            get { return rowValues; }
            set
            {
                rowValues = value;
            }
        }
        public List<int> cols
        {
            get { return colValues; }
            set
            {
                colValues = value;
            }
        }

        public List<int> nextRows
        {
            get { return rowValuesNext; }
            set
            {
                rowValuesNext = value;
            }
        }
        public List<int> nextCols
        {
            get { return colValuesNext; }
            set
            {
                colValuesNext = value;
            }
        }

        public Brush color
        {
            get { return Brushes.BlueViolet; }
        }

        public void RotateType(int rotationCount, List<int> currRow, List<int> currCol)
        {
            this.rows = currRow;
            this.cols = currCol;

            switch (rotationCount)
            {
                case 1:
                    rotateLeft();
                    break;
                case 2:
                    normalRotate();
                    break;
                case 3:
                    rotateLeft();
                    break;
                case 4:
                    normalRotate();
                    break;
            }
        }

        private void setupNext()
        {
            colValuesNext = new List<int>();
            rowValuesNext = new List<int>();
            colValuesNext.Add(2); colValuesNext.Add(2); colValuesNext.Add(3); colValuesNext.Add(3);
            rowValuesNext.Add(1); rowValuesNext.Add(2); rowValuesNext.Add(2); rowValuesNext.Add(3);
        }
    }
}
