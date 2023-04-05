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
    protected FoxController fc;
    protected Vector3 ogPos;
    // public bool hidden = false;

    // protected GameController gc;
    
    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        GameObject f = GameObject.Find("Fox");
        fc = f.GetComponent<FoxController>();
        target = GameObject.Find("Fox").transform;
        // fc = GetComponent<FoxController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(destination, target.position) > 1.0f)
        {
            if( !fc.hidden ) {
                destination = target.position;
                agent.destination = destination;
            } else {
                agent.destination = ogPos;
                print("Sneak");
            }
        }

    }
}
