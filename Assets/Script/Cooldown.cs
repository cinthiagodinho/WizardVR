using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public float fireCooldown;
    private float _fireCooldown;
    public float shieldCooldown;
    private float _shieldCooldown;
    public float zoneAttackCooldown;
    private float _zoneAttackCooldown;


    IEnumerator FireCooldown()
    {
        while (fireCooldown > -1)
        {
            fireCooldown--;
            yield return new WaitForSeconds(1f);
        }

        fireCooldown = _fireCooldown;
    }


    IEnumerator ShieldCooldown()
    {
        while (shieldCooldown > -1)
        {
            shieldCooldown--;
            yield return new WaitForSeconds(1f);
        }

        shieldCooldown = _shieldCooldown;
    }

    IEnumerator ZoneAttackCooldown()
    {
        while (zoneAttackCooldown > 0)
        {
            zoneAttackCooldown--;
            yield return new WaitForSeconds(1f);
        }
        zoneAttackCooldown = _shieldCooldown;
    }
}
