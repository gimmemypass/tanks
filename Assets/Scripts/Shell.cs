using UnityEngine;
using System.Timers;
using System.Collections.Generic;

public abstract class Shell : MonoBehaviour
{
    public float speed;
    public int damage = 10;
    public int armorResistance;
    protected Quaternion angle;
    protected Timer reflectTimer;
    protected bool reflectAllowed;
    public Rigidbody2D rb;
    public Tank owner;
    public GameObject prefab;
    //public Transform firePoint;
    //public string keyFire;
    //protected bool fire;
    //public GameObject shellPrefab;

    
  
   // public abstract void Appear(Vector3 angle, Vector3 pos);

    protected void Start()
    {
        reflectAllowed = true;
        reflectTimer = new Timer();
        reflectTimer.Elapsed += new ElapsedEventHandler(onReflectTimer);
        reflectTimer.Interval = 10;
    }


    protected abstract void onReflectTimer(object source, ElapsedEventArgs e);
    protected void Update()
    {
        rb.velocity = transform.up * speed;
    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);
        //отражение
    //столкновение
}