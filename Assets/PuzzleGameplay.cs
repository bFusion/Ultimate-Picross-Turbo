using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameplay : MonoBehaviour
{
  public PuzzleBox PuzzleBox;
  private int[,] PuzzleGrid = new int[,]
  {
    {1,0,1,0,1},
    {0,0,1,1,0},
    {0,1,1,0,0},
    {1,0,1,1,0},
    {1,0,1,0,1}
  };

  private void Start()
  {
    for(int i=0; i<PuzzleGrid.GetLength(0); i++)
    {
      for(int j=0; j<PuzzleGrid.GetLength(1); j++)
      {
        PuzzleBox tempBox = Instantiate(PuzzleBox, this.transform);
        tempBox.transform.localPosition = new Vector3(j, i, 0);
        tempBox.SetPosition(new Vector2Int(j, i));
        tempBox.SetState(PuzzleGrid[j, i]);
      }
    }
  }
}
