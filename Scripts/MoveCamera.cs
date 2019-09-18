using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveCamera : MonoBehaviour
{

    private Rigidbody Camera;
    private Vector3 offset;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 next = player.transform.position;
            next.y = -10;
            transform.position = next + offset;
        }
    }
}
