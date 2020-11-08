using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;
using System.Linq;
public class ShellStandart : Shell, IDisappear 
{
    int numbReflection = 10;

    public void Disappear()
    {
        Destroy(gameObject);
        owner.AddAmunition(1);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!reflectAllowed)
        {
            Debug.Log("reflection isn't allowed");
            return;
        }
        reflectAllowed = false;
        //Debug.Log(collision.name);
        //if(collision.name != owner.name)
        Tank attackedTank;
        int dam = this.damage;
        if(collision.TryGetComponent<Tank>(out attackedTank)){
            attackedTank.ApplyDamage(dam); 
            if (!attackedTank.IsAlive)
            {
                if(attackedTank.name == "tankGreenAI")
                {
                    Player.GetInstance().increaseScore(50);
                } 
                Destroy(attackedTank.gameObject);
                GameOver.over();
            }
            Disappear();     
        }
        else
        {
            this.numbReflection--;
            if (numbReflection == 0) Disappear();
            else {
                switch(collision.name){
                case "Wall Bottom":
                    Reflection(new Vector2(1, -1));
                    break;
                case "Wall Left":
                    Reflection(new Vector2(-1, 1));
                    break;
                }
            }
        }
    }

    protected override void onReflectTimer(object source, ElapsedEventArgs e)
    {
        Debug.Log("reflectAllowed");
        reflectAllowed = true;
        reflectTimer.Stop();
    }
    private void Reflection(Vector2 changer)
    {
        transform.up = new Vector3(transform.up.x * changer.x, transform.up.y * changer.y, transform.up.z);
        reflectTimer.Start();
        return; 
    }

}


public class Flyweight
{
    private Shell _sharedState;

    public Flyweight(Shell car)
    {
        _sharedState = car;
    }

    public void Operation(Shell uniqueState)
    {
        uniqueState.owner.Fire();
    }
}

public class FlyweightFactory
{
    private List<Tuple<Flyweight, string>> flyweights = new List<Tuple<Flyweight, string>>();

    public FlyweightFactory(params Shell[] args)
    {
        foreach (var elem in args)
        {
            flyweights.Add(new Tuple<Flyweight, string>(new Flyweight(elem), this.getKey(elem)));
        }
    }
    public string getKey(Shell key)
    {
        List<string> elements = new List<string>();
        elements.Add(key.armorResistance.ToString());

        elements.Sort();

        return string.Join("_", elements);
    }
    public Flyweight GetFlyweight(Shell sharedState)
    {

        string key = this.getKey(sharedState);
        if (flyweights.Where(t => t.Item2 == key).Count() == 0)
        {
            this.flyweights.Add(new Tuple<Flyweight, string>(new Flyweight(sharedState), key));
        }
        else
        {
        }
        return this.flyweights.Where(t => t.Item2 == key).FirstOrDefault().Item1;
    }

}

