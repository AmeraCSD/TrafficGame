using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIObjects : MonoBehaviour {

	public List<Material> vehiclesTextures;
	
	public GameObject vehiclePrefab;				//this object should be initialized in unity with the VehiclePrefab
	public GameObject ambulancePrefab;				//this object should be initialized in unity with the AmbulancePrefab
	public GameObject busPrefab;					//this object should be initialized in unity with the BusPrefab
	public GameObject caravanPrefab;		
	public GameObject serviceCarPrefab;		
	public GameObject thiefPrefab;
	public GameObject policePrefab;
	public GameObject taxiPrefab;
	public GameObject accidentCarPrefab;
	
	public GameObject intersectionPrefab;
}
