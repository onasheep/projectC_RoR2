using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MonsterUIBar : MonoBehaviour
{

    public Slider myHP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Transform Root, float Height)
    {
        StartCoroutine(Following(Root, Height));
    }

    IEnumerator Following(Transform Root, float Height)
    {
        while(Root != null)
        {
            Camera.allCameras[0].WorldToViewportPoint(Root.position);
            Vector3 pos = Camera.allCameras[0].WorldToScreenPoint(Root.position);
            pos.y += Height;
            this.GetComponent<RectTransform>().anchoredPosition = pos;
            yield return null;
        }
    }

}
