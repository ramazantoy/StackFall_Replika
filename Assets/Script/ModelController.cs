using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    [SerializeField]
    public Model[] models = null;
  public void ShutterAllModels()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
        }
        foreach(Model m  in models)
        {
            m.Shutter();
        }
StartCoroutine(removeAllModelPart());
    }
    IEnumerator removeAllModelPart()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
