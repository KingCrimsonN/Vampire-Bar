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

  // Other Buttons
  public List<Button> corpseButtons;
  public GameObject backRoomButton;

  public StartPanel startPanel;

  // Start is called before the first frame update
  void Start()
  {
    story = new Story(inkFile.text);
    // foreach (var state in story.variablesState)
    // {
    //   string value = (string)state;
    //   if (value != null)
    //     print("value");
    // }
    // Debug.Log(inkFile.text);


    choiceSelected = null;
    startPanel.Init(GetVariable("body1"), GetVariable("body2"), GetVariable("body3"));
  }

  public string GetVariable(string name)
  {
    return (string)story.variablesState[name];
  }

  // Update is called once per frame
  void Update()
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
    // dialogueNPC = null;

    // Clear UI references after canvas is removed from scene
    // dialogueCanvas = null;
    // textDisplay = null;
    // choiceButtons = null;
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
    dialogueText.text = "";
    foreach (char letter in sentence.ToCharArray())
    {
      dialogueText.text += letter;
      yield return null;
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
        case "anim":
          break;
        case "player":
          break;
        case "backroom":
          print("BACKROOM");
          CorpseButtons();
          break;
      }
    }
  }

  void CorpseButtons()
  {
    isChoosing = true;
    backRoomButton.SetActive(true);
    //   if (!isChoosing)
    //   {
    //     isChoosing = true;
    //     // Create a UI button for every choice availbale
    //     for (int choiceNum = 0; choiceNum < story.currentChoices.Count; choiceNum++)
    //     {
    //       Choice inkChoice = story.currentChoices[choiceNum];

    //       corpseButtons[choiceNum].SetActive(true);
    //       // Add an OnClick listener from code, since we can't access the button component in inspector during gameplay
    //       // This is an Anonymous Function which is only called when the button is clicked (onCLick). When called, it performs
    //       // what's after the arrow "=>". In this case, it's calling SelectChoice

    //     }
    //   }
  }

  //   void SelectCorpse(int choiceIndex)
  //   {
  //     // Tell the story which choice player has made
  //     story.ChooseChoiceIndex(choiceIndex);

  //     RefreshScreen();
  //     isChoosing = false;

  //     // Since player made their choice, we can continue with the story
  //     ProgressDialogue();
  //   }
}
