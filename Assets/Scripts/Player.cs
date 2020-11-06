using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static private Player _instance;
    static private object _syscRoot = new object();
    public int Score { get; private set; } = 0;
    public bool CatchedEgg { get; set; }
    public Tank.Color TankColor { get; set; } = Tank.Color.purple;


    private Player() { }
    public static Player GetInstance()
    {
        if(_instance is null)
        {
            lock (_syscRoot)
            {
                if(_instance is null)
                {
                    _instance = new Player();
                }
            }
        }
        return _instance;
    }
    public void increaseScore(int count) { Score += count; }

    public void LoadData(SaveData.PlayerStruct data)
    {
        this.CatchedEgg = data.catchedEgg;
        this.Score = data.score;
        this.TankColor = (Tank.Color)data.color;
    }

    //save
    //load
} 