using UnityEngine;
using System;

public class CTDelay
{
	CTIntegrator sum;
	float K;
	float lastValue = 0;
	Plant plant;
	public CTDelay (Plant p,float startCondition, float K)
	{
		sum = new CTIntegrator(p,startCondition);
		this.K = K;
		plant = p;
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


