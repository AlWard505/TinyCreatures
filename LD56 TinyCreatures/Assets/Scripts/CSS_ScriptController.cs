using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CSS_ScriptController : MonoBehaviour
{
    bool _IsEvent = false;
    public GameObject _DialogBox;
    public bool testman = false;
    [System.Serializable]
    public struct Event
    {
        public bool isRandom;
        public string[] Dialog;
        public Sprite Character;
        public PersonalityTraits ReleventTrait;
        public PersonalityTraits[] ClosestTraits;
        [NonSerialized]
        public int Progress;
    }
    public Event[] _Events;
    public enum PersonalityTraits
    {
        Lethargic,Energetic, Mischievous, Clever, FireStarter, Hungry
    }
    Event CurrentEvent;
    TMP_Text _dialog;
    public void CallEvent(Event Event)
    {
        _DialogBox.SetActive(true);
        _IsEvent = true;
        CurrentEvent = Event;
        CurrentEvent.Progress = 0;
        _dialog.text = CurrentEvent.Dialog[CurrentEvent.Progress];
        CurrentEvent.Progress++;
    }
    public void NextLine()
    {
        if(CurrentEvent.Progress == CurrentEvent.Dialog.Length)
        {
            _DialogBox.SetActive(false);
            _IsEvent = false;
            return;
        }
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
}
