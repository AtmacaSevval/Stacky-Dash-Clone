using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed =20f;
    [SerializeField] int minSwipeRecognition = 5;

    public GameObject bottomPlatform;

    private bool isTraveling;

    private Vector3 travelDirection;
    private Vector3 nextCollisionPosition;

    private Vector2 swipePosLastFrame;
    private Vector2 swipePosCurrentFrame;
    private Vector2 currentSwipe;

    private Vector3 position;
    private Vector3 notAllowedWay;
    private bool notEnoughPlatform = false;

    private bool isPortal;
    private bool cantMove;


    public bool spline;

    private void FixedUpdate()
    {
        if (GameManager.singleton.GameEnded || spline)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        if (notAllowedWay == travelDirection & notEnoughPlatform || cantMove)
        {
            rb.velocity = Vector3.zero;
            isTraveling = false;

        }

        else if (isTraveling)
        {

            notEnoughPlatform = false;
            rb.velocity = speed * travelDirection;
        }

        Debug.Log(isTraveling);
        if (nextCollisionPosition != Vector3.zero)
        {
            if (Vector3.Distance(bottomPlatform.transform.position, nextCollisionPosition) < 0.51f)
            {

                isTraveling = false;

                isPortal = true;
                travelDirection = Vector3.zero;
                nextCollisionPosition = Vector3.zero;
            }

        }


        if (isTraveling)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (!GameManager.singleton.GameStarted)
                GameManager.singleton.StartGame();

            swipePosCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if(swipePosLastFrame != Vector2.zero)
            {
                currentSwipe = swipePosCurrentFrame - swipePosLastFrame;
                if(currentSwipe.sqrMagnitude < minSwipeRecognition)
                {
                    return;
                }

                currentSwipe.Normalize();
                
                //Up/down

                if(currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    //Go up/down
                    SetDestination(currentSwipe.y>0 ? Vector3.forward : Vector3.back);
                }
                if(currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    //Go Left/Right
                    SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);

                }
            }

            swipePosLastFrame = swipePosCurrentFrame;
        }
        

        if (Input.GetMouseButtonUp(0))
        {
            swipePosLastFrame = Vector2.zero;
            currentSwipe = Vector2.zero;
        }
        
    }


   private void SetDestination(Vector3 direction)
   {
        travelDirection = direction;

        RaycastHit hit;

        if(Physics.Raycast(bottomPlatform.transform.position, direction,out hit, 100f))
        {
            nextCollisionPosition = hit.point; 
        }

        isTraveling = true;

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Wall"){
            //transform.localPosition = new Vector3(Mathf.Round(transform.localPosition.x * 10.0f) * 0.1f, 0, Mathf.Round(transform.localPosition.z * 10.0f) * 0.1f);
            Vector3 pos = transform.localPosition;
            transform.localPosition = new Vector3((float)(double)Math.Round(pos.x, 1), 0, (float)(double)Math.Round(pos.z, 1));
        }



    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Give" || other.gameObject.tag == "GiveForScore")
        {
            if (other.gameObject.tag == "GiveForScore")
                GameManager.singleton.AddToScore(1);

            if (bottomPlatform.tag == "PlayerPlatform")
            {
                rb.transform.position = position + (Vector3.up * 0.1f);
                notEnoughPlatform = true;
                return;
            }
            if (rb.transform.GetChild(transform.childCount - 2).gameObject.tag == "PlayerPlatform")
            {
                position = rb.transform.position + (other.gameObject.transform.position - bottomPlatform.transform.position);
                notAllowedWay = travelDirection;
            }
            else
            {
                notAllowedWay = Vector3.zero;
            }

            GivePlatform(other);
        }
        if (other.gameObject.tag == "MainPlatforms")
        {
            FindObjectOfType<CameraFollow>().mainPlatform = other.gameObject;
        }
        if (other.gameObject.tag == "Portal")
        {
            if (!isPortal)
                return;
            cantMove = true;
            FindObjectOfType<Portals>().onPortal(other.gameObject, bottomPlatform);
            StartCoroutine(MyCoroutine());

        }
        if (other.gameObject.tag == "PickUp")
        {
            GameManager.singleton.AddToScore(1);
            PickUpPlatform(other);
        }

      
    }

    private void PickUpPlatform(Collider other)
    {
        Vector3 delta = bottomPlatform.transform.position - other.transform.localPosition;
        foreach (Transform child in transform)
        {
            Vector3 pos = child.transform.position;
            pos.y += 0.1f;
            child.transform.position = pos;
        }
        other.gameObject.GetComponent<Stack>().AddToStack(delta);

        bottomPlatform = other.gameObject;
    }


    private void GivePlatform(Collider other)
    {
        other.gameObject.GetComponent<Stack>().RemoveFromStack();

        GameObject destroyedPlatform = bottomPlatform;
        destroyedPlatform.transform.parent = null;

        bottomPlatform = rb.transform.GetChild(transform.childCount - 1).gameObject;
        Destroy(destroyedPlatform);

        foreach (Transform child in transform)
        {
            Vector3 pos = child.transform.position;
            pos.y -= 0.1f;
            child.transform.position = pos;
        }

    }

    IEnumerator MyCoroutine()
    {
        isPortal = false;
        yield return new WaitForSeconds(0.5f);
        cantMove = false;
    }

}
