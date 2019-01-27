using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Transform player;


    public Transform startMarker;
    public Transform endMarker;
    public float speed;
    private bool finishedMove = false;
    private int direction = 0;
    public float intervalle;
    private float journeyLength;
    private float fracJourney;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    void Update()
    {
        //Aim the player
        transform.LookAt(player);

        if (!finishedMove)
        {
            if (direction == 0)
                StartCoroutine(Move_Routine(startMarker.transform.position, endMarker.transform.position));
            else if (direction == 1)
            {
                StartCoroutine(Move_Routine(endMarker.transform.position, startMarker.transform.position));
            }
        }
    }

    IEnumerator Move_Routine(Vector3 from, Vector3 to)
    {
        float startTime = Time.time;

        while (transform.position != to)
        {
            fracJourney = ((Time.time - startTime) * speed) / journeyLength;
            transform.position = Vector3.Lerp(from, to, fracJourney);
            yield return null;
        }
        if (direction == 0)
            direction = 1;
        else if (direction == 1)
            direction = 0;

        finishedMove = true;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        StopCoroutine(Move_Routine(Vector3.zero, Vector3.zero));
        yield return new WaitForSeconds(intervalle);      
        finishedMove = false;
        StopCoroutine(Wait());
    }
}
