using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twozerofoureight
{
    class TwoZeroFourEightModel : Model
    {
        protected int boardSize; // default is 4
        protected int[,] board;
        protected Random rand;
        protected int[] range;

        public TwoZeroFourEightModel() : this(4)
        {
            // default board size is 4 
        }

        public TwoZeroFourEightModel(int size)
        {
            boardSize = size;
            board = new int[boardSize, boardSize];
            range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                foreach (int j in range)
                {
                    board[i, j] = 0;
                }
            }
            rand = new Random();
            // initialize board
            HandleChanges();
        }

        public int[,] GetBoard()
        {
            return board;
        }

        public int CheckGameOver()
        {
            int gg = 0; //can continue the game
            int count = 0; //create count to count the tile to check if the board is empty or not
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i, j] == 2048) //check the tile that has 2048 
                    {
                        gg = 1; // true == win
                    }
                    if (board[i,j] != 0) //there is the empty tile in the board
                    {
                        count++; //count the tile
                    }
                }             
            }
            if (count == 16)//the grid is full.  it has 16 tiles
            {
                for(int i = 0; i < boardSize; i++)
                {
                    for(int j = 0; j < boardSize; j++)
                    {
                        //condition for check if there is/are nearby tile to return 0 for continue playing the game 
                        if (i == 0 && j == 0) //1
                        {
                            if (board[i, j] == (board[i, j + 1]) || board[i,j] == board[i+1,j]){
                                return 0;
                            }
                        }
                        if ((i == 0 && j == 1) || (i == 0 && j == 2))//2 
                        {
                            if (board[i, j] == board[i, j - 1] || (board[i, j] == board[i + 1, j]) || (board[i, j] == board[i, j + 1]))
                            {
                                return 0;
                            }
                        }
                        if (i == 0 && j == 3)//3
                        {
                            if (board[i, j] == board[i, j - 1] || board[i, j] == board[i + 1, j])
                            {
                                return 0;
                            }
                        }
                        if (i == 1 && j == 0 || i == 2 && j == 0)//4
                        {                        
                            if (board[i, j] == board[i-1, j] || board[i, j] == board[i,j+1] || board[i, j] == board[i+1, j])
                            {
                                return 0;
                            }
                        }
                        if (i == 3 && j == 0)//5
                        {
                            if (board[i, j] == board[i-1, j] || board[i, j] == board[i , j+ 1])
                            {
                                return 0;
                            }
                        }
                        if (i == 3 && j == 1 || i == 3 && j == 2)//6
                        {
                            if (board[i, j] == board[i , j- 1] || board[i, j] == board[i-1, j] || board[i, j] == board[i, j + 1])
                            {
                                return 0;
                            }
                        }
                        if (i == 3 && j == 3)//7
                        {
                            if (board[i, j] == board[i , j- 1] || board[i, j] == board[i-1, j ])
                            {
                                return 0;
                            }
                        }
                        if (i == 1 && j == 3 || i == 2 && j == 3)//8
                        {
                            if (board[i, j] == board[i, j - 1] || board[i, j] == board[i - 1, j] || board[i, j] == board[i+ 1, j ])
                            {
                                return 0;
                            }
                        }
                        if(i==1 && j==1|| i == 1 && j == 2 || i == 2 && j == 1 || i == 2 && j == 2)//9
                        {
                            if (board[i, j] == board[i, j - 1] || board[i, j] == board[i - 1, j] || board[i, j] == board[i + 1, j] || board[i, j] == board[i, j + 1])
                            {
                                return 0;
                            }
                        }                       
                    }
                   
                }
                return 2; //return 2 is mean the player is lose 
            }


            return gg;
        }

        public int Getscore() 
        {
            int score = 0;
            for (int i = 0; i < boardSize; i++)//row
            {
                for (int j = 0; j < boardSize; j++)//col
                {
                    score = score + board[i, j]; //sum all the number on the board
                }
            }
            return score;
        }

        private void AddRandomSlot()
        {
            while (true)
            {
                int x = rand.Next(boardSize);
                int y = rand.Next(boardSize);
                if (board[x, y] == 0)
                {
                    board[x, y] = 2;
                    return;
                }
            }
        }

        // Perform shift and merge to the left of the given array.
        protected bool ShiftAndMerge(int[] buffer)
        {
            bool changed = false; // whether the array has changed
            int pos = 0; // next available slot index
            int lastMergedSlot = -1; // last slot that resulted from merging
            foreach (int k in range)
            {
                if (buffer[k] != 0) // nonempty slot
                {
                    // check if we can merge with the previous slot
                    if (pos > 0 && pos - 1 > lastMergedSlot && buffer[pos - 1] == buffer[k])
                    {
                        // merge
                        buffer[pos - 1] *= 2;
                        buffer[k] = 0;
                        lastMergedSlot = pos - 1;
                        changed = true;
                    }
                    else
                    {
                        // shift to the next available slot
                        buffer[pos] = buffer[k];
                        if (pos != k)
                        {
                            buffer[k] = 0;
                            changed = true;
                        }
                        // move the next available slot
                        pos++;
                    }
                }
            }
            return changed;
        }

        protected void HandleChanges(bool changed = true)
        {
            // if the board has changed, add a new number
            // and notify all views
            if (changed)
            {
                AddRandomSlot();
                NotifyAll();
            }
        }

        public void PerformDown()
        {
            if (CheckGameOver() == 0)
            {


                bool changed = false; // whether the board has changed
                foreach (int i in range)
                {
                    int[] buffer = new int[boardSize];
                    // extract the current column from bottom to top
                    foreach (int j in range)
                    {
                        buffer[boardSize - j - 1] = board[j, i];
                    }
                    // process the extracted array
                    // also track changes
                    changed = ShiftAndMerge(buffer) || changed;
                    // copy back
                    foreach (int j in range)
                    {
                        board[j, i] = buffer[boardSize - j - 1];
                    }
                }
                HandleChanges(changed);
            }
        }

        public void PerformUp()
        {
            if (CheckGameOver() == 0)
            {
                bool changed = false; // whether the board has changed
                foreach (int i in range)
                {
                    int[] buffer = new int[boardSize];
                    // extract the current column from top to bottom
                    foreach (int j in range)
                    {
                        buffer[j] = board[j, i];
                    }
                    // process the extracted array
                    // also track changes
                    changed = ShiftAndMerge(buffer) || changed;
                    // copy back
                    foreach (int j in range)
                    {
                        board[j, i] = buffer[j];
                    }
                }
                HandleChanges(changed);
            }
        }

        public void PerformRight()
        {
            if (CheckGameOver() == 0)
            {
                bool changed = false; // whether the board has changed
                foreach (int i in range)
                {
                    int[] buffer = new int[boardSize];
                    // extract the current column from right to left
                    foreach (int j in range)
                    {
                        buffer[boardSize - j - 1] = board[i, j];
                    }
                    // process the extracted array
                    // also track changes
                    changed = ShiftAndMerge(buffer) || changed;
                    // copy back
                    foreach (int j in range)
                    {
                        board[i, j] = buffer[boardSize - j - 1];
                    }
                }
                HandleChanges(changed);
            }
        }

        public void PerformLeft()
        {
            if (CheckGameOver() == 0)
            {
                bool changed = false; // whether the board has changed
                foreach (int i in range)
                {
                    int[] buffer = new int[boardSize];
                    // extract the current column from left to right
                    foreach (int j in range)
                    {
                        buffer[j] = board[i, j];
                    }
                    // process the extracted array
                    // also track changes
                    changed = ShiftAndMerge(buffer) || changed;
                    // copy back
                    foreach (int j in range)
                    {
                        board[i, j] = buffer[j];
                    }
                }
                HandleChanges(changed);
            }
        }
    }
}
