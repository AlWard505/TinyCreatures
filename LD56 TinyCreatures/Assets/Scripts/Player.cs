using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Data Fields")]
    public PlayerSO playerSO;
    public Rigidbody2D PlayerRB { get; private set; }

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
        GameManager.Instance.Player = this;

        Anim = graphicObject.GetComponent<Animator>();
        PlayerRB = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    public void Update() {
        Move();
    }

    public void FixedUpdate() {
        
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

    private void Move() {
        float speedX = Input.GetAxisRaw("Horizontal") * playerSO.moveSpeed;
        float speedY = Input.GetAxisRaw("Vertical") * playerSO.moveSpeed;
        PlayerRB.velocity = new Vector2(speedX, speedY);
    }

    private void RotateGraphic() {
        // Rotate Graphic Towards Mouse Cursor
		Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
		float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
		graphicObject.transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));
    }
}
