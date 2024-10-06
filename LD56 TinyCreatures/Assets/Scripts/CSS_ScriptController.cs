using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
public class CSS_ScriptController : MonoBehaviour
{
    int _round = 0;
    bool _IsEvent = false;
    bool IsIntroductionDone = false;
    public GameObject _DialogBox, _customer;
    public bool testman = false;
    public Transform _CustomerSpot, _Door,_parent;
    public PersonalityTraits TestTrait;
    [System.Serializable]
    public struct Event
    {
        public bool isRandom;
        public int RoundAppearance;
        public string[] Dialog;
        public string[] RightDialog;
        public string[] WrongDialog;
        public string[] CloseDialog;
        [NonSerialized] public string[] currentDialog;

        public Sprite Character;
        public float Speed;
        public bool RecievedDog;
        public PersonalityTraits ReleventTrait;
        public PersonalityTraits[] ClosestTraits;
        [NonSerialized] public int Progress;
        public void IsRight(PersonalityTraits dog)
        {
            RecievedDog = true;

            if (dog == ReleventTrait)
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
        }
    }
    public Event[] _Events;
    public List<Event> _ScriptedEvents;
    public enum PersonalityTraits
    {
        Blank,Lethargic,Energetic, Mischievous, Clever, FireStarter, Hungry
    }
    Event CurrentEvent;
    TMP_Text _dialog;
    public void CallEvent(Event Event)
    {
        _IsEvent = true;
        CurrentEvent = Event;
        CurrentEvent.RecievedDog = false;
        IsIntroductionDone = false;
        CurrentEvent.currentDialog = CurrentEvent.Dialog;
        CurrentEvent.Progress = 0;
        StartCoroutine(EnterCustomer());
        return;

    }
    public void NextLine()
    {
        if(IsIntroductionDone)
        {
            CurrentEvent.Progress = 0;
            IsIntroductionDone=false;
            CurrentEvent.IsRight(TestTrait);
        }
        if (CurrentEvent.Progress == CurrentEvent.currentDialog.Length)
        {
            _DialogBox.SetActive(false);
            IsIntroductionDone = true;
            if (CurrentEvent.RecievedDog)
            {
                StartCoroutine(CustomerLeave());
                _IsEvent = false;
            }
            else _parent.gameObject.SetActive(false);
            return;
        }
        _DialogBox.SetActive(true);

        _dialog.text = CurrentEvent.currentDialog[CurrentEvent.Progress];
        CurrentEvent.Progress++;
    }
    public void ChooseEvent()
    {
        _round++;
        Debug.Log("Round: " + _round);
        if (_ScriptedEvents.Count != 0) if (_ScriptedEvents[0].RoundAppearance == _round)
        {
            CallEvent(_ScriptedEvents[0]);
            _ScriptedEvents.RemoveAt(0);
             return;
        }

        int rand = UnityEngine.Random.Range(0, _Events.Length);
        while (!_Events[rand].isRandom) 
        {
            rand = UnityEngine.Random.Range(0, _Events.Length);
        }
        CallEvent(_Events[rand]);

    }
    private void Start()
    {
        int highestScripted = -100;
        foreach(Event ev in _Events)
        {
            if (!ev.isRandom & ev.RoundAppearance > highestScripted) highestScripted = ev.RoundAppearance;
        }
        _ScriptedEvents = new List<Event>();
        for (int i = 0; i < highestScripted; i++)
        {
            foreach (Event ev in _Events)
            {
                if (!ev.isRandom & ev.RoundAppearance == i)
                {
                    _ScriptedEvents.Add(ev);
                    break;
                }
            }
            
        }
        _DialogBox = Instantiate(_DialogBox, _parent);
        _customer = Instantiate(_customer, _Door);
        _dialog = _DialogBox.GetComponentInChildren<TMP_Text>();
        _DialogBox.SetActive(false);
        ChooseEvent();
    }
    private void Update()
    {
        if (testman)
        {
            testman = false;
            NextLine();
        }
    }
    public IEnumerator EnterCustomer()
    {
        _customer.GetComponent<Image>().sprite = CurrentEvent.Character;
        while (_customer.transform.position.x < _CustomerSpot.position.x)
        {
            _customer.transform.Translate(Vector2.right *CurrentEvent.Speed *Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        NextLine();

    }    
    public IEnumerator CustomerLeave()
    {
        while (_customer.transform.position.x > _Door.position.x)
        {
            _customer.transform.Translate(Vector2.left *CurrentEvent.Speed *Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        ChooseEvent();
    }
}
