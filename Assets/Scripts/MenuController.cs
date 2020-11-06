using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Text scoreText;
    void Start()
    {
        scoreText.text = Player.GetInstance().Score.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void oneVSonePressed()
    {
        Dictionary<string, string> args = new Dictionary<string, string>();
        args["mode"] = "1vs1";
        Scenes.Load("battle", args);
        Debug.Log("1 vs 1 pressed");
    }

    public void oneVSaiPressed()
    {
        Dictionary<string, string> args = new Dictionary<string, string>();
        args["mode"] = "ai";
        Scenes.Load("battle", args);
        Debug.Log("1 vs ai pressed");
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }

    
}
