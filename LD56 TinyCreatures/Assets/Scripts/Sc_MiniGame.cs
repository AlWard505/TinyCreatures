using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_MiniGame : MonoBehaviour
{

    public Image resistanceBar;
    public Image promptA;
    public Image promptD;

    public float drainAmmount;
    public float fillAmmount;

    public bool struggleDirection;

    private void Start()
    {
        StartCoroutine(DirectionFlip());

        promptA.gameObject.SetActive(false);
        promptD.gameObject.SetActive(false);
    }

    private void Update()
    {
        resistanceBar.fillAmount -= drainAmmount * Time.deltaTime;

        if (struggleDirection)
        {
            promptA.gameObject.SetActive(true);
            promptD.gameObject.SetActive(false);

            if (Input.GetKey(KeyCode.A))
            {
                resistanceBar.fillAmount += fillAmmount * Time.deltaTime;
            }
        }
        else if (!struggleDirection)
        {
            promptA.gameObject.SetActive(false);
            promptD.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.D))
            {
                resistanceBar.fillAmount += fillAmmount * 2 * Time.deltaTime;
            }
        }
    }

    IEnumerator DirectionFlip()
    {
        yield return new WaitForSeconds(Random.Range(2,6));
        struggleDirection = !struggleDirection;
        StartCoroutine(DirectionFlip());
    }

}
