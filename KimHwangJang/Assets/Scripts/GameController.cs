using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject[] foods;
    public bool[] hasFoods;
    public GameObject[] jewels;

    public int jewels_Value;

    //Singleton 적용
    private static GameController instance;  
    private GameController() { }  
    public static GameController getInstance() {  
        if (instance == null) {  
            instance = new GameController();  
        }  
        return instance;  
    }  
    //Input
    private float Horizontal;
    private float Vertical;
    private bool Interaction_KeyPressed;

    //move
    private Vector3 moveVector;
    public float Player_moveForce;
    public float Player_turnSpeed;

    //Codes
    private PlayerController playerController;
    private CannonController cannonController;

    //Interaction
    public GameObject controlling_Obj;

    //Raft move
    private Transform Raft_tr;
    public float RaftSpeed;
    private float Raft_RotateSpeed;

    private GameObject player;
    private void Awake()
    {
        Raft_RotateSpeed = 10f;
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();

            Raft_tr = GameObject.FindGameObjectWithTag("Raft").transform;
            cannonController = Raft_tr.Find("Cannon").transform.GetComponent<CannonController>();
        }
        catch (System.Exception)
        {
            Debug.Log("Error in GameController.Awake()");
            throw;
        }
        
    }
    void GetInput(){
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        moveVector = new Vector3(Horizontal, 0, Vertical).normalized;
        Interaction_KeyPressed = Input.GetButtonDown("Interaction");
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        if(Interaction_KeyPressed){
            playerController.interact();
        }
        if(controlling_Obj.tag == "Food"){
            //음식 들기
            controlling_Obj.transform.position = player.transform.position + Vector3.up * 6f;
        }
    }
    private void FixedUpdate()
    {
        if(controlling_Obj.tag == "Player" || controlling_Obj.tag == "Food")
        {
            playerController.move(moveVector);
        }
        else if(controlling_Obj.tag == "Steering_Wheel"){
            //뗏목 회전
            Raft_tr.Rotate(0, Horizontal * Time.deltaTime * Raft_RotateSpeed, 0);
        }
        else if(controlling_Obj.tag == "Cannon"){
            cannonController.Aim();
            if(Input.GetMouseButtonDown(0)){
                cannonController.Reload();
            }
            if(Input.GetButtonDown("Jump")){
                cannonController.Shoot();
            }
            if(Input.GetMouseButtonDown(1)){
                cannonController.Cleanup();
            }
        }
    }
}
