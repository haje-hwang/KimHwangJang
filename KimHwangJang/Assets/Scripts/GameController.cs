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

    public GameObject controlling_Obj;
    public float RaftSpeed;
    private GameObject player;
    private PlayerController playerController;

    private void Awake()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
        }
        catch (System.Exception)
        {
            Debug.Log("Error in GameController.Awake()");
            throw;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
   
}
