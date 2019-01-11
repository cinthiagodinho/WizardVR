using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Vector3 startMarker;
    public Vector3 endMarker;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    private float distCovered = 0;
    private float fracJourney = 0;
    private int way = 1;

    void Start()
    {
        startTime = Time.time;
        startMarker = gameObject.transform.position;
        endMarker = new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y, gameObject.transform.position.z);
        journeyLength = Vector3.Distance(startMarker, endMarker);
        distCovered = (Time.time - startTime) * speed;
        fracJourney = distCovered / journeyLength;
    }

    void Update()
    {
        distCovered = (Time.time - startTime) * speed;
        fracJourney = distCovered / journeyLength;

        if (way == 1)
            StartCoroutine(MoveToTheRight(fracJourney));
        else if (way == 2)
        {
            StartCoroutine(MoveToTheLeft(fracJourney));
        }
    }

    IEnumerator MoveToTheRight(float a)
    {
        transform.position = Vector3.Lerp(startMarker, endMarker, a);
        yield return new WaitForFixedUpdate();
        StopCoroutine(MoveToTheRight(0));
        way = 2;
    }

    IEnumerator MoveToTheLeft(float a)
    {
        transform.position = Vector3.Lerp(endMarker, startMarker, a);
        yield return new WaitForFixedUpdate();
        StopCoroutine(MoveToTheLeft(0));
        way = 1;
    }
}
