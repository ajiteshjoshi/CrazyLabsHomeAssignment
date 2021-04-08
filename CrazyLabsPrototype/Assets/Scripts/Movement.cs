using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public KeyCode spaceButton;
    public Transform glider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(spaceButton)) {
            GetComponent<Rigidbody>().drag = 12;
        }
        
    }
}
