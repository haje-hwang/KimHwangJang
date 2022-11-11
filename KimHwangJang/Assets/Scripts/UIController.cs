using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Canvas MainCanvas;
    private Canvas GameCanvas;
    private Slider SailorGage;
    public float fSliderBarTime;
    private void Awake()
    {
        fSliderBarTime = 100f;
        try
        {
            GameCanvas = MainCanvas.transform.Find("GameCanvas").GetComponent<Canvas>();
            SailorGage = GameCanvas.transform.Find("SailorGage").GetComponent<Slider>();
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fSliderBarTime > 0.0f)
        {
            fSliderBarTime -= Time.deltaTime;
            SailorGage.value = fSliderBarTime;
        }
        else
        {
            Debug.Log("Time is Zero.");
        }
    }
    public void Addtime(float val)
    {
        fSliderBarTime += val;
        SailorGage.value = fSliderBarTime;
    }
}
