using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameplay : MonoBehaviour
{
  public static PuzzleGameplay instance;
  public PuzzleBox PuzzleBox;
  public GameObject PuzzleBucket { get; private set; }
  public GameObject TextBucket { get; private set; }
  private int Hearts;
  private int[][] PuzzleGrid = {
    new int[] {1,0,1,0,1},
    new int[] {0,0,1,1,0},
    new int[] {0,1,1,0,0},
    new int[] {1,0,1,1,0},
    new int[] {1,1,1,0,1}
  };
  private PuzzleBox[,] PuzzleBoxes;
  public HintText HintRowText;
  public HintText HintColText;
  public HintText[] RowTexts;
  public HintText[] ColTexts;

  private void Awake()
  {
    instance = this;
  }

  private void Start()
  {
    Hearts = 3;

    PuzzleBucket = new GameObject("PuzzleBucket");
    PuzzleBucket.transform.parent = transform;

    TextBucket = new GameObject("TextBucket");
    TextBucket.transform.parent = transform;

    RowTexts = new HintText[PuzzleGrid.Length];
    ColTexts = new HintText[PuzzleGrid[0].Length];
    PuzzleBoxes = new PuzzleBox[PuzzleGrid.Length,PuzzleGrid[0].Length];
    
    for (int y=PuzzleGrid.Length-1; y>=0; y--)
    {
      for (int x=0; x<PuzzleGrid[y].Length; x++)
      {
        PuzzleBox tempBox = Instantiate(PuzzleBox, PuzzleBucket.transform);
        tempBox.transform.localPosition = new Vector3(x, y, 0);
        tempBox.SetPosition(new Vector2Int(x, y));
        PuzzleBoxes[y, x] = tempBox;
      }
      HintText tempRowText = Instantiate(HintRowText, TextBucket.transform);
      tempRowText.transform.localPosition = new Vector3(-0.5f, y, 0);
      RowTexts[y] = tempRowText;
      UpdateRowText(y);
    }
    for (int x = 0; x < PuzzleGrid[0].Length; x++)
    {
      HintText tempColText = Instantiate(HintColText, TextBucket.transform);
      tempColText.transform.localPosition = new Vector3(x, PuzzleGrid.Length - 1f, 0);
      ColTexts[x] = tempColText;
      UpdateColText(x);
    }
    transform.position = new Vector3(-3, -3, 0);
  }

  public int CheckPuzzle(Vector2Int _checkPos)
  {
    int result = PuzzleGrid[_checkPos.y][_checkPos.x];
    if (result == 1)
    {
      PuzzleGrid[_checkPos.y][_checkPos.x] = 2;
    }
    if (result == 0 && Hearts > 0)
    {
      Hearts--;
    }
    if (Hearts == 0)
    {
      Debug.LogError("YOU LOSE! :(");
    }
    UpdateRowText(_checkPos.y);
    UpdateColText(_checkPos.x);
    return (result);
  }

  private void UpdateRowText(int _rowNum)
  {
    int[] _row = PuzzleGrid[_rowNum];
    string rowText = "";
    int rowCount = 0;
    bool continuous = false;
    bool completed = true;
    for (int x = 0; x < _row.Length; x++)
    {
      if (_row[x] == 0)
      {
        if (rowCount > 0)
        {
          if (continuous)
          {
            rowText += "<color=#999999><i>" + rowCount.ToString() + "</i></color>  ";
          } else
          {
            rowText += rowCount.ToString() + "  ";
            completed = false;
          }
          rowCount = 0;
          continuous = false;
        }
      } else
      {
        if (rowCount == 0 && _row[x] == 2)
        {
          continuous = true;
        }
        if (_row[x] == 1)
        {
          continuous = false;
        }
        rowCount++;
      }
    }

    if (rowCount > 0)
    {
      if (continuous)
      {
        rowText += "<color=#999999><i>" + rowCount.ToString() + "</i></color>  ";
      } else
      {
        rowText += rowCount.ToString() + "  ";
        completed = false;
      }
    }
    if (completed)
    {
      for (int x=0; x<PuzzleGrid[0].Length; x++)
      {
        if (_row[x] == 0)
        {
          PuzzleBoxes[_rowNum, x].SetComplete();
        }
      }
    }
    RowTexts[_rowNum].SetText(rowText);
  }

  private void UpdateColText(int _colNum)
  {
    int[] _col = new int[PuzzleGrid.Length];
    for (int i=0; i<PuzzleGrid.Length; i++)
    {
      _col[i] = PuzzleGrid[i][_colNum];
    }

    string rowText = "";
    int rowCount = 0;
    bool continuous = false;
    bool completed = true;
    for (int y = _col.Length-1; y >= 0; y--)
    {
      if (_col[y] == 0)
      {
        if (rowCount > 0)
        {
          if (continuous)
          {
            rowText += "<color=#999999><i>" + rowCount.ToString() + "</i></color>\n";
          } else
          {
            rowText += rowCount.ToString() + "\n";
            completed = false;
          }
          rowCount = 0;
          continuous = false;
        }
      } else
      {
        if (rowCount == 0 && _col[y] == 2)
        {
          continuous = true;
        }
        if (_col[y] == 1)
        {
          continuous = false;
        }
        rowCount++;
      }
    }

    if (rowCount > 0)
    {
      if (continuous)
      {
        rowText += "<color=#999999><i>" + rowCount.ToString() + "</i></color>\n";
      } else
      {
        rowText += rowCount.ToString() + "\n";
        completed = false;
      }
    }

    if (completed)
    {
      for (int y = 0; y < PuzzleGrid[0].Length; y++)
      {
        if (_col[y] == 0)
        {
          PuzzleBoxes[y, _colNum].SetComplete();
        }
      }
    }
    ColTexts[_colNum].SetText(rowText);
  }
}
