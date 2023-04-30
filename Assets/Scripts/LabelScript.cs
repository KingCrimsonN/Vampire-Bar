using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LabelScript : MonoBehaviour
{
  public TMP_Text labelText;
  public Button serveButton;
  public DialogueManager dm;
  public int index;
  // Start is called before the first frame update
  void Start()
  {
    labelText.text = dm.GetVariable("body" + index);
    serveButton.onClick.AddListener(() => dm.SelectChoice(index - 1));
    serveButton.onClick.AddListener(() => { dm.isChoosing = false; });
  }

  // Update is called once per frame
  void Update()
  {

  }
}
