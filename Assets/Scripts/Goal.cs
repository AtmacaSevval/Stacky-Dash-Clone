using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject CanvasObject;
    public GameObject player;

    private void Update()
    {
        if (player.transform.localPosition.z > transform.position.z)
        {
            Debug.Log("Game finished");
            if (transform.gameObject.tag == "ByRoad3" && player.transform.childCount == 2)
            {
                CanvasObject.SetActive(true);
                GameManager.singleton.EndGame(true);
            }
            else if(transform.gameObject.tag == "Chest")
            {
                CanvasObject.SetActive(true);
                GameManager.singleton.EndGame(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if(other.gameObject.name == "Stack")
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            CanvasObject.GetComponent<Canvas>().enabled = true;
        }*/
       
        /*
        PlayerController ball = other.GetComponent<PlayerController>();

        if (!ball || GameManager.singleton.GameEnded)
            return;

        Debug.Log("Goal was touched");

        GameManager.singleton.EndGame(true);
        */
    }
}
