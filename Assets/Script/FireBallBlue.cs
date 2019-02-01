using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBlue : MonoBehaviour
{
    public GameObject ActivatedGameObject;
    public GameObject core;
    public GameObject coreLightning;

    void OnEnable()
    {
        ActivatedGameObject.SetActive(false);
    }
    void Start()
    {
        InvokeRepeating("ActivateGO", 0.8f, 0);
    }
    void ActivateGO()
    {
        ActivatedGameObject.SetActive(true);
        core.SetActive(false);
        coreLightning.SetActive(false);
        GetComponent<Collider>().isTrigger = false;
    }
    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
