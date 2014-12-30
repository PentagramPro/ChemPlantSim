using System;

public class GasUtils
{
	public static float CalculateMass(float Pressure,float Volume,float Temp)
	{
		return Pressure*Volume / (Constants.R*Temp);
	}
}


