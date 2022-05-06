using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoder : MonoBehaviour
{
    static SceneLoder _inst = null;
    public static SceneLoder inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindObjectOfType<SceneLoder>();
                if (_inst == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "SceneLoder";
                    DontDestroyOnLoad(obj);
                    _inst = obj.AddComponent<SceneLoder>();
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
        yield return SceneManager.LoadSceneAsync("Loading");
        yield return StartCoroutine(Loading(i));
    }

    IEnumerator Loading(int i)
    {
        Slider loadingBar = GameObject.Find("LoadingProgress")?.GetComponent<Slider>();
        AsyncOperation ao = SceneManager.LoadSceneAsync(i);
        
        ao.allowSceneActivation = false; //씬 로딩이 끝나기전까진 씬을 활성화하지 않는다
        while (!ao.isDone) //로딩니 끝나면 isDone값이 true가 된다
        {
            float v = Mathf.Clamp01(ao.progress / 0.9f);
            if (loadingBar != null) loadingBar.value = v;

            if (Mathf.Approximately(v, 1.0f))
            {
                yield return new WaitForSeconds(1.0f);
                ao.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
