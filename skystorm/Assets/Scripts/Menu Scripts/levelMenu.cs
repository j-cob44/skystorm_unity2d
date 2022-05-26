using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class levelMenu : MonoBehaviour
{

    public GameObject homeMenu;
    public GameObject levelsMenu;
    public GameObject endlessMenu;

    bool successfulLoad;

    bool levelOneCompleted;
    bool levelTwoCompleted;
    bool levelThreeCompleted;
    bool levelFourCompleted;
    bool levelFiveCompleted;
    bool levelSixCompleted;
    bool levelSevenCompleted;
    bool levelEightCompleted;
    bool levelNineCompleted;

    GameObject levelTwoButton;
    GameObject levelThreeButton;
    GameObject levelFourButton;
    GameObject levelFiveButton;
    GameObject levelSixButton;
    GameObject levelSevenButton;
    GameObject levelEightButton;
    GameObject levelNineButton;

    GameObject endlessMode;


    private bool menuOpen = false;
    private bool endlessOpen = false;

    DataControl dataControl;

    void Awake()
    {
        dataControl = gameObject.GetComponent<DataControl>();
        dataControl.Load();
        
    }

    void Start()
    {
        gameObject.GetComponent<audioFading>().fadeMusic(1, 0f);
    }
    
    void Update()
    {
        gameObject.GetComponent<audioFading>().fadeMusic(1, 0.025f);
        levelOneCompleted = dataControl.levelOneCompleted;
        levelTwoCompleted = dataControl.levelTwoCompleted;
        levelThreeCompleted = dataControl.levelThreeCompleted;
        levelFourCompleted = dataControl.levelFourCompleted;
        levelFiveCompleted = dataControl.levelFiveCompleted;
        levelSixCompleted = dataControl.levelSixCompleted;
        levelSevenCompleted = dataControl.levelSevenCompleted;
        levelEightCompleted = dataControl.levelEightCompleted;
        levelNineCompleted = dataControl.levelNineCompleted;

        if (levelsMenu.activeSelf)
        {
            if (levelOneCompleted)
            {
                levelTwoButton = GameObject.Find("level2Button");
                levelTwoButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                levelTwoButton = GameObject.Find("level2Button");
                levelTwoButton.GetComponent<Button>().interactable = false;
            }
            if (levelTwoCompleted)
            {
                levelThreeButton = GameObject.Find("level3Button");
                levelThreeButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                levelThreeButton = GameObject.Find("level3Button");
                levelThreeButton.GetComponent<Button>().interactable = false;
            }
            if (levelThreeCompleted)
            {
                levelFourButton = GameObject.Find("level4Button");
                levelFourButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                levelFourButton = GameObject.Find("level4Button");
                levelFourButton.GetComponent<Button>().interactable = false;
            }
            if (levelFourCompleted)
            {
                levelFiveButton = GameObject.Find("level5Button");
                levelFiveButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                levelFiveButton = GameObject.Find("level5Button");
                levelFiveButton.GetComponent<Button>().interactable = false;
            }
            if (levelFiveCompleted)
            {
                levelSixButton = GameObject.Find("level6Button");
                levelSixButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                levelSixButton = GameObject.Find("level6Button");
                levelSixButton.GetComponent<Button>().interactable = false;
            }
            if (levelSixCompleted)
            {
                levelSevenButton = GameObject.Find("level7Button");
                levelSevenButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                levelSevenButton = GameObject.Find("level7Button");
                levelSevenButton.GetComponent<Button>().interactable = false;
            }
            if (levelSevenCompleted)
            {
                levelEightButton = GameObject.Find("level8Button");
                levelEightButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                levelEightButton = GameObject.Find("level8Button");
                levelEightButton.GetComponent<Button>().interactable = false;
            }
            if (levelEightCompleted)
            {
                levelNineButton = GameObject.Find("level9Button");
                levelNineButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                levelNineButton = GameObject.Find("level9Button");
                levelNineButton.GetComponent<Button>().interactable = false;
            }
        }
        if (homeMenu.activeSelf)
        {
            if (levelNineCompleted)
            {
                endlessMode = GameObject.Find("endlessModeButton");
                endlessMode.GetComponent<Button>().interactable = true;
            }
            else
            {
                endlessMode = GameObject.Find("endlessModeButton");
                endlessMode.GetComponent<Button>().interactable = false;
            }
        }
        
    }

    public void ToggleMenu()
    {
        menuOpen = !menuOpen;
        levelsMenu.SetActive(menuOpen);
        homeMenu.SetActive(!menuOpen);
    }

    public void ToggleEndlessMenu()
    {
        endlessOpen = !endlessOpen;
        endlessMenu.SetActive(endlessOpen);
        homeMenu.SetActive(!endlessOpen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

