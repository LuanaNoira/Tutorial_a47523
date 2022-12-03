using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour {

    GameObject[] goalLocations;
    UnityEngine.AI.NavMeshAgent agent;
    Animator anim;

    void Start() {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0,goalLocations.Length)].transform.position);
        anim = this.GetComponent<Animator>();
        anim.SetFloat("wOffset", Random.Range(0,1));
        anim.SetTrigger("isWalking");
        float sm = Random.Range(0.1f, 1.5f);
        anim.SetFloat("speedMult", sm);
        agent.speed *= sm;
    }


    void Update() {

        if(agent.remainingDistance < 1)
        {
            agent.SetDestination(goalLocations[Random.Range(0,goalLocations.Length)].transform.position);
            
        }

    }
}