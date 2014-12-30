using UnityEngine;
using System.Collections;

public class ConnectionDataSource : MonoBehaviour {

	ValueTransfer indicator;
	public enum ValueType{
		ValvePosition
	}
	public ChemConnection TargetConnection;
	public ValueType TargetType;
	
	void Awake(){
		indicator = GetComponent<ValueTransfer>();
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch(TargetType)
		{
		case ValueType.ValvePosition:
			indicator.OnUpdateValue(TargetConnection.GateGap);
			break;
		}
	}
}
