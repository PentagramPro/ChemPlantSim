using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanelController : MonoBehaviour {

	public Plant plant;
	public Text infoText;
	public Compressor Comp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		infoText.text = string.Format("dM={0}\n dH={1}\n scale={2}\n LastFlow={3}",
		            plant.MaxDeltaM,plant.MaxDeltaH,plant.PlantTimeScale,Comp.LastFlow);
	}
}
