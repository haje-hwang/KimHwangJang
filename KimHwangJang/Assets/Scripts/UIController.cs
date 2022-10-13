using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Canvas
{
    Game,
    GameOver
}
public class UIController : MonoBehaviour
{
    [SerializeField]
    private Canvas GameCanvas;
    [SerializeField]
    private Canvas GameoverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
