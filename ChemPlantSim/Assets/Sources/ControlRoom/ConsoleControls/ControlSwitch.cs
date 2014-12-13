using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent (typeof (ValueTransfer))]
public class ControlSwitch : MonoBehaviour{

	ValueTransfer vtransfer;
	public Animator SwitchAnimator;
	bool turnedOn = false;

	void Awake(){
		vtransfer= GetComponent<ValueTransfer>();
		
	}

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
		{
			SwitchAnimator.SetTrigger("TurnOff");
			vtransfer.OnUpdateValue(0);
		}
		else 
		{
			SwitchAnimator.SetTrigger("TurnOn");
			vtransfer.OnUpdateValue(1);
		}
		turnedOn = !turnedOn;
	}
}
