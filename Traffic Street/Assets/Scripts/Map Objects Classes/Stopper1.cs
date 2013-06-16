using UnityEngine;
using System.Collections;

public class Stopper1 : MonoBehaviour {
	
	public bool rotateTheTaxi;
	RaycastHit hit;
	VehicleController hitVehicleController;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray =  new Ray(transform.position, transform.forward);
		if(Physics.Raycast(ray, out hit, 10)){
			Debug.DrawLine (ray.origin, hit.point);
			hitVehicleController = hit.collider.gameObject.GetComponent<VehicleController>();
			if(hitVehicleController.vehType == VehicleType.Taxi){
				Debug.Log("taxi stopping here");
				//rotateTheTaxi = true;
				hitVehicleController.taxiStop = true;
					
			}
		}
		
		
	}
}
