using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsData : MonoBehaviour
{
    public int fireDamages;
    public int fireIntervalle;
    public int fireDuration;

	public int getFireDamages(){
		return fireDamages;
	}
	public int getFireIntervalle(){
		return fireIntervalle;
	}
	public int getFireDuration(){
		return fireDuration;
	}

}
