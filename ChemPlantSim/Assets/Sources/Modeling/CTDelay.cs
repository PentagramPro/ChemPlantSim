using UnityEngine;
using System;

public class CTDelay
{
	CTIntegrator sum;
	float K;
	float lastValue = 0;
	public CTDelay (float startCondition, float K)
	{
		sum = new CTIntegrator(startCondition);
		this.K = K;
	}

	public float Gain{
		get{
			return K;
		}
	}

	public float Next(float x)
	{
		x-=lastValue*K;
		x = sum.Next(x);
		lastValue = x;
		return x*K;
	}
}


