using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ChemVolume))]
public class OpenVolume : MonoBehaviour {

	public ChemElement O2,N2,CO2,InertGas;
	public float frO2,frN2,frCO2;
	ChemVolume volume;


	void Awake()
	{
		volume = GetComponent<ChemVolume>();


	}
	// Use this for initialization
	void Start () {
		float m = GasUtils.CalculateMass(Constants.WorldPressure,volume.Volume, Constants.WorldTemp);
		volume.Mix.AddFraction(new ChemFraction(O2,m*frO2));
		volume.Mix.AddFraction(new ChemFraction(N2,m*frN2));
		volume.Mix.AddFraction(new ChemFraction(CO2,m*frCO2));
		volume.Mix.AddFraction(new ChemFraction(InertGas,m*(1-frO2-frN2-frCO2)));
		volume.Mix.Heat = m*volume.Mix.HeatCapacity*Constants.WorldTemp;
		volume.Mix.Infinite = true;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}
}
