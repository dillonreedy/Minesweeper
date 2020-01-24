using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
  public class HelperFunctions
  {
    public MineSweeperButton FindButton(Panel mineButtonPanel, int i, int j)
    {
      MineSweeperButton thisBtn = new MineSweeperButton();
      foreach (Control control in mineButtonPanel.Controls)
      {
        var temp = ((MineSweeperButton)control);
        if (temp.x == i && temp.y == j)
          thisBtn = temp;
      }

      return thisBtn;
    }

    public void ColorPanel(int NUM_ROWS, int NUM_COLS, char[,] rgBoard, ref Panel currentMineButtonPanel)
    {
      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          MineSweeperButton b = FindButton(currentMineButtonPanel, i, j);
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

    public bool IsEndGame_Win(int NUM_ROWS, int NUM_COLS, int NUM_MINES, Panel curMineButtonPanel, bool[,] flagHere)
    {
      int flagCounter = 0;
      int clickedButtonCounter = 0;
      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          MineSweeperButton b = FindButton(curMineButtonPanel, i, j);
          if (flagHere[i, j]) flagCounter++;
          else if (!b.Enabled && !flagHere[i, j]) clickedButtonCounter++;
        }
      }

      if (flagCounter + clickedButtonCounter == (NUM_ROWS * NUM_COLS) && flagCounter == NUM_MINES) return true;
      else return false;
    }

    public void GetZeroButtonsThroughBFS(int num_rows, int num_cols, MineSweeperButton curButton, int[,] mineCount, Panel curMineButtonPanel, ref char[,] userState)
    {
      bool[,] beenHere = new bool[num_rows, num_cols];
      List<MineSweeperButton> results = new List<MineSweeperButton>();
      Queue<Point> q = new Queue<Point>();
      q.Enqueue(new Point(curButton.x, curButton.y));

      while (q.Count != 0)
      {
        Point curSearchPt = q.Dequeue();

        List<Point> surroundingPts = GetSurroundingPointsInBounds(num_rows, num_cols, curSearchPt);

        foreach (Point surroundingPt in surroundingPts)
          if (mineCount[surroundingPt.x, surroundingPt.y] == 0 && !beenHere[curSearchPt.x, curSearchPt.y]) q.Enqueue(surroundingPt);

        foreach (Point surroundingPt in surroundingPts)
        {
          MineSweeperButton b = FindButton(curMineButtonPanel, surroundingPt.x, surroundingPt.y);

          // if the mine count is not zero, put that text in the button.
          if (mineCount[surroundingPt.x, surroundingPt.y] != 0) b.Text = mineCount[surroundingPt.x, surroundingPt.y].ToString();

          b.BackColor = Color.Gray;
          b.Enabled = false;
          userState[surroundingPt.x, surroundingPt.y] = mineCount[surroundingPt.x, surroundingPt.y].ToString()[0];
        }

        beenHere[curSearchPt.x, curSearchPt.y] = true;
      }
    }
  }
}
