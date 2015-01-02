using UnityEngine;
using System.Collections;

public class BoilerActuator : Actuator {
	public enum BoilerValue{ FuelRate }
	
	public BoilerValue BoilerVal;
	
	public Boiler TargetBoiler;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void OnValueUpdated(float val)
	{
		switch(BoilerVal)
		{
		case BoilerValue.FuelRate:
			TargetBoiler.BurnRate = val;
			break;
		}
	}
}
