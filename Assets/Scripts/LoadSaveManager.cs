using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class LoadSaveManager : MonoBehaviour
{
    string filePath;
    private Player player;
    public Text scoreText;

    private void Start()
    {
        filePath = Application.persistentDataPath ;
        player = Player.GetInstance();
    }
    /// <summary>
    /// Save 
    /// </summary>
    /// <param name="fileNumber">number of file, which will be saved (0 - 4)</param>
    public void Save(int fileNumber)
    {
        if (fileNumber > 4 || fileNumber < 0) throw new FileNotFoundException();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath + "/save" + fileNumber.ToString() + ".gamesave", FileMode.Create);

        SaveData data = new SaveData();
        data.PlayerToStruct(player);

        bf.Serialize(fs,data);
        fs.Close();
        Debug.Log($"saved Player {player.Score} {player.CatchedEgg} {player.TankColor}");
    } 

    /// <summary>
    /// Load
    /// </summary>
    /// <param name="fileNumber">number of file, which will be loaded (0 - 4)</param>
    public void Load(int fileNumber)
    {
        if(!File.Exists(filePath + "/save" + fileNumber.ToString() + ".gamesave"))
        {
            throw new FileNotFoundException();
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath + "/save" + fileNumber.ToString() + ".gamesave", FileMode.Open);

        SaveData data = (SaveData)bf.Deserialize(fs);
        fs.Close();

        player.LoadData(data.playerData);
        scoreText.text = player.Score.ToString();
        Debug.Log($"loaded Player {player.Score} {player.CatchedEgg} {player.TankColor}");
    }

}

[System.Serializable]
public class SaveData
{
    [System.Serializable]
    public struct PlayerStruct
    {
        public int score;
        public bool catchedEgg;
        public int color;

        public PlayerStruct(int score, int color, bool catchedEgg)
        {
            this.score = score;
            this.color = color;
            this.catchedEgg = catchedEgg;
        }
    }

    public PlayerStruct playerData;

    public void PlayerToStruct(Player player)
    {
        playerData = new PlayerStruct(player.Score, (int)player.TankColor, player.CatchedEgg);
    }
}
