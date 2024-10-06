using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_MiniGameStart : MonoBehaviour
{
    public GameObject miniGame;

    public GameObject dogInGame;

    public List<GameObject> devilDogs;

    public GameObject traitCompare;

    public void MGOn()
    {
        miniGame.SetActive(true);
        miniGame.GetComponent<Sc_MiniGame>().devilDog = dogInGame;
    }

    public void DogBeGone()
    {
        if(devilDogs != null)
        {
            foreach(GameObject go in devilDogs)
            {
                Destroy(go);
            }
        }
    }

    public void TraitComapre()
    {
        traitCompare.GetComponent<CSS_ScriptController>().TestTrait = dogInGame.GetComponent<Sc_DevilDog>().dogTrait;
    }
}
