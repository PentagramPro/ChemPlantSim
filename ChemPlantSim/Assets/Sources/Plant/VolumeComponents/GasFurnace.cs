using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ChemVolume))]
public class GasFurnace : MonoBehaviour {

	public float FuelHeat;
	public float MaxFuelMassPerSec;

	ChemVolume vol;
	Plant plant;

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
		plant = GetComponent<Plant>();
	}
	// Use this for initialization
	void Start () {
	
	}

	public float RemoveHeat()
	{
		float h = vol.Mix.Heat;
		vol.Mix.Heat = 0;
		return h;
	}
	// Update is called once per frame
	void FixedUpdate () {
		vol.Mix.Heat+=FuelHeat*MaxFuelMassPerSec*FuelRate*plant.PlantDeltaTime;
	}


}
