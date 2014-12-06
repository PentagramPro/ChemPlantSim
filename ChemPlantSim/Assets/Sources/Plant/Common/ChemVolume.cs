using UnityEngine;
using System.Collections.Generic;

public class ChemVolume : MonoBehaviour {

	public List<ChemConnection> Connections {get;set;}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos()
	{
		Gizmos.color = SchemeStyle.Volume;
		Gizmos.DrawSphere(transform.position,0.5f);
	}
}
