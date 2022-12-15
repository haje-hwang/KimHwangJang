using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelScript : MonoBehaviour
{
    public JewelController controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource CoinSound = GetComponent<AudioSource>();
            CoinSound.Play();
            controller.GetScore();
            Destroy(gameObject);
        }
    }
}
