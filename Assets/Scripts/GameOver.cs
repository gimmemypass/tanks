using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour
{
    public void Next()
    {
        ReloadStaticElements.Reload();
        Scenes.Load(SceneManager.GetActiveScene().name);
        Debug.Log("Next pressed");
    }

    public static void over()
    {
        Pause.PauseGame();
        GameObject.Find("Canvas").transform.Find("GameOver").gameObject.SetActive(true);   
        
    }
}

