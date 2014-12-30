using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ValueTransfer))]
public class CompressorDataSource : MonoBehaviour {

	ValueTransfer indicator;
	public enum ValueType{
		Rpm
	}
	public Compressor TargetCompressor;
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
		case ValueType.Rpm:
			indicator.OnUpdateValue(TargetCompressor.CurrentRevs);
			break;
		}
	}
}
