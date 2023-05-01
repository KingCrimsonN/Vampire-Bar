using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VampireManager : MonoBehaviour
{

  public Image Vampire;

  public Animator animator;

  public List<string> vampires;

  public List<Sprite> vampireTextures;
  private int vampireIndex = -1;

  public void VampireOut()
  {
    animator.SetBool("Served", true);
  }

  public void ChangeVampire()
  {
    vampireIndex++;
    Vampire.sprite = vampireTextures[vampireIndex];
    animator.SetBool("Served", false);
  }

  //   public Sprite 
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
