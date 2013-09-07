using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataEnteryMaster : MonoBehaviour {
	
	//*************7agat 3la2
	
	//Satisfy bar
	public int angerMinTime;
	public float angerMinAmount;
	public int nextAngerIncreaseRate;
	
	//vehicles numbers
	public List<int> groups = new List<int>();
	public List<int> generation_rates;
	public List<int> at_time;
	public List<int []> generation_rates_intervals;
	
	//vehicles Properties
	public float starting_normal_avg_speed;
	public float speed_increase_rate;
	
	
	//Game Huds
	public float GameTime;
	public int Score;
	
	void Awake(){
		/*
		Globals.angerMinTime = angerMinTime;
		Globals.angerMinAmount = angerMinAmount;
		Globals.nextAngerIncreaseRate = nextAngerIncreaseRate;
		
		Globals.groups = groups;
		Globals.generation_rates_intervals = new List<int[]>();
		
		
		for(int i=0; i<generation_rates.Count ; i++){
			Globals.generation_rates_intervals[i] = new int[2];
			Globals.generation_rates_intervals[i][0] = generation_rates[i];
			Globals.generation_rates_intervals[i][1] = at_time[i];
		}
		
		Globals.starting_normal_avg_speed = starting_normal_avg_speed;
		Globals.speed_increase_rate = speed_increase_rate;
		*/
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
