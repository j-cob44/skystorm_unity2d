using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataControl : MonoBehaviour {

    public bool levelOneCompleted;
    public bool levelTwoCompleted;
    public bool levelThreeCompleted;
    public bool levelFourCompleted;
    public bool levelFiveCompleted;
    public bool levelSixCompleted;
    public bool levelSevenCompleted;
    public bool levelEightCompleted;
    public bool levelNineCompleted;

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamedata.dat");

        GameData data = new GameData();

        data.levelOneCompleted = levelOneCompleted;
        data.levelTwoCompleted = levelTwoCompleted;
        data.levelThreeCompleted = levelThreeCompleted;
        data.levelFourCompleted = levelFourCompleted;
        data.levelFiveCompleted = levelFiveCompleted;
        data.levelSixCompleted = levelSixCompleted;
        data.levelSevenCompleted = levelSevenCompleted;
        data.levelEightCompleted = levelEightCompleted;
        data.levelNineCompleted = levelNineCompleted;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gamedata.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamedata.dat", FileMode.Open);

            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            levelOneCompleted = data.levelOneCompleted;
            levelTwoCompleted = data.levelTwoCompleted;
            levelThreeCompleted = data.levelThreeCompleted;
            levelFourCompleted = data.levelFourCompleted;
            levelFiveCompleted = data.levelFiveCompleted;
            levelSixCompleted = data.levelSixCompleted;
            levelSevenCompleted = data.levelSevenCompleted;
            levelEightCompleted = data.levelEightCompleted;
            levelNineCompleted = data.levelNineCompleted;
        }
    }

    public void Create()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamedata.dat");

        GameData data = new GameData();
        data.levelOneCompleted = false;
        data.levelTwoCompleted = false;
        data.levelThreeCompleted = false;
        data.levelFourCompleted = false;
        data.levelFiveCompleted = false;
        data.levelSixCompleted = false;
        data.levelSevenCompleted = false;
        data.levelEightCompleted = false;
        data.levelNineCompleted = false;

        bf.Serialize(file, data);
        file.Close();

        Load();
    }

    public void Delete()
    {
        File.Delete(Application.persistentDataPath + "/gamedata.dat");
        Create();
    }
}

[Serializable]
class GameData
{
    public bool levelOneCompleted;
    public bool levelTwoCompleted;
    public bool levelThreeCompleted;
    public bool levelFourCompleted;
    public bool levelFiveCompleted;
    public bool levelSixCompleted;
    public bool levelSevenCompleted;
    public bool levelEightCompleted;
    public bool levelNineCompleted;
}