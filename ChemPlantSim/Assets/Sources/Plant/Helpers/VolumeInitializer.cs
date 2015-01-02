using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (ChemVolume))]
public class VolumeInitializer : MonoBehaviour {

	public ChemElement Element;

	// In pascals
	public float Pressure;

	// In Kelvins
	public float Temperature;

	// Use this for initialization
	void Start () {

		ChemVolume vol = GetComponent<ChemVolume>();
		bool old = vol.Mix.Infinite;
		vol.Mix.Infinite=false;
		ChemFraction fraction = new ChemFraction(Element);
		fraction.Mass = Pressure*vol.Volume / (Constants.R*Temperature);
		vol.Mix.AddFraction(fraction);
		vol.Mix.Heat = Temperature*Element.HeatCap*fraction.Mass;
		vol.Mix.RebuildCache();
		vol.Mix.Infinite = old;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
