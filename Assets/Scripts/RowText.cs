using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowText : MonoBehaviour
{
  public void SetText(string _text)
  {
    GetComponent<TextMesh>().text = _text;
  }
}
