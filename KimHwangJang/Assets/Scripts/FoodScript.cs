using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Food
{
    Potato, Fish, Meat, Apple, AppleJuice
};
public class FoodScript : Liftable
{
    //음식 몇번 칼질해야 요리가 되는지(질긴 정도)
    private int CookingToughness;

    public Food foodName;
    public float fValue;
    private void Awake()
    {
        CookingToughness = 3;
    }
    private void Start()
    {
        init();
    }

    // Start is called before the first frame update
    public int GetCookingToughness(){
        return CookingToughness;
    }
}
