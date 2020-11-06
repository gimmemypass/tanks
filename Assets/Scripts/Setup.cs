using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Setup : MonoBehaviour
{

    public GameObject tankGreenAI;
    public GameObject tankGreen;
    public Tank purpleTank;
    public Text Score;
    // Use this for initialization
    void Start()
    {
        string mode = Scenes.getParam("mode");
        var player = Player.GetInstance();
        switch (mode)
        {
            case "ai":
                Destroy(tankGreen);
                switch (player.TankColor)
                {
                    case Tank.Color.blue:
                        purpleTank.GetComponent<SpriteRenderer>().color = new Color(0,233,255) ;
                        break;
                    case Tank.Color.orange:
                        purpleTank.GetComponent<SpriteRenderer>().color = new Color(238, 255, 0);
                        break;
                    default:
                        break;
                }
                Score.text = player.Score.ToString();
                break;
            case "1vs1": 
                Destroy(tankGreenAI);
                Score.enabled = false;
                break;
            default:
                break;
        }

    }

}
