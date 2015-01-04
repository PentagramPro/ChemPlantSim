using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ChemReaction))]
public class ChemReactionEditor : Editor {

	public override void OnInspectorGUI()
	{
		ChemReaction cr = (ChemReaction) target;

		if(GUILayout.Button("Add ingredient"))
		{
			cr.Ingredients.Add(new ChemFraction(null));

		}

		if(GUILayout.Button("Add product"))
		{
		}
		EditorGUILayout.LabelField("Ingredients:");
		foreach(ChemFraction fraction in cr.Ingredients)
		{
			GUILayout.BeginHorizontal();

			fraction.Element = EditorGUILayout.ObjectField(fraction.Element,typeof(ChemElement),true) as ChemElement;
			fraction.Mass = EditorGUILayout.FloatField(fraction.Mass);

			GUILayout.EndHorizontal();

		}

	}
}
