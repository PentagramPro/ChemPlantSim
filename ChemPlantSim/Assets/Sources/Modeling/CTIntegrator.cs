using System;
using UnityEngine;

public class CTIntegrator
{
	float value = 0;
	public CTIntegrator (float initialCondition)
	{
		value = initialCondition;
	}

	public float Next(float input)
	{
		value+=input*Time.fixedDeltaTime;
		return value;
	}
}


