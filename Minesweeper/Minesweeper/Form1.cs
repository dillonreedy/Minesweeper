using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
  public partial class Form1 : Form
  {
    #region Private Variables

    private int NUM_COLS, NUM_ROWS, NUM_MINES;
    private bool[,] isMineHere;
    private char[,] userState;
    private int[,] mineCount;
    private bool[,] flagHere;
    private HelperFunctions _helperFunctions;
    private InitializationFunctions _initializationFunctions;
    #endregion

    #region Form Constructor
    public Form1()
    {
      InitializeComponent();
      _helperFunctions = new HelperFunctions();
      _initializationFunctions = new InitializationFunctions();
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
      btnPlayerState.Text = Constants.USER_PLAYING;
      numUpDownMines.Maximum = (numUpDownCols.Value * numUpDownRows.Value) - 1;
      NUM_COLS = Int32.Parse(numUpDownCols.Value.ToString());
      NUM_ROWS = Int32.Parse(numUpDownRows.Value.ToString());
      NUM_MINES = Int32.Parse(numUpDownMines.Value.ToString());

      InitializeMineButtonPanel(); // Fills the button panel with buttons based on the number of rows and columns input by the user
      isMineHere = _initializationFunctions.InitializeMineLocationBoard(NUM_ROWS, NUM_COLS, NUM_MINES);
      mineCount = _initializationFunctions.InitializeMineCountBoard(NUM_ROWS, NUM_COLS, NUM_MINES, isMineHere);
      userState = _initializationFunctions.InitializeUserStateBoard(NUM_ROWS, NUM_COLS);
      flagHere = new bool[NUM_ROWS, NUM_COLS];
    }

    public void InitializeMineButtonPanel()
    {
      mineButtonPanel.Controls.Clear();
      if (NUM_COLS * Constants.MINE_BUTTON_SIZE_X > mineButtonPanel.Size.Width) mineButtonPanel.Size = new Size((NUM_COLS * Constants.MINE_BUTTON_SIZE_X) + 50, mineButtonPanel.Size.Height);
      if (NUM_ROWS * Constants.MINE_BUTTON_SIZE_Y > mineButtonPanel.Size.Height) mineButtonPanel.Size = new Size(mineButtonPanel.Size.Width, (NUM_ROWS * Constants.MINE_BUTTON_SIZE_Y) + 20);

      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          Button b = new Button
          {
            Enabled = true,
            Name = i.ToString() + j.ToString(),
            Width = Constants.MINE_BUTTON_SIZE_X,
            Height = Constants.MINE_BUTTON_SIZE_Y,
            Top = i * Constants.MINE_BUTTON_SIZE_Y,
            Left = j * Constants.MINE_BUTTON_SIZE_X
          };
          b.MouseDown += btnMine_MouseDown;
          mineButtonPanel.Controls.Add(b);
        }
      }
    }
    #endregion

    #region End Game Actions

    public void EndGame_Loss(Button b)
    {
      btnPlayerState.Text = Constants.USER_LOSES;
      b.BackColor = Color.Red;

      for (int i = 0; i < NUM_ROWS; i++)
      {
        for (int j = 0; j < NUM_COLS; j++)
        {
          Button thisBtn = ((Button)mineButtonPanel.Controls[i.ToString() + j.ToString()]);
          if (isMineHere[i, j])
          {
            thisBtn.Image = Properties.Resources.mine;
          }
          thisBtn.Enabled = false;
        }
      }
    }

    public void EndGame_Win()
    {
      btnPlayerState.Text = Constants.USER_WINS;

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
      Point curLoc = _helperFunctions.getPointFromButton(b);

      if (!flagHere[curLoc.x, curLoc.y])
      {
        if (isMineHere[curLoc.x, curLoc.y]) EndGame_Loss(b);
        else
        {
          if (mineCount[curLoc.x, curLoc.y] != 0)
          {
            b.Text = mineCount[curLoc.x, curLoc.y].ToString();
            userState[curLoc.x, curLoc.y] = mineCount[curLoc.x, curLoc.y].ToString()[0];
          }
          else _helperFunctions.ZeroBFS(NUM_ROWS, NUM_COLS, curLoc, ref mineButtonPanel, mineCount, ref userState);

          b.Enabled = false;
          if (_helperFunctions.IsEndGame_Win(NUM_ROWS, NUM_COLS, NUM_MINES, ref mineButtonPanel, flagHere)) EndGame_Win();
          else
          {
            RedGreenBoard rgBoard = new RedGreenBoard(NUM_ROWS, NUM_COLS, userState);
            _helperFunctions.ColorPanel(NUM_ROWS, NUM_COLS, rgBoard.GetRedGreenBoard(), ref mineButtonPanel);
          }
        }
      }
    }

    public void RightMouseButtonClick(Button b)
    {
      Point curLoc = _helperFunctions.getPointFromButton(b);

      if (!flagHere[curLoc.x, curLoc.y])
      {
        b.Image = Properties.Resources.flag;
        flagHere[curLoc.x, curLoc.y] = true;
        if (_helperFunctions.IsEndGame_Win(NUM_ROWS, NUM_COLS, NUM_MINES, ref mineButtonPanel, flagHere)) EndGame_Win();
      }
      else
      {
        b.Image = null;
        flagHere[curLoc.x, curLoc.y] = false;
      }
    }
    #endregion
  }
}
