using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 1f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    public GameObject mainPlatform;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    private void Update()
    {

        Vector3 position = transform.position;
        position.x = mainPlatform.GetComponent<Renderer>().bounds.center.x;
        position.y = (target.position + offset).y;
        position.z = (target.position + offset).z;

        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, speed);
        

    }
}
