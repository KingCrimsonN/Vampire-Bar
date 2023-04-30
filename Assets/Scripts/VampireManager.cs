using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VampireManager : MonoBehaviour
{

  public Image Vampire;

  public List<string> vampires;

  public List<Sprite> vampireTextures;

  public void ChangeVampire()
  {
    Vampire.sprite = vampireTextures[1];
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
