using UnityEngine;
using System.Collections;

public class Compressor : MonoBehaviour {

	// Density of the gas in this model (not real)
	float IdealDensity = 1;

	public float TargetRevs {get;set;}
	public float CurrentRevs{get;internal set;}

	public ChemVolume SteamIn,SteamOut,GasIn,GasOut;
	float steamToMass = 0.002f;
	float revRegulatorGain = 9e-6f;
	public ChemConnection SteamValve;
	CTDelay turbineInertia = new CTDelay(0,0.2f);
	CTDelay revRegDelay = new CTDelay(0,0.66f);
	CTIntegrator revRegIntegrator = new CTIntegrator(0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = SteamIn.Pressure-SteamOut.Pressure;
		x*=steamToMass;
		x = turbineInertia.Next(x);
		CurrentRevs = x;
		float flow = CalculateFlow(GasOut.Pressure,x);
		float kgPerSec = flow*IdealDensity/60f;

		GasOut.Mix.AddMix(GasIn.Mix.TakeMix(kgPerSec*Time.fixedDeltaTime));
	
	}

	float CalculateFlow(float pressure,float revs)
	{
		float pressureShift = 35+0.1f*(revs-3150);
		float flowShift = 1450+0.8f*(revs-3150);
		float x = pressure*1e-5f-pressureShift;
		x*=0.0005f;
		x = Mathf.Min(x,-0.0005f);
		x = 1/x+flowShift;
		return x;
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
