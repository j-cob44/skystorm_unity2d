using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

    public void startGame(string level)
    {
        StartCoroutine(ChangeLevel(level));
    }

    IEnumerator ChangeLevel(string changeToLevel)
    {
        float fadeTime = gameObject.GetComponent<gameFading>().StartFade(1);
        yield return new WaitForSeconds(fadeTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(changeToLevel);
    }
}
