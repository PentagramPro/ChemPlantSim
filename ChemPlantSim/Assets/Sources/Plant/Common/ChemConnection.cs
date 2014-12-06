using UnityEngine;
using System.Collections;

public class ChemConnection : MonoBehaviour {

	public bool TransfersMass = true, TransfersHeat = false;
	public ChemVolume VolumeIn,VolumeOut;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos()
	{
		Gizmos.color = SchemeStyle.Connection;
		Gizmos.DrawCube(transform.position,new Vector3(0.5f,0.5f,0.5f));
		if( (TransfersHeat || TransfersMass) && VolumeIn!=null && VolumeOut!=null)
		{
			if(TransfersHeat && TransfersMass)
				Gizmos.color = SchemeStyle.ConnectionLinkBoth;
			else if(TransfersHeat)
				Gizmos.color = SchemeStyle.ConnectionLinkHeat;
			else
				Gizmos.color = SchemeStyle.ConnectionLinkMass;

			Gizmos.DrawLine(transform.position,VolumeIn.transform.position);
			Gizmos.DrawLine(transform.position,VolumeOut.transform.position);
			Gizmos.DrawWireSphere(Vector3.Lerp(transform.position,VolumeOut.transform.position,0.5f ),0.2f);
		}
	}
}
