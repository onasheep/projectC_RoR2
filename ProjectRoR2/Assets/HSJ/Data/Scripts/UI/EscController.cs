using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscController : MonoBehaviour
{
    public InGameUI myUI;
    public void keepGoing()
    {
        myUI.EscMenu();
    }
    public void StartSceneLoader()
    {
        SceneManager.LoadSceneAsync(0);
    }


    public void CharselScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
