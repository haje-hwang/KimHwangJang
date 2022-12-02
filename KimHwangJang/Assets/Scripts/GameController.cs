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
    [SerializeField]
    private float Player_moveForce, Player_Turn_speed, Player_maxSpeed;

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

        // cannonController = GameObject.FindGameObjectWithTag("Cannon").GetComponent<CannonController>();

        // raftController = GameObject.FindGameObjectWithTag("Raft").GetComponent<RaftController>();
        // raftController.setRaftSpeed(RaftSpeed);
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
