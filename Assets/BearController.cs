using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BearController : MonoBehaviour
{
    protected Animator anim;
    protected Transform target;
    protected Vector3 destination;
    protected NavMeshAgent agent;
    protected float elapsed = 0;

    // protected GameController gc;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Fox").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(destination, target.position) > 1.0f)
        {
            destination = target.position;
            agent.destination = destination;
        }

        if(elapsed > 0.0f){
            elapsed += Time.deltaTime;
        }
        if(elapsed > 3.0f){
            Destroy(gameObject);
        }
    }
}
