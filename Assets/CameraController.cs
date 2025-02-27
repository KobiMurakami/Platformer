using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () 
    {
    	transform.position = new Vector3 (player.position.x, 7, -10); // Camera follows the player but 6 to the right
    }
}
