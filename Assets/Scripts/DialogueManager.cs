using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
  // Ink variables
  [Header("Ink Variables")]
  public TextAsset inkFile;
  public bool isChoosing = false;
  static Story story;
  static Choice choiceSelected;
  List<string> tags;
  // Dialogue Panel
  [Header("Dialogue Variables")]
  public GameObject dialoguePanel;
  public TMP_Text dialogueText;
  public GameObject optionsPanel;
  public Button customButton;

  // Money Variables
  [Header("Economy Variables")]
  public TMP_Text moneyText;
  public Animator moneyAnimator;

  // Other Buttons
  [Header("Other Buttons")]
  public List<Button> corpseButtons;
  public GameObject backRoomButton;

  // End Stuff
  [Header("Black Screen")]
  public GameObject endPanel;
  public TMP_Text endText;
  public GameObject endOptions;
  public Button endButton;
  // Bools
  private bool dayEnd;
  private bool income;
  private bool busy = false;

  //Managers 
  [Header("Managers")]
  public VampireManager vampireManager;
  public AudioManager audioManager;

  // Start is called before the first frame update
  void Start()
  {
    story = new Story(inkFile.text);
    choiceSelected = null;
    moneyText.text = "Money: " + story.variablesState["money"];
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
    Button button;
    // Instantiate the button prefab and make it a child of Choice Buttons
    if (dayEnd)
      button = Instantiate(endButton, endOptions.transform);
    else
      button = Instantiate(customButton, optionsPanel.transform);

    // Make the button's text that of the dialogue choice
    button.GetComponentInChildren<TMP_Text>().text = choiceText;
    return button;
  }

  Button CreateEndButton()
  {
    print("FUCK");
    Button button;
    endPanel.transform.GetChild(0).transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
    button = Instantiate(endButton, endPanel.transform.GetChild(0).transform.GetChild(1).transform);

    button.GetComponentInChildren<TMP_Text>().text = "Play Again";
    button.onClick.AddListener(delegate { SceneManager.LoadScene(0); });

    return button;
  }

  public void SelectChoice(int choiceIndex)
  {
    // Tell the story which choice player has made
    story.ChooseChoiceIndex(choiceIndex);
    audioManager.PlaySound(0);
    if (backRoomButton.activeSelf)
    {
      backRoomButton.SetActive(false);
    }


    RefreshScreen();
    isChoosing = false;

    // Since player made their choice, we can continue with the story
    ProgressDialogue();
  }

  IEnumerator GainMoney()
  {
    moneyAnimator.SetBool("Income", true);
    yield return new WaitForSeconds(0.5f);
    moneyAnimator.SetBool("Income", false);
    income = false;
    yield return null;
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
    if (dayEnd)
    {
      foreach (Transform choiceButton in endOptions.transform)
      {
        Destroy(choiceButton.gameObject);
      }
      return;
    }
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
    audioManager.PlaySound(1);
    string currentSentence = story.Continue();
    ParseTags();
    moneyText.text = "Money: " + story.variablesState["money"];
    StopAllCoroutines();
    if (income)
    {
      StartCoroutine(GainMoney());
    }
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
    if (dayEnd)
    {
      endText.text = sentence;
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
      yield return new WaitForSeconds(0.01f);
    }
    yield return null;
  }

  // Parsing the Ink tags
  void ParseTags()
  {
    tags = story.currentTags;
    foreach (string t in tags)
    {
      string prefix = t.Split(' ')[0];
      // In case we'll make it bigger, parameters will be useful
      //   string param = t.Split(' ')[1];

      switch (prefix.ToLower())
      {
        case "black":
          EndScreen();
          break;
        case "bar":
          NewDay();
          break;
        case "char":
          busy = true;
          StartCoroutine(ChangeVampire());
          break;
        case "sound":
          audioManager.PlaySound(2);
          income = true;
          break;
        case "backroom":
          CorpseButtons();
          break;
        case "game_end":
          Button endButton = CreateEndButton();
          break;
      }
    }
  }

  void EndScreen()
  {
    dayEnd = true;
    endPanel.SetActive(true);
    vampireManager.MakeInvisible();
    audioManager.Stop();
  }

  void NewDay()
  {
    dayEnd = false;
    endPanel.SetActive(false);
    audioManager.Play();
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
