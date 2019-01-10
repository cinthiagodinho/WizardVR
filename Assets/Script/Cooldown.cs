using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public float shieldCooldown;
    private float _shieldCooldown;
    public Text shieldTextCooldown;
    public float zoneAttackCooldown;
    private float _zoneAttackCooldown;
    public Text zoneAttackTextCooldown;

    IEnumerator ShieldCooldown()
    {
        while (shieldCooldown > -1)
        {
            shieldTextCooldown.text = shieldCooldown.ToString();
            shieldCooldown--;
            yield return new WaitForSeconds(1f);
        }
        shieldTextCooldown.text = "";
        shieldCooldown = _shieldCooldown;      
    }

    IEnumerator ZoneAttackCooldown()
    {
        while (zoneAttackCooldown > 0)
        {
            zoneAttackTextCooldown.text = zoneAttackCooldown.ToString();
            zoneAttackCooldown--;
            yield return new WaitForSeconds(1f);
        }
        zoneAttackTextCooldown.text = "";
        zoneAttackCooldown = _shieldCooldown; 
    }
}
