using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    private Vector2 cameraPos;
    private Transform Player;
    private Transform win;
    private float cameraOffset=4f;
    private void Awake()//oyun çalıştığında player'in transformunu bulma
    {
        Player = GameObject.FindObjectOfType<PlayerController>().transform;
       // win = GameObject.Find("win(Clone)").GetComponent<Transform>();
       // win = GameObject.FindGameObjectWithTag("Finish").GetComponent<Transform>();
    }
    void Start()
    {
    
    }

  
    void Update()
    {
        if (win == null){
           // win = GameObject.Find("win(Clone)").GetComponent<Transform>();
            win = GameObject.FindGameObjectWithTag("Finish").GetComponent<Transform>();
        }
        else  if(transform.position.y>Player.position.y && transform.position.y > win.position.y + cameraOffset)
        {
            cameraPos = new Vector3(transform.position.x, Player.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, cameraPos.y, -5);//-5 cameranın z  değeri
        }
        
    }
    
} 
