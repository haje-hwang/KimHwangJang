using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenScript : MonoBehaviour
{
    [SerializeField]
    private Transform Pools;
    [SerializeField]
    private ObjectPooling ApplePool;    
    [SerializeField]
    private ObjectPooling AppleJuicePool;    
    private Transform PlaceHere;
    private GameObject PlacedFood;
    private bool isCooking;
    private int CookingToughness;
    private int CookingProgress;
    // private int Cook
    Dictionary<Food, GameObject> recipe = new Dictionary<Food, GameObject>();

    private void Awake()
    {
        PlaceHere = this.transform.Find("PlaceHere");
        isCooking = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        // recipe.TryAdd(Food.Apple, );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Plate(GameObject food){
        if(!food.Equals(PlacedFood)){
            PlacedFood = food;
            this.CookingToughness = food.GetComponent<FoodScript>().GetCookingToughness();
        }
    }
    public GameObject Cooking(){
        if(!isCooking){
            isCooking = true;
            CookingProgress = 0;
        }
        else{
            if(CookingProgress < CookingToughness)
            {
                CookingProgress += 1;
                Debug.Log("요리 중... " + CookingProgress + " / " + CookingToughness);
            }
            else
            {
                isCooking = false;
                Food name = PlacedFood.GetComponent<FoodScript>().foodName;
                if(name.Equals(Food.Apple)){
                    ApplePool.ReturnObject(PlacedFood);
                    PlacedFood = AppleJuicePool.GetObject();
                    PlacedFood.transform.SetParent(PlaceHere);
                    PlacedFood.transform.SetPositionAndRotation(PlaceHere.position, Quaternion.identity);
                    Debug.Log("요리 완성!");
                    return PlacedFood;
                }
                PlacedFood.GetComponent<FoodScript>().demount_on_head();
            }
        }
        return null;  
    }
}
