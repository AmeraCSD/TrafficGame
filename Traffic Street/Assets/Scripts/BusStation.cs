using UnityEngine;
using System.Collections;

public class BusStation : MonoBehaviour {
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray =  new Ray(transform.position, transform.forward);
		RaycastHit hit ;
		if(Physics.Raycast(ray, out hit, 10)){
			Debug.DrawLine (ray.origin, hit.point);
			VehicleController hitVehicleController = hit.collider.gameObject.GetComponent<VehicleController>();
			if(hitVehicleController.vehType == VehicleType.Bus && hitVehicleController.busStopTimer ==0){
				Debug.Log("bus in stationnn");
				hitVehicleController.speed = 0;
				hitVehicleController.haveToReduceMySpeed = true;
				//if(hitVehicleController.busStopTimer ==0)
					hitVehicleController.SetStopTimeForBus();
			}
		}
	}
}
