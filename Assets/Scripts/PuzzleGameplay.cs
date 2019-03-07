using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameplay : MonoBehaviour
{
  public static PuzzleGameplay instance;
  public PuzzleBox PuzzleBox;
  public RowText RowText;
  public GameObject PuzzleBucket { get; private set; }
  public GameObject TextBucket { get; private set; }
  private int Hearts;
  private int[,] PuzzleGrid = new int[,]
  {
    {1,0,1,0,1},
    {0,0,1,1,0},
    {0,1,1,0,0},
    {1,0,1,1,0},
    {1,0,1,0,1}
  };

  int[][] jaggedArray3 =
{
    new int[] { 1, 3, 5, 7, 9 },
    new int[] { 0, 2, 4, 6 },
    new int[] { 11, 22 }
};

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
    
    for (int y=PuzzleGrid.GetLength(0)-1; y>=0; y--)
    {
      for (int x=0; x<PuzzleGrid.GetLength(1); x++)
      {
        PuzzleBox tempBox = Instantiate(PuzzleBox, PuzzleBucket.transform);
        tempBox.transform.localPosition = new Vector3(x, y, 0);
        tempBox.SetPosition(new Vector2Int(x, y));

        RowText tempRowText = Instantiate(RowText, TextBucket.transform);
        tempRowText.SetText(RowCountText(PuzzleGrid[y]));
        tempRowText.transform.localPosition = new Vector3(-1f, y, 0);
      }
    }
  }

  public int CheckPuzzle(Vector2Int _checkPos)
  {
    int result = PuzzleGrid[_checkPos.y, _checkPos.x];
    if (result == 0 && Hearts > 0)
    {
      Hearts--;
    }
    if (Hearts == 0)
    {
      Debug.LogError("YOU LOSE! :(");

    }
    return (result);
  }

  private string RowCountText(int[] _row)
  {
    string rowText = "";
    int rowCount = 0;
    for (int x=0; x<_row.Length; x++)
    {
      if (_row[x] == 0)
      {
        if (rowCount > 0)
        {
          rowText += rowCount + "  ";
        }
      } else
      {

      }
    }
    return (rowText);
  }
}
