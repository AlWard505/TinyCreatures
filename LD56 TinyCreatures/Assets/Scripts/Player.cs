using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Data Fields")]
    public PlayerSO playerSO;
    public Rigidbody2D PlayerRB { get; private set; }
    public Transform tetherPoint;

    public GameObject lassoObj;
    private GameObject lassoInstance;

    private bool isLasso = false;

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

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        Vector2 moveVector = new Vector2 (inputX, inputY);

        switch (State) {
            case PlayerStates.Idle:
                PlayerRB.velocity = Vector2.zero;

                if (Input.GetMouseButton(0)){
                    State = PlayerStates.Lasso;
                    break;
                }

                if (moveVector != Vector2.zero) {
                    State = PlayerStates.Move;
                }

                break;

            case PlayerStates.Move:
                if (Input.GetMouseButton(0)) {
                    State = PlayerStates.Lasso;
                    break;
                }

                if (moveVector != Vector2.zero) {
                    Move();
                }
                else {
                    State = PlayerStates.Idle;
                }

                break;

            case PlayerStates.Lasso:
                PlayerRB.velocity = Vector2.zero;

                if (!isLasso) {
                    isLasso = true;
                    InstantiatedLasso();    
                }

                MoveLasso();

                break;

        }
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

    private void InstantiatedLasso() {
        // Rotate Graphic Towards Mouse Cursor
		Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
		float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        // Code Storage: Quaternion.Euler (new Vector3(0f,0f,angle))

        lassoInstance = Instantiate(lassoObj, transform.position, Quaternion.identity);
        lassoInstance.TryGetComponent(out Lasso lasso);
        lasso.targetPos = (Vector2)Camera.main.ViewportToWorldPoint(Camera.main.ScreenToViewportPoint(Input.mousePosition));
        Debug.Log((Vector2)Camera.main.ViewportToWorldPoint(Camera.main.ScreenToViewportPoint(Input.mousePosition)));
    }

    private void MoveLasso(){
        if (lassoInstance != null) {
            lassoInstance.TryGetComponent(out Lasso lasso);

            lassoInstance.transform.position = Vector2.MoveTowards(lassoInstance.transform.position, lasso.targetPos, 10f * Time.deltaTime);

            if (Vector2.Distance(lassoInstance.transform.position, lasso.targetPos) < 0.1f) {
                Debug.Log("Reached Target Position");
                Destroy(lassoInstance);
                lassoInstance = null;
                isLasso = false;
                State = PlayerStates.Idle;
            }
        }
    }
}
