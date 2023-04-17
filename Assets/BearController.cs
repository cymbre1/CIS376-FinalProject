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
    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        GameObject f = GameObject.Find("Fox");
        fc = f.GetComponent<FoxController>();
        target = GameObject.Find("Fox").transform;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(AnimatorControllerParameter parameter in anim.parameters) {            
            anim.SetBool(parameter.name, false);            
        }


        if(Vector3.Distance(destination, target.position) > 1.0f )
        {
            if(!fc.hidden && (Vector3.Distance(target.position, transform.position) < 50.0f)) {
                anim.SetBool("RunForward", true);
                destination = target.position;
                agent.destination = destination;
                agent.speed = 10;
            }
            if( !fc.hidden && (Vector3.Distance(target.position, transform.position) < 100.0f)) 
            {
                anim.SetBool("WalkForward", true);
                destination = target.position;
                agent.destination = destination;
            } 
            else 
            {
                anim.SetBool("Idle", true);
                agent.destination = ogPos;
            }
        }

    }

    // private void ChangeDirection() {
    //      float angle = Random.Range(0f, 360f);
    //      Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
    //      Vector3 newUp = quat * Vector3.up;
    //      newUp.z = 0;
    //      newUp.Normalize();
    //      transform.up = newUp;
    //      timeToChangeDirection = 1.5f;
    //  }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player") {
            print("GAME OVER");
            // Destroy(col.gameObject);
            // TODO add the code that actually ends the game lol
        }
    }
}
