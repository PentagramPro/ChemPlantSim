using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ControlSwitch : MonoBehaviour{


	public Animator SwitchAnimator;
	bool turnedOn = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsTurnedOn()
	{
		if(SwitchAnimator.GetCurrentAnimatorStateInfo(0).IsName("TurnedOn"))
			return true;
		return false;
	}

	public void OnSwitchClick()
	{

		if(turnedOn)
			SwitchAnimator.SetTrigger("TurnOff");
		else 
			SwitchAnimator.SetTrigger("TurnOn");
		turnedOn = !turnedOn;
	}
}
