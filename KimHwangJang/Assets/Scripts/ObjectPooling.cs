using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    //https://wergia.tistory.com/203
    private Transform PoolTransform;
    [SerializeField]
    private GameObject Prefab;
    public List<GameObject> ready_Objects = new List<GameObject>();
    public List<GameObject> using_Objects = new List<GameObject>();
    private void Awake()
    {
        PoolTransform = this.transform;
        foreach(Transform child in PoolTransform) {
            ready_Objects.Add(child.gameObject);
        }
    }
    public GameObject GetObject(){
        GameObject Bullet_instance;
        if(ready_Objects.Count < 1){
            AddObject();
        }

        Bullet_instance = ready_Objects[ready_Objects.Count - 1];
        ready_Objects.Remove(Bullet_instance);
        using_Objects.Add(Bullet_instance);
        Bullet_instance.SetActive(true);
        
        return Bullet_instance;
    }
    public void ReturnObject(GameObject cannonball){
        if(using_Objects.Contains(cannonball)){
            using_Objects.Remove(cannonball);
        }
        ready_Objects.Add(cannonball);
        cannonball.transform.position = Vector3.zero;
        cannonball.SetActive(false);
    }

    private void AddObject(){
        GameObject tmp = Instantiate(Prefab, PoolTransform);
        ready_Objects.Add(tmp);
        tmp.SetActive(false);
    }
}
