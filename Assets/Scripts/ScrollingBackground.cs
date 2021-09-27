using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public float Speed = 1;
    public List<GameObject> sprites = new List<GameObject>();
    public Direction Dir = Direction.Right;

    private float heightCamera;
    private float widthCamera;

    private Vector3 PositionCam;
    private Camera cam;

    public PlayerController player;
    public bool canStartAnyway;

    private void Awake()
    {
        cam = Camera.main;
        heightCamera = 2f * cam.orthographicSize;
        widthCamera = heightCamera * cam.aspect;
    }

    void Update()
    {
        if(!player && GameObject.Find("Player")){
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        foreach (var item in sprites)
        {
            if (Dir == Direction.Left)
            {
                if (item.transform.position.x + item.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2 < cam.transform.position.x - widthCamera / 2)
                {
                    GameObject sprite = sprites[0];
                    foreach (var i in sprites)
                    {
                        if (i.transform.position.x > sprite.transform.position.x)
                            sprite = i;
                    }

                    item.transform.position = new Vector2((sprite.transform.position.x + (sprite.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2) + (item.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2)), sprite.transform.position.y);
                }
            }
            else if (Dir == Direction.Right)
            {
                if (item.transform.position.x - item.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2 > cam.transform.position.x + widthCamera / 2)
                {
                    GameObject sprite = sprites[0];
                    foreach (var i in sprites)
                    {
                        if (i.transform.position.x < sprite.transform.position.x)
                            sprite = i;
                    }

                    item.transform.position = new Vector2((sprite.transform.position.x - (sprite.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2) - (item.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2)), sprite.transform.position.y);
                }
            }
            else if (Dir == Direction.Down)
            {
                if (item.transform.position.y + item.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2 < cam.transform.position.y - heightCamera / 2)
                {
                    GameObject sprite = sprites[0];
                    foreach (var i in sprites)
                    {
                        if (i.transform.position.y > sprite.transform.position.y)
                            sprite = i;
                    }

                    item.transform.position = new Vector2(sprite.transform.position.x, (sprite.transform.position.y + (sprite.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2) + (item.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2)));
                }
            }
            else if (Dir == Direction.Up)
            {
                if (item.transform.position.y - item.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2 > cam.transform.position.y + heightCamera / 2)
                {
                    GameObject sprite = sprites[0];
                    foreach (var i in sprites)
                    {
                        if (i.transform.position.y < sprite.transform.position.y)
                            sprite = i;
                    }

                    item.transform.position = new Vector2(sprite.transform.position.x, (sprite.transform.position.y - (sprite.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2) - (item.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2)));
                }
            }

            if(player || canStartAnyway){
                if(!player){
                    Speed = 1;
                }else if(!player.gameObject.activeSelf){
                    Speed = 0;
                }else{
                    Speed = player.moveSpeed/2;
                }

                if (Dir == Direction.Left)
                    item.transform.Translate(new Vector2(Time.deltaTime * Speed * -1, 0));
                else if (Dir == Direction.Right)
                    item.transform.Translate(new Vector2(Time.deltaTime * Speed, 0));
                else if (Dir == Direction.Down)
                    item.transform.Translate(new Vector2(0, Time.deltaTime * Speed * -1));
                else if (Dir == Direction.Up)
                    item.transform.Translate(new Vector2(0, Time.deltaTime * Speed));
            }
        }

    }
}