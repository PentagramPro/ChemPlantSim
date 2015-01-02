using UnityEngine;
using System.Collections;

[RequireComponent (typeof (TurbineGasodynamic))]
public class Compressor : MonoBehaviour {

	// Density of the gas in this model (not real)
	float IdealDensity = 1;

	public float TargetRevs {get;set;}
	public float CurrentRevs{get;internal set;}

	public ChemVolume SteamIn,SteamOut,GasIn,GasOut;
	float steamToMass = 0.002f;
	float revRegulatorGain = 8e-6f;
	public ChemConnection SteamValve;
	CTDelay turbineInertia = new CTDelay(0,0.2f);
	//CTDelay revRegDelay = new CTDelay(0,0.66f);
	CTIntegrator revRegIntegrator = new CTIntegrator(0);
	CTDelay revRegDelay = new CTDelay(0, 0.666f);
	TurbineGasodynamic turbine;


	// Use this for initialization
	void Start () {
		turbine = GetComponent<TurbineGasodynamic>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = SteamIn.Pressure-SteamOut.Pressure;
		x*=steamToMass;
		SteamOut.Mix.AddMix(SteamIn.Mix.TakeMix(x*Time.fixedDeltaTime));
		x = turbineInertia.Next(x);
		CurrentRevs = x;
		float flow = turbine.CalculateFlow(GasOut.Pressure,x);
		if(flow<0)
			flow = 0;
		float kgPerSec = Time.fixedDeltaTime* flow*IdealDensity/60f;

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
