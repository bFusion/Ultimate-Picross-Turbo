using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBox:MonoBehaviour
{
  public Sprite Empty, Wrong, Marked;
  private SpriteRenderer BoxSprite;
  public Vector2Int BoxPosition { get; private set; }

  private void Awake()
  {
    BoxSprite = GetComponent<SpriteRenderer>();
  }

  public void SetPosition(Vector2Int _boxPosition)
  {
    BoxPosition = _boxPosition;
  }

  public void SetState(int _state)
  {
    if (_state == 1)
    {
      BoxSprite.sprite = Marked;
    }
  }

  public void SetColor(Color _color)
  {
    BoxSprite.color = _color;
  }
}
