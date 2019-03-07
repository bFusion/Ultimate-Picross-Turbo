using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameplay : MonoBehaviour
{
  public static PuzzleGameplay instance;
  public PuzzleBox PuzzleBox;
  public GameObject PuzzleBucket;
  private int[,] PuzzleGrid = new int[,]
  {
    {1,0,1,0,1},
    {0,0,1,1,0},
    {0,1,1,0,0},
    {1,0,1,1,0},
    {1,0,1,0,1}
  };

  private void Awake()
  {
    instance = this;
  }

  private void Start()
  {
    PuzzleBucket = new GameObject("PuzzleBucket");
    PuzzleBucket.transform.parent = transform;

    for (int y = PuzzleGrid.GetLength(0) - 1; y >= 0; y--)
    {
      for(int x = 0; x < PuzzleGrid.GetLength(1); x++)
      {
        PuzzleBox tempBox = Instantiate(PuzzleBox, PuzzleBucket.transform);
        tempBox.transform.localPosition = new Vector3(x, y, 0);
        tempBox.SetPosition(new Vector2Int(x, y));
      }
    }
  }

  public int CheckPuzzle(Vector2Int _checkPos)
  {
    return (PuzzleGrid[_checkPos.y, _checkPos.x]);
  }
}
