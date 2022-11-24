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
    private float Horizontal, Vertical;
    private bool Mouse_Left_Down;
    private bool Mouse_Right_Down;
    private bool Interaction_Key_Down;
    private bool Jump_Key_Down;
    

    //move
    private Vector3 moveVector;
    public float Player_moveForce, Player_turnSpeed;

    //Codes
    private PlayerController playerController;
    private CannonController cannonController;

    //Interaction
    public GameObject controlling_Obj;

    //Raft move
    private Transform Raft_tr;
    public float RaftSpeed;
    public float Raft_RotateSpeed;

    private GameObject player;
    private void Awake()
    {
        Raft_RotateSpeed = 10f;
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>(); 
            cannonController = GameObject.FindGameObjectWithTag("Cannon").GetComponent<CannonController>();
        }
        catch (System.Exception)
        {
            Debug.Log("Error while GetComponent in GameController.Awake()");
            throw;
        }
            // Raft_tr = GameObject.FindGameObjectWithTag("Raft").transform;
        
    }
    void GetInput(){
        try
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            moveVector = new Vector3(Horizontal, 0, Vertical).normalized;
            Interaction_Key_Down = Input.GetButtonDown("Interaction");
            Mouse_Left_Down = Input.GetMouseButtonDown(0);
            Mouse_Right_Down = Input.GetMouseButtonDown(1);
            Jump_Key_Down = Input.GetButtonDown("Jump");
        }
        catch (System.Exception)
        {
            Debug.Log("Error in GameController.GetInput()");
            throw;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        if(Interaction_Key_Down){
            playerController.interact();
        }
        if(controlling_Obj.tag == "Food"){
            //음식 들기
            controlling_Obj.transform.position = player.transform.position + Vector3.up * 6f;
        }
    }
    private void FixedUpdate()
    {
        switch (controlling_Obj.tag)
        {
            case "Player":
            case "Food":
                playerController.move(moveVector);
                break;
            case "Steering_Wheel":
                //뗏목 회전
                Raft_tr.Rotate(0, Horizontal * Time.deltaTime * Raft_RotateSpeed, 0);
                break;
            case "Cannon":
                cannonController.Aim();
                if(Mouse_Left_Down){
                    cannonController.Reload();
                }
                if(Jump_Key_Down){
                    cannonController.Shoot();
                }
                if(Mouse_Right_Down){
                    cannonController.Cleanup();
                }
                break;
            default:
                break;
        }
    }
}
