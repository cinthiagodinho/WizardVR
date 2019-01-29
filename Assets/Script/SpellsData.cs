using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsData : MonoBehaviour
{
    public int fireDamages;
    public float fireIntervalle;
    public float fireDuration;

    public int areaSpellDamages;
    public float areaSpellIntervalle;
    public float areaSpellDuration;
	public float areaSpellLimit;

    public int getFireDamages()
    {
        return fireDamages;
    }
    public float getFireIntervalle()
    {
        return fireIntervalle;
    }
    public float getFireDuration()
    {
        return fireDuration;
    }
    public int getAreaSpellDamages()
    {
        return areaSpellDamages;
    }
    public float getAreaSpellIntervalle()
    {
        return areaSpellIntervalle;
    }
    public float getAreaSpellDuration()
    {
        return areaSpellDuration;
    }

	public float getAreaSpellLimit(){
		return areaSpellLimit;
	}

}
