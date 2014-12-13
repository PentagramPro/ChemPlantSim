using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ChemVolume))]
public class ChemVolumeEditor : Editor {

	public override void OnInspectorGUI()
	{
		ChemVolume cv = (ChemVolume) target;
		DrawDefaultInspector();

		GUILayout.Label("Heat: "+cv.Mix.Heat);
		GUILayout.Label("Mass: "+cv.Mix.Mass);
		GUILayout.Label("Heat Capacity: "+cv.Mix.HeatCapacity);
		GUILayout.Label("Pressure: "+cv.Pressure);
	}
}
