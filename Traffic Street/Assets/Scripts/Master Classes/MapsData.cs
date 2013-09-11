using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class MapsData : MonoBehaviour {
	
	private List<TrafficLight> Lights;
	private List<LightsGroup> lightsGroups;
	private List<Street> Streets;
	
	//Map 1 Data
	
	// Streets
	public List<Street> GetMap1Streets(){
		Lights = new List<TrafficLight>();
		Streets = new List<Street>();
		
		string [] lines = DataCalculations.SplitStringOnLines(Map1_Data.streetsString);
		
		List<string[]> StreetsAttributes = new List<string[]>();
		
		for(int i=0; i<lines.Length;i++){
			StreetsAttributes.Add(DataCalculations.SplitOneStreetLine(lines[i]));
		}
		
		for(int i=0; i<StreetsAttributes.Count;i++){	
			Streets.Add(new Street( int.Parse(StreetsAttributes[i][0]),
									DataCalculations.ConvertStringToVector(StreetsAttributes[i][1]),
									DataCalculations.ConvertStringToVector(StreetsAttributes[i][2]),
									DataCalculations.MakeTheTrafficLight(StreetsAttributes[i][4], StreetsAttributes[i][5], StreetsAttributes[i][6], Lights), 
									float.Parse(StreetsAttributes[i][3]), 
									Globals.STREET_WIDTH, 	
									int.Parse(StreetsAttributes[i][7]) 
									));
									
		}
		Map1AttachStreetsToLights();
		return Streets;
	}
	
	// Intersections
	public List<Vector3> GetMap1Intersections(){
		return Map1_Data.Intersections();
	}
	
	// Paths
	public List<GamePath> GetMap1Paths(){
		return Map1_Data.Paths(Streets);
	}
	
	// Lights
	public List<TrafficLight> GetMap1Lights(){
		return Lights;
	}
	private void Map1AttachStreetsToLights(){
		lightsGroups = Map1_Data.Lights(lightsGroups, Streets, Lights);
	}
	public List<LightsGroup> GetMap1LightsGroups(){
		return lightsGroups;
	}
	
	//Map 1 Data End
	
	//**************************************************************************************************************//
	
	//Map 2 Data
	
	// Streets
	public List<Street> GetMap2Streets(){
		Lights = new List<TrafficLight>();
		Streets = new List<Street>();
		
		string [] lines = DataCalculations.SplitStringOnLines(Map2_Data.streetsString);
		
		List<string[]> StreetsAttributes = new List<string[]>();
		
		for(int i=0; i<lines.Length;i++){
			StreetsAttributes.Add(DataCalculations.SplitOneStreetLine(lines[i]));
		}
		
		for(int i=0; i<StreetsAttributes.Count;i++){
			Streets.Add(new Street( int.Parse(StreetsAttributes[i][0]),
									DataCalculations.ConvertStringToVector(StreetsAttributes[i][1]),
									DataCalculations.ConvertStringToVector(StreetsAttributes[i][2]),
									DataCalculations.MakeTheTrafficLight(StreetsAttributes[i][4], StreetsAttributes[i][5], StreetsAttributes[i][6], Lights), 
									float.Parse(StreetsAttributes[i][3]), 
									Globals.STREET_WIDTH, 	
									int.Parse(StreetsAttributes[i][7]) 
									));
									
		}
		
		Map2AttachStreetsToLights();
		return Streets;
	}
	
	//Intersections
	public List<Vector3> GetMap2Intersections(){
		return Map2_Data.Intersections();
	}
	
	// Paths
	public List<GamePath> GetMap2Paths(){
		return Map2_Data.Paths(Streets);
	}
	
	// Lights
	public List<TrafficLight> GetMap2Lights(){
		return Lights;
	}	
	private void Map2AttachStreetsToLights(){
		lightsGroups = Map2_Data.Lights(lightsGroups, Streets, Lights);
	}
	public List<LightsGroup> GetMap2LightsGroups(){
		return lightsGroups;
	}
	
	//Map 2 Data End
	
	//**************************************************************************************************************//
	
}
