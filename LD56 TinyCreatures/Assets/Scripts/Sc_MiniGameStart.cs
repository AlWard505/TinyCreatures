using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_MiniGameStart : MonoBehaviour
{
    public GameObject miniGame;

    public List<GameObject> devilDogs;

    public void MGOn()
    {
        miniGame.SetActive(true);
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
}
