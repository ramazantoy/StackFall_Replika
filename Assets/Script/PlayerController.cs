using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody r;
    bool crash;//çarpışmaları kontrol etmesi amacıyla
    float currentTime;
    bool invincible;//yenilmez
    public GameObject FireShield;//particle efect
    public AudioClip win, death, inv_destroy, destroy, bounce;
    public int totalObsNumber;//toplam  obstacle item sayısı
    public int currentObsNumber;//kırılan obstacle sayısı
    public Image invictible_slider;// ölümsüzlük  göstergesi
    public GameObject invictibleOBJ;
    public GameObject gameOverUI;
    public GameObject finishUI;
    public Text ScoreTXT;
    public Text BestscoreTXT;
    public enum playerState//oyuncunun durumları
    {
        prepare,
        playing,
        died,
        finish

    }

    [HideInInspector]
    public playerState playerstate = playerState.prepare;
    void Start()
    {
        totalObsNumber =((PlayerPrefs.GetInt("level", 1)+8)*2);//bu script'in ekli olduğu tüm nesnelerin sayısı
        Debug.Log("total  start : " + totalObsNumber);
    }
    private void Awake()
    {
 
        r = GetComponent<Rigidbody>();
        currentObsNumber = 0;
    }


    void Update()
    {
        if (playerstate == playerState.playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                crash = true;

            }
            if (Input.GetMouseButtonUp(0))
            {
                crash = false;
            }
            if (crash)
            {
                currentTime += Time.deltaTime * 0.8f;
            }
            else
            {
                currentTime -= Time.deltaTime * 0.5f;
            }
            if (invincible)//yenilmez ise
            {
                currentTime -= Time.deltaTime * 0.35f;
                if (!FireShield.activeInHierarchy)
                {
                    FireShield.SetActive(true);// ateş efekti
                }
            }
                if(currentTime>=0.15 || invictible_slider.color == Color.red)
                {
                    invictibleOBJ.SetActive(true);
                }
                else
                {
                    invictibleOBJ.SetActive(false);
                }
                if (currentTime >= 1)
                {
                    Debug.Log("invinceble");
                    invincible = true;
                    currentTime = 1;
                    invictible_slider.color = Color.red;
                }
                else if (currentTime <= 0)
                {
                    invincible = false;
                    currentTime = 0;
                    invictible_slider.color = Color.white;
                FireShield.SetActive(false);// ateş efekti kapama
                                            // Debug.Log("normal");
                 }
               if(invictibleOBJ.activeInHierarchy)
                {
                 //  Debug.Log("current time : "+currentTime);
                    invictible_slider.fillAmount = currentTime / 1;
                }   
        }
        /*
        if (playerstate == playerState.prepare)//beklemede ise 
        {
            if (Input.GetMouseButtonDown(0))//ekrana dokunur ise oynamaya geç
            {
                playerstate = playerState.playing;
            }
        }*/
        if (playerstate == playerState.finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().nextLevel();//sonraki levelin yüklenmesi
            }
        }
        if (playerstate == playerState.died)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().replay();
            }
  
        }
        if (playerstate == playerState.prepare && Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();

        }

    }
    private void FixedUpdate()
    {
        if (playerstate == playerState.playing)
        {
            if (crash == true)//çarpma var ise aşağı in
            {
                r.velocity = new Vector3(0f, -100 * Time.fixedDeltaTime * 7.0f, 0f);
            }
        }
  
  
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!crash) //çarpma yok ise yukarı
        {
            r.velocity = new Vector3(0f, 250 * Time.deltaTime, 0);
        }
        else
        {
            if (invincible)
            {
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag=="plane")
                {
                   // Destroy(collision.transform.parent.gameObject);//çarpılan şeyin atası yani komple tüm parçayı yok etmek amacıyla
                    collision.transform.parent.GetComponent<ModelController>().ShutterAllModels();
                    shutterDown();
                    SoundManager.instance2.playSoundFx(inv_destroy, 1f);
                    currentObsNumber++;
                }
            }
            else
            {
                if (collision.gameObject.tag == "enemy")
                {
                   // Destroy(collision.transform.parent.gameObject);//çarpılan şeyin atası yani komple tüm parçayı yok etmek amacıyla
                    collision.transform.parent.GetComponent<ModelController>().ShutterAllModels();
                    shutterDown();
                   SoundManager.instance2.playSoundFx(destroy, 1f);
                    currentObsNumber++;
                }
                else if (collision.gameObject.tag == "plane")
                {
                    //Debug.Log("Game Over");
                    gameOverUI.SetActive(true);
                    playerstate = playerState.died;
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    SoundManager.instance2.playSoundFx(death, 1f);
                    ScoreTXT.text = "Score : " + FindObjectOfType<ScoreManager>().getScore();
                    BestscoreTXT.text = "Best Score : " + PlayerPrefs.GetInt("Highscore", 1);
                    ScoreManager.instance.resetScore();

                }
            }
              
         
        }
        FindObjectOfType<GameUI>().levelSliderFill(currentObsNumber / (float)(totalObsNumber));

        if (collision.gameObject.tag=="Finish" && playerstate == playerState.playing)
        {
            playerstate = playerState.finish;
            SoundManager.instance2.playSoundFx(win, 2f);
            finishUI.SetActive(true);

        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!crash || collision.gameObject.tag=="Finish") //çarpma yok ise yukarı
        {
            r.velocity = new Vector3(0f, 250 * Time.deltaTime, 0);
            SoundManager.instance2.playSoundFx(bounce, 1f);
        }

    }
    public void shutterDown()
    {
        if(invincible)
        ScoreManager.instance.addScore(1);
        else
        {
            ScoreManager.instance.addScore(4);
        }
    }
}