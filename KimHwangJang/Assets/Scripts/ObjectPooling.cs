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
    }
    private void Start()
    {
        // Transform[] Pool = PoolTransform.GetComponentsInChildren<Transform>(true);
        foreach(Transform child in PoolTransform) {
            if(child.gameObject.activeSelf){
                using_Objects.Add(child.gameObject);
            }
            else{
                ready_Objects.Add(child.gameObject);
            }
        }
    }
    public GameObject GetObject(){
        GameObject Called_instance;
        if(ready_Objects.Count < 1){
            AddObject();
        }

        Called_instance = ready_Objects[ready_Objects.Count - 1];
        ready_Objects.Remove(Called_instance);
        using_Objects.Add(Called_instance);
        Called_instance.SetActive(true);
        
        return Called_instance;
    }
    public void ReturnObject(GameObject Called_instance){
        if(using_Objects.Contains(Called_instance)){
            using_Objects.Remove(Called_instance);
        }
        ready_Objects.Add(Called_instance);
        Called_instance.transform.SetParent(PoolTransform);
        Called_instance.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        Called_instance.SetActive(false);
    }

    private void AddObject(){
        GameObject tmp = Instantiate(Prefab, PoolTransform);
        ready_Objects.Add(tmp);
        tmp.SetActive(false);
    }
}
