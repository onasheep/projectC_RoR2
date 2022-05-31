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
        myUI.EscMenu();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadSceneAsync(0);
    }


    public void CharselScene()
    {
        myUI.EscMenu();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadSceneAsync(1);
    }
}
