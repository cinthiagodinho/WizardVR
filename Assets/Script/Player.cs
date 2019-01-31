﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int lifePoints;
    private int _lifePoints;
    private int stepLife;
    public int damages;
    private Rigidbody rb;
    private float speed = 2.0f;
    public Image[] redView;
    protected int isTouched = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stepLife = lifePoints / 4;
        _lifePoints = lifePoints;
    }
    public void setIsTouched(int value)
    {
        isTouched = value;
    }
    /*   void OnCollisionEnter(Collision collision)
      {

          if (collision.gameObject.GetComponentInParent<FireBallSpell>())
          {
              lifePoints -= damages;
              //            Debug.Log("Life points : " + lifePoints);

              if (lifePoints <= (_lifePoints - stepLife))
                  redView[0].gameObject.SetActive(true);

              if (lifePoints <= (_lifePoints - stepLife * 2))
              {
                  redView[0].gameObject.SetActive(false);
                  redView[1].gameObject.SetActive(true);
              }

              if (lifePoints <= (_lifePoints - stepLife * 3))
              {
                  redView[1].gameObject.SetActive(false);
                  redView[2].gameObject.SetActive(true);
              }
              if (lifePoints <= (_lifePoints - stepLife * 4))
              {
                  redView[2].gameObject.SetActive(false);
                  redView[3].gameObject.SetActive(true);
              }
          }
      }*/

    void Update()
    {
        if (isTouched != 0)
        {
            lifePoints -= damages;

            if (lifePoints <= (_lifePoints - stepLife))
                redView[0].gameObject.SetActive(true);

            if (lifePoints <= (_lifePoints - stepLife * 2))
            {
                redView[0].gameObject.SetActive(false);
                redView[1].gameObject.SetActive(true);
            }

            if (lifePoints <= (_lifePoints - stepLife * 3))
            {
                redView[1].gameObject.SetActive(false);
                redView[2].gameObject.SetActive(true);
            }
            if (lifePoints <= (_lifePoints - stepLife * 4))
            {
                redView[2].gameObject.SetActive(false);
                redView[3].gameObject.SetActive(true);
            }

            isTouched = 0;
        }
    }
}

