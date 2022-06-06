using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MJ_Inventory : MonoBehaviour
{
    public Transform inven;         //Tab_InvenBase 바인딩
    public GameObject invenItem;  //invenItem 이라는 프리팹 바인딩
    public Transform _additem;  //인벤토리에서 Content_Item바인딩
    public List<Texture> itemimg = new List<Texture>();
    public List<string> invenitemname = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        inven.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inven.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            inven.gameObject.SetActive(true);
        }
        if (inven.gameObject.activeSelf && Input.GetKeyUp(KeyCode.Tab))
        {
            inven.gameObject.SetActive(false);
        }

    }

    public void AddItem(int i,string s)
    {
        invenItem.GetComponent<RawImage>().texture = itemimg[i];
        Instantiate(invenItem, _additem).name = s;
    }

}
