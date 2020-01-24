using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
  class HelperFunctions
  {
    public Point getPointFromButton(Button b)
    {
      return new Point(b.Top / Constants.MINE_BUTTON_SIZE_X, b.Left / Constants.MINE_BUTTON_SIZE_Y);
    }

    public void ColorPanel(int NUM_ROWS, int NUM_COLS, char[,] rgBoard, ref Panel currentMineButtonPanel)
    {
      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          Button b = ((Button)currentMineButtonPanel.Controls[i.ToString() + j.ToString()]);
          if (rgBoard[i, j] == 'G')
          {
            b.BackColor = Color.Green;
          }
          else if (rgBoard[i, j] == 'R')
          {
            b.BackColor = Color.Red;
          }
        }
      }
    }

    public bool InBounds(int NUM_ROWS, int NUM_COLS, Point p)
    {
      return (0 <= p.x && p.x < NUM_ROWS && 0 <= p.y && p.y < NUM_COLS);
    }

    public List<Point> GetSurroundingPointsInBounds(int NUM_ROWS, int NUM_COLS, Point curPt)
    {
      List<Point> surroundingPts = new List<Point>();
      int[] xs = { 1, 1, 1, 0, 0, -1, -1, -1 };
      int[] ys = { 1, 0, -1, 1, -1, 1, 0, -1 };

      for (int i = 0; i < xs.Length; i++)
      {
        Point testPt = new Point(curPt.x + xs[i], curPt.y + ys[i]);
        if (InBounds(NUM_ROWS, NUM_COLS, testPt))
        {
          surroundingPts.Add(testPt);
        }
      }

      return surroundingPts;
    }

    public bool IsEndGame_Win(int NUM_ROWS, int NUM_COLS, int NUM_MINES, ref Panel curMineButtonPanel, bool[,] flagHere)
    {
      int flagCounter = 0;
      int clickedButtonCounter = 0;
      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          Button b = ((Button)curMineButtonPanel.Controls[i.ToString() + j.ToString()]);
          if (flagHere[i, j]) flagCounter++;
          else if (!b.Enabled && !flagHere[i, j]) clickedButtonCounter++;
        }
      }

      if (flagCounter + clickedButtonCounter == (NUM_ROWS * NUM_COLS) && flagCounter == NUM_MINES) return true;
      else return false;
    }

    public void ZeroBFS(int NUM_ROWS, int NUM_COLS, Point curLoc, ref Panel curMineButtonPanel, int[,] mineCount, ref char[,] userState)
    {
      bool[,] beenHere = new bool[NUM_ROWS, NUM_COLS];
      beenHere[curLoc.x, curLoc.y] = true;
      Queue<Point> q = new Queue<Point>();
      q.Enqueue(curLoc);

      while (q.Count != 0)
      {
        Point curSearchPt = q.Dequeue();

        List<Point> surroundingPts = GetSurroundingPointsInBounds(NUM_ROWS, NUM_COLS, curSearchPt);

        foreach (Point p in surroundingPts)
        {
          Button b = ((Button)curMineButtonPanel.Controls[p.x.ToString() + p.y.ToString()]);
          if (mineCount[p.x, p.y] == 0)
          {
            if (!beenHere[p.x, p.y]) q.Enqueue(p);
          }
          else
          {
            b.Text = mineCount[p.x, p.y].ToString();
          }
          b.BackColor = Color.Gray;
          b.Enabled = false;
          beenHere[p.x, p.y] = true;
          userState[p.x, p.y] = mineCount[p.x, p.y].ToString()[0];
        }
      }
    }
  }
}
