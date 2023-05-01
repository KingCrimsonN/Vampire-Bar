using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class DialogueManager : MonoBehaviour
{
  // Ink variables
  public TextAsset inkFile;
  public bool isChoosing = false;
  static Story story;
  static Choice choiceSelected;
  List<string> tags;
  // Dialogue Panel
  public GameObject dialoguePanel;
  public TMP_Text dialogueText;
  public GameObject optionsPanel;
  public Button customButton;
  public TMP_Text moneyText;

  // Other Buttons
  public List<Button> corpseButtons;
  public GameObject backRoomButton;

  public StartPanel startPanel;
  // End Stuff
  public GameObject endPanel;
  public TMP_Text endText;
  // Bools
  private bool isEnd;
  private bool busy = false;

  public VampireManager vampireManager;
  public AudioManager audioManager;

  // Start is called before the first frame update
  void Start()
  {
    story = new Story(inkFile.text);
    choiceSelected = null;
    moneyText.text = "Money: " + story.variablesState["money"];
    startPanel.Init(GetVariable("body1"), GetVariable("body2"), GetVariable("body3"));
  }

  public string GetVariable(string name)
  {
    return (string)story.variablesState[name];
  }

  // Update is called once per frame
  void Update()
  {
    if (!busy)
    {
      if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && story != null)
      {
        if (story.canContinue)
        {
          // dialogueText.text = story.Continue();
          ProgressDialogue();
        }
        if (story.currentChoices.Count > 0)
        {
          if (!isChoosing)
          {
            isChoosing = true;
            // Create a UI button for every choice availbale
            for (int choiceNum = 0; choiceNum < story.currentChoices.Count; choiceNum++)
            {
              Choice inkChoice = story.currentChoices[choiceNum];
              Button choiceButton = CreateChoiceButton(inkChoice.text);


              // Add an OnClick listener from code, since we can't access the button component in inspector during gameplay
              // This is an Anonymous Function which is only called when the button is clicked (onCLick). When called, it performs
              // what's after the arrow "=>". In this case, it's calling SelectChoice
              choiceButton.onClick.AddListener(() => SelectChoice(inkChoice.index));
            }
          }
        }
        else if (!story.canContinue)
        {
          EndDialogue();
        }
      }
    }
  }

  Button CreateChoiceButton(string choiceText)
  {
    // Instantiate the button prefab and make it a child of Choice Buttons
    Button button = Instantiate(customButton, optionsPanel.transform);

    // Make the button's text that of the dialogue choice
    button.GetComponentInChildren<TMP_Text>().text = choiceText;
    return button;
  }

  public void SelectChoice(int choiceIndex)
  {
    // Tell the story which choice player has made
    story.ChooseChoiceIndex(choiceIndex);


    RefreshScreen();
    isChoosing = false;

    // Since player made their choice, we can continue with the story
    ProgressDialogue();
  }

  void EndDialogue()
  {
    RefreshScreen();
    dialoguePanel.SetActive(false);
    ClearReferences();
  }

  void ClearReferences()
  {
    // Clear story references
    story = null;
  }

  void RefreshScreen()
  {
    // Clear displayed choices
    foreach (Transform choiceButton in optionsPanel.transform)
    {
      Destroy(choiceButton.gameObject);
    }

    // Clear displayed text
    // textDisplay.text = "";
  }

  public void ProgressDialogue()
  {
    string currentSentence = story.Continue();
    ParseTags();
    moneyText.text = "Money: " + story.variablesState["money"];
    // dialogueText.text = currentSentence;
    StopAllCoroutines();
    StartCoroutine(TypeSentence(currentSentence));

  }

  public static void SetDecision(object element)
  {
    choiceSelected = (Choice)element;
    story.ChooseChoiceIndex(choiceSelected.index);
  }

  void AdvanceFromDecision()
  {
    optionsPanel.SetActive(false);
    for (int i = 0; i < optionsPanel.transform.childCount; i++)
    {
      Destroy(optionsPanel.transform.GetChild(i).gameObject);
    }
    choiceSelected = null;
    ProgressDialogue();
  }

  IEnumerator TypeSentence(string sentence)
  {
    // Debug.Log(sentence);
    if (isEnd)
    {
      dialogueText.text = sentence;
      yield return null;
    }
    dialogueText.text = "";
    if (busy)
    {
      yield return new WaitForSeconds(3f);
      vampireManager.VampireStop();
      busy = false;
    }
    foreach (char letter in sentence.ToCharArray())
    {
      dialogueText.text += letter;
      yield return new WaitForSeconds(0.02f);
    }
    yield return null;
    // Character
  }

  // Parsing the Ink tags
  void ParseTags()
  {
    tags = story.currentTags;
    foreach (string t in tags)
    {
      string prefix = t.Split(' ')[0];
      //   string param = t.Split(' ')[1];

      switch (prefix.ToLower())
      {
        case "black":
          EndScreen();
          break;
        case "char":
          busy = true;
          StartCoroutine(ChangeVampire());
          // ChangeVampire();
          break;
        case "backroom":
          print("BACKROOM");
          // vampireManager.Backroom();
          CorpseButtons();
          break;
      }
    }
  }

  void EndScreen()
  {
    isEnd = true;
    endPanel.SetActive(true);
    audioManager.Stop();
  }

  IEnumerator ChangeVampire()
  {
    vampireManager.VampireOut();
    yield return null;
  }

  void CorpseButtons()
  {
    isChoosing = true;
    backRoomButton.SetActive(true);
  }

}
