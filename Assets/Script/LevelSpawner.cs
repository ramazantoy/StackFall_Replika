using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSpawner : MonoBehaviour
{
    public GameObject[] model;
    [HideInInspector]//inspector'den gizleme
    public GameObject[] modelPrefab = new GameObject[4];
    public GameObject winPrefab;
    private GameObject tempModel, temp2Model;
    int level = 1,addNumber=7;
    float modelNumber;//model sayısı
    public Transform RotateManager;
    public Material plateMat, baseMat;
    public MeshRenderer playerMeshRendere;

    int sayac = 0;

    void Start()
    {
        level = PlayerPrefs.GetInt("level", 1);//level'in kayıttan çekilmesi varsayılan olarak 1 değeri
        randomModelGenerator();
        float randomnumber = Random.value;
        for (modelNumber = 0; modelNumber > -level - addNumber; modelNumber-= 0.5f)
        {
            if (level <= 20)//level'e göre gösterilecek model şeklinin seçilmesi
            {
                tempModel = Instantiate(modelPrefab[Random.Range(0, 2)]);//model'in kopyalanması
            }
            else if (level <= 50)
            {
                tempModel = Instantiate(modelPrefab[Random.Range(1, 3)]);//model'in kopyalanması
            }
            else if (level <= 100)
            {
                tempModel = Instantiate(modelPrefab[Random.Range(2, 4)]);//model'in kopyalanması
            }
            else
            {
                tempModel = Instantiate(modelPrefab[Random.Range(3, 4)]);//model'in kopyalanması
            }
            tempModel.transform.position = new Vector3(0, modelNumber - 0.01f, 0);//modellerin alt alta gelecek şekilde pozisyonlarının ayarlanması
            tempModel.transform.eulerAngles = new Vector3(0, modelNumber * 8, 0);
            tempModel.transform.parent = RotateManager;//dönme yapmak amacıyla hepsini dönen nesnenin cocugu yapıyorum
            if (Mathf.Abs(modelNumber) >=level*0.3f && Mathf.Abs(modelNumber) <= level * 0.06f)// arasıra farklı yöne dönük nesneler eklemek için
            {
                tempModel.transform.eulerAngles = new Vector3(0f, modelNumber * 8, 0f);
                tempModel.transform.eulerAngles+= Vector3.up*180f;
            }
            else if (Mathf.Abs(modelNumber) > level*0.8f)// 90 derece
            {
                tempModel.transform.eulerAngles = new Vector3(0f, modelNumber * 8, 0f);
                if (randomnumber > 0.75f)
                {
                    tempModel.transform.eulerAngles += Vector3.up * 180f;
                }

            }
            sayac++;
        }
        Debug.Log(sayac);
        temp2Model = Instantiate(winPrefab);//kazanma modeli
        temp2Model.transform.position = new Vector3(0, modelNumber - 0.01f, 0);//kazanmanın en   alta gelecek şekilde pozisyonlarının ayarlanması


    }

 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            plateMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
            baseMat.color = plateMat.color + Color.gray;
            playerMeshRendere.material.color = baseMat.color;
        }
    }
    public void randomModelGenerator()//oyun başlarken prefab'ın tür seçimi
    {
        int random = Random.Range(0, 5);
        switch (random)
        {
            case 0:
                {
                    for(int i = 0; i < 4; i++)
                    {
                        modelPrefab[i] = model[i];
                    }
                    break;

                }
            case 2:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        modelPrefab[i] = model[i+4];
                    }
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        modelPrefab[i] = model[i+8];
                    }
                    break;
                }
            case 4:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        modelPrefab[i] = model[i+12];
                    }
                    break;
                }
            default:
                break;
        }
    }
    public void nextLevel()
    {
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);//level arttırma
        SceneManager.LoadScene(0);
    }
    public void replay()
    {
        SceneManager.LoadScene(0);
    }

    
}
