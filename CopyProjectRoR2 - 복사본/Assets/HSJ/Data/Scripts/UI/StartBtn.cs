using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartBtn : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadSceneAsync(1);
        // 게임 시작 클릭시 캐릭터 선택창으로 씬 변경
        SceneManager.LoadScene("HSJcharacterSelect");
    }


}
