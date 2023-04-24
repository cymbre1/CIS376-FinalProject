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
    
    protected bool foxAlive =  true;
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

        // Gives the bear an initial position to go towards
        ChangeDirection();        
        
        endGrowl = Resources.Load("Bear Growl 1") as AudioClip;
        bearLunch = Resources.Load("Lunch") as AudioClip;

    }

    // Update is called once per frame and has the bear move based on either a random position, or the fox's position
    void Update()
    {
        // Starts by turning off each possible animation
        foreach(AnimatorControllerParameter parameter in anim.parameters) {            
            anim.SetBool(parameter.name, false);            
        }

        // Checks that the current goal is within range of the fox position
        if(Vector3.Distance(destination, target.position) > 1.0f )
        {
            // Checks if the fox is within 100 feet
            if((Vector3.Distance(target.position, transform.position) < 100.0f)) {
                // checks if the fox is hidden, if it isn't, it starts to chase the fox
                if(!fc.hidden) {
                    chasing = true;
                    anim.SetBool("WalkForward", true);
                    destination = target.position;
                    agent.destination = destination;
                    if(!fc.bearMusic) {
                        fc.StartBearMusic();
                    }
                    agent.speed = 10;
                } else {
                    // makes sure the bear stops chasing the fox 
                    if(chasing) {
                        ChangeDirection();
                        chasing = false;
                    }
                }
            } 
            else 
            {
                chasing = false;
                anim.SetBool("WalkForward", true);
                if(agent.remainingDistance <= 1.0f){
                    ChangeDirection();
                }
            }
        }

        // has the bear growl every 300 frames
        if(timeSinceGrowl == 300) {
            sounds.PlayOneShot(bearLunch, 0.5f);
            timeSinceGrowl = 0;
        }

        timeSinceGrowl += 1;

    }

    // Changes the target position of the bear within a range of 200
    private void ChangeDirection() {
        Vector3 randomDirection = Random.insideUnitSphere * 200;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 200, 1);
        agent.destination = hit.position;
     }

    // checks the bear's collisions and moves to the end screen if it collides with player
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player") {
            if(foxAlive){
                Debug.Log("Dead Fox");
                sounds.PlayOneShot(endGrowl, 0.9f);
                foxAlive = false;
            }
            StartCoroutine(triggerGameOver(endGrowl.length));
        }
    }

    // Allows a wait time so before scene changes
    // Parameters:
    // float secs is the time you want to wait
    IEnumerator triggerGameOver(float secs) {
        yield return new WaitForSeconds(secs);
        FindObjectOfType<GameManager>().ChangeScene("End_Scene_Died");
    }
}
