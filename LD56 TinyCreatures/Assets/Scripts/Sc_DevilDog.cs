using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_DevilDog : MonoBehaviour
{

    public int resistance;

    public GameObject miniGame, mgStart;

    public CSS_ScriptController.PersonalityTraits dogTrait;

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
        }
    }

    public void MiniGameHook()
    {
        mgStart.GetComponent<Sc_MiniGameStart>().MGOn();
        miniGame.GetComponent<Sc_MiniGame>().devilDog = gameObject;
    }

}
