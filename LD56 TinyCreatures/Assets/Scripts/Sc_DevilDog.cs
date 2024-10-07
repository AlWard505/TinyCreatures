using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Sc_DevilDog : MonoBehaviour
{

    public int resistance;

    public GameObject miniGame, mgStart;

    public CSS_ScriptController.PersonalityTraits dogTrait;

    private void Awake()
    {
        resistance = Random.Range(0,3);
        gameObject.transform.Find("ThisGuy").GetComponent<TMP_Text>().text = dogTrait.ToString();
    }
    public void UpdateTag(CSS_ScriptController.PersonalityTraits trait)
    {
        dogTrait = trait;
        gameObject.transform.Find("ThisGuy").GetComponent<TMP_Text>().text = dogTrait.ToString();
    }
    private void Update()
    {
        miniGame = GameObject.FindGameObjectWithTag("MiniGame");
        mgStart = GameObject.FindGameObjectWithTag("MiniGameStart");
    }

    public void MiniGameSuccess()
    {
        if (resistance > 0)
        {
            resistance--;
            miniGame.GetComponent<Sc_MiniGame>().Refresh();
        }
        else if (resistance <= 0)
        {
            miniGame.SetActive(false);
            miniGame.GetComponent<Sc_MiniGame>().devilDog = null;
            miniGame.GetComponent<Sc_MiniGame>().Refresh();
            mgStart.GetComponent<Sc_MiniGameStart>().DogBeGone();
            mgStart.GetComponent<Sc_MiniGameStart>().TraitComapre();
            GameObject.Find("EventSystem").GetComponent<CSS_ScriptController>().BackToDialog();

        }
    }

    public void MiniGameHook()
    {
        mgStart.GetComponent<Sc_MiniGameStart>().dogInGame = gameObject;
        mgStart.GetComponent<Sc_MiniGameStart>().MGOn();

    }

    private void OnMouseEnter()
    {
        gameObject.transform.Find("ThisGuy").gameObject.SetActive(true);

    }


    private void OnMouseExit()
    {

        gameObject.transform.Find("ThisGuy").gameObject.SetActive(false);

    }

}
