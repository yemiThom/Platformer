﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPoint : MonoBehaviour {

    public GameObject player;
    public Text readyText;
    public float readyTextBlinkSpeed;
    private ScoreManager scoreManager;

    // Use this for initialization
    void Start () {
        scoreManager = FindObjectOfType<ScoreManager>();
        StartCoroutine(GameStartWaiting());
    }

    // Update is called once per frame
    void Update() {
        readyText.color = new Color(readyText.color.r, readyText.color.g, readyText.color.b, Mathf.Round(Mathf.PingPong(Time.time * readyTextBlinkSpeed, 1.0f)));
    }

    IEnumerator GameStartWaiting() {
        yield return new WaitForSeconds(2.15f);
        Instantiate(GameManager.Instance.appearAnim, transform.position, transform.rotation);
        GameManager.Instance.warpPopSource.Play();
        GameManager.Instance.gameMusicSource.Play();

        yield return new WaitForSeconds(0.80f);

        readyText.enabled = false;
        player.SetActive(true);
        scoreManager.scoreIncreasing = true;
    }
}