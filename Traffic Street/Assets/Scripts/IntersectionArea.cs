using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntersectionArea : MonoBehaviour {
	
	public bool haveVehicleOnMe;
	public List<GameObject> vehiclesOnMe;
	
	// Use this for initialization
	void Start () {
		//haveVehicleOnMe = false;
		vehiclesOnMe = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("there is a vehicle entered the plane ");
		
		
		
		if(other.tag == "vehicle"){
		//	haveVehicleOnMe = true;
			if(!(vehiclesOnMe.Contains(other.gameObject))){
				vehiclesOnMe.Add(other.gameObject);
			}
			
		}
		
   	}	
	
	
	
	void OnTriggerExit(Collider other) {
		if(other.tag == "vehicle"){
			vehiclesOnMe.Remove(other.gameObject);
			if(vehiclesOnMe.Count>0){
				vehiclesOnMe[0].GetComponent<VehicleController>().haveToReduceMySpeed = false;
				vehiclesOnMe[0].GetComponent<VehicleController>().speed = vehiclesOnMe[0].GetComponent<VehicleController>().myVehicle.Speed;
			}
		}
   	}
}
