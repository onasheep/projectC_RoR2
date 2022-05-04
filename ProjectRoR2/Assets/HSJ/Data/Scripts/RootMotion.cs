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
    private void OnAnimatorMove()  // 이걸 사용하게되면 유니티상에서 루트모션을 제어할수없음.
                                   // 애니메이터가 달려있는 게임오브젝트에서만 동작한다.
    {

        myRoot.GetComponent<Rigidbody>().MovePosition(myRoot.position + myAnim.deltaPosition * 2.0f);      // 해당 포지션으로의 이동 물리적인 이동
        //myRoot.Translate(myAnim.deltaPosition, Space.World);     // 해당 파라메터만큼 으로의 이동
        //Vector3 Rot = myAnim.deltaRotation.eulerAngles;
        //Rot.y = 0.0f;
        //this.transform.Rotate(Rot);
        // 애니메이터가 동작할때마다 움직임이 존재한다면 deltaPosition에 값이 저장됨
        // deltaRotation은 쿼터니언 값이기떄문에 오일러값으로 변환해주어야함
    }
}
