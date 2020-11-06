using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject WallLeft;
    public GameObject WallBottom;
    public static int size = 10;

    private void Start()
    {
        WallLeft.transform.localScale = new Vector3(1, size, 1);
        WallBottom.transform.localScale = new Vector3(size, 1, 1);
    }
    public static Vector2Int GetCellVector(Vector2 pos) //функция принимает обычные координаты, а возвращает положение в лабиринте( т е Х и Y клетки, например (1, 2))
    {
        Vector2Int vector = new Vector2Int();
        vector.x = (int)((pos.x + size/2) / size - 0.5);
        vector.y = (int)((pos.y + size/2) / size - 0.5);
        return vector;
    }

}
