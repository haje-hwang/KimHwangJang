using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Food
{
    Potato, Fish, Meat, Apple, AppleJuice
};
public class FoodScript : MonoBehaviour
{
    private Transform tr;
    private Rigidbody rb;
    //원래 이 옵젝의 부모였던 오브젝트 풀
    private Transform OriginalParent;
    //음식 몇번 칼질해야 요리가 되는지(질긴 정도)
    private int CookingToughness;

    public Food foodName;
    public bool isCarring;
    public float fValue;
    private void Awake()
    {
        isCarring = false;
        tr = transform;
        rb = GetComponent<Rigidbody>();

        CookingToughness = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        OriginalParent = tr.parent;
    }
    public void mount_on_head(Transform where){
        isCarring = true;
        tr.SetParent(where);
        rb.isKinematic = true;
        tr.SetPositionAndRotation(where.position, Quaternion.identity);
    }
    public void demount_on_head(){
        isCarring = false;
        tr.SetParent(OriginalParent);
        rb.isKinematic = false;
    }
    public int GetCookingToughness(){
        return CookingToughness;
    }
}
