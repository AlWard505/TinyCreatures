using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_DogSpawn : MonoBehaviour
{

    public GameObject[] dogToSpawn;
    public GameObject MiniGameManager;
    GameObject dog;


    public void SpawnDevilDogs()
    {
       dog = Instantiate(dogToSpawn[Random.Range(0, dogToSpawn.Length)], gameObject.transform);
        MiniGameManager.GetComponent<Sc_MiniGameStart>().devilDogs.Add(dog);

    }

}
