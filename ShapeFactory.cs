using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tetris
{
    public class ShapeFactory
    {
        public List<Rectangle> shapeVector;
        const string MOVING = "MOVING";
        private static readonly Random getRandom = new Random();

        public ShapeFactory()
        {
            
        }

        public static int GetRandomNr(int min, int max)
        {
            return getRandom.Next(min, max);
        }

        public static Block SetupType(int typeName)
        {
            Block blockType = null;
            switch (typeName)
            {               
                case 1: blockType = new Type1();
                    break;
                case 2: blockType = new Type2();
                    break;
                case 3: blockType = new Type3();
                    break;
                case 4: blockType = new Type4();
                    break;
                case 5: blockType = new Type5();
                    break;
                case 6: blockType = new Type6();
                    break;
            }

            return blockType;

        }
        
        public static Rectangle CreateRectangle(Brush currColor)
        {
            Rectangle newRect = new Rectangle()
            {
                Width = 23,
                Height = 23,
                RadiusX = 1,
                RadiusY = 1,
                Stroke = Brushes.Black,
                Fill = currColor,
                StrokeThickness = 1,

            };
            newRect.Name = MOVING;

            return newRect;
        }

        public List<Rectangle> getBlock(List<int> blocks)
        {
            List<Rectangle> shapeVec = new List<Rectangle>();

            foreach(int blockValue in blocks)
            {
                shapeVec.Add(shapeVector[blockValue]);
            }

            return shapeVec;
        }
        

        public static Thickness getPosition(int blockValue)
        {
            Margin margin = null;

            switch (blockValue) {
                case 1:
                    Margin block_1 = new Margin(0, 0, 0, 0);
                    margin = block_1;
                    break;
                case 2:
                    Margin block_2 = new Margin(20, 0, 0, 0);
                    margin = block_2;
                    break;
                case 3:
                    Margin block_3 = new Margin(40, 0, 0, 0);
                    margin = block_3;
                    break;
                case 4:
                    Margin block_4 = new Margin(60, 0, 0, 0);
                    margin = block_4;
                    break;
                case 5:
                    Margin block_5 = new Margin(0, 20, 0, 0);
                    margin = block_5;
                    break;
                case 6:
                    Margin block_6 = new Margin(20, 20, 0, 0);
                    margin = block_6;
                    break;
                case 7:
                    Margin block_7 = new Margin(40, 20, 0, 0);
                    margin = block_7;
                    break;
                case 8:
                    Margin block_8 = new Margin(60, 20, 0, 0);
                    margin = block_8;
                    break;
                case 9:
                    Margin block_9 = new Margin(0, 40, 0, 0);
                    margin = block_9;
                    break;
                case 10:
                    Margin block_10 = new Margin(20, 40, 0, 0);
                    margin = block_10;
                    break;
                case 11:
                    Margin block_11 = new Margin(40, 40, 0, 0);
                    margin = block_11;
                    break;
                case 12:
                    Margin block_12 = new Margin(60, 40, 0, 0);
                    margin = block_12;
                    break;
                case 13:
                    Margin block_13 = new Margin(0, 60, 0, 0);
                    margin = block_13;
                    break;
                case 14:
                    Margin block_14 = new Margin(20, 60, 0, 0);
                    margin = block_14;
                    break;
                case 15:
                    Margin block_15 = new Margin(40, 60, 0, 0);
                    margin = block_15;
                    break;
                case 16:
                    Margin block_16 = new Margin(60, 60, 0, 0);
                    margin = block_16;
                    break;
            }

            Thickness newThickness = new Thickness(margin.left, margin.top, margin.right, margin.bottom);

            //Margin block_1 = new Margin(0, 0, 0, 0);
            //Margin block_2 = new Margin(20, 0, 0, 0);
            //Margin block_3 = new Margin(40, 0, 0, 0);
            //Margin block_4 = new Margin(60, 0, 0, 0);

            //Margin block_5 = new Margin(0, 20, 0, 0);
            /*
            Margin block_6 = new Margin(20, 20, 0, 0);
            Margin block_7 = new Margin(40, 20, 0, 0);
            Margin block_8 = new Margin(60, 20, 0, 0);

            Margin block_9 = new Margin(0, 40, 0, 0);
            Margin block_10 = new Margin(20, 40, 0, 0);
            Margin block_11 = new Margin(40, 40, 0, 0);
            Margin block_12 = new Margin(60, 40, 0, 0);

            Margin block_13 = new Margin(0, 60, 0, 0);
            Margin block_14 = new Margin(20, 60, 0, 0);
            Margin block_15 = new Margin(40, 60, 0, 0);
            Margin block_16 = new Margin(60, 60, 0, 0);
            */
            return newThickness;
        }
    }

    
}
