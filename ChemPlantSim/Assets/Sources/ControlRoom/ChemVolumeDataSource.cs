using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Indicator))]
public class ChemVolumeDataSource : MonoBehaviour {

	public enum ValueType{
		Pressure, Temperature
	}
	public ChemVolume TargetVolume;
	public ValueType TargetType;

	Indicator indicator;
	// Use this for initialization
	void Awake(){
		indicator = GetComponent<Indicator>();
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		switch(TargetType)
		{
		case ValueType.Pressure:
			indicator.OnUpdateValue(TargetVolume.Pressure);
			break;
		case ValueType.Temperature:
			indicator.OnUpdateValue(TargetVolume.Mix.Temp);
			break;
		}
	}
}
