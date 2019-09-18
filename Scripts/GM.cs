using UnityEngine;
using System;


public class GM : MonoBehaviour
{
    public static float vertVel = 0;
    public static float distance = 0;
    public static float zVelAdj = 1;
    public static bool dead = false;
    public static float playerSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        UnitySystemConsoleRedirector.Redirect();
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.dead)
        {
            Debug.Log("dead");
        }
    }
}
