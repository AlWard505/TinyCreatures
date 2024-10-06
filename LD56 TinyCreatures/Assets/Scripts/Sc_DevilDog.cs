using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_DevilDog : MonoBehaviour
{

    public int resistance;

    public GameObject miniGame;

    private void Start()
    {
        miniGame = GameObject.FindGameObjectWithTag("MiniGame");
    }

    public void MiniGameSuccess()
    {
        if (resistance > 0)
        {
            resistance--;
        }
    }

    public void MiniGameHook()
    {
        miniGame.GetComponent<Sc_MiniGame>().devilDog = gameObject;
    }

}
