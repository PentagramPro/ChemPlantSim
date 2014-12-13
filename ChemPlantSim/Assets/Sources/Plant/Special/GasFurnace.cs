using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ChemVolume))]
public class GasFurnace : MonoBehaviour {

	public float FuelHeat;
	public float MaxFuelMassPerSec;

	ChemVolume vol;

	float fuelRate =0;
	public float FuelRate{
		get{
			return fuelRate;
		}
		set{
			fuelRate = Mathf.Clamp01(value);
		}
	}
	void Awake(){
		vol = GetComponent<ChemVolume>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		vol.Mix.Heat+=FuelHeat*MaxFuelMassPerSec*FuelRate*Time.fixedDeltaTime;
	}


}
