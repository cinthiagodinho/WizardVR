using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    public Text lifeText;
    private int lifePoints = 100;

    void Start()
    {
        lifeText.text = lifePoints.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FirePower>())
        {
            lifePoints--;
            lifeText.text = lifePoints.ToString();
            //StartCoroutine("Hurt");
        }

    }
    /*IEnumerator Hurt()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.red;
        lifePoints--;
        lifeText.text = lifePoints.ToString();
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<MeshRenderer>().material.color = Color.black;
        StopCoroutine("Hurt");
    }*/
}
