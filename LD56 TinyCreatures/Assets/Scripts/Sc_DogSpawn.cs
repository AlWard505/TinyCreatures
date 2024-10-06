using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_DogSpawn : MonoBehaviour
{

    public GameObject[] dogToSpawn;


    public void SpawnDevilDogs()
    {
        Instantiate(dogToSpawn[Random.Range(0, dogToSpawn.Length)], gameObject.transform);
    }

}
