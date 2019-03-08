using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBox:MonoBehaviour
{
  public Sprite Empty, Wrong, Marked;
  private SpriteRenderer BoxSprite;
  public bool isMarked { get; private set; }
  public Vector2Int BoxPosition { get; private set; }

  private void Awake()
  {
    BoxSprite = GetComponent<SpriteRenderer>();    
  }

  public void SetPosition(Vector2Int _boxPosition)
  {
    BoxPosition = _boxPosition;
  }

  public void SetColor(Color _color)
  {
    BoxSprite.color = _color;
  }

  public void SetComplete()
  {
    if (BoxSprite.sprite != Wrong)
    {
      BoxSprite.sprite = Marked;
    }
    GetComponent<Collider2D>().enabled = false;
  }

  private void OnMouseOver()
  {
    if (Input.GetMouseButtonDown(0))
    {
      int clickResult = PuzzleGameplay.instance.CheckPuzzle(BoxPosition);
      if (clickResult == 0)
      {
        BoxSprite.sprite = Wrong;
      } else
      {
        SetColor(Color.black);
      }
      GetComponent<Collider2D>().enabled = false;
    }
    if (Input.GetMouseButtonDown(1))
    {
      isMarked = !isMarked;
      if (isMarked)
      {
        BoxSprite.sprite = Marked;
      } else
      {
        BoxSprite.sprite = Empty;
      }
    }
  }
}
