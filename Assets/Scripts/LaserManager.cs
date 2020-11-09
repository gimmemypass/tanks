using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserManager : MonoBehaviour
{
 

    private int indexLine;
    public GameObject LinePrefab;
    static private List<Gun> lasers = new List<Gun>();
    static bool showLaser = true;
    static public void AddLaser(Gun gun) { 
        lasers.Add(gun);
    }


    static private List<GameObject> lines = new List<GameObject>();
    static public void RemoveLaser(Gun gun) { lasers.Remove(gun); }
    void RemoveOldLines()
    {
       while(lines.Count  - indexLine > 0)
        {
            Destroy(lines[lines.Count - 1]);
            lines.RemoveAt(lines.Count - 1);
        } 
    }
    

    void Start()
    {
        ConsoleController.visionChanged += changeShowingLaser; 
    }

    void Update()
    {
        indexLine = 0;
       foreach(var laser in lasers)
        {
            CalcLaserLine(laser, laser.transform.position, laser.transform.up, 3);
        }
        if (showLaser)
        {
            RemoveOldLines();
        }

    }

    void CalcLaserLine(Gun laser, Vector2 startPos, Vector2 direction, int n)
    {
        if (n == 0) return;
        indexLine++;
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction);
        try
        {
            if (hit.transform.name == laser.enemy.name)
            {
                //начать поиск
                laser.tank.Fire();
                laser.tank.ChangeMovingType(AITankController.movingType.search);

            }
        }
        catch (Exception ) { }

        Vector2 hitPos = hit.point;
        if (showLaser)
        {
            RedrawLine(startPos, hitPos);
        }
        CalcLaserLine(laser, hitPos, Vector2.Reflect(direction, hit.normal), n - 1);
    }

    void RedrawLine(Vector2 startPos, Vector2 endPos)
    {
        LineRenderer line;
        if(indexLine > lines.Count )
        {
            GameObject go = Instantiate(LinePrefab, Vector2.zero, Quaternion.identity);
            lines.Add(go);
            line = go.GetComponent<LineRenderer>();
        }
        else
        {
            line = lines[indexLine - 1].GetComponent<LineRenderer>(); 
        }
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }

    static public void Reload()
    {
        LaserManager.lasers.Clear();
        lines.Clear();
    }

    private void changeShowingLaser(string param)
    {
        if (param == "on")
        {
            showLaser = true;
            indexLine = 0;
            foreach (Gun laser in lasers)
            {
                CalcLaserLine(laser, laser.transform.position, laser.transform.up, 3);
            }
        }
        else { 
            showLaser = false;
            indexLine = 0;
            RemoveOldLines();
        }

    }
}
