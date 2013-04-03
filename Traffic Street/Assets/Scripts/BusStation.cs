using UnityEngine;
using System.Collections;

public class BusStation : MonoBehaviour {
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray =  new Ray(transform.position, Vector3.back);
		RaycastHit hit ;
		if(Physics.Raycast(ray, out hit, 20)){
			Debug.DrawLine (ray.origin, hit.point);
			VehicleController hitVehicleController = hit.collider.gameObject.GetComponent<VehicleController>();
			if(hitVehicleController.vehType == VehicleType.Bus){
				Debug.Log("bus in stationnn");
				hitVehicleController.myVehicle.Speed = 0;
				//hitVehicleController.haveToStop = true;
				
			}
		}
	}
}
