using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public Transform player;
    
    // This calls in any extra variables the minimap needs.
    // Adds the transform of the fox to the player variable
    void Start()
    {
        player = GameObject.Find("Fox").transform;
    }

    // Shifts the camera to follow the fox's position.  
    // This is after the fox has moved in the update.
    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;

        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
