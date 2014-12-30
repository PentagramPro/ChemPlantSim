using UnityEngine;
using System.Collections;

public class CompressorActuator : Actuator {

	public enum ValueType{
		Rpm
	}
	public Compressor TargetCompressor;
	public ValueType TargetType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of Actuator

	protected override void OnValueUpdated (float val)
	{
		switch(TargetType)
		{
		case ValueType.Rpm:
			TargetCompressor.TargetRevs = val;
			break;
		}
	}

	#endregion
}
