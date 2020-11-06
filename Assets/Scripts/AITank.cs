using System.Collections.Generic;
using UnityEngine;
using System;



public class AITank : Tank 
{
    public Tank enemy;
    public Graph graph;
    public GameObject wayCircle;
    public GameObject wayPoint;
    public List<GameObject> circles;
    public List<GameObject> wayPoints;
    public int toPoint;                    // индекс номера точки, в которую танк направляется, предоставляется AITankController 
    public static bool showWay = true;
    private Vector2Int prevEnemyPos;
    private AITankController controller;



    // Start is called before the first frame update
    private void OnDestroy()
    {
        RemoveOldPrefabs();
        ConsoleController.visionChanged -= changeShowingWay;
    }
    void Start()
    {
        transform.position = new Vector2(UnityEngine.Random.Range(1, MazeGenerator.Width - 1) * Cell.size - Cell.size / 2,
                                                UnityEngine.Random.Range(1, MazeGenerator.Height - 1) * Cell.size - Cell.size / 2);
        
        graph = Graph.GetInstance(MazeGenerator.Height - 1, MazeGenerator.Width - 1);
        circles = new List<GameObject>();
        wayPoints = new List<GameObject>();
        var currentEnemyPos = enemy.transform.position;
        prevEnemyPos = Cell.GetCellVector(currentEnemyPos);
        controller = GetComponent<AITankController>();
        ConsoleController.visionChanged += changeShowingWay;
        GetWay();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (Time.timeScale == 1)
            {
                if (Input.GetKeyDown(keyFire))
                    GetWay();
                var currentEnemyPos = Cell.GetCellVector(enemy.transform.position); //получаем по позиции объекта на сцене целочисленные X и Y квадрата в лабиринте, где он находится
                if (currentEnemyPos.x != prevEnemyPos.x ||
                    currentEnemyPos.y != prevEnemyPos.y)
                {
                    GetWay();
                    prevEnemyPos = currentEnemyPos;
                }
            }
        }
        catch(MissingReferenceException ex){ }
    }


    public void GetWay()
    {
        toPoint = 0;
        RemoveOldPrefabs(); 
        var enemyPos = Cell.GetCellVector(enemy.transform.position);
        var pos = Cell.GetCellVector(transform.position);
        List<int> way = graph.shortestWay(enemyPos.x, enemyPos.y, pos.x, pos.y);
        int x_circle, y_circle;
        for (int i = 0; i < way.Count; i++)
        {

            y_circle = (int)(way[i] / (MazeGenerator.Width - 1));
            x_circle = way[i] - y_circle * (MazeGenerator.Width - 1);
            //        Debug.Log($"{way[i]} -- {y_circle} - {x_circle}");
            if (showWay)
            {
                circles.Add(Instantiate(wayCircle, new Vector3(x_circle * Cell.size + Cell.size/2, y_circle * Cell.size + Cell.size/2, 1), Quaternion.identity));
            }
            wayPoints.Add(Instantiate(wayPoint, new Vector2(x_circle * Cell.size + UnityEngine.Random.Range(-1, 1) + Cell.size/2,
                                                            y_circle * Cell.size + UnityEngine.Random.Range(-1, 1) + Cell.size/2), Quaternion.identity));
        }
        controller.GetNextWayPoint();
    }

    void RemoveOldPrefabs()
    {
        for (int i = 0; i < wayPoints.Count; i++)
        {
            if (i < circles.Count) Destroy(circles[i]);
            Destroy(wayPoints[i]);
        }
        circles.Clear();
        wayPoints.Clear();
    }
    /// <summary>
    /// Change movingType 
    /// </summary>
    /// <param name="type">moving, turning, search</param>
    public void ChangeMovingType(AITankController.movingType type)
    {
        controller.type = type;
        if(type == AITankController.movingType.search)
        {
            controller.updateSearchTimer();
        }
    }

    private void changeShowingWay(string param)
    {
        if (param == "on") AITank.showWay = true;
        else AITank.showWay = false;
        this.GetWay(); 
    }
}
