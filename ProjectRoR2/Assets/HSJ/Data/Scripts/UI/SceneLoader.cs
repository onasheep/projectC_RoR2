using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    static SceneLoader _inst = null;
    public static SceneLoader Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindObjectOfType<SceneLoader>();
                if (_inst == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "SceneLoader";
                    DontDestroyOnLoad(obj);
                    _inst = obj.AddComponent<SceneLoader>();
                }
            }
            return _inst;

        }
    }

  
    public void LoadScene(int i)
    {
        StartCoroutine(SceneLoading(i));
    }

    IEnumerator SceneLoading(int i)
    {
        yield return SceneManager.LoadSceneAsync("Loading"); // 로딩창이 백그라운드에서 로딩될때까지 기다리고 
        yield return StartCoroutine(Loading(i));

    }

    IEnumerator Loading(int i)
    {
        Slider loadingBar = GameObject.Find("LoadingProgress")?.GetComponent<Slider>();
        AsyncOperation ao = SceneManager.LoadSceneAsync(i);
        // 씬로딩이 끝나기 전까지는 씬을 활성화 하지 않는다. false로 해두면 씬로딩이 끝나도 씬이 바뀌지 않음 
        ao.allowSceneActivation = false;
        while (!ao.isDone)   // false 중에는 로딩중 true가 되면 로딩이 끝났다는 말
        {
            float v = Mathf.Clamp01(ao.progress / 0.9f);
            if (loadingBar != null) loadingBar.value = v;

            if (Mathf.Approximately(v, 1.0f))
            {
                ao.allowSceneActivation = true;

            }

            yield return null;
        }
    }

    public void LoadScene(string sceneName)
    {

    }

}
