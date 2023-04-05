using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using 

public class FoxController : MonoBehaviour
{
    float horizontalSpeed = 5.0f;
    float verticalSpeed = 10.0f;

    protected float elapsedTime = 0;
    protected Transform tf;
    protected BoxCollider bc;
    protected Vector2 rotation;
    public bool hidden = false;
    protected Animator anim;
    protected Vector3 previousPos;


    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        bc = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(AnimatorControllerParameter parameter in anim.parameters) {            
            anim.SetBool(parameter.name, false);            
        }

        // anim.SetBool("WalkForward", true);
        
        elapsedTime += Time.deltaTime;

        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * verticalSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        
        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rigidbody player = GetComponent<Rigidbody>();
        // Vector3 vel = player.velocity;
        
        print("WHAT THE FUCK");
        print(tf.hasChanged);
        // print(previousPos);
        // if(tf.position != previousPos) {
        //     print("I'm moving");
        //     if(Input.GetAxis("Mouse X") > 0) {
        //         anim.SetBool("WalkLeft", true);
        //     } else if(Input.GetAxis("Mouse X") < 0) {
        //         anim.SetBool("WalkRight", true);
        //     } else {
        //         anim.SetBool("WalkForward", true);
        //     }
        // } 

        rotation.y += Input.GetAxis("Mouse X");
		rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -10.0f, 10.0f);
		transform.eulerAngles = (Vector2)rotation * horizontalSpeed;
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "RedBush") {
            hidden = true;
            print("What does the fox say");
        } 

        // print(col.gameObject.tag);


    }

    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "RedBush") {
            hidden = false;
            print("DINGDINGDINGDINGDINGDINGDINGDINGDING");
        } 
    }
}
