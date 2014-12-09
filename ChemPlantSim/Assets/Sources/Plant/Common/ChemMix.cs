using UnityEngine;
using System.Collections.Generic;

public class ChemMix  {

	Dictionary<string, ChemFraction> Fractions = new Dictionary<string, ChemFraction>();

	float massCache = 0;
	float heatCache = 0;
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
			return heatCache/(hc*massCache);
		}
	}

	public void RebuildCache()
	{
		massCache = 0;
		heatCache = 0;
		heatCapSumCache = 0;

		foreach(ChemFraction f in Fractions.Values)
		{
			massCache+=f.Mass;
			heatCache+=f.Heat;
			heatCapSumCache+=f.Mass*f.Element.HeatCap;
		}
	}
	public void AddFraction(ChemFraction fraction)
	{
		ChemFraction newFrac = null;
		if(Fractions.ContainsKey(fraction.Element.Name))
		{
			newFrac = Fractions[fraction.Element.Name];
			newFrac.Mass+=fraction.Mass;
			newFrac.Heat+=fraction.Heat;
		}
		else
		{
			newFrac = fraction;
			Fractions[fraction.Element.Name] = newFrac;
		}
		massCache+=fraction.Mass;
		heatCache+=fraction.Heat;

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
				source.Mass = 0;
			}
			else
			{
				deltaM = mass;
				source.Mass -= mass;
			}

			// refresh caches
			float deltaH = heatCache*deltaM/massCache;
			heatCapSumCache-=deltaM*target.Element.HeatCap;
			heatCache -= deltaH;
			massCache -= deltaM;
			target.Mass+=deltaM;
			target.Heat+=deltaH;
		}
	}

	public void AddMix(ChemMix mix)
	{
		foreach(ChemFraction f in mix.Fractions.Values)
		{
			AddFraction(f);
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
		index = 0;
		foreach(ChemFraction f in Fractions.Values)
		{
			ChemFraction target = new ChemFraction(f.Element);
			TakeFraction(target,mass*weights[index]);
			index ++;
			res.AddFraction(target);
		}


		return res;
	}
}
