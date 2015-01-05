using UnityEngine;
using System.Collections;

[RequireComponent (typeof (TurbineGasodynamic))]
public class Compressor : MonoBehaviour {

	// Density of the gas in this model (not real)
	float IdealDensity = 1;

	public float TargetRevs {get;set;}
	public float CurrentRevs{get;internal set;}

	public ChemVolume SteamIn,SteamOut,GasIn,GasOut;
	public ChemConnection SteamValve;

	float MassToRpmGain = 1000f;

	float steamToMass = 5e-7f;
	float revRegulatorGain = 8e-6f;
	Plant plant;
	public float LastFlow=0;
	CTDelay turbineInertia;

	CTIntegrator revRegIntegrator;
	CTDelay revRegDelay;
	TurbineGasodynamic turbine;


	// Use this for initialization
	void Awake () {
		turbine = GetComponent<TurbineGasodynamic>();
		plant = GetComponentInParent<Plant>();

		turbineInertia = new CTDelay(plant,0,0.02f);

		revRegIntegrator = new CTIntegrator(plant,0);
		revRegDelay = new CTDelay(plant,0, 0.666f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = SteamIn.Pressure-SteamOut.Pressure;
		x*=steamToMass;
		if(x>0)
			SteamOut.Mix.AddMix(SteamIn.Mix.TakeMix(x*plant.PlantDeltaTime));
		else
			SteamIn.Mix.AddMix(SteamOut.Mix.TakeMix(-x*plant.PlantDeltaTime));

		x = turbineInertia.Next(x);
		x*=MassToRpmGain;
		CurrentRevs = x;
		float flow = turbine.CalculateFlow(GasOut.Pressure,x);
		if(flow<0)
			flow = 0;

		// flow in kg per minute 
		float kgPerSec = plant.PlantDeltaTime* flow*IdealDensity/60f;
		LastFlow = flow;
		if(kgPerSec!=0)
		{

			GasOut.Mix.AddMix(GasIn.Mix.TakeMix(kgPerSec));
		}
		RevolutionRegulator();

	}



	void RevolutionRegulator()
	{
		float x = TargetRevs - CurrentRevs;






		x = revRegIntegrator.Next(x);
		x = revRegDelay.Next(x);

		x*=revRegulatorGain;

		x = Mathf.Clamp01(x);
		SteamValve.GateGap = x;
	}
}
