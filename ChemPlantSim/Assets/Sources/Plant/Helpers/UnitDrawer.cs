using UnityEngine;
using System.Collections;

public class UnitDrawer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnDrawGizmos() 
	{
		Transform[] tr =  GetComponentsInChildren<Transform>();
		Vector3 pos1 = transform.position, pos2 = transform.position;
		foreach(Transform t in tr)
		{
			if(t.position.x>pos2.x)
				pos2.x = t.position.x;
			else if(t.position.x<pos1.x)
				pos1.x = t.position.x;

			if(t.position.y>pos2.y)
				pos2.y = t.position.y;
			else if(t.position.y<pos1.y)
				pos1.y = t.position.y;

			if(t.position.z>pos2.z)
				pos2.z= t.position.z;
			else if(t.position.z<pos1.z)
				pos1.z = t.position.z;
		}	


		pos2+=new Vector3(2,2,2);
		pos1-=new Vector3(2,2,2);
		Gizmos.DrawWireCube( (pos1+pos2)/2,(pos2-pos1)/1.5f);
	}
}
