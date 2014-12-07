using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlDisplay : MonoBehaviour {

	public ChemVolume TargetVolume;
	Text Label;
	void Awake(){
		Label = GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Label.text = TargetVolume.Pressure.ToString("0.00");
	}
}
