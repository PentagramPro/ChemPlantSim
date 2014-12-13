using UnityEngine;
using System.Collections;

public class GasFurnaceActuator : Actuator {

	public GasFurnace TargetFurnace;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	#region implemented abstract members of Actuator
	protected override void OnValueUpdated (float val)
	{
		TargetFurnace.FuelRate = val;
	}
	#endregion
}
