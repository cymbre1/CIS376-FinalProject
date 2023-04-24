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
    protected AudioSource sounds;

    protected float timeSinceGrowl = 0;

    protected AudioClip endGrowl;
    protected AudioClip bearLunch;

    public bool chasing;
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
        bearLunch = Resources.Load("Lunch") as AudioClip;

    }

    // Update is called once per frame
    void Update()
    {
        foreach(AnimatorControllerParameter parameter in anim.parameters) {            
            anim.SetBool(parameter.name, false);            
        }


        if(Vector3.Distance(destination, target.position) > 1.0f )
        {
            if((Vector3.Distance(target.position, transform.position) < 100.0f)) {
                // if the bear is within 100 feet, start chasing towards the fox
                if(!fc.hidden) {
                    print("CHASING");
                    chasing = true;
                    // fc.chased = true;
                    anim.SetBool("WalkForward", true);
                    destination = target.position;
                    agent.destination = destination;
                    if(!fc.bearMusic) {
                        fc.StartBearMusic();
                    }
                    agent.speed = 10;
                } else {
                    print("Not Chasing");
                    // fc.chased = false;
                    if(chasing) {
                        ChangeDirection();
                        chasing = false;
                    }
                }
            } 
            else 
            {
                anim.SetBool("WalkForward", true);
                if(agent.remainingDistance <= 1.0f){
                    ChangeDirection();
                }
            }
        }

        if(timeSinceGrowl == 300) {
            sounds.PlayOneShot(bearLunch, 0.5f);
            timeSinceGrowl = 0;
        }

        timeSinceGrowl += 1;

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
            StartCoroutine(triggerGameOver(endGrowl.length));
        }
    }

    IEnumerator triggerGameOver(float secs) {
        yield return new WaitForSeconds(secs);
        FindObjectOfType<GameManager>().ChangeScene("End_Scene_Died");
    }
}
