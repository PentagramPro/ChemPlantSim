using UnityEngine;
using System.Collections;


public class TurbineGasodynamic : MonoBehaviour {
	public float RpmMid = 3150;
	public float PressureMid = 35;
	public float FlowMid = 1450;
	public float PressureDif = 100;
	public float FlowDif = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float CalculateFlow(float pressure,float revs)
	{
		float Pref = revs/RpmMid*PressureMid;
		float Fref = revs/RpmMid*FlowMid;
		float b=0,k=0;
		if(pressure>Pref)
		{
			b = Pref+PressureDif;
			k = -PressureDif/Fref;
		}
		else
		{
			b=Pref*(Fref+FlowDif)/FlowDif;
			k=-Pref/FlowDif;
		}
		float flow = (pressure-b)/k;
		return flow;
	}
}
