using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NormalVehicle : MonoBehaviour {

	
	public static void GenerateNormalVehicle(int pos,GameObject vehiclePrefab, Texture2D tx, List<GamePath> Paths, bool cancelInvokeFirst15Vehicles, Queue existedVehicles){	
		
			if(!cancelInvokeFirst15Vehicles){
	//			while(Paths[pos].PathStreets[0].VehiclesNumber >= Paths[pos].PathStreets[0].StreetCapacity){
					pos = Random.Range(0, Paths.Count);
	//			}
			}
			//pos = 0;
			if(vehiclePrefab != null){
				//*****************************optimization
				GameObject vehicle;
				if(existedVehicles.Count == 0){
					vehicle = Instantiate(vehiclePrefab, Paths[pos].GenerationPointPosition ,Quaternion.identity) as GameObject;
					vehicle.renderer.material.mainTexture = tx;
					Paths[pos].PathStreets[0].VehiclesNumber ++;
					vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
					//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
					vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Normal, 
																						25.0f, 
																						MathsCalculatios.getVehicleLargeSize(vehicle), 
																						Paths[pos].PathStreets[0].StreetLight.Type, 
																						Paths[pos].PathStreets[0], 
																						Paths[pos].PathStreets[1], 
																						0,
																						Paths[pos]);
				}
				else{
					vehicle = existedVehicles.Dequeue() as GameObject;
					vehicle.transform.position = Paths[pos].GenerationPointPosition;
					vehicle.transform.rotation = Quaternion.identity;
					vehicle.GetComponent<VehicleController>().initInstancesAtFirst();
					vehicle.SetActive(true);
					Paths[pos].PathStreets[0].VehiclesNumber ++;
					vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
					
					//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
					vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Normal, 
																						25.0f, 
																						MathsCalculatios.getVehicleLargeSize(vehicle), 
																						Paths[pos].PathStreets[0].StreetLight.Type, 
																						Paths[pos].PathStreets[0], 
																						Paths[pos].PathStreets[1], 
																						0,
																						Paths[pos]);
					vehicle.GetComponent<VehicleController>().InitStreetAndVehicleAttributes();
					
				}
				
			}
			
		
	}
	
}
