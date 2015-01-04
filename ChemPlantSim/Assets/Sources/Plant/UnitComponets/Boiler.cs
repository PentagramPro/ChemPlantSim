using UnityEngine;
using System.Collections;

public class Boiler : MonoBehaviour {



	//public GasFurnace Furnace;
	public float FuelHeat,MaxFuelPerSec;
	public float BurnRate {get;set;}
	public ChemVolume Tank;
	public ChemElement SteamElement;

	Plant plant;
	CTDelay boilerDelay;
	CTIntegrator boilerHeat;
	float CMboiler = 4.2e6f;
	float Csteam = 2.3e6f;
	float Msteam = 100;
	float Qremove = 0;
	float Tremove = 373f;
	float Tboiler = 0;

	// Use this for initialization
	void Start () {
		plant = GetComponentInParent<Plant>();
		ChemFraction fraction = new ChemFraction(SteamElement);
		fraction.Mass = Constants.WorldPressure*Tank.Volume / (Constants.R*Constants.WorldTemp);
		Tank.Mix.AddFraction(fraction);
		Tank.Mix.Heat=Constants.WorldTemp*SteamElement.HeatCap*fraction.Mass;
		Tank.Mix.RebuildCache();
		boilerHeat = new CTIntegrator(plant,300f*CMboiler);
		boilerDelay = new CTDelay(plant,0,0.05f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = FuelHeat*MaxFuelPerSec*BurnRate;

		x-= Qremove;
		x = boilerDelay.Next(x);
		x = boilerHeat.Next(x);
		//Tank.Mix.Heat = x;

		x/=CMboiler;

		// x is boiler temperature
		Tboiler = x;
		Tank.Mix.Temp = x;

		x-=Tremove;
		x*=Csteam;
		if(x>0)
		{
			x = Mathf.Min(x,Msteam*Csteam);
		}
		else
		{
			x=0;
		}
		Qremove = x;
		x/=Csteam;

		// x is a mass of steam produced
		Tank.Mix.AddFraction(new ChemFraction(SteamElement,x));

		x = Tank.Mix.Mass*Constants.R*Tboiler/Tank.Volume;
		x*=1e-6f;
		Tremove = 179.47f*(Mathf.Pow(x,0.2391f))+273f;


	}

	void OnDrawGizmos()
	{

	}
}
