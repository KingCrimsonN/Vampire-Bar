using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartPanel : MonoBehaviour
{

  public TMP_Text corpse1;
  public TMP_Text corpse2;
  public TMP_Text corpse3;

  public void Init(string text1, string text2, string text3)
  {
    corpse1.text += text1;
    corpse2.text += text2;
    corpse3.text += text3;
  }

}
