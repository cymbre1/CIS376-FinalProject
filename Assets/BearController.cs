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
    protected bool bearMusic = false;
    protected AudioSource sounds;

    protected AudioClip endGrowl;
    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;

        GameObject f = GameObject.Find("Fox");
        target = f.transform;

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        fc = f.GetComponent<FoxController>();
        sounds = GetComponent<AudioSource>();

        ChangeDirection();        
        
        endGrowl = Resources.Load("Bear Growl 1") as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(AnimatorControllerParameter parameter in anim.parameters) {            
            anim.SetBool(parameter.name, false);            
        }


        if(Vector3.Distance(destination, target.position) > 1.0f )
        {
            if(!fc.hidden && (Vector3.Distance(target.position, transform.position) < 75.0f)) {
                anim.SetBool("Run Forward", true);
                destination = target.position;
                agent.destination = destination;
                agent.speed = 10;
            }
            if( !fc.hidden && (Vector3.Distance(target.position, transform.position) < 150.0f)) 
            {
                anim.SetBool("WalkForward", true);
                destination = target.position;
                agent.destination = destination;
                if(!bearMusic) {
                    fc.StartBearMusic();
                    bearMusic = true;
                }
                agent.speed = 5;
            } 
            else 
            {
                anim.SetBool("WalkForward", true);
                if(bearMusic || agent.remainingDistance <= 1.0f){
                    ChangeDirection();
                }
                if(bearMusic) {
                    fc.StartMainTheme();
                    bearMusic = false;
                }
            }
        }

    }

    private void ChangeDirection() {
        Vector3 randomDirection = Random.insideUnitSphere * 200;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 200, 1);
        agent.destination = hit.position;
     }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player") {
            sounds.PlayOneShot(endGrowl, 0.9f);
            Debug.Log("Player hit");
            print("GAME OVER");
            // Destroy(col.gameObject);
            // TODO add the code that actually ends the game lol
        }
    }
}
