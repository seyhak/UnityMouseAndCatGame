using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sniff : StateMachineBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    // Use this for initialization
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim = animator;
        //agent = GetComponentInParent<NavMeshAgent>();
        //StartCoroutine(Sniffing());
	}

    IEnumerator Sniffing()
    {

        Debug.Log("Pocz wąchania");
        float time = 0;
        agent.enabled = false;
        //sniffing = true;
        //body.velocity.Set(0, 0, 0);
        anim.SetBool("Sniff", true);
        anim.SetFloat("Move", 0f);
        while (time < 10)
        {
            time += Time.deltaTime;
        }
        Debug.Log("Koniec wąchania");
        anim.SetBool("Sniff", false);
        agent.enabled = true;
        yield return null;

    }
}
