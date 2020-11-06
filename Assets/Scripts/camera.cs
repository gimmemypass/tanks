using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public MazeSpawner maze;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        transform.position = new Vector3 { x = startPos.x +  MazeGenerator.Width *Cell.size/2  - Cell.size/2,  
                                           y = startPos.y +  MazeGenerator.Height * Cell.size/2 - Cell.size/2, 
                                           z = startPos.z };
        GetComponent<Camera>().orthographicSize = Cell.size * 5; 
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
