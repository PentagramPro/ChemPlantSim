using UnityEngine;
using System.Collections;

public class CameraMovementController : MonoBehaviour {

	Vector3 horizontal = new Vector3(30,0,0);
	Vector3 vertical = new Vector3(0,30,0);
	Vector3 zoom = new Vector3(0,0,50);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody.AddForce(horizontal*Input.GetAxis("Horizontal"));
		rigidbody.AddForce(vertical*Input.GetAxis("Vertical"));
		rigidbody.AddForce(zoom*Input.GetAxis("Zoom"));

	}
}
