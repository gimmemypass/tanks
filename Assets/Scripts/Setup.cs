using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Setup : MonoBehaviour
{

    [SerializeField] private GameObject _tankGreenAI;
    [SerializeField] private GameObject _tankGreen;
    [SerializeField] private GameObject _purpleTank;
    [SerializeField] private Text _score;
    // Use this for initialization
    void Start()
    {
        string mode = Scenes.getParam("mode");
        var player = Player.GetInstance();
        switch (mode)
        {
            case "ai":
                Destroy(_tankGreen);
                switch (player.TankColor)
                {
                    case Tank.Color.blue:
                        _purpleTank.GetComponent<SpriteRenderer>().color = new Color(0,233,255) ;
                        break;
                    case Tank.Color.orange:
                        _purpleTank.GetComponent<SpriteRenderer>().color = new Color(238, 255, 0);
                        break;
                    default:
                        break;
                }
                _score.text = player.Score.ToString();
                break;
            case "1vs1": 
                Destroy(_tankGreenAI);
                _score.enabled = false;
                break;
            default:
                break;
        }

    }

}
