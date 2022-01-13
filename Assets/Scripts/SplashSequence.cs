using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SplashSequence : MonoBehaviour
{
    public static int sceneNumber;
    // Start is called before the first frame update
    void Start()
    {
        if(sceneNumber == 0)
        {
            StartCoroutine(ToSplashTwo());
        }
        if (sceneNumber == 1)
        {
            StartCoroutine(ToLevelScreen());
        }
    }

    IEnumerator ToSplashTwo()
    {
        yield return new WaitForSeconds(3);
        sceneNumber = 1;
        SceneManager.LoadScene(1);
    }
    IEnumerator ToLevelScreen()
    {
        yield return new WaitForSeconds(3);
        sceneNumber = 2;
        SceneManager.LoadScene(2);
    }
}
