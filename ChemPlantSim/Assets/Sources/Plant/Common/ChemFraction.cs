using UnityEngine;
using System.Collections;

public class ChemFraction  {

	public ChemElement Element;
	public float Mass = 0;
	//public float Heat = 0;

	public ChemFraction (ChemElement element)
	{
		this.Element = element;
	}

	public ChemFraction (ChemElement element, float mass)
	{
		this.Element = element;
		this.Mass = mass;
	}
	
}
