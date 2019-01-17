using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Transform player;


    public Transform startMarker;
    public Transform endMarker;
    public float speed;
    private float startTime;
    private float journeyLength;
    private bool finishedMove = false;
    private int direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        direction = 1; // 0 means to the left, 1 means to the right
    }

    void Update()
    {
        //Aim the player
        transform.LookAt(player);

        if (!finishedMove)
        {
            //For smoothing moves to the two positions
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            if (direction == 0)
            {
                transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
                //Target has finished to go on the left
                if (transform.position == endMarker.position)
                    finishedMove = true;
            }
            else if (direction == 1)
            {
                transform.position = Vector3.Lerp(endMarker.position, startMarker.position, fracJourney);
                //Target has finished to go on the right
                if (transform.position == startMarker.position)
                    finishedMove = true;
            }
        }

        if (finishedMove)
        {
            if (transform.position == endMarker.position)
            {
                direction = 1;
            }
            else if (transform.position == startMarker.position)
            {
                direction = 0;
            }
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        finishedMove = false;
        startTime = Time.time;
    }

}
