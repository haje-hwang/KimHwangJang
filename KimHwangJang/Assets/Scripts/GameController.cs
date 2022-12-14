using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject[] foods;
    public bool[] hasFoods;
    public int score = 0;




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
    [SerializeField]
    private float Player_moveForce, Player_Turn_speed, Player_maxSpeed, Player_jumpForce;

    //Codes
    private PlayerController playerController;
    private CannonController cannonController;
    private RaftController raftController;

    //Interaction
    public GameObject controlling_Obj;

    //Raft move
    private Transform Raft_tr;
    public float RaftSpeed;
    public float Raft_RotateSpeed;

    private GameObject player;
    private Transform CamTr;
    private void Awake()
    {
        // Raft_RotateSpeed = 10f; 
        // Player_moveForce = 40f; 
        // Player_turnSpeed = 25f; 
        // Player_maxSpeed = 8f; 
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerController.SetmoveForce(Player_moveForce);
        playerController.SetTurn_speed(Player_Turn_speed);
        playerController.SetmaxSpeed(Player_maxSpeed);
        playerController.SetjumpForce(Player_jumpForce);

        CamTr = Camera.main.transform;

        cannonController = GameObject.FindGameObjectWithTag("Cannon").GetComponent<CannonController>();

        raftController = GameObject.FindGameObjectWithTag("Raft").GetComponent<RaftController>();
        //raftController.setRaftSpeed(RaftSpeed);
        Raft_tr = GameObject.FindGameObjectWithTag("Raft").transform;
    }
    void GetInput(){
        try
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            moveVector = (CamTr.right * Horizontal + new Vector3(CamTr.forward.x, 0, CamTr.forward.z)  * Vertical).normalized;
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
    }
    private void FixedUpdate()
    {
        switch (controlling_Obj.tag)
        {
            case "Player":
            case "Food":
            case "Table":
            case "Cannon_Ball":
                playerController.move(moveVector);
                if(Jump_Key_Down){
                    playerController.Jump();
                }
                // playerController.jumpAnim();
                break;
            case "Steering_Wheel":
                playerController.RunStop();
                //뗏목 회전
                raftController.SpeedControl();
                raftController.TurnControl(Horizontal * Raft_RotateSpeed * Time.fixedDeltaTime);
                break;
            case "Cannon":
                cannonController.Aim();
                playerController.AddOn(controlling_Obj.transform);
                if(Mouse_Left_Down){
                    cannonController.Shoot();
                }
                if(Jump_Key_Down){
                    cannonController.Reload();
                }
                // if(Mouse_Right_Down){
                //     cannonController.Cleanup();
                // }
                break;
            default:
                break;
        }
    }

    
}
