using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airloop : StateMachineBehaviour
{
    public GameObject REffect = null;
    public AudioClip sound = null;
    int count = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        count++;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody>().AddForce(Vector3.down * 20.0f, ForceMode.Force);
        Debug.Log(count);
        if (count == 1 && Physics.Raycast(animator.transform.position + new Vector3(0.0f,0.5f,0.0f), Vector3.down,0.6f, 1 << LayerMask.NameToLayer("Ground")))
        {

            animator.GetComponent<Rigidbody>().AddForce(Vector3.up * 40.0f, ForceMode.VelocityChange);
            Instantiate(REffect, GameObject.Find("pelvis").transform);
            AudioSource.PlayClipAtPoint(sound, animator.transform.position,0.2f);
            
            count = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{


    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
