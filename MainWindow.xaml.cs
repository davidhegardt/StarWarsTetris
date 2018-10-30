using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatchTimer = new DispatcherTimer();
        int counter = 10;
        const int MAXROW = 15;

        const string MOVING = "MOVING";
        int CURRENT_TYPE = 2;
        int NEXT_TYPE = 0;
        List<Block> currentBlocks;
        List<Block> setBlocks;
        int score = 0;
        bool rotationEnabled = false;

        public MainWindow()
        {
            dispatchTimer.Tick += new EventHandler(dispatchTimer_tick);
            dispatchTimer.Interval = new TimeSpan(0,0,0,0,300);
            currentBlocks = new List<Block>();
            setBlockCoords = new List<Point>();
            currentBlockCoords = new List<Point>();
            setBlocks = new List<Block>();
            InitializeComponent();
            CURRENT_TYPE = ShapeFactory.GetRandomNr(1,6);
            createShape();
            NEXT_TYPE = ShapeFactory.GetRandomNr(1, 6);
            setNextShape();
            dispatchTimer.Start();
            colCounter = new List<int>();
            PlaySound();
            
        }

        private void dispatchTimer_tick(object sender,EventArgs e)
        {
            counter+= 10;

            if (checkCollision("DOWN"))
            {
                handleCollision();
            }

            if (!checkStop())
            {
                removeShape();
                moveShape("DOWN");
            }

        }

        int lowestRow = MAXROW;
        int latestMin = MAXROW;

        private void handleCollision()
        {
            dispatchTimer.Stop();
            setBlocks.Add(currentBlocks[0]);
            latestMin = currentBlocks[0].rows.Min();
            currentBlocks.RemoveAt(0);
            checkLowestBlock();
            nextBlock();
        }

        private void checkLowestBlock()
        {
            if(lowestRow > latestMin)
            {
                lowestRow = latestMin;
                Trace.WriteLine("Lowest row: " + lowestRow);
            }
        }

        private Boolean checkStop()
        {
            var rows = currentBlocks[0].rows;

            Boolean reachedBottom = rows.Contains(MAXROW);

            if (reachedBottom)
            {                
                dispatchTimer.Stop();
                setBlocks.Add(currentBlocks[0]);
                latestMin = currentBlocks[0].rows.Min();
                currentBlocks.RemoveAt(0);
                checkLowestBlock();
                nextBlock();
                return true;
            } else
            {
                return false;
            }
        }

        private void nextBlock()
        {
            saveShapes();
            checkBlocks();
            saveShapes();
            //CURRENT_TYPE = ShapeFactory.GetRandomNr(1, 6);
            CURRENT_TYPE = NEXT_TYPE;
            NEXT_TYPE = ShapeFactory.GetRandomNr(1, 6);
            setNextShape();
            createShape();
            dispatchTimer.Start();
            
        }
        MediaPlayer player = new MediaPlayer();
        private void PlaySound()
        {
            Uri uri = new Uri(@"../../Sounds/theme_song.mp3", UriKind.Relative);
            player = new MediaPlayer();
            player.Open(uri);
            player.Play();
        }

        
        private bool checkCollision(String Direction)
        {
            var currentBlockRow = currentBlocks[0].rows;
            var currentBlockCol = currentBlocks[0].cols;

            switch (Direction)
            {
                case "DOWN":
                    currentBlockRow = currentBlockRow.ConvertAll(i => i + 1);
                    break;
                case "LEFT":
                    currentBlockCol = currentBlockCol.ConvertAll(x => x - 1);
                    var hitLimitLeft = currentBlockCol.Any(x => x < 0);
                    if(hitLimitLeft) { return true; }
                    break;
                case "RIGHT":
                    currentBlockCol = currentBlockCol.ConvertAll(y => y + 1);
                    var hitLimitRight = currentBlockCol.Any(y => y > 9);
                    if(hitLimitRight) { return true; }
                    break;
            }

            
            
            for(int i = 0; i < currentBlockRow.Count; i++)
            {
                Point currentPoint = new Point(currentBlockRow[i], currentBlockCol[i]);
                currentBlockCoords.Add(currentPoint);
            }

            var test = setBlockCoords.Intersect(currentBlockCoords).Any();

            if (test)
            {
                foreach (Point row in setBlockCoords)
                {
                    Trace.WriteLine("Set block coords row " + row.X);
                }
                currentBlockCoords.Clear();
                return true;
            } else
            {
                currentBlockCoords.Clear();
                return false;
            }

/*
            foreach (Block setBlock in setBlocks)
            {
                var loopingRow = setBlock.rows;
                var loopingCol = setBlock.cols;
                for (int i = 0; i < loopingRow.Count; i++)
                {
                    //if (currentBlockRow[i] == loopingRow[i])
                      if (currentBlockRow[i] == lowestRow)
                        {
                        Trace.WriteLine("moving block" + currentBlockCol[i]);
                        Trace.WriteLine("set block" + loopingCol[i]);
                        if (currentBlockCol[i] == loopingCol[i])
                        {
                            Trace.WriteLine("moving block" + currentBlockCol[i]);
                            Trace.WriteLine("set block" + loopingCol[i]);
                            return true;
                        }
                    }
                }

                
            }
            return false;

            */
        }

        private void moveShape(String Direction)
        {
            var currentColumn = currentBlocks[0].cols;
            var currentRow = currentBlocks[0].rows;

            switch (Direction)
            {
                case "DOWN":
                    currentRow = currentRow.ConvertAll(i => i + 1);
                    break;
                case "LEFT":
                    currentColumn = currentColumn.ConvertAll(x => x - 1);
                    break;
                case "RIGHT":
                    currentColumn = currentColumn.ConvertAll(y => y + 1);
                    break;
            }

            

            currentBlocks.RemoveAt(0);

            //Type3 test2 = new Type3();
            Block moveBlock = ShapeFactory.SetupType(CURRENT_TYPE);
            for (int i = 0; i < 4; i++)
            {
                /*
                Rectangle newRect = new Rectangle()
                {
                    Width = 20,
                    Height = 20,
                    RadiusX = 2,
                    RadiusY = 2,
                    Stroke = Brushes.Black,
                    Fill = Brushes.Gold,
                    StrokeThickness = 1,

                };
                newRect.Name = MOVING;
                */
                Rectangle newRect = ShapeFactory.CreateRectangle(moveBlock.color);
                moveBlock.cols = currentColumn;
                moveBlock.rows = currentRow;
                tetrisGrid.Children.Add(newRect);
                Grid.SetColumn(newRect, currentColumn[i]);
                Grid.SetRow(newRect, currentRow[i]);

            }
            currentBlocks.Add(moveBlock);
        }
        List<Point> setBlockCoords;
        List<Point> currentBlockCoords;
        private void saveShapes()
        {
            setBlockCoords.Clear();
            int index = 0;
            while (index < tetrisGrid.Children.Count)
            {
                UIElement blockTest = tetrisGrid.Children[index];                
                if (blockTest is Rectangle)
                {
                    Rectangle block = (Rectangle)blockTest;
                    var rowValue = Grid.GetRow(block);
                    var colValue = Grid.GetColumn(block);
                    Trace.WriteLine("ROW VALUE FOR LANDED SHAPE" + rowValue);
                    Trace.WriteLine("COL VALUE FOR LANDED SHAPE" + colValue);
                    Point testPoint = new Point(rowValue, colValue);
                    setBlockCoords.Add(testPoint);
                    if (block.Name == MOVING)
                    {
                        //tetrisGrid.Children.Remove(blockTest);
                        //index = -1;                        
                        block.Name = "STOP";
                        
                    }
                }
                index++;
            }
        }

        private void checkBlocks()
        {
            int gridRows = tetrisGrid.RowDefinitions.Count;
            int gridColumn = tetrisGrid.ColumnDefinitions.Count;
            int blockCount = 0;
            for (int row = gridRows; row >= 0; row--)
            {
                blockCount = 0;
                for (int col = gridColumn; col >= 0; col--)
                {
                    Rectangle blockTest;
                    blockTest = (Rectangle)tetrisGrid.Children.Cast<UIElement>().
                        FirstOrDefault(b => Grid.GetRow(b) == row && Grid.GetColumn(b) == col);
                    if(blockTest != null)
                    {
                        blockCount++;
                    }
                }

                if(blockCount == gridColumn)
                {
                    removeRow(row);
                    updateScore();
                    checkBlocks();
                }
            }

        }

        List<int> colCounter;

        private void removeRow(int row)
        {
            int columnCount = tetrisGrid.ColumnDefinitions.Count;

            for (int i = 0; i < columnCount; i++)
            {
                Rectangle square;
                try
                {
                    square = (Rectangle)tetrisGrid.Children.Cast<UIElement>().
                        FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == i);
                    tetrisGrid.Children.Remove(square);
                } catch { }
            }

            for(int p = 0; p < setBlockCoords.Count; p++)
            {
                if (setBlockCoords[p].X == row)
                {
                    setBlockCoords[p] = new Point(setBlockCoords[p].X + 1, setBlockCoords[p].Y);
                }
                
            }
            
            foreach(UIElement element in tetrisGrid.Children)
            {
                Rectangle square = (Rectangle)element;
                
                if(square.Name.Equals("STOP") && Grid.GetRow(square) <= row)
                {
                    Grid.SetRow(square, Grid.GetRow(square) + 1);
                }
                
            }

            score = score+100;
        }
        
        private void updateScore()
        {
            lblScore.Content = "Score : " + score;
        }
       


        public void rotatePiece()
        {
            var currentRow = currentBlocks[0].rows;
            var currentCol = currentBlocks[0].cols;

            if (currentBlocks[0].Blocktype.Equals("Type"))
            {
                return;
            }

            currentBlocks.RemoveAt(0);
            removeShape();
            //Type3 Type3Rotate = new Type3();
            Block RotateBlock = ShapeFactory.SetupType(CURRENT_TYPE);
            RotateBlock.RotateType(rotation, currentRow, currentCol);

            for (int i = 0; i < 4; i++)
            {
                /*
                Rectangle newRect = new Rectangle()
                {
                    Width = 20,
                    Height = 20,
                    RadiusX = 2,
                    RadiusY = 2,
                    Stroke = Brushes.Black,
                    Fill = Brushes.Gold,
                    StrokeThickness = 1,

                };
                newRect.Name = MOVING;
                */
                Rectangle newRect = ShapeFactory.CreateRectangle(RotateBlock.color);

                tetrisGrid.Children.Add(newRect);
                Grid.SetColumn(newRect, RotateBlock.cols[i]);
                Grid.SetRow(newRect, RotateBlock.rows[i]);
            }
            currentBlocks.Add(RotateBlock);
        }

        private void removeShape()
        {
            int index = 0;
            while (index < tetrisGrid.Children.Count)
            {
                UIElement blockTest = tetrisGrid.Children[index];
                if (blockTest is Rectangle)
                {
                    Rectangle block = (Rectangle)blockTest;
                    if (block.Name == MOVING)
                    {
                        tetrisGrid.Children.Remove(blockTest);
                        index = -1;
                    }
                }
                index++;
            }
        }

        private void setNextShape()
        {
            //List<int> colValShape1 = new List<int>();
            //List<int> rowValShape1 = new List<int>();
            //colValShape1.Add(3); colValShape1.Add(3); colValShape1.Add(3); colValShape1.Add(3);
            //rowValShape1.Add(1); rowValShape1.Add(2); rowValShape1.Add(3); rowValShape1.Add(4);
            nextBlockGrid.Children.Clear();
            Block currBlock = ShapeFactory.SetupType(NEXT_TYPE);
            var cols = currBlock.nextCols;
            var rows = currBlock.nextRows;

            for (int i = 0; i < 4; i++)
            {
                Rectangle testRect = ShapeFactory.CreateRectangle(currBlock.color);
                nextBlockGrid.Children.Add(testRect);
                Grid.SetColumn(testRect, cols[i]);
                Grid.SetRow(testRect, rows[i]);
            }
        }

        private void createShape()
        {
            //Type3 test1 = new Type3();
            //Type3 test2 = new Type3();
            //var cols = test2.cols;
            //var rows = test2.rows;

            Block newBlock = ShapeFactory.SetupType(CURRENT_TYPE);
            var cols = newBlock.cols;
            var rows = newBlock.rows;

            for (int i = 0; i < 4; i++)
            {
                /*
                Rectangle newRect = new Rectangle()
                {
                    Width = 20,
                    Height = 20,
                    RadiusX = 2,
                    RadiusY = 2,
                    Stroke = Brushes.Black,
                    Fill = Brushes.Gold,
                    StrokeThickness = 1,

                };
                newRect.Name = MOVING;
                */
                Rectangle newRect = ShapeFactory.CreateRectangle(newBlock.color);
                tetrisGrid.Children.Add(newRect);
                Grid.SetColumn(newRect,cols[i]);
                Grid.SetRow(newRect, rows[i]);
                
            }
            currentBlocks.Add(newBlock);

            rotationEnabled = true;
            rotation = 0;
        }
        public int rotation = 0;



        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "Space":
                    if (dispatchTimer.IsEnabled)
                    {
                        dispatchTimer.Stop();
                        lblPause.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        dispatchTimer.Start();
                        lblPause.Visibility = Visibility.Hidden;
                    }
                    break;
            }

            if (dispatchTimer.IsEnabled && rotationEnabled)
            {
                switch (e.Key.ToString())
                {
                    case "Up":
                        rotation++;
                        if (rotation > 4)
                        {
                            rotation = 1;
                        }
                        rotatePiece();
                        break;
                    case "Left":
                        if (!checkCollision("LEFT")) { removeShape(); moveShape("LEFT"); }
                        break;
                    case "Right":
                        if (!checkCollision("RIGHT")) { removeShape(); moveShape("RIGHT"); }
                        break;                    
                }
            }
        }
    }
}
