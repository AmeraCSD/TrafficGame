using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class DataCalculations : MonoBehaviour {

	public static string [] SplitStringOnLines(string str){
		char[] delimiters = new char[] { '\n' };
		string[] lines = str.Split(delimiters);
		
		return lines;
	}
	
	public static string [] SplitOneStreetLine(string str){
		char[] delimiters = new char[] {'%'};
		string[] attributes = str.Split(delimiters);
		
		return attributes;
	}
	
	public static Vector3 ConvertStringToVector(string str){
		Vector3 temp;
		char[] delimiters = new char[] {','};
		string [] numbers = str.Split(delimiters);
		
		temp = new Vector3( float.Parse(numbers[0].Substring(numbers[0].IndexOf('(')+1)),
							float.Parse(numbers[1]),
							float.Parse(numbers[2].Substring(0, numbers[2].IndexOf(')')-1)));
		
		return temp;
	}
	
	public static  TrafficLight MakeTheTrafficLight(string dirStr, string name, string stoppedStr, List<TrafficLight> Lights){
		TrafficLight light;
		StreetDirection direction =	 (StreetDirection)Enum.Parse(typeof(StreetDirection), dirStr);
		bool stopped = bool.Parse(stoppedStr);
		
		GameObject go;
		if(name == "none"){
			go = null;	
			light = new TrafficLight(direction, go, stopped);
			
		}
		else{
			go = GameObject.Find(name);
			int index = DataCalculations.ContainsLight(go, Lights);
			if(index == -1){
				light = new TrafficLight(direction, go, stopped);
				Lights.Add(light);
			}
			else{
				light = Lights[index];
			}
		}
		
		return light;
	}
	
	public static  int ContainsLight(GameObject go, List<TrafficLight> Lights){
		for(int i=0; i<Lights.Count; i++){
			if(Lights[i].tLight.Equals(go)){
				return i;
			}
		}
		return -1;
	}
	
	
}
