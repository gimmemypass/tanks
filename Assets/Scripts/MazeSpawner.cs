using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CellPrefab;
    public MazeGenerator gen = new MazeGenerator();
    void Awake()
    {
        MazeGeneratorCell[,] maze = gen.GenerateMaze();
        Graph graph = Graph.GetInstance(MazeGenerator.Height - 1, MazeGenerator.Width - 1);
        graph.makeWaysMatrix();
        for (int y = 0; y < maze.GetLength(0); y++)
        {
            for (int x = 0; x < maze.GetLength(1); x++)
            {
                Cell c = Instantiate(CellPrefab, new Vector2(x * Cell.size, y * Cell.size), Quaternion.identity).GetComponent<Cell>();
                c.WallLeft.SetActive(maze[y, x].WallLeft);
                c.WallBottom.SetActive(maze[y, x].WallBottom);
            }
        } 
            


    }

}
