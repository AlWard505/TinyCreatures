using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Data Fields")]
    public PlayerSO playerSO;

    private Camera mainCamera;

    [Header("Logic Fields")]
    public PlayerStates State { get; private set; }
    public enum PlayerStates {
        Idle,
        Move,
        Lasso
    }
    

    [Header("Visual Fields")]
    public GameObject graphicObject;
    public Animator Anim {get; private set;}

    public void Awake() {

    }

    public void Start() {
        Anim = graphicObject.GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    public void Update() {
        // Rotate View Towards Mouse Cursor
		Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
		float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
		graphicObject.transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));

        

        switch (State) {
            case PlayerStates.Idle:
                break;
            case PlayerStates.Move:
                break;
            case PlayerStates.Lasso:
                break;
        }
    }

    public void FixedUpdate() {
        
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

    private Vector2 MoveInput() {
        Vector2 newVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) {
            newVector.y += 1;
        }

        if (Input.GetKey(KeyCode.S)) {
            newVector.y -= 1;
        }

        if (Input.GetKey(KeyCode.A)) {
            newVector.x -= 1;
        }

        if (Input.GetKey(KeyCode.D)) {
            newVector.x += 1;
        }

        return newVector;
    }
}
