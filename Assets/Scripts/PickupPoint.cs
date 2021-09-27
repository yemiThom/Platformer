using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PickupPoint : MonoBehaviour {
    public int score;
    private ScoreManager scoreManager;
    private AudioSource coinSound;

    public GameObject collectedDust;

    // Use this for initialization
    void Start() {
        scoreManager = FindObjectOfType<ScoreManager>();
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Equals("Player")) {
            if (coinSound.isPlaying) {
                coinSound.Stop();
            }
            coinSound.Play();

            //if(collectedDust)
            Instantiate(collectedDust, transform.position, transform.rotation);

            scoreManager.AddScore(score);
            gameObject.SetActive(false);
        }
    }
}