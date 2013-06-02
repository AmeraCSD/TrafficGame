using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Globals : MonoBehaviour {
	
	public static bool vibrationEnabled;
	
	public static float STREET_WIDTH = 23;
	public static int ANGER_TIMER = 10;
	
	public static float NORMAL_AVG_VEHICLE_SPEED = 25;
	
	public static float AMBULANCE_SPEED = 36;
	public static float BUS_SPEED = 12;
	public static float CARAVAN_SPEED = 10;
	public static float SERVICE_CAR_SPEED = 10;
	public static float TAXI_SPEED = 25;
	public static float THIEF_SPEED = 30;
	
	public static List<AudioClip> Horns;
	public List<AudioClip> HornsGos;
	
	
	// Use this for initialization
	void Start () {
		Horns = HornsGos;
		//vibrationEnabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log(vibrationEnabled);
	}
}
