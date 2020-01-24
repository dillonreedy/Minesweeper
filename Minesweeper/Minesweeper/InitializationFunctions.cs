using System;
using System.Collections.Generic;

namespace Minesweeper
{
  public class InitializationFunctions
  {
    private HelperFunctions _helperFunctions;

    public InitializationFunctions() {
      _helperFunctions = new HelperFunctions();
    }

    public bool[,] InitializeMineLocationBoard(int NUM_ROWS, int NUM_COLS, int NUM_MINES)
    {
      bool[,] isMineHere = new bool[NUM_ROWS, NUM_COLS];

      int numMinesLeft = NUM_MINES;
      Random r = new Random();
      while (numMinesLeft != 0)
      {
        int row = r.Next(NUM_ROWS);
        int col = r.Next(NUM_COLS);
        if (!isMineHere[row, col])
        {
          isMineHere[row, col] = true;
          numMinesLeft--;
        }
      }

      return isMineHere;
    }

    public int[,] InitializeMineCountBoard(int NUM_ROWS, int NUM_COLS, int NUM_MINES, bool[,] isMineHere)
    {
      int[,] mineCount = new int[NUM_ROWS, NUM_COLS];

      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          Point curLoc = new Point(i, j);
          List<Point> surroundingPts = _helperFunctions.GetSurroundingPointsInBounds(NUM_ROWS, NUM_COLS, curLoc);
          int mineCounter = 0;

          foreach (Point p in surroundingPts) if (isMineHere[p.x, p.y]) mineCounter++;

          mineCount[curLoc.x, curLoc.y] = mineCounter;
        }
      }

      return mineCount;
    }

    internal char[,] InitializeUserStateBoard(int NUM_ROWS, int NUM_COLS)
    {
      char[,] userState = new char[NUM_ROWS, NUM_COLS];
      for (int i = 0; i < NUM_ROWS; i++)
        for (int j = 0; j < NUM_COLS; j++)
          userState[i, j] = 'U';

      return userState;
    }
  }
}
