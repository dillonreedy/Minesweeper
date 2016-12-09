﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
  public class RedGreenBoard
  {
    private int R, C;
    private char[,] board;

    public RedGreenBoard(int givenR, int givenC, char[,] givenBoard)
    {
      R = givenR;
      C = givenC;
      board = new char[R, C];
      for (int i = 0; i < givenR; i++)
      {
        for (int j = 0; j < givenC; j++)
        {
          board[i, j] = givenBoard[i, j];
        }
      }
    }

    public char[,] GetRedGreenBoard()
    {
      List<Point> numberedPts = new List<Point>();

      for (int i = 0; i < R; i++)
      {
        for (int j = 0; j < C; j++)
        {
          if (!board[i, j].Equals('0') && !board[i, j].Equals('U'))
          {
            numberedPts.Add(new Minesweeper.Point(i, j));
          }
        }
      }

      return ColorInBoard(numberedPts);
    }

    public char[,] ColorInBoard(List<Point> numberedPts)
    {
      bool changed = true;
      while (changed)
      {
        changed = false;
        foreach (Point p in numberedPts)
        {
          MarkAllMines(p, ref changed);
          MarkAllClickables(p, ref changed);
        }
      }

      return board;
    }

    public void MarkAllClickables(Point p, ref bool changed)
    {
      List<Point> surroundingPts = GetSurroundingPointsInBounds(p);

      int numOnBoard = Int32.Parse(board[p.x, p.y] + String.Empty);
      int numUnknownPts = CountNumberOfCharOnBoard(surroundingPts, 'U');
      int numMinePts = CountNumberOfCharOnBoard(surroundingPts, 'R');

      if (numOnBoard == numMinePts && numUnknownPts > 0)
      {
        foreach (Point q in surroundingPts)
        {
          if (board[q.x, q.y] == 'U')
          {
            board[q.x, q.y] = 'G';
            changed = true;
          }
        }
      }
    }

    public void MarkAllMines(Point p, ref bool changed)
    {
      List<Point> surroundingPts = GetSurroundingPointsInBounds(p);

      int numOnBoard = Int32.Parse(board[p.x, p.y] + String.Empty);
      int numUnknownPts = CountNumberOfCharOnBoard(surroundingPts, 'U');
      int numMinePts = CountNumberOfCharOnBoard(surroundingPts, 'R');

      if (numOnBoard == (numUnknownPts + numMinePts))
      {
        foreach (Point q in surroundingPts)
        {
          if (board[q.x, q.y] == 'U')
          {
            board[q.x, q.y] = 'R';
            changed = true;
          }
        }
      }
    }

    /// <summary>
    /// Given a list of points that are in bounds, it returns back the number of that character.
    /// </summary>
    /// <param name="givenPts"></param>
    /// <returns></returns>
    public int CountNumberOfCharOnBoard(List<Point> givenPts, char ch)
    {
      int chCount = 0;
      foreach (Point p in givenPts)
        if (board[p.x, p.y] == ch) chCount++;

      return chCount;
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

    public bool InBounds(Point p)
    {
      return (0 <= p.x && p.x < R && 0 <= p.y && p.y < C);
    }
  }
}