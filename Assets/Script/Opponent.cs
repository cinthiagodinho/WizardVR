using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opponent : MonoBehaviour
{   
    public int health;
    public SkinnedMeshRenderer mesh;
    public SkinnedMeshRenderer joint;
    private Color baseMesh;
    private Color baseJoint;
   
    public float fireDuration;
    public float infernoDuration;
    public Transform opp;
    public int isTouched = 0;

    void Start()
    {
        //lifeText.text = lifePoints.ToString();
        baseMesh = mesh.material.color;
        baseJoint = joint.material.color;  
    }

    void Update()
    {
        if (isTouched != 0)
        {
            mesh.material.color = Color.red;
            joint.material.color = Color.red;

            if (isTouched == 1)
                StartCoroutine(Hurt(2, fireDuration));

            isTouched = 0;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<AreaSpell>())
            StartCoroutine(Hurt(3, infernoDuration));
    }

    IEnumerator Hurt(int damage, float duration)
    {
        while (duration > 0)
        {
            health--;
            duration--;
            Debug.Log("health : " + health + " - duration : " + duration);
            yield return new WaitForSeconds(1);
        }

        mesh.material.color = baseMesh;
        joint.material.color = baseJoint;
        StopCoroutine(Hurt(0, 0));
    }

    public void setIsTouched(int value)
    {
        isTouched = value;
    }
}
