using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Indicator))]
public class ControlDisplay : MonoBehaviour {


	Text Label;
	void Awake(){
		Label = GetComponent<Text>();
		Indicator indicator = GetComponent<Indicator>();
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
