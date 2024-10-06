using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasso : MonoBehaviour
{
    public Transform lassoTetherA;
    public Transform lassoTetherB;

    public Vector2 targetPos;

    public LineRenderer line;

    public void Update() {
        lassoTetherB = GameManager.Instance.Player.tetherPoint;
        line.SetPosition(0, lassoTetherA.position);
        line.SetPosition(1, lassoTetherB.position);
    }

    public void OnTriggerEnter2D(Collider2D other) {

    }
}
