using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{

    [SerializeField] AudioClip PickUpCoinSFX;
    [SerializeField] int pointsForCoin = 100;

    bool wasCollected = false;
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().IncreaseScore(pointsForCoin);
            AudioSource.PlayClipAtPoint(PickUpCoinSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
