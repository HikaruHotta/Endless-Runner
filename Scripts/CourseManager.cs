using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{

    public GameObject[] courseFabs;

    private GameObject playerObject;
    private float spawnZ = 100.0f;
    private float courseLength = 100.0f;
    private int numCoursesOnScreen = 5;
    private List<GameObject> activeCourses;

    // Start is called before the first frame update
    void Start()
    {
        activeCourses = new List<GameObject>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < numCoursesOnScreen; i++)
        {
            spawnCourse(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject && playerObject.transform.position.z > (spawnZ + 100.0f - numCoursesOnScreen * courseLength))
        {
            spawnCourse();
            deleteCourse();
            GM.playerSpeed += 1.0f;
        }
    }

    private void spawnCourse(int prefabindex = -1)
    {
        GameObject go;
        go = Instantiate(courseFabs[0]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(0, 0, spawnZ);
        spawnZ += courseLength;
        activeCourses.Add(go);
    }

    private void deleteCourse()
    {
        Destroy(activeCourses[0]);
        activeCourses.RemoveAt(0);
    }
}
