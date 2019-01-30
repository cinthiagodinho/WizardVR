using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public float fireCooldown;
    private float _fireCooldown;
    public static int fireBall = 0;
    public GameObject fireBallSign;

    public float shieldCooldown;
    private float _shieldCooldown;
    public static int shield = 0;
    public GameObject shieldSign;

    public float zoneAttackCooldown;
    private float _zoneAttackCooldown;
    public static int zoneAttack = 0;
    public GameObject circleFireSign;


    void Start()
    {
        _fireCooldown = fireCooldown;
        _shieldCooldown = shieldCooldown;
        _zoneAttackCooldown = zoneAttackCooldown;
    }
    void Update()
    {
        if (fireBall == 1)
        {
            StartCoroutine(FireCooldown());
            fireBallSign.SetActive(false);
        }
        if (shield == 1)
        {
            StartCoroutine(ShieldCooldown());
            shieldSign.SetActive(false);
        }
        if (zoneAttack == 1)
        {
            StartCoroutine(ZoneAttackCooldown());
            circleFireSign.SetActive(false);
        }
    }

    IEnumerator FireCooldown()
    {
        fireCooldown = _fireCooldown;
        fireBall = 2;

        while (fireCooldown >= 0)
        {
            //Debug.Log("Fire Cooldown :" + fireCooldown);
            fireCooldown--;
            yield return new WaitForSeconds(1f);
        }

        fireBall = 0;
        fireBallSign.SetActive(true);
        StopCoroutine(FireCooldown());
    }

    IEnumerator ShieldCooldown()
    {
        shieldCooldown = _shieldCooldown;
        shield = 2;

        while (shieldCooldown > 0)
        {
            //Debug.Log("Shield Cooldown :" + shieldCooldown);
            shieldCooldown--;
            yield return new WaitForSeconds(1f);
        }
        shield = 0;
        shieldSign.SetActive(true);
        StopCoroutine(ShieldCooldown());
    }

    IEnumerator ZoneAttackCooldown()
    {
        zoneAttackCooldown = _zoneAttackCooldown;
        zoneAttack = 2;

        while (zoneAttackCooldown > 0)
        {           
            zoneAttackCooldown--;
            yield return new WaitForSeconds(1f);
        }

        zoneAttack = 0;
        circleFireSign.SetActive(true);
        StopCoroutine(ZoneAttackCooldown());
    }
}
