using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator ani, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ani.gameObject.GetComponent<EnemyAI>().Patrol();
    }

}
