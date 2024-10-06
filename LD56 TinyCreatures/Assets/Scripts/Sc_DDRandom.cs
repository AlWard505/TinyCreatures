using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_DDRandom : MonoBehaviour
{
    public GameObject[] details;
    int showDetail;

    private void Start()
    {
        if (details != null)
        {
            foreach (GameObject go in details)
            {
                showDetail = Random.Range(0, 2);
                if (showDetail == 1)
                {
                    go.SetActive(true);
                }
            }
        }
    }
}
