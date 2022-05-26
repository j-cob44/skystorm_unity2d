using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockAll : MonoBehaviour
{

    private DataControl dataControl;

    void Start()
    {
        DataControl dataControl = gameObject.GetComponent<DataControl>();
        dataControl.Load();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Keypad5))
        {
            DataControl dataControl = gameObject.GetComponent<DataControl>();

            dataControl.levelOneCompleted = true;
            dataControl.levelTwoCompleted = true;
            dataControl.levelThreeCompleted = true;
            dataControl.levelFourCompleted = true;
            dataControl.levelFiveCompleted = true;
            dataControl.levelSixCompleted = true;
            dataControl.levelSevenCompleted = true;
            dataControl.levelEightCompleted = true;
            dataControl.levelNineCompleted = true;

            dataControl.Save();

            Debug.Log("Code Entered for Full Debug.");
        }
    }
}