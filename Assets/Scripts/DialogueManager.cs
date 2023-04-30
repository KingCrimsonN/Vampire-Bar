using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class DialogueManager : MonoBehaviour
{
  public TextAsset inkFile;
  public GameObject dialoguePanel;

  public TMP_Text dialogueText;
  public GameObject optionsPanel;
  public Button customButton;
  public bool isTalking = false;

  static Story story;
  static Choice choiceSelected;

  // Start is called before the first frame update
  void Start()
  {
    story = new Story(inkFile.text);
    Debug.Log(inkFile.text);

    choiceSelected = null;
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

        if (story.currentChoices.Count > 0)
        {
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

  void SelectChoice(int choiceIndex)
  {
    // Tell the story which choice player has made
    story.ChooseChoiceIndex(choiceIndex);

    RefreshScreen();

    // Since player made their choice, we can continue with the story
    ProgressDialogue();
  }

  void EndDialogue()
  {
    RefreshScreen();
    dialoguePanel.SetActive(false);
    // ClearReferences();
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
    // dialogueText.text = currentSentence;
    StopAllCoroutines();
    StartCoroutine(TypeSentence(currentSentence));

  }

  //   IEnumerator ShowChoices()
  //   {
  //     Debug.Log("WE HAVE SOME CHOICES");
  //     List<Choice> choices = story.currentChoices;

  //     for (int i = 0; i < choices.Count; i++)
  //     {
  //       GameObject temp = Instantiate(customButton, optionsPanel.transform);
  //       temp.transform.GetChild(0).GetComponent<TMP_Text>().text = choices[i].text;
  //       temp.AddComponent<Selectable>();
  //       temp.GetComponent<Selectable>().element = choices[i];
  //       temp.GetComponent<Button>.onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
  //     }

  //     optionsPanel.setActive(true);

  //     yield return new WaitUntil(() => { return choiceSelected != null; });
  //     AdvanceFromDecision();
  //   }

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
    Debug.Log(sentence);
    dialogueText.text = "";
    foreach (char letter in sentence.ToCharArray())
    {
      dialogueText.text += letter;
      yield return null;
    }
    yield return null;
    // Character
  }
}
