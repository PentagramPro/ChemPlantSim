using UnityEngine;
using System.Collections.Generic;

public class ChemConnection : MonoBehaviour {

	public bool TransfersMass = true, TransfersHeat = false;
	public ChemVolume VolumeIn,VolumeOut;
	public float Kheat = 1, Kmass=  1;

	// gate controls the amount of gas moving through this connection
	public bool HasGate = false;
	public bool HasCheckValve = false;

	float gateGap = 1f;
	public float GateGap{
		get{
			return gateGap;
		}
		set{
			gateGap = Mathf.Clamp01(value);
		}
	}
	void Awake(){

	}
	// Use this for initialization
	void Start () {
		if(VolumeIn!=null)
		{
			VolumeIn.Connections.Add(this);
		}
		if(VolumeOut!=null)
		{
			VolumeOut.Connections.Add(this);
		}
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

	private ChemVolume GetSourceForReceiver(ChemVolume receiverVol)
	{
		if(VolumeIn==receiverVol)
			return VolumeOut;
		else if(VolumeOut==receiverVol)
			return VolumeIn;

		throw new UnityException("receiverVol is set incorrectly. Possibly a bug");
	}

	// >0 if it is more mass in source volume 
	public float GetMassBalance(ChemVolume receiverVol)
	{
		ChemVolume sourceVol = GetSourceForReceiver(receiverVol);

		return (sourceVol.Pressure - receiverVol.Pressure)*Kmass*gateGap;

	}

	// >0 if it is more heat in source volume 
	public float GetHeatBalance(ChemVolume receiverVol)
	{
		ChemVolume sourceVol = GetSourceForReceiver(receiverVol);
		if(sourceVol.Mix.Mass==0 || receiverVol.Mix.Mass==0)
			return 0;
		return (sourceVol.Mix.Temp - receiverVol.Mix.Temp)*Kheat;
		
	}

	public void MoveMass(ChemVolume receiverVol, float mass)
	{
		ChemVolume sourceVol = GetSourceForReceiver(receiverVol);

		ChemMix mix = sourceVol.Mix.TakeMix(mass);
		receiverVol.Mix.AddMix(mix);
	}

	public void MoveHeat(ChemVolume receiverVol, float heat)
	{
		ChemVolume sourceVol = GetSourceForReceiver(receiverVol);
		float dh = Mathf.Min(sourceVol.Mix.Heat,heat);
		sourceVol.Mix.Heat-=dh;
		receiverVol.Mix.Heat+=dh;

	}
}
