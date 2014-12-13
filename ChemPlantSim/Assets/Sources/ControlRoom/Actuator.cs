using UnityEngine;
using System.Collections;


[RequireComponent (typeof (ValueTransfer))]
public abstract class Actuator : MonoBehaviour {

	ValueTransfer vtransfer;

	protected virtual void Awake(){
		vtransfer = GetComponent<ValueTransfer>();
		vtransfer.OnValueUpdated+=OnValueUpdated;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected abstract void OnValueUpdated(float val);
}
