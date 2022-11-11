using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    private Transform player_tr;

    public enum Food
    {
        Potato,Fish,Meat
    };

    public Food food;
    public float fValue;
    private void Awake()
    {
        player_tr = GameObject.Find("GameObjects").transform.Find("Raft").transform.Find("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void mount_on_head(){

    }
    public void demount_on_head(){
        
    }
}
