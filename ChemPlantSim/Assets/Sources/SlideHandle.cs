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
		Vector3 planeNorm =new Vector3(0,0,-1);
		plane =  new Plane(transform.TransformDirection(planeNorm),transform.position);
		posDir = new Vector3(0,Range,0);
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
			Vector3 projPoint = transform.parent.InverseTransformPoint(ray.GetPoint(dist));

			Vector3 delta = Vector3.Project(projPoint,posDir);
			if(delta.sqrMagnitude>posDir.sqrMagnitude)
				delta = posDir*Mathf.Sign(Vector3.Dot(delta,posDir));
			transform.localPosition = startPos+delta;

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
