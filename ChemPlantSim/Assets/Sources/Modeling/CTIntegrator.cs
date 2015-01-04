using System;
using UnityEngine;

public class CTIntegrator
{
	float value = 0;
	Plant plant;
	public CTIntegrator (Plant p,float initialCondition)
	{
		value = initialCondition;
		plant = p;
	}

	public float Next(float input)
	{
		value+=input*plant.PlantDeltaTime;
		return value;
	}
}


