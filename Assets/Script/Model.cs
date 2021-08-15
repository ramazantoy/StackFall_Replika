using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{ //modellerin patlama efektlerini yapmak amacıyla
   private Rigidbody rigi;
    private MeshRenderer mesh;
    private Collider coll;
    private ModelController modcont;
    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();
        modcont = transform.parent.GetComponent<ModelController>();
    }
    void Start()
    {
        
    }
    public void Shutter()//yok olma animasyonu
    {
        rigi.isKinematic = false;
        coll.enabled = false;
        Vector3 forcePoint = transform.parent.position;//Parant'in konumundan güç uygulamak için
        float parentXpos = transform.parent.position.x;//parantimizin x pozisyonu
        float xPos = mesh.bounds.center.x;//orta noktamız x
        Vector3 subdir = (parentXpos - xPos < 0) ? Vector3.right : Vector3.left;//parantimiz x pozisyonu ile bizim x pozisyonumuza göre kuvvetin yönünün belirlenmesi
        Vector3 dir = (Vector3.up * 1.5f+subdir).normalized;//gidiş ayarlaması
        float force = Random.Range(20, 55);
        float torq = Random.Range(110, 180);
        rigi.AddForceAtPosition(force*dir, forcePoint, ForceMode.Impulse);// gücün verilmesi
        rigi.AddTorque(Vector3.left * torq);//torkun verilmesi
        rigi.velocity = Vector3.down;// hız
    }

  
    void Update()
    {
        
    }
}
