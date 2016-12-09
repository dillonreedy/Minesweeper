using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
  public partial class Form1 : Form
  {
    #region Private Variables
    private int MINE_BUTTON_SIZE_X = 20;
    private int MINE_BUTTON_SIZE_Y = 20;
    private int NUM_COLS, NUM_ROWS, NUM_MINES;
    private string USER_PLAYING = "ಠ_ಠ";
    private string USER_LOSES = "ಠ╭╮ಠ";
    private string USER_WINS = "(ʘ‿ʘ)";
    private char[,] mineLocations;
    private int[,] mineCount;
    private char[,] userState;
    private bool[,] flagHere;
    #endregion

    #region Form Constructor
    public Form1()
    {
      InitializeComponent();
      OnNewGameInitialization();
    }
    #endregion

    #region Events

    #region Mouse Down Event

    private void btnMine_MouseDown(object sender, MouseEventArgs e)
    {
      switch (e.Button)
      {
        case MouseButtons.Left:
          LeftMouseButtonClick(((Button)sender));
          break;
        case MouseButtons.Right:
          RightMouseButtonClick(((Button)sender));
          break;
      }
    }
    #endregion

    private void numUpDownCols_ValueChanged(object sender, EventArgs e)
    {
      OnNewGameInitialization();
    }

    private void numUpDownRows_ValueChanged(object sender, EventArgs e)
    {
      OnNewGameInitialization();
    }


    private void numUpDownMines_ValueChanged(object sender, EventArgs e)
    {
      OnNewGameInitialization();
    }

    private void btnPlayerState_Click(object sender, EventArgs e)
    {
      OnNewGameInitialization();
    }
    #endregion

    #region Initialization Methods
    public void OnNewGameInitialization()
    {
      InitializePrivateVariables();
      InitializeMineButtonPanel();
      InitializeMineLocationBoard();
      InitializeMineCountBoard();
      InitializeUserStateBoard();
      flagHere = new bool[NUM_ROWS, NUM_COLS];
    }

    public void InitializePrivateVariables()
    {
      btnPlayerState.Text = USER_PLAYING;
      numUpDownMines.Maximum = (numUpDownCols.Value * numUpDownRows.Value) - 1;
      NUM_COLS = Int32.Parse(numUpDownCols.Value.ToString());
      NUM_ROWS = Int32.Parse(numUpDownRows.Value.ToString());
      NUM_MINES = Int32.Parse(numUpDownMines.Value.ToString());
    }

    public void InitializeMineButtonPanel()
    {
      mineButtonPanel.Controls.Clear();
      if (NUM_COLS * MINE_BUTTON_SIZE_X > mineButtonPanel.Size.Width) mineButtonPanel.Size = new Size((NUM_COLS * MINE_BUTTON_SIZE_X) + 50, mineButtonPanel.Size.Height);
      if (NUM_ROWS * MINE_BUTTON_SIZE_Y > mineButtonPanel.Size.Height) mineButtonPanel.Size = new Size(mineButtonPanel.Size.Width, (NUM_ROWS * MINE_BUTTON_SIZE_Y) + 20);

      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          Button b = new Button();
          b.Enabled = true;
          b.Name = i.ToString() + j.ToString();
          b.Width = MINE_BUTTON_SIZE_X;
          b.Height = MINE_BUTTON_SIZE_Y;
          b.Top = i * MINE_BUTTON_SIZE_Y;
          b.Left = j * MINE_BUTTON_SIZE_X;
          b.MouseDown += btnMine_MouseDown;
          mineButtonPanel.Controls.Add(b);
        }
      }
    }

    public void InitializeMineLocationBoard()
    {
      mineLocations = new char[NUM_ROWS, NUM_COLS];
      for (int i = 0; i < NUM_ROWS; i++)
        for (int j = 0; j < NUM_COLS; j++)
          mineLocations[i, j] = 'E';

      int numMinesLeft = NUM_MINES;
      Random r = new Random();
      while (numMinesLeft != 0)
      {
        int row = r.Next(NUM_ROWS);
        int col = r.Next(NUM_COLS);
        if (mineLocations[row, col].Equals('E'))
        {
          mineLocations[row, col] = 'M';
          numMinesLeft--;
        }
      }
    }

    public void InitializeMineCountBoard()
    {
      mineCount = new int[NUM_ROWS, NUM_COLS];

      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          Point curLoc = new Point(i, j);
          List<Point> surroundingPts = GetSurroundingPointsInBounds(curLoc);
          int mineCounter = 0;
          foreach (Point p in surroundingPts) if (mineLocations[p.x, p.y].Equals('M')) mineCounter++;
          mineCount[curLoc.x, curLoc.y] = mineCounter;
        }
      }
    }

    public void InitializeUserStateBoard()
    {
      userState = new char[NUM_ROWS, NUM_COLS];
      for (int i = 0; i < NUM_ROWS; i++)
        for (int j = 0; j < NUM_COLS; j++)
          userState[i, j] = 'U';
    }
    #endregion

    #region End Game Actions

    public void EndGame_Loss(Button b)
    {
      btnPlayerState.Text = USER_LOSES;
      b.BackColor = Color.Red;

      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          Button thisBtn = ((Button)mineButtonPanel.Controls[i.ToString() + j.ToString()]);
          if (mineLocations[i, j].Equals('M'))
          {
            thisBtn.Image = Minesweeper.Properties.Resources.mine;
          }
          thisBtn.Enabled = false;
        }
      }
    }

    public void EndGame_Win()
    {
      btnPlayerState.Text = USER_WINS;

      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          Button b = ((Button)mineButtonPanel.Controls[i.ToString() + j.ToString()]);
          b.Enabled = false;
        }
      }
    }
    #endregion

    #region MouseButtonClickLogic
    public void LeftMouseButtonClick(Button b)
    {
      Point curLoc = new Point(b.Top / MINE_BUTTON_SIZE_X, b.Left / MINE_BUTTON_SIZE_Y);
      if (!flagHere[curLoc.x, curLoc.y])
      {
        if (mineLocations[curLoc.x, curLoc.y] == 'M') EndGame_Loss(b);
        else
        {
          if (mineCount[curLoc.x, curLoc.y] != 0)
          {
            b.Text = mineCount[curLoc.x, curLoc.y].ToString();
            b.BackColor = Color.Gray;
            userState[curLoc.x, curLoc.y] = mineCount[curLoc.x, curLoc.y].ToString()[0];
          }
          else ZeroBFS(curLoc);

          b.Enabled = false;
          if (IsEndGame_Win()) EndGame_Win();
          else
          {
            RedGreenBoard rgBoard = new RedGreenBoard(NUM_ROWS, NUM_COLS, userState);
            ColorPanel(rgBoard.GetRedGreenBoard());
          }
        }
      }
    }

    public void RightMouseButtonClick(Button b)
    {
      Point curLoc = new Point(b.Top / MINE_BUTTON_SIZE_X, b.Left / MINE_BUTTON_SIZE_Y);

      if (!flagHere[curLoc.x, curLoc.y])
      {
        b.Image = Minesweeper.Properties.Resources.flag;
        flagHere[curLoc.x, curLoc.y] = true;
        if (IsEndGame_Win()) EndGame_Win();
      }
      else
      {
        b.Image = null;
        flagHere[curLoc.x, curLoc.y] = false;
      }
    }
    #endregion

    #region Helper Functions/Methods
    
    public void ColorPanel(char[,] rgBoard)
    {
      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          Button b = ((Button)mineButtonPanel.Controls[i.ToString() + j.ToString()]);
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

    public bool InBounds(Point p)
    {
      return (0 <= p.x && p.x < NUM_ROWS && 0 <= p.y && p.y < NUM_COLS);
    }

    public List<Point> GetSurroundingPointsInBounds(Point curPt)
    {
      List<Point> surroundingPts = new List<Point>();
      int[] xs = { 1, 1, 1, 0, 0, -1, -1, -1 };
      int[] ys = { 1, 0, -1, 1, -1, 1, 0, -1 };

      for (int i = 0; i < xs.Length; i++)
      {
        Point testPt = new Minesweeper.Point(curPt.x + xs[i], curPt.y + ys[i]);
        if (InBounds(testPt))
        {
          surroundingPts.Add(testPt);
        }
      }

      return surroundingPts;
    }

    public bool IsEndGame_Win()
    {
      int flagCounter = 0;
      int clickedButtonCounter = 0;
      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          Button b = ((Button)mineButtonPanel.Controls[i.ToString() + j.ToString()]);
          if (flagHere[i, j]) flagCounter++;
          else if (!b.Enabled && !flagHere[i,j]) clickedButtonCounter++;
        }
      }

      if (flagCounter + clickedButtonCounter == (NUM_ROWS * NUM_COLS)) return true;
      else return false;
    }
    
    public void ZeroBFS(Point curLoc)
    {
      bool[,] beenHere = new bool[NUM_ROWS, NUM_COLS];
      beenHere[curLoc.x, curLoc.y] = true;
      Queue<Point> q = new Queue<Point>();
      q.Enqueue(curLoc);

      while (q.Count != 0)
      {
        Point curSearchPt = q.Dequeue();

        List<Point> surroundingPts = GetSurroundingPointsInBounds(curSearchPt);

        foreach (Point p in surroundingPts)
        {
          Button b = ((Button)mineButtonPanel.Controls[p.x.ToString() + p.y.ToString()]);
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
    #endregion

  }
}
