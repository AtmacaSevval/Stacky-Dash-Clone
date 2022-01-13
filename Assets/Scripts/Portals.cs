using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Portal2;
    public GameObject Player;


    private Vector3 delta;
    void Start()
    {
        delta = Portal.gameObject.transform.position - Portal2.gameObject.transform.position;
    }

    public void onPortal(GameObject portal, GameObject bottom ){
        if (portal.gameObject == Portal)
        {

            Vector3 pos = Portal.transform.position - bottom.transform.position;
            Player.transform.position += -delta + pos;
        }
        else if(portal.gameObject == Portal2)
        {
            Vector3 pos = Portal2.transform.position - bottom.transform.position;
            Player.transform.position += delta + pos;
        }
    }
}
