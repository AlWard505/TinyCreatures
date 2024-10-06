using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class CSS_ScriptController : MonoBehaviour
{
    bool _IsEvent = false;
    public GameObject _DialogBox, _customer;
    public bool testman = false;
    public Transform _CustomerSpot, _Door;
    [System.Serializable]
    public struct Event
    {
        public bool isRandom;
        public string[] Dialog;
        public string[] RightDialog;
        public string[] WrongDialog;
        public string[] CloseDialog;
        [NonSerialized] public string[] currentDialog;

        public Sprite Character;
        public float Speed;
        public PersonalityTraits ReleventTrait;
        public PersonalityTraits[] ClosestTraits;
        [NonSerialized] public int Progress;
        public string[] IsRight(PersonalityTraits dog)
        {
            if(dog == ReleventTrait)
            {
                currentDialog = RightDialog;
            }
            else if (ClosestTraits.Contains(dog))
            {
                currentDialog = CloseDialog;
            }
            else
            {
                currentDialog = WrongDialog;
            }
            return currentDialog;
        }
    }
    public Event[] _Events;
    public enum PersonalityTraits
    {
        Blank,Lethargic,Energetic, Mischievous, Clever, FireStarter, Hungry
    }
    Event CurrentEvent;
    TMP_Text _dialog;
    public void CallEvent(Event Event, PersonalityTraits Dog = PersonalityTraits.Blank)
    {
        if(Dog == PersonalityTraits.Blank)
        {
            _IsEvent = true;
            CurrentEvent = Event;
            CurrentEvent.Progress = 0;
            StartCoroutine(EnterCustomer());
            return;
        }


    }
    public void NextLine()
    {
        if(CurrentEvent.Progress == CurrentEvent.Dialog.Length)
        {
            _DialogBox.SetActive(false);
            _IsEvent = false;
            return;
        }
        _DialogBox.SetActive(true);

        _dialog.text = CurrentEvent.Dialog[CurrentEvent.Progress];
        CurrentEvent.Progress++;
    }
    private void Start()
    {
        _DialogBox = Instantiate(_DialogBox, GameObject.Find("Canvas").transform);
        _dialog = _DialogBox.GetComponentInChildren<TMP_Text>();
        _DialogBox.SetActive(false);
    }
    private void Update()
    {
        if (testman)
        {
            testman = false;
            if (!_IsEvent)
            {
                CallEvent(_Events[0]);
            }
            else
            {
                NextLine();
            }
        }
    }
    public IEnumerator EnterCustomer()
    {
        _customer.GetComponent<Image>().sprite = CurrentEvent.Character;
        while (_customer.transform.position.x > _CustomerSpot.position.x)
        {
            _customer.transform.Translate(Vector2.left *CurrentEvent.Speed *Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        NextLine();

    }    
    public IEnumerator CustomerLeave()
    {
        while (_customer.transform.position.x < _Door.position.x)
        {
            _customer.transform.Translate(Vector2.right *CurrentEvent.Speed *Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
