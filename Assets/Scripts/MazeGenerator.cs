using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int X;
    public int Y;
    public bool WallLeft = true;
    public bool WallBottom = true;

    public bool visited = false;
}
public class MazeGenerator 
{
    public static int Width = 15;
    public static int Height = 10;
    //private static MazeGenerator _instance;
    //private static object _syscRoot = new object();
    // Start is called before the first frame update

    public MazeGenerator() { }
    //public static MazeGenerator GetInstance()
    //{
    //   if(_instance is null)
    //    {
    //        lock (_syscRoot)
    //        {
    //            if (_instance is null)
    //                _instance = new MazeGenerator();
    //        }
    //    }
    //    return _instance;
    //} 
    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[Height, Width];
        for(int y = 0; y < maze.GetLength(0); y++)
        {
            for(int x = 0; x < maze.GetLength(1); x++)
            {
                maze[y, x] = new MazeGeneratorCell { X = x, Y = y };
            }
        }
        for (int x = 0; x < maze.GetLength(1); x++)
            maze[maze.GetLength(0) - 1, x].WallLeft = false;
        for (int y = 0; y < maze.GetLength(0); y++)
            maze[y, maze.GetLength(1) - 1].WallBottom = false;
        RemoveWallAlgorithm(maze);
        return maze;
    }

    public void RemoveWallAlgorithm(MazeGeneratorCell[,] maze)
    {
        Graph graph = Graph.GetInstance(Height - 1, Width - 1);
        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        MazeGeneratorCell current = maze[0, 0];
        current.visited = true;
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();
            int x = current.X;
            int y = current.Y;
            if (x > 0 && !maze[y, x - 1].visited) { 
                unvisitedNeighbours.Add(maze[y, x - 1]);
            }
            if (y > 0 && !maze[y - 1, x].visited) {
                unvisitedNeighbours.Add(maze[y - 1,x]);
            }
            if (x < Width - 2 && !maze[y,x + 1].visited) {
                unvisitedNeighbours.Add(maze[y, x + 1]);
            }
            if (y < Height - 2 && !maze[y + 1, x].visited) {
                unvisitedNeighbours.Add(maze[y + 1, x]);
            }

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen, ref graph);
                
                chosen.visited = true;
                stack.Push(chosen);
                current = chosen;
            }
            else
                current = stack.Pop();


        } while (stack.Count > 0);

        LoopsAdding(maze, ref graph);
    }

    private void LoopsAdding(MazeGeneratorCell[,] maze, ref Graph graph)
    {
        int requiredCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Width / 7)));
        for (int i = 0; i < requiredCount * Height; i++)
        {
            List<MazeGeneratorCell> Neighbors = new List<MazeGeneratorCell>();
            MazeGeneratorCell current;
            MazeGeneratorCell next;
            do
            {
                current = maze[UnityEngine.Random.Range(0, Height - 1), UnityEngine.Random.Range(0, Width - 1)];
                int x = current.X;
                int y = current.Y;
                if (x > 0 && maze[y, x].WallLeft) Neighbors.Add(maze[y, x - 1]);
                if (y > 0 && maze[y, x].WallBottom) Neighbors.Add(maze[y - 1, x]);
                if (x < Width - 2 && maze[y, x + 1].WallLeft) Neighbors.Add(maze[y, x + 1]);
                if (y < Height - 2 && maze[y + 1, x].WallBottom) Neighbors.Add(maze[y + 1, x]);
            } while (Neighbors.Count == 0);
            next = Neighbors[UnityEngine.Random.Range(0, Neighbors.Count - 1)];
            RemoveWall(current, next, ref graph);
        }
    }
    private void RemoveWall(MazeGeneratorCell A, MazeGeneratorCell B, ref Graph graph) {
        if (A.X == B.X)
        {
            if (A.Y > B.Y)  A.WallBottom = false;
            else  B.WallBottom = false;

        }
        else
        {
            if (A.X > B.X)  A.WallLeft = false;
            else  B.WallLeft = false;
        }
        int first = A.Y * (Width - 1) + A.X;
        int second = B.Y * (Width - 1) + B.X;
        graph.matrix[first, second ] = true;
        graph.matrix[second, first] = true;
    }
    //private bool Breaker(ref bool element)
    //{
    //    if (element)
    //    {
    //        element = false;
    //        return true;
    //    }
    //    else return false;
    //}

    // Update is called once per frame

}
