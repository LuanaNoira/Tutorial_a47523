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
        anim.SetTrigger("isWalking");
    }


    void Update() {

        if(agent.remainingDistance < 1)
        {
            agent.SetDestination(goalLocations[Random.Range(0,goalLocations.Length)].transform.position);
            
        }

    }
}