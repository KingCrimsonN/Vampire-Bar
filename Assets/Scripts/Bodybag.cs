using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bodybag : MonoBehaviour
{

  [SerializeField]
  private string text;
  [SerializeField]
  private TMP_Text UI_text;
  [SerializeField]
  private string type;
  [SerializeField]
  private ServingManager sm;

  public void Show()
  {
    UI_text.text = text;
  }

  public void Serve()
  {
    sm.blood_type = type;
  }

}
