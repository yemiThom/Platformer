using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDelaySpawn : MonoBehaviour
{
    private Animator anim;
    private AnimationClip clip;

    void Start () {
        anim = GetComponent<Animator>();
    }


    public void DoCoroutine()
    {
        StartCoroutine(WaitToSpawnPlayer());
    }

    IEnumerator WaitToSpawnPlayer(){
        yield return new WaitForSeconds(0.45f);
        
        GameManager.Instance.playerController.gameObject.SetActive(true);
    }
}
