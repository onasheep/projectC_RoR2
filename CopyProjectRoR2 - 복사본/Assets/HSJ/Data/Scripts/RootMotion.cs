using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : Character

{
    public Transform myRoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnAnimatorMove()  // �̰� ����ϰԵǸ� ����Ƽ�󿡼� ��Ʈ����� �����Ҽ�����.
                                   // �ִϸ����Ͱ� �޷��ִ� ���ӿ�����Ʈ������ �����Ѵ�.
    {

        myRoot.GetComponent<Rigidbody>().MovePosition(myRoot.position + myAnim.deltaPosition * 2.0f);      // �ش� ������������ �̵� �������� �̵�
        //myRoot.Translate(myAnim.deltaPosition, Space.World);     // �ش� �Ķ���͸�ŭ ������ �̵�
        //Vector3 Rot = myAnim.deltaRotation.eulerAngles;
        //Rot.y = 0.0f;
        //this.transform.Rotate(Rot);
        // �ִϸ����Ͱ� �����Ҷ����� �������� �����Ѵٸ� deltaPosition�� ���� �����
        // deltaRotation�� ���ʹϾ� ���̱⋚���� ���Ϸ������� ��ȯ���־����
    }
}
