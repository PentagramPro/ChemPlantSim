using UnityEngine;
using System.Collections.Generic;

public class ChemMix  {

	Dictionary<string, ChemFraction> Fractions = new Dictionary<string, ChemFraction>();
	public float Heat = 0;

	public bool Infinite = false;
	float massCache = 0;
	//float heatCache = 0;
	float heatCapSumCache = 0; // m1q1+m2q2+m3q3+...
	public float Mass{
		get{
			return massCache;
		}
	}

	public float HeatCapacity{
		get{
			if(massCache==0)
				return 0;
			return heatCapSumCache/massCache;
		}
	}
	public float Temp{
		get{
			float hc = HeatCapacity;
			if(hc==0)
				return 0;
			return Heat/(hc*massCache);
		}
		set{

			Heat = Mass*value*HeatCapacity;
		}
	}

	public void RebuildCache()
	{
		massCache = 0;
		//heatCache = 0;
		heatCapSumCache = 0;

		foreach(ChemFraction f in Fractions.Values)
		{
			massCache+=f.Mass;
			//heatCache+=f.Heat;
			heatCapSumCache+=f.Mass*f.Element.HeatCap;
		}
	}
	public void AddFraction(ChemFraction fraction)
	{
		if(Infinite)
			return;

		ChemFraction newFrac = null;
		if(Fractions.ContainsKey(fraction.Element.Name))
		{
			newFrac = Fractions[fraction.Element.Name];
			newFrac.Mass+=fraction.Mass;
			//newFrac.Heat+=fraction.Heat;
		}
		else
		{
			newFrac = fraction;
			Fractions[fraction.Element.Name] = newFrac;
		}
		massCache+=fraction.Mass;
		//heatCache+=fraction.Heat;

		heatCapSumCache+=fraction.Mass*fraction.Element.HeatCap;

	}

	public void TakeFraction(ChemFraction target, float mass)
	{
		if(Fractions.ContainsKey(target.Element.Name))
		{
			ChemFraction source = Fractions[target.Element.Name];
			float deltaM = 0;
			if(source.Mass<=mass)
			{
				deltaM = source.Mass;
				if(!Infinite)
					source.Mass = 0;
			}
			else
			{
				deltaM = mass;
				if(!Infinite)
					source.Mass -= mass;
			}

			if(!Infinite)
			{
				heatCapSumCache-=deltaM*target.Element.HeatCap;

				massCache -= deltaM;
			}
			target.Mass+=deltaM;
			//target.Heat+=deltaH;
		}
	}

	public void AddMix(ChemMix mix)
	{
		if(Infinite)
			return;
		foreach(ChemFraction f in mix.Fractions.Values)
		{
			AddFraction(f);
			Heat+=mix.Heat;
		}
	}

	public ChemMix TakeMix(float mass)
	{
		ChemMix res = new ChemMix();

		float[] weights = new float[Fractions.Count];
		int index = 0;
		foreach(ChemFraction f in Fractions.Values)
		{
			weights[index] = f.Mass/Mass;
			index++;
		}

		float deltaH = Heat*mass/massCache;
		index = 0;
		foreach(ChemFraction f in Fractions.Values)
		{
			ChemFraction target = new ChemFraction(f.Element);
			TakeFraction(target,mass*weights[index]);
			index ++;
			res.AddFraction(target);
		}
		if(!Infinite)
			Heat-=deltaH;
		res.Heat+=deltaH;

		return res;
	}
}
