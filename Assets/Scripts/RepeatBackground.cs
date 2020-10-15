using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    void Start()
    {
        startPos = transform.position;
        //get startpos
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        //get width of object
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth) //when far enough left
        {
            transform.position = startPos;
            //reset to startpos and keep going
        }
    }
}
