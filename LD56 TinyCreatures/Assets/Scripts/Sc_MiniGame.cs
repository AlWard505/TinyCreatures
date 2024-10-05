using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_MiniGame : MonoBehaviour
{

    public Image resistanceBar;

    public float drainAmmount;

    public bool struggleDirection;

    private void Start()
    {
        StartCoroutine(DirectionFlip());
    }

    private void Update()
    {
        resistanceBar.fillAmount -= drainAmmount * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) && struggleDirection)
        {
            resistanceBar.fillAmount += 0.2f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) && !struggleDirection)
        {
            resistanceBar.fillAmount += 0.2f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            resistanceBar.fillAmount -= 0.05f * Time.deltaTime;
        }
    }

    IEnumerator DirectionFlip()
    {
        yield return new WaitForSeconds(Random.Range(3,12));
        struggleDirection = !struggleDirection;
        StartCoroutine(DirectionFlip());
    }

}
