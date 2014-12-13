using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SlideHandle : MonoBehaviour, IDragHandler {

	Plane plane;
	Vector3 posDir;
	Vector3 startPos;
	public float Range = 0.05f;
	public float Value {get;internal set;}
	public ControlSlider Slider;

	// Use this for initialization
	void Start () {
		Vector3 planeNorm =new Vector3(0,0,-1);
		plane =  new Plane(transform.TransformDirection(planeNorm),transform.position);
		posDir = new Vector3(0,Range,0);
		startPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSliderPosition(float val)
	{
		val = Mathf.Clamp01(val);
		Value = val;

		transform.localPosition = startPos+posDir*(val*2f-1f);
		Slider.OnValueChanged(Value);

	}

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		Ray ray = Camera.main.ScreenPointToRay(eventData.position);

		//Ray newp = Camera.main.ScreenPointToRay(eventData.position+eventData.delta);
		//float oldd,newd;
		float dist;
		if(plane.Raycast(ray,out dist))
		{
			Vector3 projPoint = transform.parent.InverseTransformPoint(ray.GetPoint(dist));

			Vector3 delta = Vector3.Project(projPoint,posDir);
			float sign = Mathf.Sign(Vector3.Dot(delta,posDir));

			if(delta.sqrMagnitude>posDir.sqrMagnitude)
			{
				delta = posDir*sign;
				Value = (1f+sign)*0.5f;
			}
			else
			{
				Value = (1f+sign*delta.magnitude/posDir.magnitude)*0.5f;
			}
			transform.localPosition = startPos+delta;

			Slider.OnValueChanged(Value);
		}
	}

	#endregion

	void OnDrawGizmosSelected()
	{


		Start ();
		Gizmos.color = Color.white;
		Gizmos.DrawLine(transform.TransformPoint(plane.normal),transform.position);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position,transform.parent.TransformPoint(posDir+transform.localPosition));
		Gizmos.DrawSphere(transform.parent.TransformPoint(posDir+transform.localPosition),0.005f);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position,transform.parent.TransformPoint(-posDir+transform.localPosition));
		//Gizmos.DrawSphere(transform.position,0.1f);
	}
}
