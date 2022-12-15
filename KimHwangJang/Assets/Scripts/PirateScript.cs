using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateScript : MonoBehaviour
{
    public JewelController controller;

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Cannon_Ball"))
        {
            controller.GetScore();
            Destroy(gameObject);
        }
    }
}