using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistantSingleton<GameManager>
{
    public Player Player { get; private set; }
    
    public GameState State { get; private set;}
    public enum GameState {
        Store,
        Dogs
    }

    protected override void Awake() {
        base.Awake();
    }

    private void Start() {

    }

    private void Update() {

    }
}
