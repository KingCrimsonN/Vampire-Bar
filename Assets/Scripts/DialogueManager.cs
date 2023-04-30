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
  public GameObject customButton;
  public bool isTalking = false;

  static Story story;
  static Choice choiceSelected;

  // Start is called before the first frame update
  void Start()
  {
    story = new Story(inkFile.text);
    dialogueText.text = "HEHEHEH";
    choiceSelected = null;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space) && story != null)
    {
      if (story.canContinue)
      {
        ProgressDialogue();

        if (story.currentChoices.Count > 0)
        {
          // Create a UI button for every choice availbale
          for (int choiceNum = 0; choiceNum < story.currentChoices.Count; choiceNum++)
          {
            Debug.Log("CHOICES");
            // Choice inkChoice = story.currentChoices[choiceNum];
            // Button choiceButton = CreateChoiceButton(inkChoice.text);


            // Add an OnClick listener from code, since we can't access the button component in inspector during gameplay
            // This is an Anonymous Function which is only called when the button is clicked (onCLick). When called, it performs
            // what's after the arrow "=>". In this case, it's calling SelectChoice
            // choiceButton.onClick.AddListener(() => SelectChoice(inkChoice.index));
          }
        }
      }
      else
      {
        EndDialogue();
      }

    }
  }

  void EndDialogue()
  {
    // RefreshScreen();
    dialoguePanel.SetActive(false);
    // ClearReferences();
  }

  //   void RefreshScreen()
  //   {
  //     // Clear displayed choices
  //     foreach (Transform choiceButton in choiceButtons.transform)
  //     {
  //       Destroy(choiceButton.gameObject);
  //     }

  //     // Clear displayed text
  //     textDisplay.text = "";
  //   }

  public void ProgressDialogue()
  {

    string currentSentence = story.Continue();
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

  //   public static void SetDecision(object element)
  //   {
  //     choiceSelected = (Choice)element;
  //     story.ChooseChoiceIndex(choiceSelected.index);
  //   }

  //   void AdvanceFromDecision()
  //   {
  //     optionsPanel.setActive(false);
  //     for (int i = 0; i < optionsPanel.transform.childCount; i++)
  //     {
  //       Destroy(optionsPanel.transform.GetChild(i).gameObject);
  //     }
  //     choiceSelected = null;
  //     ProgressDialogue();
  //   }

  IEnumerator TypeSentence(string sentence)
  {
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
