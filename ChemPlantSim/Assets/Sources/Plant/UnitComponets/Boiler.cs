using UnityEngine;
using System.Collections;

public class Boiler : MonoBehaviour {



	public GasFurnace Furnace;
	public ChemVolume Tank;

	float integrator = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = Furnace.RemoveHeat();

		integrator+=x;
		x = integrator;
	}
}
