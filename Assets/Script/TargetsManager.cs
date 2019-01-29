using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsManager : MonoBehaviour
{

    public Target[] targets;

    void Start()
    {

    }
    void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.Keypad1))
            Destroy(targets[0].gameObject);*/

        if (!targets[1])
        {
            if (targets[1].gameObject.activeSelf == false)
            {
                if (!targets[0])
                    targets[1].gameObject.SetActive(true);
            }
        }
    }
}
