using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    public GameObject confetti; 
    public GameObject confetti1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        GameObject obj = Instantiate(confetti);

        GameObject obj1 = Instantiate(confetti1);
        Destroy(obj1, 2.5f);
        Destroy(obj, 2.5f);

    }
}
