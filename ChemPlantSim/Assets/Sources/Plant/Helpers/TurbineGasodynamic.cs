using UnityEngine;
using System.Collections;


public class TurbineGasodynamic : MonoBehaviour {
	public float RmpMid = 3150;
	public float PressureMid = 35;
	public float FlowMid = 1450;
	public float PressureScale = 0.1f;
	public float FlowScale = 0.8f;
	public float CurveFactor = 0.0005f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float CalculateFlow(float pressure,float revs)
	{
		float pressureShift = Mathf.Max(1f, PressureMid+PressureScale*(revs-RmpMid));
		float flowShift = Mathf.Max(FlowMid+FlowScale*(revs-RmpMid));
		float x = pressure*1e-5f-pressureShift;
		x*=CurveFactor;
		x = Mathf.Min(x,-CurveFactor);
		x = 1/x+flowShift;
		return x;
	}
}
