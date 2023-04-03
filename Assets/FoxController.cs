using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using 

public class FoxController : MonoBehaviour
{
    float horizontalSpeed = 10.0f;
    float verticalSpeed = 20.0f;

    protected float elapsedTime = 0;
    protected Transform tf;
    protected BoxCollider bc;
    protected Vector2 rotation;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * verticalSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        rotation.y += Input.GetAxis ("Mouse X");
		rotation.x += -Input.GetAxis ("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -10.0f, 10.0f);
		transform.eulerAngles = (Vector2)rotation * horizontalSpeed;
    }
}
