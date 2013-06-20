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
		
		
		
			if(vehiclesOnMe.Count> 1 ){
				if(vehiclesOnMe[0] == vehiclesOnMe[1]){
					vehiclesOnMe = new List<GameObject>();
				}
				if(vehiclesOnMe[0]!= null ){
					if(vehiclesOnMe.Count == 2 && vehiclesOnMe[1]== null){
						vehiclesOnMe = new List<GameObject>();
					}
					else if(vehiclesOnMe.Count == 3 && vehiclesOnMe[2]== null){
						vehiclesOnMe = new List<GameObject>();
					}
					else{
						//Debug.Log(vehiclesOnMe[1].gameObject);
						if( checkFaceToFaceVehicles(vehiclesOnMe[0].GetComponent<VehicleController>(), vehiclesOnMe[1].GetComponent<VehicleController>()) ||
							checkFaceToFaceVehicles(vehiclesOnMe[1].GetComponent<VehicleController>(), vehiclesOnMe[0].GetComponent<VehicleController>())){
							
								vehiclesOnMe[0].GetComponent<VehicleController>().speed = vehiclesOnMe[0].GetComponent<VehicleController>().myVehicle.Speed;
							/*
							if(vehiclesOnMe[0].GetComponent<VehicleController>().ImTheOneToMove){
								vehiclesOnMe[0].GetComponent<VehicleController>().speed = vehiclesOnMe[0].GetComponent<VehicleController>().myVehicle.Speed;
							}
							else if(vehiclesOnMe[1].GetComponent<VehicleController>().ImTheOneToMove){
								vehiclesOnMe[1].GetComponent<VehicleController>().speed = vehiclesOnMe[1].GetComponent<VehicleController>().myVehicle.Speed;
							}
							else{
								vehiclesOnMe[0].GetComponent<VehicleController>().ImTheOneToMove = true;
							}
							*/
							
							//GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>().gameOver = true;
						}
						//Debug.Log(transform.position +" ---->  " +Vector3.Angle(vehiclesOnMe[0].transform.forward, vehiclesOnMe[1].transform.forward));
					}
					
				}
				else{
					vehiclesOnMe = new List<GameObject>();
				}
		}
		
		
	}
	
	void OnTriggerEnter(Collider other) {
	//	Debug.Log("there is a vehicle entered the plane ");
		
		
		if(other.tag == "vehicle"){
		//	haveVehicleOnMe = true;
			if(!(vehiclesOnMe.Contains(other.gameObject))){
				vehiclesOnMe.Add(other.gameObject);
			}
			/*
			if(vehiclesOnMe.Count == 2){
				if(vehiclesOnMe[0].GetComponent<VehicleController>().haveToReduceMySpeed == false || vehiclesOnMe[1].GetComponent<VehicleController>().haveToReduceMySpeed == false)
					if(vehiclesOnMe[0].GetComponent<VehicleController>().speed ==0 && vehiclesOnMe[1].GetComponent<VehicleController>().speed == 0)
						GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>().gameOver = true;
			}
			*/
			
		}
		
		
		
   	}
	
	private bool checkFaceToFaceVehicles(VehicleController vehCtrl_1, VehicleController vehCtrl_2){
		
		if(vehCtrl_1._direction == StreetDirection.Up && vehCtrl_2._direction == StreetDirection.Down){
			if(vehCtrl_1.gameObject.transform.position.z < vehCtrl_2.gameObject.transform.position.z)
				return true;
		}
		
		if(vehCtrl_1._direction == StreetDirection.Right && vehCtrl_2._direction == StreetDirection.Left){
			if(vehCtrl_1.gameObject.transform.position.x < vehCtrl_2.gameObject.transform.position.x)
				
				return true;
		}
		
		if(vehCtrl_1._direction == StreetDirection.Up && vehCtrl_2._direction == StreetDirection.Left){
			if( vehCtrl_1.gameObject.transform.position.z < vehCtrl_2.gameObject.transform.position.z && 
				vehCtrl_1.gameObject.transform.position.x < vehCtrl_2.gameObject.transform.position.x){
				Debug.Log("checkedddddddddddd");
				return true;
				
			}
			
		}
		
		if(vehCtrl_1._direction == StreetDirection.Up && vehCtrl_2._direction == StreetDirection.Right){
			if( vehCtrl_1.gameObject.transform.position.z < vehCtrl_2.gameObject.transform.position.z &&
				vehCtrl_1.gameObject.transform.position.x > vehCtrl_2.gameObject.transform.position.x)
				
				return true;
		}
		
		if(vehCtrl_1._direction == StreetDirection.Down && vehCtrl_2._direction == StreetDirection.Left){
			if( vehCtrl_1.gameObject.transform.position.z > vehCtrl_2.gameObject.transform.position.z &&
				vehCtrl_1.gameObject.transform.position.x < vehCtrl_2.gameObject.transform.position.x)
				
				return true;
		}
		
		if(vehCtrl_1._direction == StreetDirection.Down && vehCtrl_2._direction == StreetDirection.Right){
			if( vehCtrl_1.gameObject.transform.position.z > vehCtrl_2.gameObject.transform.position.z &&
				vehCtrl_1.gameObject.transform.position.x > vehCtrl_2.gameObject.transform.position.x)
				
				return true;
		}
		
		return false;
	}
	
	
	void OnTriggerExit(Collider other) {
		if(other.tag == "vehicle"){
			vehiclesOnMe.Remove(other.gameObject);
			if(vehiclesOnMe.Count == 1){
				
				vehiclesOnMe[0].GetComponent<VehicleController>().haveToReduceMySpeed = false;
				vehiclesOnMe[0].GetComponent<VehicleController>().speed = vehiclesOnMe[0].GetComponent<VehicleController>().myVehicle.Speed;
				
			}
		}
   	}
}
