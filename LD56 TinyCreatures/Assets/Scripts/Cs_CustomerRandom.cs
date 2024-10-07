using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cs_CustomerRandom : MonoBehaviour
{
    public Image head;
    public Image body;
    public Sprite[] heads;
    public Sprite[] bodys;

    public void randomPerson()
    {
        head.sprite = heads[Random.Range(1,heads.Length)];
        body.sprite = bodys[Random.Range(1, bodys.Length)];
    }

    public void BlankCustomer()
    {
        body.sprite = bodys[0];
        head.sprite = heads[0];
    }
}
