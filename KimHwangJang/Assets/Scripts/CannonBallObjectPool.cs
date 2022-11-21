using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallObjectPool : MonoBehaviour
{
    //https://wergia.tistory.com/203
    private Transform CannonBallPool;
    [SerializeField]
    private GameObject CannonBall_pref;
    public List<GameObject> CannonBalls = new List<GameObject>();
    public List<GameObject> using_CannonBalls = new List<GameObject>();
    private void Awake()
    {
        CannonBallPool = this.transform;
        foreach(Transform child in CannonBallPool) {
            CannonBalls.Add(child.gameObject);
        }
    }
    public GameObject GetObject(){
        GameObject Bullet_instance;
        if(CannonBalls.Count < 1){
            AddObject();
        }

        Bullet_instance = CannonBalls[CannonBalls.Count - 1];
        CannonBalls.Remove(Bullet_instance);
        using_CannonBalls.Add(Bullet_instance);
        Bullet_instance.SetActive(true);
        
        return Bullet_instance;
    }
    public void ReturnObject(GameObject cannonball){
        if(using_CannonBalls.Contains(cannonball)){
            using_CannonBalls.Remove(cannonball);
        }
        CannonBalls.Add(cannonball);
        cannonball.transform.position = Vector3.zero;
        cannonball.SetActive(false);
    }

    private void AddObject(){
        GameObject tmp = Instantiate(CannonBall_pref, CannonBallPool);
        CannonBalls.Add(tmp);
        tmp.SetActive(false);
    }
}
