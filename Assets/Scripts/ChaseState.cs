using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator ani, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ani.gameObject.GetComponent<EnemyAI>().Chase();
    }

}

