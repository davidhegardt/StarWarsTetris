using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris
{
    public class Type3 : Block
    {
        public string blockType = "Type3";
        /*
        *      XX 
        *      XX
        */
        public List<int> rowValues;
        public List<int> colValues;
        public List<int> colValuesNext;
        public List<int> rowValuesNext;

        public Type3()
        {
            rowValues = new List<int>();
            colValues = new List<int>();
            colValues.Add(5); colValues.Add(5); colValues.Add(6); colValues.Add(6);
            rowValues.Add(1); rowValues.Add(2); rowValues.Add(1); rowValues.Add(2);
            setupNext();
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
            get { return Brushes.HotPink; }
        }

        public void RotateType(int rotationCount, List<int> currRow, List<int> currCol)
        {
            
        }



        private void setupNext()
        {
            colValuesNext = new List<int>();
            rowValuesNext = new List<int>();
            colValuesNext.Add(1); colValuesNext.Add(2); colValuesNext.Add(1); colValuesNext.Add(2);
            rowValuesNext.Add(1); rowValuesNext.Add(1); rowValuesNext.Add(2); rowValuesNext.Add(2);
        }
    }
}
