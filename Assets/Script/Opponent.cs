using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opponent : MonoBehaviour
{
    //public Text lifeText;
    public int health;
    public SkinnedMeshRenderer mesh;
    public SkinnedMeshRenderer joint;
    private Color baseMesh;
    private Color baseJoint;
    public GameObject fire;
    public float fireDuration;
    public float infernoDuration;
    Transform opp;
    public int isTouched = 0;

    void Start()
    {
        //lifeText.text = lifePoints.ToString();
        opp = gameObject.transform;
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

        // if (Input.GetKeyDown(KeyCode.Keypad7))
        //     DoFire();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<ZoneAttackSpell>())
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

    void DoFire()
    {
        Quaternion rotation = Quaternion.LookRotation(-opp.forward, Vector3.up);
        Vector3 vect = new Vector3(opp.position.x, opp.position.y, (opp.position.z - 0.5f));
        Debug.Log(vect);
        GameObject fireInstance = GameObject.Instantiate(fire, vect, rotation) as GameObject;
        StartCoroutine(IEDoFire(fireInstance));
    }

    IEnumerator IEDoFire(GameObject fireInstance)
    {
        yield return new WaitForSeconds(.1f);
        fireInstance.GetComponent<Collider>().enabled = true;
    }

    public void setIsTouched(int value)
    {
        isTouched = value;
    }
}
