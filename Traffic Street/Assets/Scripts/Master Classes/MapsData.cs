using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class MapsData : MonoBehaviour {
	
	private List<TrafficLight> Lights;
	private List<LightsGroup> lightsGroups;
	private List<Street> Streets;
	
	
	private string Map2FileString =  "0%(-145.0, 5.0, -45.0)%(-65.0, 5.0, -45.0)%-75%Right%lightRight%true%5\n" +
		"1%(-145.0, 5.0, -35.0)%(-55.0, 5.0, -35.0)%-75%Right%lightRight%true%5\n" +
		"2%(-65.0, 5.0, -45.0)%(-65.0, 5.0, -75.0)%0%Down%none%false%2\n" +
		"3%(-55.0, 5.0, -35.0)%(-55.0, 5.0, -75.0)%0%Down%none%false%2\n" +
		"4%(-45.0, 5.0, -85.0)%(-45.0, 5.0, -25.0)%-55%Up%lightUp1%true%2\n" +
		"5%(-35.0, 5.0, -85.0)%(-35.0, 5.0, -15.0)%-55%Up%lightUp1%true%2\n" +
		"6%(-65.0, 5.0, -45.0)%(35.0, 5.0, -45.0)%0%Right%none%false%5\n" +
		"7%(-55.0, 5.0, -35.0)%(55.0, 5.0, -35.0)%0%Right%none%false%5\n" +
		"8%(35.0, 5.0, -45.0)%(35.0, 5.0, -75.0)%0%Down%none%false%2\n" +
		"9%(45.0, 5.0, -25.0)%(45.0, 5.0, -75.0)%0%Down%none%false%2\n" +
		"10%(55.0, 5.0, -75.0)%(55.0, 5.0, -35.0)%-55%Up%lightUp%true%2\n" +
		"11%(65.0, 5.0, -75.0)%(65.0, 5.0, -15.0)%-55%Up%lightUp%true%2\n" +
		"12%(145.0, 5.0, -25.0)%(45.0, 5.0, -25.0)%75%Left%lightLeft%true%5\n" +
		"13%(145.0, 5.0, -15.0)%(65.0, 5.0, -15.0)%75%Left%lightLeft%true%5\n" +
		"14%(45.0, 5.0, -25.0)%(-35.0, 5.0, -25.0)%0%Left%none%false%5\n" +
		"15%(65.0, 5.0, -15.0)%(-35.0, 5.0, -15.0)%0%Left%none%false%5\n" +
		"16%(-65.0, 5.0, 15.0)%(-65.0, 5.0, -45.0)%0%Down%none%false%2\n" +
		"17%(-55.0, 5.0, 25.0)%(-55.0, 5.0, -35.0)%0%Down%none%false%2\n" +
		"18%(-45.0, 5.0, -25.0)%(-45.0, 5.0, 35.0)%0%Up%none%false%2\n" +
		"19%(-35.0, 5.0, -15.0)%(-35.0, 5.0, 15.0)%0%Up%none%false%2\n" +
		"20%(35.0, 5.0, 15.0)%(35.0, 5.0, -15.0)%0%Down%none%false%2\n" +
		"21%(45.0, 5.0, 35.0)%(45.0, 5.0, -25.0)%0%Down%none%false%2\n" +
		"22%(55.0, 5.0, -35.0)%(55.0, 5.0, 25.0)%0%Up%none%false%2\n" +
		"23%(65.0, 5.0, -15.0)%(65.0, 5.0, 45.0)%0%Up%none%false%2\n" +
		"24%(-145.0, 5.0, 15.0)%(-65.0, 5.0, 15.0)%-75%Right%lightRight1%true%5\n" +
		"25%(-145.0, 5.0, 25.0)%(-55.0, 5.0, 25.0)%-75%Right%lightRight1%true%5\n" +
		"26%(-35.0, 5.0, 15.0)%(35.0, 5.0, 15.0)%0%Right%none%false%5\n" +
		"27%(-55.0, 5.0, 25.0)%(55.0, 5.0, 25.0)%0%Right%none%false%5\n" +
		"28%(45.0, 5.0, 35.0)%(-45.0, 5.0, 35.0)%0%Left%none%false%5\n" +
		"29%(65.0, 5.0, 45.0)%(-35.0, 5.0, 45.0)%0%Left%none%false%5\n" +
		"30%(145.0, 5.0, 35.0)%(45.0, 5.0, 35.0)%75%Left%lightLeft1%true%5\n" +
		"31%(145.0, 5.0, 45.0)%(65.0, 5.0, 45.0)%75%Left%lightLeft1%true%5\n" +
		"32%(-65.0, 5.0, 85.0)%(-65.0, 5.0, 15.0)%55%Down%lightDown1%true%2\n" +
		"33%(-55.0, 5.0, 85.0)%(-55.0, 5.0, 25.0)%55%Down%lightDown1%true%2\n" +
		"34%(-45.0, 5.0, 35.0)%(-45.0, 5.0, 75.0)%0%Up%none%false%2\n" +
		"35%(-35.0, 5.0, 45.0)%(-35.0, 5.0, 75.0)%0%Up%none%false%2\n" +
		"36%(35.0, 5.0, 85.0)%(35.0, 5.0, 15.0)%55%Down%lightDown%true%2\n" +
		"37%(45.0, 5.0, 85.0)%(45.0, 5.0, 25.0)%55%Down%lightDown%true%2\n" +
		"38%(55.0, 5.0, 25.0)%(55.0, 5.0, 75.0)%0%Up%none%false%2\n" +
		"39%(65.0, 5.0, 45.0)%(65.0, 5.0, 85.0)%0%Up%none%false%2\n" +
		"40%(-35.0, 5.0, -25.0)%(-45.0, 5.0, -25.0)%0%Left%none%false%1\n" +
		"41%(-65.0, 5.0, 15.0)%(-35.0, 5.0, 15.0)%0%Right%none%false%1\n" +
		"42%(-35.0, 5.0, 15.0)%(-35.0, 5.0, 45.0)%0%Up%none%false%1";
	
	private string Map1FileString = "0%(-115.0, 5.0, -35.0)%(25.0, 5.0, -35.0)%15%Right%lightRight%true%8\n" +
		"1%(25.0, 5.0, -75.0)%(25.0, 5.0, -35.0)%-45%Up%lightUp%true%3\n" +
		"2%(25.0, 5.0, -35.0)%(115.0, 5.0, -35.0)%0%Right%none%false%8\n" +
		"3%(-115.0, 5.0, -25.0)%(35.0, 5.0, -25.0)%15%Right%lightRight%true%8\n" +
		"4%(35.0, 5.0, -25.0)%(75.0, 5.0, -25.0)%0%Right%none%false%2\n" +
		"5%(75.0, 5.0, -25.0)%(115.0, 5.0, -25.0)%0%Right%none%false%2\n" +
		"6%(25.0, 5.0, -15.0)%(-105.0, 5.0, -15.0)%0%Left%none%false%8\n" +
		"7%(75.0, 5.0, -15.0)%(25.0, 5.0, -15.0)%50%Left%lightLeft1%true%3\n" +
		"8%(115.0, 5.0, -15.0)%(75.0, 5.0, -15.0)%95%Left%lightLeft%true%2\n" +
		"9%(25.0, 5.0, -5.0)%(-115.0, 5.0, -5.0)%0%Left%none%false%8\n" +
		"10%(85.0, 5.0, -5.0)%(25.0, 5.0, -5.0)%50%Left%lightLeft1%true%3\n" +
		"11%(115.0, 5.0, -5.0)%(85.0, 5.0, -5.0)%95%Left%lightLeft%true%2\n" +
		"12%(25.0, 5.0, 15.0)%(25.0, 5.0, -5.0)%0%Down%none%false%1\n" +
		"13%(35.0, 5.0, 35.0)%(35.0, 5.0, -25.0)%0%Down%none%false%3\n" +
		"14%(75.0, 5.0, -15.0)%(75.0, 5.0, 35.0)%0%Up%none%false%3\n" +
		"15%(85.0, 5.0, -5.0)%(85.0, 5.0, 45.0)%0%Up%none%false%4\n" +
		"16%(-105.0, 5.0, 15.0)%(-55.0, 5.0, 15.0)%-60%Right%none%false%5\n" +
		"17%(-55.0, 5.0, 15.0)%(25.0, 5.0, 15.0)%0%Right%none%false%7\n" +
		"18%(-105.0, 5.0, 25.0)%(-45.0, 5.0, 25.0)%-60%Right%none%false%6\n" +
		"19%(25.0, 5.0, 45.0)%(25.0, 5.0, 15.0)%0%Down%none%false%1\n" +
		"20%(-55.0, 5.0, 35.0)%(-55.0, 5.0, 15.0)%0%Down%none%false%1\n" +
		"21%(-45.0, 5.0, 25.0)%(-45.0, 5.0, 35.0)%0%Up%none%false%1\n" +
		"22%(-55.0, 5.0, 35.0)%(-115.0, 5.0, 35.0)%0%Left%none%false%5\n" +
		"23%(35.0, 5.0, 35.0)%(-55.0, 5.0, 35.0)%0%Left%none%false%6\n" +
		"24%(75.0, 5.0, 35.0)%(35.0, 5.0, 35.0)%45%Left%lightLeft2%true%3\n" +
		"25%(-45.0, 5.0, 45.0)%(-115.0, 5.0, 45.0)%0%Left%none%false%5\n" +
		"26%(25.0, 5.0, 45.0)%(-45.0, 5.0, 45.0)%0%Left%none%false%6\n" +
		"27%(85.0, 5.0, 45.0)%(25.0, 5.0, 45.0)%45%Left%lightLeft2%true%4\n" +
		"28%(-55.0, 5.0, 85.0)%(-55.0, 5.0, 35.0)%55%Down%lightDown1%true%2\n" +
		"29%(-45.0, 5.0, 45.0)%(-45.0, 5.0, 85.0)%0%Up%none%false%2\n" +
		"30%(25.0, 5.0, 85.0)%(25.0, 5.0, 45.0)%55%Down%lightDown%true%2\n" +
		"31%(35.0, 5.0, 85.0)%(35.0, 5.0, 35.0)%55%Down%lightDown%true%2\n" +
		"32%(75.0, 5.0, -25.0)%(75.0, 5.0, -15.0)%0%Up%none%false%1\n" +
		"33%(-45.0, 5.0, 35.0)%(-45.0, 5.0, 45.0)%0%Up%none%false%1\n"+
		"34%(25.0, 5.0, -35.0)%(25.0, 5.0, -15.0)%0%Up%none%false%2";

	public List<Street> GetMap2Streets(){
		Lights = new List<TrafficLight>();
		Streets = new List<Street>();
		
		string [] lines = SplitStringOnLines(Map2FileString);
		
		List<string[]> StreetsAttributes = new List<string[]>();
		
		for(int i=0; i<lines.Length;i++){
			StreetsAttributes.Add(SplitOneStreetLine(lines[i]));
			for(int j= 0; j<lines.Length; j++){
//				Debug.Log("wawaaa "+StreetsAttributes[i][j]);
			}
		}
		
		for(int i=0; i<StreetsAttributes.Count;i++){
			
		//	Debug.Log(ConvertStringToVector(StreetsAttributes[i][1]));
			
			Streets.Add(new Street( int.Parse(StreetsAttributes[i][0]),
									ConvertStringToVector(StreetsAttributes[i][1]),
									ConvertStringToVector(StreetsAttributes[i][2]),
									MakeTheTrafficLight(StreetsAttributes[i][4], StreetsAttributes[i][5], StreetsAttributes[i][6]), 
									float.Parse(StreetsAttributes[i][3]), 
									Globals.STREET_WIDTH, 	
									int.Parse(StreetsAttributes[i][7]) 
									));
									
		}
		
		Map2AttachStreetsToLights();
		return Streets;
	}
	
	public List<Street> GetMap1Streets(){
		Lights = new List<TrafficLight>();
		Streets = new List<Street>();
		
		string [] lines = SplitStringOnLines(Map1FileString);
		
		List<string[]> StreetsAttributes = new List<string[]>();
		
		for(int i=0; i<lines.Length;i++){
			StreetsAttributes.Add(SplitOneStreetLine(lines[i]));
			for(int j= 0; j<lines.Length; j++){
//				Debug.Log("wawaaa "+StreetsAttributes[i][j]);
			}
		}
		
		for(int i=0; i<StreetsAttributes.Count;i++){
			
		//	Debug.Log(ConvertStringToVector(StreetsAttributes[i][1]));
			
			Streets.Add(new Street( int.Parse(StreetsAttributes[i][0]),
									ConvertStringToVector(StreetsAttributes[i][1]),
									ConvertStringToVector(StreetsAttributes[i][2]),
									MakeTheTrafficLight(StreetsAttributes[i][4], StreetsAttributes[i][5], StreetsAttributes[i][6]), 
									float.Parse(StreetsAttributes[i][3]), 
									Globals.STREET_WIDTH, 	
									int.Parse(StreetsAttributes[i][7]) 
									));
									
		}
		
		Map1AttachStreetsToLights();
		return Streets;
	}
	
	
	public List<Vector3> GetMap2Intersections(){
		List<Vector3> intersectionsList = new List<Vector3>();
		
		//here weeee goooooooooooo
		intersectionsList.Add(new Vector3(-65.0f, 5.0f, -45.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, -45.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, -45.0f));
		intersectionsList.Add(new Vector3(-35.0f, 5.0f, -45.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, -45.0f));
		intersectionsList.Add(new Vector3(45.0f, 5.0f, -45.0f));
		intersectionsList.Add(new Vector3(55.0f, 5.0f, -45.0f));
		intersectionsList.Add(new Vector3(65.0f, 5.0f, -45.0f));
		intersectionsList.Add(new Vector3(65.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(55.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(45.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(-35.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(-65.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(-35.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(45.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(55.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, -15.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, -15.0f));
		intersectionsList.Add(new Vector3(-35.0f, 5.0f, -15.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, -15.0f));
		intersectionsList.Add(new Vector3(45.0f, 5.0f, -15.0f));
		intersectionsList.Add(new Vector3(55.0f, 5.0f, -15.0f));
		intersectionsList.Add(new Vector3(65.0f, 5.0f, -15.0f));
		intersectionsList.Add(new Vector3(55.0f, 5.0f, 15.0f));
		intersectionsList.Add(new Vector3(45.0f, 5.0f, 15.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, 15.0f));
		intersectionsList.Add(new Vector3(-35.0f, 5.0f, 15.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, 15.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, 15.0f));
		intersectionsList.Add(new Vector3(-65.0f, 5.0f, 15.0f));
		intersectionsList.Add(new Vector3(-65.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(-35.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(45.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(55.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(65.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(55.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(45.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(-35.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(-65.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(-35.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(45.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(55.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(65.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(85.0f, 5.0f, -85.0f));	

		
		return intersectionsList;
	}
	
	public List<Vector3> GetMap1Intersections(){
		List<Vector3> intersectionsList = new List<Vector3>();
		
		intersectionsList.Add(new Vector3 (25.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(25.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(75.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(75.0f, 5.0f, -15.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, -15.0f));
		intersectionsList.Add(new Vector3(25.0f, 5.0f, -15.0f));
		intersectionsList.Add(new Vector3(85.0f, 5.0f, -5.0f));
		intersectionsList.Add(new Vector3(75.0f, 5.0f, -5.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, -5.0f));
		intersectionsList.Add(new Vector3(25.0f, 5.0f, -5.0f));
		intersectionsList.Add(new Vector3(25.0f, 5.0f, 15.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, 15.0f));
		intersectionsList.Add(new Vector3(75.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(25.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(25.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(85.0f, 5.0f, 45.0f));
		intersectionsList.Add(new Vector3(75.0f, 5.0f, -75.0f));
		intersectionsList.Add(new Vector3(-55.0f, 5.0f, 15.0f));
	//	intersectionsList.Add(new Vector3(-55.0f, 5.0f, -35.0f));
	//	intersectionsList.Add(new Vector3(-55.0f, 5.0f, -25.0f));
		
		
		return intersectionsList;
		
	}
	
	public List<GamePath> GetMap2Paths(){
		
		List<GamePath> Paths = new List<GamePath>();
		
		//here weeee goooooooooooo
		
		List<Street> tempPath;
		
		//path 0
		tempPath = new List<Street>();
		Debug.Log(Streets[0].ID);
		tempPath.Add(Streets[0]);
		tempPath.Add(Streets[6]);
		tempPath.Add(Streets[8]);
		
		Paths.Add(new GamePath(tempPath, Streets[0].StartPoint , Streets[8].EndPoint, false));
		
		//path 1
		tempPath = new List<Street>();
		tempPath.Add(Streets[1]);
		tempPath.Add(Streets[7]);
		tempPath.Add(Streets[22]);
		tempPath.Add(Streets[38]);
		
		Paths.Add(new GamePath(tempPath, Streets[1].StartPoint , Streets[38].EndPoint, false));
		
		//path 2
		tempPath = new List<Street>();
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[18]);
		tempPath.Add(Streets[34]);
		
		Paths.Add(new GamePath(tempPath, Streets[4].StartPoint , Streets[34].EndPoint, false));
		
		//path 3
		tempPath = new List<Street>();
		tempPath.Add(Streets[5]);
		tempPath.Add(Streets[19]);
		tempPath.Add(Streets[42]);
		tempPath.Add(Streets[35]);
		
		Paths.Add(new GamePath(tempPath, Streets[5].StartPoint , Streets[35].EndPoint, false));
		
		/*
		//path 4
		tempPath = new List<Street>();
		tempPath.Add(Streets[5]);
		tempPath.Add(Streets[6]);
		tempPath.Add(Streets[8]);
		
		Paths.Add(new GamePath(tempPath, Streets[5].StartPoint , Streets[8].EndPoint, false));
		*/
		//path 5
		tempPath = new List<Street>();
		tempPath.Add(Streets[10]);
		tempPath.Add(Streets[22]);
		tempPath.Add(Streets[38]);
		
		Paths.Add(new GamePath(tempPath, Streets[10].StartPoint , Streets[38].EndPoint, false));
		
		//path 6
		tempPath = new List<Street>();
		tempPath.Add(Streets[11]);
		tempPath.Add(Streets[23]);
		tempPath.Add(Streets[39]);
		
		Paths.Add(new GamePath(tempPath, Streets[11].StartPoint , Streets[39].EndPoint, false));
		
		//path 7
		tempPath = new List<Street>();
		tempPath.Add(Streets[12]);
		tempPath.Add(Streets[9]);
		
		Paths.Add(new GamePath(tempPath, Streets[12].StartPoint , Streets[9].EndPoint, false));
		
		//path 8
		tempPath = new List<Street>();
		tempPath.Add(Streets[13]);
		tempPath.Add(Streets[23]);
		tempPath.Add(Streets[39]);
		
		Paths.Add(new GamePath(tempPath, Streets[13].StartPoint , Streets[39].EndPoint, false));
		
		//path 9
		tempPath = new List<Street>();
		tempPath.Add(Streets[13]);
		tempPath.Add(Streets[15]);
		tempPath.Add(Streets[19]);
		tempPath.Add(Streets[42]);
		tempPath.Add(Streets[35]);
		
		Paths.Add(new GamePath(tempPath, Streets[13].StartPoint , Streets[35].EndPoint, false));
		
		//path 10
		tempPath = new List<Street>();
		tempPath.Add(Streets[30]);
		tempPath.Add(Streets[21]);
		tempPath.Add(Streets[9]);
		
		Paths.Add(new GamePath(tempPath, Streets[30].StartPoint , Streets[9].EndPoint, false));
		
		//path 11
		tempPath = new List<Street>();
		tempPath.Add(Streets[31]);
		tempPath.Add(Streets[29]);
		tempPath.Add(Streets[35]);
		
		Paths.Add(new GamePath(tempPath, Streets[31].StartPoint , Streets[35].EndPoint, false));
		
		//path 12
		tempPath = new List<Street>();
		tempPath.Add(Streets[36]);
		tempPath.Add(Streets[20]);
		tempPath.Add(Streets[8]);
		
		Paths.Add(new GamePath(tempPath, Streets[36].StartPoint , Streets[8].EndPoint, false));
		
		//path 13
		tempPath = new List<Street>();
		tempPath.Add(Streets[37]);
		tempPath.Add(Streets[21]);
		tempPath.Add(Streets[9]);
		
		Paths.Add(new GamePath(tempPath, Streets[37].StartPoint , Streets[9].EndPoint, false));
		
		//path 14
		tempPath = new List<Street>();
		tempPath.Add(Streets[32]);
		tempPath.Add(Streets[16]);
		tempPath.Add(Streets[2]);
		
		Paths.Add(new GamePath(tempPath, Streets[32].StartPoint , Streets[2].EndPoint, false));
		
		//path 15
		tempPath = new List<Street>();
		tempPath.Add(Streets[33]);
		tempPath.Add(Streets[17]);
		tempPath.Add(Streets[3]);
		
		Paths.Add(new GamePath(tempPath, Streets[33].StartPoint , Streets[3].EndPoint, false));
		
		//path 16
		tempPath = new List<Street>();
		tempPath.Add(Streets[12]);
		tempPath.Add(Streets[14]);
		tempPath.Add(Streets[40]);
		tempPath.Add(Streets[18]);
		tempPath.Add(Streets[34]);
		
		Paths.Add(new GamePath(tempPath, Streets[12].StartPoint , Streets[34].EndPoint, false));
		
		//path 17
		tempPath = new List<Street>();
		tempPath.Add(Streets[24]);
		tempPath.Add(Streets[41]);
		tempPath.Add(Streets[26]);
		tempPath.Add(Streets[20]);
		tempPath.Add(Streets[8]);
		
		Paths.Add(new GamePath(tempPath, Streets[24].StartPoint , Streets[8].EndPoint, false));
		
		//path 18
		tempPath = new List<Street>();
		tempPath.Add(Streets[25]);
		tempPath.Add(Streets[27]);
		tempPath.Add(Streets[38]);
		
		Paths.Add(new GamePath(tempPath, Streets[25].StartPoint , Streets[38].EndPoint, false));
		
		//path 19
		tempPath = new List<Street>();
		tempPath.Add(Streets[24]);
		tempPath.Add(Streets[16]);
		tempPath.Add(Streets[2]);
		
		Paths.Add(new GamePath(tempPath, Streets[24].StartPoint , Streets[2].EndPoint, false));
		
		
			
		
		return Paths;
	}
	
	public List<GamePath> GetMap1Paths(){
		
		List<GamePath> Paths = new List<GamePath>();
		
		//here weeee goooooooooooo
		
		List<Street> tempPath;
		
		//path 0
		tempPath = new List<Street>();
		tempPath.Add(Streets[0]);
		tempPath.Add(Streets[2]);
		
		Paths.Add(new GamePath(tempPath, Streets[0].StartPoint , Streets[2].EndPoint, false));
		
		//path 1
		tempPath = new List<Street>();
		tempPath.Add(Streets[1]);
		tempPath.Add(Streets[34]);
		tempPath.Add(Streets[6]);
		
		Paths.Add(new GamePath(tempPath, Streets[1].StartPoint , Streets[6].EndPoint, false));
		
		//path 2
		tempPath = new List<Street>();
		tempPath.Add(Streets[3]);
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[5]);
		
		Paths.Add(new GamePath(tempPath, Streets[3].StartPoint , Streets[5].EndPoint, false));
		
		//path 3
		tempPath = new List<Street>();
		tempPath.Add(Streets[3]);
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[32]);
		tempPath.Add(Streets[14]);
		tempPath.Add(Streets[24]);
		tempPath.Add(Streets[23]);
		tempPath.Add(Streets[22]);
		
		Paths.Add(new GamePath(tempPath, Streets[3].StartPoint , Streets[22].EndPoint, false));
		
		//path 4
		tempPath = new List<Street>();
		tempPath.Add(Streets[8]);
		tempPath.Add(Streets[7]);
		tempPath.Add(Streets[6]);
		
		Paths.Add(new GamePath(tempPath, Streets[8].StartPoint , Streets[6].EndPoint, false));
		
		//path 5
		tempPath = new List<Street>();
		tempPath.Add(Streets[11]);
		tempPath.Add(Streets[10]);
		tempPath.Add(Streets[9]);
		
		Paths.Add(new GamePath(tempPath, Streets[11].StartPoint , Streets[9].EndPoint, false));
		
		//path 6
		tempPath = new List<Street>();
		tempPath.Add(Streets[11]);
		tempPath.Add(Streets[15]);
		tempPath.Add(Streets[27]);
		tempPath.Add(Streets[26]);
		tempPath.Add(Streets[25]);
		
		Paths.Add(new GamePath(tempPath, Streets[11].StartPoint , Streets[25].EndPoint, false));
		
		//path 7
		tempPath = new List<Street>();
		tempPath.Add(Streets[11]);
		tempPath.Add(Streets[15]);
		tempPath.Add(Streets[27]);
		tempPath.Add(Streets[26]);
		tempPath.Add(Streets[29]);
		
		Paths.Add(new GamePath(tempPath, Streets[11].StartPoint , Streets[29].EndPoint, false));
		
		//path 8
		tempPath = new List<Street>();
		tempPath.Add(Streets[31]);
		tempPath.Add(Streets[13]);
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[5]);
		
		Paths.Add(new GamePath(tempPath, Streets[31].StartPoint , Streets[5].EndPoint, false));
		
		//path 9
		tempPath = new List<Street>();
		tempPath.Add(Streets[30]);
		tempPath.Add(Streets[19]);
		tempPath.Add(Streets[12]);
		tempPath.Add(Streets[9]);
		
		Paths.Add(new GamePath(tempPath, Streets[30].StartPoint , Streets[9].EndPoint, false));
		
		//path 10
		tempPath = new List<Street>();
		tempPath.Add(Streets[30]);
		tempPath.Add(Streets[26]);
		tempPath.Add(Streets[25]);
		
		Paths.Add(new GamePath(tempPath, Streets[30].StartPoint , Streets[25].EndPoint, false));
		
		//path 11
		tempPath = new List<Street>();
		tempPath.Add(Streets[28]);
		tempPath.Add(Streets[20]);
		tempPath.Add(Streets[17]);
		tempPath.Add(Streets[12]);
		tempPath.Add(Streets[9]);
		
		Paths.Add(new GamePath(tempPath, Streets[28].StartPoint , Streets[9].EndPoint, false));
		
		//path 12
		tempPath = new List<Street>();
		tempPath.Add(Streets[16]);
		tempPath.Add(Streets[17]);
		tempPath.Add(Streets[12]);
		tempPath.Add(Streets[9]);
		
		Paths.Add(new GamePath(tempPath, Streets[16].StartPoint , Streets[9].EndPoint, false));
		
		//path 13
		tempPath = new List<Street>();
		tempPath.Add(Streets[18]);
		tempPath.Add(Streets[21]);
		tempPath.Add(Streets[33]);
		tempPath.Add(Streets[29]);
		
		Paths.Add(new GamePath(tempPath, Streets[18].StartPoint , Streets[29].EndPoint, false));
		
		/*
		//path 14
		tempPath = new List<Street>();
		tempPath.Add(Streets[3]);
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[32]);
		tempPath.Add(Streets[14]);
		tempPath.Add(Streets[24]);
		tempPath.Add(Streets[23]);
		tempPath.Add(Streets[22]);
		
		//Paths.Add(new GamePath(tempPath, Streets[3].StartPoint , Streets[22].EndPoint, false));
		*/
		
		return Paths;
	}
	
	private string [] SplitStringOnLines(string str){
		char[] delimiters = new char[] { '\n' };
		string[] lines = str.Split(delimiters);
		
		return lines;
	}
	
	private string [] SplitOneStreetLine(string str){
		char[] delimiters = new char[] {'%'};
		string[] attributes = str.Split(delimiters);
		
		return attributes;
	}
	
	private Vector3 ConvertStringToVector(string str){
		Vector3 temp;
		char[] delimiters = new char[] {','};
		string [] numbers = str.Split(delimiters);
		
		temp = new Vector3( float.Parse(numbers[0].Substring(numbers[0].IndexOf('(')+1)),
							float.Parse(numbers[1]),
							float.Parse(numbers[2].Substring(0, numbers[2].IndexOf(')')-1)));
		
		return temp;
	}
	
	
	
	
	private TrafficLight MakeTheTrafficLight(string dirStr, string tag, string stoppedStr){
		TrafficLight light;
		StreetDirection direction =	 (StreetDirection)Enum.Parse(typeof(StreetDirection), dirStr);
		bool stopped = bool.Parse(stoppedStr);
		
		GameObject go;
		if(tag == "none"){
			go = null;	
			light = new TrafficLight(direction, go, stopped);
			
		}
		else{
			go = GameObject.FindGameObjectWithTag(tag);
			int index = ContainsLight(go);
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
	
	private int ContainsLight(GameObject go){
		for(int i=0; i<Lights.Count; i++){
			if(Lights[i].tLight.Equals(go)){
				return i;
			}
		}
		return -1;
	}
	
	//}
	// Use this for initialization
	
	public List<TrafficLight> GetMap1Lights(){
		//Map1AttachStreetsToLights();
		//Debug.Log("hereeeeeeeeeeeeeeeeeeeeeeee    "+Lights.Count);
		return Lights;
	}
	
	public List<TrafficLight> GetMap2Lights(){
		//Map2AttachStreetsToLights();
		//Debug.Log("hereeeeeeeeeeeeeeeeeeeeeeee    "+Lights.Count);
		return Lights;
	}
	
	private void Map2AttachStreetsToLights(){
		List<Street> temp;
		
		for(int i= 0; i<Lights.Count; i++){
			temp = new List<Street>();
			if(Lights[i].tLight.tag == "lightRight"){
				temp.Add(Streets[0]);
				temp.Add(Streets[1]);
				Lights[i].AttachedStreets = temp;
				
			}
			else if(Lights[i].tLight.tag == "lightRight1"){
				temp.Add(Streets[24]);
				temp.Add(Streets[25]);
				Lights[i].AttachedStreets = temp;
			}
			else if(Lights[i].tLight.tag == "lightLeft"){
				temp.Add(Streets[12]);
				temp.Add(Streets[13]);
				Lights[i].AttachedStreets = temp;
			}
			else if(Lights[i].tLight.tag == "lightLeft1"){
				temp.Add(Streets[30]);
				temp.Add(Streets[31]);
				Lights[i].AttachedStreets = temp;
			}
			else if(Lights[i].tLight.tag == "lightUp1"){
				temp.Add(Streets[4]);
				temp.Add(Streets[5]);
				Lights[i].AttachedStreets = temp;
			}
			else if(Lights[i].tLight.tag == "lightUp"){
				temp.Add(Streets[10]);
				temp.Add(Streets[11]);
				Lights[i].AttachedStreets = temp;
			}
			else if(Lights[i].tLight.tag == "lightDown1"){
				temp.Add(Streets[32]);
				temp.Add(Streets[33]);
				Lights[i].AttachedStreets = temp;
			}
			else if(Lights[i].tLight.tag == "lightDown"){
				temp.Add(Streets[36]);
				temp.Add(Streets[37]);
				Lights[i].AttachedStreets = temp;
			}
		}
	}
	
	private void Map1AttachStreetsToLights(){
		List<Street> temp;
		
		lightsGroups = new List<LightsGroup>() ;
		
		List<TrafficLight> lightsList1 = new List<TrafficLight>();
		List<TrafficLight> lightsList2 = new List<TrafficLight>();
		List<TrafficLight> lightsList3 = new List<TrafficLight>();
		List<TrafficLight> lightsList4 = new List<TrafficLight>();
		
		
		for(int i= 0; i<Lights.Count; i++){
			temp = new List<Street>();
			
			if(Lights[i].tLight.tag == "lightLeft"){
				temp.Add(Streets[11]);
				temp.Add(Streets[8]);
				Lights[i].AttachedStreets = temp;
				lightsList1.Add(Lights[i]);
			}
			else if(Lights[i].tLight.tag == "lightLeft1"){
				temp.Add(Streets[10]);
				temp.Add(Streets[7]);
				Lights[i].AttachedStreets = temp;
				lightsList1.Add(Lights[i]);
			}
			else if(Lights[i].tLight.tag == "lightUp"){
				temp.Add(Streets[1]);
				Lights[i].AttachedStreets = temp;
				lightsList2.Add(Lights[i]);
			}
			else if(Lights[i].tLight.tag == "lightRight"){
				temp.Add(Streets[0]);
				temp.Add(Streets[3]);
				Lights[i].AttachedStreets = temp;
				lightsList2.Add(Lights[i]);
			}
			else if(Lights[i].tLight.tag == "lightDown1"){
				temp.Add(Streets[28]);
				Lights[i].AttachedStreets = temp;
				lightsList4.Add(Lights[i]);
			}
			else if(Lights[i].tLight.tag == "lightDown"){
				temp.Add(Streets[30]);
				temp.Add(Streets[31]);
				Lights[i].AttachedStreets = temp;
				lightsList3.Add(Lights[i]);
			}
			else if(Lights[i].tLight.tag == "lightLeft2"){
				temp.Add(Streets[24]);
				temp.Add(Streets[27]);
				Lights[i].AttachedStreets = temp;
				lightsList3.Add(Lights[i]);
			}
			
		}
		lightsGroups.Add(new LightsGroup(lightsList1));
		lightsGroups.Add(new LightsGroup(lightsList2));
		lightsGroups.Add(new LightsGroup(lightsList3));
		lightsGroups.Add(new LightsGroup(lightsList4));
		
	}
	
	public List<LightsGroup> GetMap1LightsGroups(){
		return lightsGroups;
	}
	
	void Start () {
		
		
		
		char[] delimiters = new char[]{','};
		Debug.Log(float.Parse("-45.0)".Substring(0, "-45.0)".IndexOf(')')-1)));
		Debug.Log("(-125.0, 5.0, -45.0)".Split(delimiters)[1]);
		Debug.Log("(-125.0, 5.0, -45.0)".Split(delimiters)[2]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
