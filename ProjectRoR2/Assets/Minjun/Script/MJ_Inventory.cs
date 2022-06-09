using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MJ_Inventory : MonoBehaviour
{
    public Transform inven;         //Tab_InvenBase ���ε�
    public GameObject invenItem;  //invenItem �̶�� ������ ���ε�
    public Transform _additem;  //�κ��丮���� Content_Item���ε�
    public List<Texture> itemimg = new List<Texture>();
    public List<string> invenitemname = new List<string>();
    public TMPro.TMP_Text PlayerName = null;
    // Start is called before the first frame update
    void Start()
    {
        inven.gameObject.SetActive(false);
        if(DontDestroyobject.instance.CharSelected == 1)
        {
            PlayerName.text = "�ڸ���";
        }
        if (DontDestroyobject.instance.CharSelected == 2)
        {
            PlayerName.text = "�δ�";
        }
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
