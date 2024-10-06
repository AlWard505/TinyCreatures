using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_DogSpawn : MonoBehaviour
{

    public GameObject[] dogToSpawn;

    private void Start()
    {

        Instantiate(dogToSpawn[Random.Range(0,dogToSpawn.Length +1)], gameObject.transform);

    }

}
