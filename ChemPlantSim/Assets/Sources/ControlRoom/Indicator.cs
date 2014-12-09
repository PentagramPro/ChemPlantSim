using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {

	public delegate void ValueUpdated(float val);
	public event ValueUpdated OnValueUpdated;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnUpdateValue(float value)
	{
		if(OnValueUpdated!=null)
			OnValueUpdated(value);
	}
}
