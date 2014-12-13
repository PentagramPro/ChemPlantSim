using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (ValueTransfer))]
public class ControlDisplay : MonoBehaviour {


	Text Label;
	void Awake(){
		Label = GetComponent<Text>();
		ValueTransfer indicator = GetComponent<ValueTransfer>();
		indicator.OnValueUpdated+=OnValueUpdated;
	}

	void OnValueUpdated(float val)
	{
		Label.text = val.ToString("0.00");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
