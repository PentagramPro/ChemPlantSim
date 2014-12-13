using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ValueTransfer))]
public class ConnectionActuator : Actuator {
	public enum ConnectionValue{ GateGap }

	public ConnectionValue ConnectionVal;

	public ChemConnection TargetConnection;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void OnValueUpdated(float val)
	{
		switch(ConnectionVal)
		{
		case ConnectionValue.GateGap:
			TargetConnection.GateGap = val;
			break;
		}
	}
}
