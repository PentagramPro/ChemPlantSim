using UnityEngine;
using System.Collections;

public class SchemeStyle  {
	public static readonly Color Volume = FromRGB(204,255,204);
	public static readonly Color Connection = FromRGB(127,127,127);

	public static readonly Color ConnectionLinkMass = FromRGB(0,0,255);
	public static readonly Color ConnectionLinkHeat = FromRGB(255,0,0);
	public static readonly Color ConnectionLinkBoth = FromRGB(255,0,255);

	public static Color FromRGB(int r, int g, int b)
	{
		return new Color(r/255f,g/255f,b/255f);
	}

}
