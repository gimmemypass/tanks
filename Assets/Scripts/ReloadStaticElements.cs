using UnityEngine;
using UnityEditor;

static public class ReloadStaticElements
{
    static public void Reload()
    {
        //граф
        Pause.PauseOff();
        Graph.Reload();
        LaserManager.Reload();
        ConsoleController.Reload();
    }
}