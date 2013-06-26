using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntersectionArea : MonoBehaviour {
	
	public bool haveVehicleOnMe;
	public List<GameObject> vehiclesOnMe;
	
	private GameMaster masterScript;
	
	private bool soundPlayed;
	private bool accidentHappen;
	
	// Use this for initialization
	void Start () {
		//haveVehicleOnMe = false;
		soundPlayed = false;
		accidentHappen = false;
		
		vehiclesOnMe = new List<GameObject>();
		
		masterScript = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>();
	}
	
	// Update is called once per frame
	void Update () {
		
		RemoveNullVehicles();
		
		if(vehiclesOnMe.Count> 1 ){
				
			if( checkFaceToFaceVehicles(vehiclesOnMe[0].GetComponent<VehicleController>(), vehiclesOnMe[1].GetComponent<VehicleController>()) ||
				checkFaceToFaceVehicles(vehiclesOnMe[1].GetComponent<VehicleController>(), vehiclesOnMe[0].GetComponent<VehicleController>())){
				//here we goo accidenttttttttttt ************************************************************************
				
				
				
				if(!accidentHappen ){
					
					int chance = Random.Range(0,20);
					Debug.Log("the chance is ---> "+chance);
					if(!masterScript.accidentHereLocked && chance == 1){
						if(!soundPlayed){
							masterScript.accidentSmoke.audio.Play();
							soundPlayed = true;
							
							
							masterScript.accidentSmoke.SetActive(true);
							masterScript.accidentSmoke.transform.position = transform.position;
							masterScript.accidentHere = true;
							masterScript.accidentHereLocked = true;
							accidentHappen = true;
						
							
							Debug.Log("Accident heeeeeeeeeeeeeeeeeeeeeee");
						
						}
					}
						
				//	vehiclesOnMe[0].rigidbody.isKinematic = false;
				//	vehiclesOnMe[1].rigidbody.isKinematic = false;
					
				//	vehiclesOnMe[0].rigidbody.AddForce(vehiclesOnMe[0].transform.forward );
					
					
					else{
						vehiclesOnMe[0].GetComponent<VehicleController>().speed = vehiclesOnMe[0].GetComponent<VehicleController>().myVehicle.Speed;
												
					}
				}
				else{
					vehiclesOnMe[0].GetComponent<VehicleController>().speed = 0;
					vehiclesOnMe[1].GetComponent<VehicleController>().speed = 0;
					vehiclesOnMe[0].GetComponent<VehicleController>().haveToReduceMySpeed = true;
					vehiclesOnMe[1].GetComponent<VehicleController>().haveToReduceMySpeed = true;
				}
			}
			
		//	soundPlayed = false;
			
		}
		
		
	}
	
	private void RemoveNullVehicles(){
		for(int i=0; i<vehiclesOnMe.Count; i++){
			if(!vehiclesOnMe[i].activeSelf){
				vehiclesOnMe.Remove(vehiclesOnMe[i]);
				accidentHappen = false;
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
