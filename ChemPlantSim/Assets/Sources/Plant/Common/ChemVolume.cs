using UnityEngine;
using System.Collections.Generic;

public class ChemVolume : MonoBehaviour {

	public float Volume = 1f;
	public float HeatLoss = 0.001f;
	public List<ChemConnection> Connections {get;set;}
	public ChemMix Mix = new ChemMix();
	/*
	struct InputConnection{
		public ChemConnection Connection;
		public float balance = 0;
		public InputConnection (ChemConnection connection, float balance)
		{
			this.Connection = connection;
			this.balance = balance;
		}
		
	}*/
	void Awake(){
		Connections = new List<ChemConnection>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float Pressure 
	{
		// mRT/V
		get{
			return Mix.Mass*Constants.R*Mix.Temp/Volume;
		}
	}

	public void OnMoveMass()
	{

		foreach(ChemConnection con in Connections)
		{
			if(!con.TransfersMass)
				continue;

			float bal = con.GetMassBalance(this);
			if(bal>0)
			{
				con.MoveMass(this,bal*Time.fixedDeltaTime);
			}
		}

		foreach(ChemConnection con in Connections)
		{
			if(!con.TransfersHeat)
				continue;
			
			float bal = con.GetHeatBalance(this);
			if(bal>0)
			{
				con.MoveHeat(this,bal*Time.fixedDeltaTime);
			}
		}

		float heatToWorld = HeatLoss*(Mix.Temp-Constants.WorldTemp);
		if(heatToWorld!=0)
		{
			Mix.Heat-=heatToWorld;
		}

	}

	void OnDrawGizmos()
	{
		Gizmos.color = SchemeStyle.Volume;
		Gizmos.DrawSphere(transform.position,0.5f);
	}
}
