using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

    private NavMeshAgent agent;////(1)
    private Vector3 target;
    void Start () {
        agent = GetComponent<NavMeshAgent>();////(2)
        target = transform.position;
    }
    void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);////(3)
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) ////(4)
            && Physics.Raycast(ray, out hit) 
            && hit.transform.gameObject.tag == "Ground")
        {
            target = hit.point;////(5)
        }
        agent.SetDestination(target);////(6)
    }
}
