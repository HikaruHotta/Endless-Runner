using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{

    public float jump = 30;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;
    public Text distanceText;


    private Rigidbody rb;

    public LayerMask groundLayers;

    public SphereCollider col;

    public Transform boomObj;

    private float startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        rb.velocity = new Vector3(0, 0, GM.playerSpeed); //initial velocity
        startPosition = rb.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        distanceText.text = "Distance: " + (GM.distance/10).ToString() + "m";
        float currentPosition = rb.position.z;
        GM.distance = currentPosition - startPosition;
        // optimization for faster jump and fall (less floating around)
        if (rb.velocity.y < 0) 
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb.velocity.y > 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        //updates velocity of rigidbody, namely player per frame
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 1.0f) * GM.playerSpeed;
        movement.y = rb.velocity.y;
        rb.velocity = movement;

        // jump animation
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }

    // checks whether the player is on the ground to prevent double jumping
    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center,
            new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z),
            col.radius * 0.9f, groundLayers);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "lethal")
        {
            
            GM.zVelAdj = 0;
            Instantiate(boomObj, transform.position, boomObj.rotation);
            string score = (GM.distance).ToString();
            GM.dead = true;
            Destroy(gameObject);
            Debug.Log(score);
            Application.Quit();
        }
    }
}
