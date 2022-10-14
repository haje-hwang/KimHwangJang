using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Singleton 적용
    private static GameController instance;  
    private GameController() { }  
  
    public static GameController getInstance() {  
        if (instance == null) {  
            instance = new GameController();  
        }  
        return instance;  
    }  
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
