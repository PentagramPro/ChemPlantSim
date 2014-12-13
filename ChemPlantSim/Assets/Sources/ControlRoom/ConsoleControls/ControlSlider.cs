using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (ValueTransfer))]
public class ControlSlider : MonoBehaviour {

	ValueTransfer vtransfer;
	SlideHandle Handle;
	public float StartValue = 0f;

	void Awake(){
		vtransfer= GetComponent<ValueTransfer>();
		Handle = GetComponentInChildren<SlideHandle>();
	}
	// Use this for initialization
	void Start () {
		Handle.SetSliderPosition(StartValue);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnValueChanged(float val)
	{
		vtransfer.OnUpdateValue(val);
	}
}
