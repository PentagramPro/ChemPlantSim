using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	public ValueTransfer Transfer;
	public float StartAngle=63, EndAngle=-63;
	Vector3 initialRotation;
	void Awake(){
		initialRotation = transform.rotation.eulerAngles;
		Transfer.OnValueUpdated+=OnValueUpdated;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnValueUpdated(float val)
	{
		val = Mathf.Clamp01(val);
		float angle = EndAngle*val+StartAngle*(1-val);
		transform.rotation = Quaternion.Euler(initialRotation.x,initialRotation.y,initialRotation.z+angle);
	}
}
