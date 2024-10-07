using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Sc_DevilDog : MonoBehaviour
{

    public int resistance;
    public float moveSpeed;
    public GameObject[] destinations;
    private GameObject destination;

    public GameObject miniGame, mgStart;

    public CSS_ScriptController.PersonalityTraits dogTrait;

    private void Awake()
    {
        StartCoroutine(DestinationUpdate());

        destinations = GameObject.FindGameObjectsWithTag("Nav");

        resistance = Random.Range(0,3);
        gameObject.transform.Find("ThisGuy").GetComponent<TMP_Text>().text = dogTrait.ToString();
    }
    public void UpdateTag(CSS_ScriptController.PersonalityTraits trait)
    {
        dogTrait = trait;
        gameObject.transform.Find("ThisGuy").GetComponent<TMP_Text>().text = dogTrait.ToString();
    }
    private void Update()
    {
        if (miniGame == null)
        {
            miniGame = GameObject.FindGameObjectWithTag("MiniGame");
        }
        if (mgStart == null)
        {
            mgStart = GameObject.FindGameObjectWithTag("MiniGameStart");
        }

        transform.position = Vector2.MoveTowards(transform.position, destination.transform.position, moveSpeed * Time.deltaTime);
    }

    public void MiniGameSuccess()
    {
        if (resistance > 0)
        {
            resistance--;
            miniGame.GetComponent<Sc_MiniGame>().Refresh();
        }
        else if (resistance <= 0)
        {
            miniGame.SetActive(false);
            miniGame.GetComponent<Sc_MiniGame>().devilDog = null;
            miniGame.GetComponent<Sc_MiniGame>().Refresh();
            mgStart.GetComponent<Sc_MiniGameStart>().DogBeGone();
            mgStart.GetComponent<Sc_MiniGameStart>().TraitComapre();
            GameObject.Find("EventSystem").GetComponent<CSS_ScriptController>().BackToDialog();

        }
    }

    public void MiniGameHook()
    {
        mgStart.GetComponent<Sc_MiniGameStart>().dogInGame = gameObject;
        mgStart.GetComponent<Sc_MiniGameStart>().MGOn();

    }

    private void OnMouseEnter()
    {
        gameObject.transform.Find("ThisGuy").gameObject.SetActive(true);

    }


    private void OnMouseExit()
    {

        gameObject.transform.Find("ThisGuy").gameObject.SetActive(false);

    }

    IEnumerator DestinationUpdate()
    {
        yield return new WaitForSeconds(Random.Range(2,7));
        destination = destinations[Random.Range(0, destinations.Length)];
        StartCoroutine(DestinationUpdate());
    }

}
