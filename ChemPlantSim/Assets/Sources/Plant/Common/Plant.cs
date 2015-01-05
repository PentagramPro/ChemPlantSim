using UnityEngine;
using System.Collections.Generic;

public class Plant : MonoBehaviour {

	List<ChemVolume> Volumes = new List<ChemVolume>();
	public float PlantTimeScale = 1;

	public float MaxDeltaM,MaxDeltaH;


	public float PlantDeltaTime{
		get{
			return Time.fixedDeltaTime*PlantTimeScale;
		}
	}

	// Use this for initialization
	void Start () {
		GetComponentsInChildren<ChemVolume>(false,Volumes);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		if(MaxDeltaH>3e7f || MaxDeltaM>50)
			PlantTimeScale*=0.9f;
		else if(MaxDeltaM<5 && MaxDeltaH<1e6)
			PlantTimeScale/=0.9f;
		if(PlantTimeScale>10)
			PlantTimeScale=10;
		MaxDeltaH=0;MaxDeltaM=0;
		foreach(ChemVolume v in Volumes)
		{
			v.OnPrepareStep();
		}
		foreach(ChemVolume v in Volumes)
		{
			v.OnMoveMass();
		}

	
	}
}
