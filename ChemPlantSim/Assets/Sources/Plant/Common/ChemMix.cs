using UnityEngine;
using System.Collections.Generic;

public class ChemMix  {

	Dictionary<string, ChemFraction> Fractions = new Dictionary<string, ChemFraction>();
	private float _heat = 0;
	public float Heat{
		get{
			return _heat;
		}
		set{
			if(float.IsNaN(value) || float.IsInfinity(value))
				throw new UnityException("Tried to set Heat field to Nan or Infinity");
			if(value<0)
				throw new UnityException("Tried to set negative Heat");
			_heat = value;
		}
	}

	public bool Infinite = false;
	float massCache = 0;
	public string VolumeName {get;set;}
	Plant plant;

	public ChemMix(Plant plant)
	{
		this.plant = plant;
	}
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

	void TakeFraction(ChemFraction target, float mass,List<string> toRemove)
	{
		if(float.IsNaN(mass) || float.IsInfinity(mass))
			throw new UnityException("TakeFraction argument is Nan or Infinity!");

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

			plant.MaxDeltaM=Mathf.Max(plant.MaxDeltaM,deltaM);
			if(!Infinite)
			{
				if(source.Mass==0)
					toRemove.Add(target.Element.Name);

				heatCapSumCache-=deltaM*target.Element.HeatCap;

				massCache -= deltaM;
				if(Fractions.Count==0)
				{
					heatCapSumCache = 0;
					massCache = 0;
					Heat = 0;
				}

				if(heatCapSumCache<0)
					RebuildCache();
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
			if(f.Mass>0)
				AddFraction(f);
		}
		Heat+=mix.Heat;
	}

	public ChemMix TakeMix(float mass)
	{
		ChemMix res = new ChemMix(plant);

		if(Mass==0)
			return res;

		float[] weights = new float[Fractions.Count];
		int index = 0;
		foreach(ChemFraction f in Fractions.Values)
		{
			weights[index] = f.Mass/Mass;
			index++;
		}

		float deltaH = Heat*mass/massCache;
		plant.MaxDeltaH = Mathf.Max(plant.MaxDeltaH,deltaH);
		index = 0;
		List<string> toRemove = new List<string>();
		foreach(ChemFraction f in Fractions.Values)
		{
			ChemFraction target = new ChemFraction(f.Element);
			TakeFraction(target,mass*weights[index],toRemove);
			index ++;
			res.AddFraction(target);
		}

		foreach(string name in toRemove)
			Fractions.Remove(name);

		if(!Infinite)
		{
			Heat-=Mathf.Min(deltaH,Heat);

		}
		res.Heat+=deltaH;

		return res;
	}
}
