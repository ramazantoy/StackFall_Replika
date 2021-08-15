using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image levelSlider;
    public Image currentLevelImg;
    public Image nextLevelImg;
    public Material playerMat;
    //public GameObject Settings;
    public GameObject allBtn;
   // public GameObject soundOn;
   // public GameObject soundOff;
   // public bool buttonSettingBo;
    private PlayerController player;
    bool uiOff;
    public Text currentLevelTXT;
    public Text nextLevelTXT;

    void Start()
    {
        allBtn.SetActive(true);
        int clevel = PlayerPrefs.GetInt("level", 1);
        currentLevelTXT.text = "" + clevel;
        clevel++;
        nextLevelTXT.text = "" + clevel;
        uiOff = false;
        player = FindObjectOfType<PlayerController>();
        playerMat = GameObject.Find("Player").transform.GetChild(0).GetComponent<MeshRenderer>().material;//player mat renk
        levelSlider.transform.GetComponent<Image>().color = playerMat.color + Color.gray;//level rengi player rengi +gri
        levelSlider.color = playerMat.color;
        currentLevelImg.color = playerMat.color;
        nextLevelImg.color = playerMat.color;

        //   soundOn.GetComponent<Button>().onClick.AddListener(call: (() => SoundManager.instance2.soundOnOff()));
        //  soundOff.GetComponent<Button>().onClick.AddListener(call: (() => SoundManager.instance2.soundOnOff()));

    }


 
    

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().playerstate = PlayerController.playerState.playing;
            allBtn.SetActive(false);
        }
     





        /*
        if (SoundManager.instance2.sound)
        {
           soundOn.SetActive(true);
           soundOff.SetActive(false);
        }
        else
        {
           soundOn.SetActive(false);
           soundOff.SetActive(true);
        }*/
    }
    public void levelSliderFill(float fillAmount)//kaydırma değeri
    {
        levelSlider.fillAmount = fillAmount;
    }
    /*
    public void settingShow()
    {
        //buttonSettingBo = !buttonSettingBo;
       // allBtn.SetActive(buttonSettingBo);
        Debug.Log("settings");
    }
    */
    //ekranda yoksa sayma fonksiyonu
    /*
    private bool IgnoreUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        for(int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.GetComponent<IgnoreUI>() != null)
            {
                raycastResults.RemoveAt(i);
                i--;
            }
        }
        return raycastResults.Count > 0;
    }
    */
}
