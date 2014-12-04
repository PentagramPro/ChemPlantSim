using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SlideHandle : MonoBehaviour, IDragHandler {

	Plane plane;
	Vector3 posDir;
	Vector3 startPos;
	public float Range = 0.05f;

	// Use this for initialization
	void Start () {
		Vector3 planeNorm = transform.TransformDirection(0,0,-1);
		plane =  new Plane(planeNorm,transform.position);
		posDir = transform.TransformDirection(0,1,0);
		startPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
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
			Vector3 projPoint = ray.GetPoint(dist);

			Vector3 delta = Vector3.Project(projPoint-transform.position,posDir);
			if(delta.sqrMagnitude>posDir.sqrMagnitude)
				delta = posDir*Vector3.Dot(delta,posDir);
			transform.localPosition = startPos+delta;

		}
	}

	#endregion

	void OnDrawGizmosSelected()
	{


		Start ();
		Gizmos.color = Color.white;
		Gizmos.DrawLine(plane.normal*Range+transform.position,transform.position);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position,transform.position+posDir*Range);
		Gizmos.DrawSphere(transform.position+posDir*Range,Range*0.05f);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position,transform.position-posDir*Range);
		//Gizmos.DrawSphere(transform.position,0.1f);
	}
}
