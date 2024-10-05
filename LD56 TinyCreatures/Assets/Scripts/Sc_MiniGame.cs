using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Animations;

public class Sc_MiniGame : MonoBehaviour
{

    public Image resistanceBar;
    public Image promptA;
    public Image promptD;

    public float drainAmmount;
    public float fillAmmount;

    public bool struggleDirection;

    public Animator animator;

    private void Start()
    {
        StartCoroutine(DirectionFlip());

        resistanceBar.fillAmount = Random.Range(0.4f,0.6f);

        promptA.gameObject.SetActive(false);
        promptD.gameObject.SetActive(false);
    }

    private void Update()
    {
        resistanceBar.fillAmount -= drainAmmount * Time.deltaTime;
        animator.SetFloat("Blend", resistanceBar.fillAmount);

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
                resistanceBar.fillAmount += fillAmmount * Time.deltaTime;
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
