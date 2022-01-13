using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public GameObject ParentStack;

    public void AddToStack(Vector3 delta)
    {
        gameObject.tag = "Normal";
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z) + delta;
        GetComponent<Collider>().isTrigger = false;

        gameObject.transform.SetParent(ParentStack.transform);
        //Destroy(GetComponent<Stack>());
    }

    public void RemoveFromStack()
    {
        tag = "Normal";
        gameObject.GetComponent<Renderer>().enabled = true;
    }
}
