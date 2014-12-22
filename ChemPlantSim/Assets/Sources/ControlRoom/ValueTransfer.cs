using UnityEngine;
using System.Collections;

public class ValueTransfer : MonoBehaviour {

	public delegate void ValueUpdated(float val);
	public event ValueUpdated OnValueUpdated;

	public float Offset=0,Multiplier=1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnUpdateValue(float value)
	{
		if(OnValueUpdated!=null)
			OnValueUpdated((Offset+value)*Multiplier);
	}
}
