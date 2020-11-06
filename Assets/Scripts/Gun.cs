using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public AITank tank;
    public Tank enemy;
    void Start()
    {
        LaserManager.AddLaser(this); 
    }

    //~LaserGun()
    //{
    //    LaserManager.RemoveLaser(this); 
    //}

    private void OnDestroy()
    {
        LaserManager.RemoveLaser(this); 
    }
}
