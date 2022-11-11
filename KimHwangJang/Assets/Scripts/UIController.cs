using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
     //Singleton 적용
    private static UIController instance;  
    private UIController() { }  
    public static UIController getInstance() {  
        if (instance == null) {  
            instance = new UIController();  
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
