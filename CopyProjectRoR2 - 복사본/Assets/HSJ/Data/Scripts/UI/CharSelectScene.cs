using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelectScene : MonoBehaviour
{


    public void InGameSceneLoad()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
