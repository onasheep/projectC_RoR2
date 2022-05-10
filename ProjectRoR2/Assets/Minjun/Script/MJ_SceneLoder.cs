using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MJ_SceneLoder : MonoBehaviour
{
    static MJ_SceneLoder _inst = null;
    public static MJ_SceneLoder inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindObjectOfType<MJ_SceneLoder>();
                if (_inst == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "SceneLoder";
                    DontDestroyOnLoad(obj);
                    _inst = obj.AddComponent<MJ_SceneLoder>();
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
        
        ao.allowSceneActivation = false; //�� �ε��� ������������ ���� Ȱ��ȭ���� �ʴ´�
        while (!ao.isDone) //�ε��� ������ isDone���� true�� �ȴ�
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
