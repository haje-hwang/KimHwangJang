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
    private Canvas FishingCanvas;
    // private Slider SailorGage;
    private Slider FishingSlider;
    [SerializeField]
    private float noteSpeed;
    public float minPos;
    public float maxPos;
    public RectTransform pass;
    private void Start()
    {
        FishingCanvas = MainCanvas.transform.Find("FishingCanvas").GetComponent<Canvas>();
        // SailorGage = GameCanvas.transform.Find("SailorGage").GetComponent<Slider>();
        FishingSlider = FishingCanvas.transform.Find("FishingSlider").GetComponent<Slider>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            StartFishing();
        }
    }

    public void StartFishing(){
        FishingSlider.value = 0;
        minPos = pass.anchoredPosition.x;
        maxPos = pass.sizeDelta.x +minPos;
        StartCoroutine(Fishing());
    }
    IEnumerator Fishing()
    {
        bool moveRight = true;
        yield return null;
        while(!(Input.GetKeyDown(KeyCode.Space)))
        {
            if(moveRight){
                FishingSlider.value += Time.deltaTime * noteSpeed;
                if(FishingSlider.value.Equals(FishingSlider.maxValue)){
                    moveRight = false;
                }
                yield return null;
            }
            else{
                FishingSlider.value -= Time.deltaTime * noteSpeed;
                if(FishingSlider.value.Equals(FishingSlider.minValue)){
                    moveRight = true;
                }
                yield return null;
            }
        }
        if(FishingSlider.value >= minPos && FishingSlider.value <= maxPos)
        {
            Debug.Log("Fishing success /" + FishingSlider.value);
        }
        else
        {
            Debug.Log("Fishing failed / "+ FishingSlider.value);
        }
    }
}
