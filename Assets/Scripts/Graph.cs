using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    // Start is called before the first frame update
    private int size;
    public bool[,] matrix;
    public int[,] waysMatrix;
    public int[,] parents;
    private static Graph _instance = null;
    private static object _syscRoot = new object();

    public delegate void graphIsReadyHandler();
    public static event graphIsReadyHandler graphIsReady;
    void Start()
    {
        
    } 
    private Graph(int h, int w)
    {
        this.size = h*w;
        matrix = new bool[size, size] ;
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                matrix[i,j] = false;
            }
        }
    }
    // Update is called once per frame
    public static Graph GetInstance(int h, int w)
    {
        if(_instance is null)
        {
            lock (_syscRoot)
            {
                if (_instance is null)
                    _instance = new Graph(h, w);
            }
        }
        return _instance;
    } 

    public static void Reload()
    {
        _instance = null;
        graphIsReady = null;
    }
    void Update()
    {
        
    }
    public void makeWaysMatrix()
    {
        int INF = 10000000;
        waysMatrix = new int[size, size];
        parents = new int[size, size];
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
            {
                if (matrix[i, j])
                    waysMatrix[i, j] = 1;
                else
                {
                    if (i == j)
                        waysMatrix[i, i] = 0;
                    else waysMatrix[i, j] = INF;
                }
                
            }
        for (int k = 0; k < size; ++k)
            for (int i = 0; i < size; ++i)
                for (int j = 0; j < size; ++j)
                    if(waysMatrix[i,j] > waysMatrix[i,k] + waysMatrix[k, j])
                    {
                        waysMatrix[i,j] = waysMatrix[i,k] + waysMatrix[k,j];
                        parents[i, j] = k;
                    }
        graphIsReady?.Invoke();
    }

    public List<int> shortestWay(int start_x, int start_y,  int end_x, int end_y)
    {
        int start = start_y * (MazeGenerator.Width - 1) + start_x;
        int end = end_y * (MazeGenerator.Width - 1) + end_x;
        List<int> way = new List<int>();
        int length = waysMatrix[start, end];
        int ind = end;
        int c = 0;
        int area = (MazeGenerator.Width - 1) * (MazeGenerator.Height - 1);
        while (length != 0 &&  c != area)
        {
            for (int i = 0; i < size; i++)
            {
                if (i == ind || i == start)
                {
                    continue;
                }
                if (matrix[ind,i] && (waysMatrix[start, i] + waysMatrix[ind, i] == length))
                {
                    ind = i;
                    length = waysMatrix[start, i];
                    way.Add(ind);
                    break;

                }
            }
            c++;
        }
        //int ind = end;
        //while(length != 1)
        //{
        //    ind = parents[start, ind];
        //    way.Add(ind);
        //    length--;
        //}

        return way;
        
    }
}
