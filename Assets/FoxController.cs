using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public bool apple = false;
    public bool banana = false;
    public bool carrot = false;
    public bool onion = false;
    public bool garlic = false;
    public bool egg = false;
    public bool ham = false;
    public bool cake = false;
    public bool pumpkin = false;
    public bool tomato = false;

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

        elapsedTime += Time.deltaTime;

        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * verticalSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        
        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        if(Input.GetAxis("Vertical") > 0.02 || Input.GetAxis("Vertical") < -0.02) {
            if(Input.GetAxis("Mouse X") > 0.3) {
                anim.SetBool("WalkRight", true);
            } else if(Input.GetAxis("Mouse X") < -0.3) {
                anim.SetBool("WalkLeft", true);
            } else {
                print("WALk");
                anim.SetBool("WalkForward", true);
            }
        } else {
            anim.SetBool("Stop", true);
            print("stopped");            
        }

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x = Mathf.Clamp(rotation.x, -10.0f, 10.0f);
		transform.eulerAngles = (Vector2)rotation * horizontalSpeed;

        previousPos = transform.position;
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "RedBush") {
            hidden = true;
            print("What does the fox say");
        } 

        if(col.gameObject.tag == "Collectible") {
            if(col.gameObject.name == "apple") {
                apple = true;
            } else if(col.gameObject.name == "banana") {
                banana = true;
            } else if(col.gameObject.name == "carrot") {
                carrot = true;
            } else if(col.gameObject.name == "onion") {
                onion = true;
            } else if(col.gameObject.name == "garlic") {
                garlic = true;
            } else if(col.gameObject.name == "egg") {
                egg = true;
            } else if(col.gameObject.name == "ham") {
                ham = true;
            } else if(col.gameObject.name == "cake") {
                cake = true;
            } else if(col.gameObject.name == "pumpkin") {
                pumpkin = true;
            } else if(col.gameObject.name == "tomato") {
                tomato = true;
            }

            Destroy(col.gameObject);
        }


    }

    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "RedBush") {
            hidden = false;
            print("DINGDINGDINGDINGDINGDINGDINGDINGDING");
        } 
    }
}
