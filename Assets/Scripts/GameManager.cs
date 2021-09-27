using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform platformGenerator;
    public PlayerController playerController;
    public DeathMenu deathMenu;
    public GameObject appearAnim;
    public AudioSource warpPopSource, gameMusicSource, loseMusicSource;

    private Vector3 platformGeneratorStartPoint;
    private Vector3 playerStartPoint;
    private ObjectDestroyer[] objectDestroyers;
    private ScoreManager scoreManager;
    private PowerUpManager powerUpManager;
    private GameObject parentScrollingBG, scrollingBG, scrollingBG1;

    private static GameManager instance;

    public static GameManager Instance{
        get{
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }



    // Use this for initialization
    void Start () {
        platformGeneratorStartPoint = platformGenerator.position;
        playerStartPoint = playerController.transform.position;
        powerUpManager = FindObjectOfType<PowerUpManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        parentScrollingBG = GameObject.Find("ScrollingBackground");
        scrollingBG = GameObject.Find("Backround");
        scrollingBG1 = GameObject.Find("Backround (1)");
        warpPopSource = GameObject.Find("WarpPopSound").GetComponent<AudioSource>();
        gameMusicSource = GameObject.Find("MusicSound").GetComponent<AudioSource>();
        loseMusicSource = GameObject.Find("LoseMusicSound").GetComponent<AudioSource>();
    }

    public void RestartGame() {
        scoreManager.scoreIncreasing = false;
        playerController.gameObject.SetActive(false);
        deathMenu.gameObject.SetActive(true);
    }

    public void ResetGame() {
        powerUpManager.InActivePowerUpMode();
        deathMenu.gameObject.SetActive(false);
        objectDestroyers = FindObjectsOfType<ObjectDestroyer>();
        foreach (ObjectDestroyer destroyer in objectDestroyers) {
            destroyer.gameObject.SetActive(false);
        }

        parentScrollingBG.transform.position = new Vector3(-60,0,0);
        scrollingBG.transform.localPosition = new Vector3(0,0,10);
        scrollingBG1.transform.localPosition = new Vector3(17.7f,0,10);

        playerController.transform.position = playerStartPoint;
        platformGenerator.position = platformGeneratorStartPoint;
        //playerController.gameObject.SetActive(true);

        Instantiate(appearAnim, playerStartPoint, transform.rotation);
        warpPopSource.Play();

        if(loseMusicSource.isPlaying){
            loseMusicSource.Stop();
        }

        gameMusicSource.Play();

        StartCoroutine(ShowPlayerChar());

        scoreManager.scoreCounts = 0;
        scoreManager.scoreIncreasing = true;
    }

    IEnumerator ShowPlayerChar() {
        yield return new WaitForSeconds(0.80f);

        playerController.gameObject.SetActive(true);
    }
}