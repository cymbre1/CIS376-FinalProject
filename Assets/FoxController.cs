using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using 

public class FoxController : MonoBehaviour
{
    float horizontalSpeed = 10.0f;
    float verticalSpeed = 20.0f;
    Transform tf;
    
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d")){
			tf.position += Vector3.right * horizontalSpeed * Time.deltaTime;
		}
		if (Input.GetKeyDown("a")){
			tf.position += Vector3.left* horizontalSpeed * Time.deltaTime;
		}
		if (Input.GetKeyDown("w")){
			tf.position += Vector3.forward * horizontalSpeed * Time.deltaTime;
		}
		if (Input.GetKeyDown("s")){
			tf.position += Vector3.back* horizontalSpeed * Time.deltaTime;
		}        
        // float translation = Input.GetAxis("Vertical") * verticalSpeed;
        // float rotation = Input.GetAxis("Mouse X") * horizontalSpeed;
        // translation *= Time.deltaTime;
        // rotation *= Time.deltaTime;

        // transform.Translate(0, 0, translation);
        // transform.Rotate(0,rotation,0);
    }
}
