using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour
{
    public NavMeshAgent NavAgent;
    public Animator HeroAnimator;


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        
    }


    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        HeroAnimator.SetBool("IsMoving", NavAgent.velocity != Vector3.zero);

        if (Input.GetMouseButtonDown(1))
        {
            NavAgent.ResetPath();
            HeroAnimator.SetTrigger("DoAttack");
        }
    }
}
