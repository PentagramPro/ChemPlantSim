using UnityEngine;
using System.Collections.Generic;

public class Plant : MonoBehaviour {

	List<ChemVolume> Volumes = new List<ChemVolume>();

	// Use this for initialization
	void Start () {
		GetComponentsInChildren<ChemVolume>(false,Volumes);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		foreach(ChemVolume v in Volumes)
		{
			v.OnMoveMass();
		}
	}
}
