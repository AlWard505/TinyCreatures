using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasso : MonoBehaviour
{
    public Transform lassoTetherA;
    public Transform lassoTetherB;

    public Vector2 targetPos;
    public LayerMask dogLayer;

    public bool hitSomething = false;

    public LineRenderer line;

    public Sc_DevilDog caughtDog;

    public void Update() {
        lassoTetherB = GameManager.Instance.Player.tetherPoint;
        line.SetPosition(0, lassoTetherA.position);
        line.SetPosition(1, lassoTetherB.position);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("Hit: " + other.gameObject.name);
        if (other.gameObject.layer == 6) {
            hitSomething = true;
            other.GetComponent<Sc_DevilDog>().MiniGameHook();
            caughtDog = other.GetComponent<Sc_DevilDog>();
        }
    }
}
