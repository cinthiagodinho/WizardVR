using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisSpell : MonoBehaviour
{
    private Color baseColor;
    void Start()
    {
        baseColor = this.GetComponent<MeshRenderer>().material.color;
        this.GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
