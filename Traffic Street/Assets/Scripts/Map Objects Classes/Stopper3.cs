using UnityEngine;
using System.Collections;

public class Stopper3 : MonoBehaviour {
	
	bool rotateTheTaxi;
	RaycastHit hit;
	VehicleController hitVehicleController;
	
	private bool done;
	
	// Use this for initialization
	void Start () {
		done = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(! done){
			Ray ray =  new Ray(transform.position, transform.forward);
			if(Physics.Raycast(ray, out hit, 12)){
				Debug.DrawLine (ray.origin, hit.point);
				hitVehicleController = hit.collider.gameObject.GetComponent<VehicleController>();
				if(hitVehicleController.vehType == VehicleType.Taxi){
					Debug.Log("taxi stopping 3 ");
					
				//	hitVehicleController.taxiStop3 = true;
					
					done = true;
				}	
			}
		}
		/*
		if(rotateTheTaxi && hit.collider!= null){
			if(hitVehicleController.speed > 0){
				hitVehicleController.speed -= 15 ;
				
				GameObject.FindGameObjectWithTag("taxiStop").GetComponent<StopperArea>().rotateTheTaxi = false;
				hit.collider.gameObject.transform.Rotate(-1*Vector3.up* 80 * Time.deltaTime, Space.Self);
				Debug.Log("rotate bel3aksss");
				//transform.RotateAround(rotateAroundPosition, Vector3.up, worldRotateSpeed * Time.deltaTime);
				if(-1*hit.collider.gameObject.transform.forward.z >=0){
					Debug.Log("what the hell " + -1*hit.collider.gameObject.transform.forward.z);
					hitVehicleController.speed =0;
					rotateTheTaxi =false;
					hitVehicleController.haveToReduceMySpeed = true;
					//hit.collider.gameObject.transform.forward = -1*Vector3.right;
				}
			}
			else{
				hitVehicleController.SetStopTimeForTaxi();
			}
			hitVehicleController.haveToReduceMySpeed = true;
		}
		
		*/
	}
}
