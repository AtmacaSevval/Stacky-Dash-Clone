using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Triggers : MonoBehaviour
{
    private SplineComputer spline;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        spline = GetComponent<SplineComputer>();
        for (int i = 0; i < spline.triggerGroups.Length; i++){
            Debug.Log(spline.triggerGroups[i].name);
            Debug.Log(spline.triggerGroups[i].triggers.Length);
        }
    }


    public void Active()
    {
        Debug.Log("asdew");
        FindObjectOfType<PlayerController>().spline = true;
    }

    public void DeActive()
    {
        Debug.Log("asdew");
        FindObjectOfType<PlayerController>().spline = false;
        foreach (Transform child in player.transform)
        {
            child.GetComponent<SplineFollower>().enabled = false;
        }
    }


}
