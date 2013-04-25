using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StreetsGenerator : MonoBehaviour {
	
	private List<Street>  Streets;				//this is a list of the all of the streets in the game (should be used in GameMaster and LightsGamer)
	private List<GamePath> Paths;
	private List<Vector3> Intersections;
	public const float STREET_WIDTH = 23;
	private const int MAX_STREET_VEHICLES_NUMBER = 4;
	
	public GameObject lightPrefab = null;		//this should be initialized in unity with the traffic light
	
	// variables for storing the streets at first (GRAPHICS NEEDS)
	public string direction = "";				//this should be initialized in unity when the game starts
	public float size = 0;						//this should be initialized in unity when the game starts
	private bool pressed;
	private bool finished;
	private Vector3 mousePos;
	private int streetsCounter;
	//
	
	
	
	public List<Street> getStreets(){
		return Streets;
	}
	
	public List<GamePath> getPaths(){
		return Paths;
	}
	
	public List<Vector3> getIntersections(){
		return Intersections;
	}
	
	void Awake(){
		streetsCounter = -1;
		Streets = new List<Street>();
		Intersections = new List<Vector3>();
	//	GenerateTempStreetsLevel1(); 					//for temp test
	//	GenerateFirstLevelStreets(); 					//for temp test
		
		GenerateSecondLevelStreets();
		
	//	InitFirstLevelPaths();
		
		InitSecondLevelPaths();
		InitStreetsIntersections();
		//Debug.Log("we henaaa el Paths count === "+ Paths.Count);
	}
	
	private void GenerateSecondLevelStreets(){
		//Lefts
		
		
		TrafficLight light_0_1 = new TrafficLight(StreetDirection.Left,
												FindTagObject("lightLeft"),
												true);
		streetsCounter ++ ;
		Street s0 = new Street( streetsCounter,
								new Vector3(85, 5, -15), 
								new Vector3(25, 5, -15),  
								light_0_1,
								33.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		
		
		streetsCounter ++ ;
		Street s1 = new Street( streetsCounter,
								new Vector3(85, 5, -25), 
								new Vector3(15, 5, -25),  
								light_0_1,
								33.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		
		
		streetsCounter ++ ;
		Street s2 = new Street( streetsCounter,
								new Vector3(25, 5, -15), 
								new Vector3(-15, 5, -15),  
								new TrafficLight(StreetDirection.Left,
												null,
												false),
								-15.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								3);
		
		streetsCounter ++ ;
		Street s3 = new Street( streetsCounter,
								new Vector3(15, 5, -25), 
								new Vector3(-25, 5, -25),  
								new TrafficLight(StreetDirection.Left,
												null,
												false),
								-5.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		TrafficLight light_4_5 = new TrafficLight(StreetDirection.Left,
												FindTagObject("lightLeft1"),
												true);
		
		streetsCounter ++ ;
		Street s4 = new Street( streetsCounter,
								new Vector3(25, 5, 25), 
								new Vector3(-15, 5, 25),  
								light_4_5,
								-4.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		streetsCounter ++ ;
		Street s5 = new Street( streetsCounter,
								new Vector3(15, 5, 15), 
								new Vector3(-25, 5, 15),  
								light_4_5,
								-4.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								3);
		
		streetsCounter ++ ;
		Street s6 = new Street( streetsCounter,
								new Vector3(-15, 5, 25), 
								new Vector3(-85, 5, 25),  
								new TrafficLight(StreetDirection.Left,
												null,
												false),
								0.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		streetsCounter ++ ;
		Street s7 = new Street( streetsCounter,
								new Vector3(-25, 5, 15), 
								new Vector3(-85, 5, 15),  
								new TrafficLight(StreetDirection.Left,
												null,
												false),
								0.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		//Ups
		
		TrafficLight light_8_9 = new TrafficLight(StreetDirection.Up,
												FindTagObject("lightUp"),
												true);
		
		streetsCounter ++ ;
		Street s8 = new Street( streetsCounter,
								new Vector3(25, 5, -85), 
								new Vector3(25, 5, -15),  
								light_8_9,
								-36.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		
		
		
		streetsCounter ++ ;
		Street s9 = new Street( streetsCounter,
								new Vector3(15, 5, -85),
								new Vector3(15, 5, -25),  
								light_8_9,
								-36.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		
		streetsCounter ++ ;
		Street s10 = new Street( streetsCounter,
								new Vector3(25, 5, -15),
								new Vector3(25, 5, 25),  
								new TrafficLight(StreetDirection.Up,
												null,
												false),
								15.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		streetsCounter ++ ;
		Street s11 = new Street( streetsCounter,
								new Vector3(15, 5, -25),
								new Vector3(15, 5, 15),  
								new TrafficLight(StreetDirection.Up,
												null,
												false),
								5.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								3);
		
		TrafficLight light_12_13 = new TrafficLight(StreetDirection.Up,
												FindTagObject("lightUp1"),
												true);
		
		streetsCounter ++ ;
		Street s12 = new Street( streetsCounter,
								new Vector3(-15, 5, -15),
								new Vector3(-15, 5, 25),  
								light_12_13,
								4.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								3);
		
		streetsCounter ++ ;
		Street s13 = new Street( streetsCounter,
								new Vector3(-25, 5, -25),
								new Vector3(-25, 5, 15),  
								light_12_13,
								4.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		streetsCounter ++ ;
		Street s14 = new Street( streetsCounter,
								new Vector3(-15, 5, 25),
								new Vector3(-15, 5, 85),  
								new TrafficLight(StreetDirection.Up,
												null,
												false),
								0.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		streetsCounter ++ ;
		Street s15 = new Street( streetsCounter,
								new Vector3(-25, 5, 15),
								new Vector3(-25, 5, 85),  
								new TrafficLight(StreetDirection.Up,
												null,
												false),
								0.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		//adding streets
		
		Streets.Add(s0);
		Streets.Add(s1);
		Streets.Add(s2);
		Streets.Add(s3);
		Streets.Add(s4);
		Streets.Add(s5);
		Streets.Add(s6);
		Streets.Add(s7);
		Streets.Add(s8);
		Streets.Add(s9);
		Streets.Add(s10);
		Streets.Add(s11);
		Streets.Add(s12);
		Streets.Add(s13);
		Streets.Add(s14);
		Streets.Add(s15);
		
		
	}
	
	private void InitSecondLevelPaths(){
		Paths = new List<GamePath>();
		List<Street> tempPath;

		//Path0 (left, up, left, up)1
		tempPath = new List<Street>();
		tempPath.Add(Streets[0]);
		tempPath.Add(Streets[10]);
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[14]);
		
		Paths.Add(new GamePath(tempPath, Streets[0].StartPoint , Streets[14].EndPoint, false));
		
		//Path1 (left, up, left, left)
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[0]);
		tempPath.Add(Streets[10]);
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[6]);
		
		Paths.Add(new GamePath(tempPath, Streets[0].StartPoint , Streets[6].EndPoint, false));
		
		//Path2 (left, left, up, up)
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[0]);
		tempPath.Add(Streets[2]);
		tempPath.Add(Streets[12]);
		tempPath.Add(Streets[14]);
		
		Paths.Add(new GamePath(tempPath, Streets[0].StartPoint , Streets[14].EndPoint, false));
		
		//Path3 (left, left,up, left )
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[1]);
		tempPath.Add(Streets[3]);
		tempPath.Add(Streets[13]);
		tempPath.Add(Streets[7]);
		
		Paths.Add(new GamePath(tempPath, Streets[1].StartPoint , Streets[7].EndPoint, false));
		
		//Path4 (left, left,up, up )
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[1]);
		tempPath.Add(Streets[3]);
		tempPath.Add(Streets[13]);
		tempPath.Add(Streets[15]);
		
		Paths.Add(new GamePath(tempPath, Streets[1].StartPoint , Streets[15].EndPoint, false));
		
		//Path5 (up, up,left, left )
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[8]);
		tempPath.Add(Streets[10]);
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[6]);
		
		Paths.Add(new GamePath(tempPath, Streets[8].StartPoint , Streets[6].EndPoint, false));
		
		//Path6 (up, up,left, up )
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[8]);
		tempPath.Add(Streets[10]);
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[14]);
		
		Paths.Add(new GamePath(tempPath, Streets[8].StartPoint , Streets[14].EndPoint, false));
		
		//Path7 (up, up,left, left )
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[9]);
		tempPath.Add(Streets[11]);
		tempPath.Add(Streets[5]);
		tempPath.Add(Streets[7]);
		
		Paths.Add(new GamePath(tempPath, Streets[9].StartPoint , Streets[7].EndPoint, false));
		
		
		//Path8 (up, left, up, left )
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[9]);
		tempPath.Add(Streets[3]);
		tempPath.Add(Streets[13]);
		tempPath.Add(Streets[7]);
		
		Paths.Add(new GamePath(tempPath, Streets[9].StartPoint , Streets[7].EndPoint, false));
		
		//Path9 (up, left, up, up )
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[9]);
		tempPath.Add(Streets[3]);
		tempPath.Add(Streets[13]);
		tempPath.Add(Streets[15]);
		
		Paths.Add(new GamePath(tempPath, Streets[9].StartPoint , Streets[15].EndPoint, false));
		
		
		
		
	}
	
	private void InitStreetsIntersections(){
		Intersections.Add(Streets[0].EndPoint);
		Intersections.Add(Streets[1].EndPoint);
		Intersections.Add(new Vector3(25, 5, -25));
		Intersections.Add(Streets[2].EndPoint);
		Intersections.Add(Streets[3].EndPoint);
		Intersections.Add(Streets[10].EndPoint);
		Intersections.Add(Streets[11].EndPoint);
		Intersections.Add(Streets[12].EndPoint);
		Intersections.Add(Streets[13].EndPoint);
	}
	
	private void GenerateFirstLevelStreets(){
		
		//Downs
		
		
		TrafficLight light_0_2 = new TrafficLight(StreetDirection.Down,
												FindTagObject("lightDown"),
												true);
		streetsCounter ++ ;
		Street s0 = new Street( streetsCounter,
								new Vector3(-15, 5, 55), 
								new Vector3(-15, 5, 5),  
								light_0_2,
								14.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		streetsCounter ++ ;
		Street s1 = new Street( streetsCounter,
								new Vector3(-15, 5, 5), 
								new Vector3(-15, 5, -50),  
								new TrafficLight(StreetDirection.Down,
												null,
												false),
								0.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		streetsCounter ++ ;
		Street s2 = new Street( streetsCounter,
								new Vector3(-5, 5, 55), 
								new Vector3(-5, 5, -5),  
								light_0_2,
								14.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH/4,
								MAX_STREET_VEHICLES_NUMBER);
		
		streetsCounter ++ ;
		Street s3 = new Street( streetsCounter,
								new Vector3(-5, 5, -5), 
								new Vector3(-5, 5, -50),  
								new TrafficLight(StreetDirection.Down,
												null,
												false),
								0.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		//Lefts
		streetsCounter ++ ;
		TrafficLight light_4_5 = new TrafficLight(StreetDirection.Left,
												FindTagObject("lightLeft"),
												true);
		Street s4 = new Street( streetsCounter,
								new Vector3(50, 5, 5), 
								new Vector3(-15, 5, 5),  
								light_4_5,
								4.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		streetsCounter ++ ;
		Street s5 = new Street( streetsCounter,
								new Vector3(50, 5, -5), 
								new Vector3(-5, 5, -5),  
								light_4_5,
								4.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		streetsCounter ++ ;
		Street s6 = new Street( streetsCounter,
								new Vector3(-15, 5, 5), 
								new Vector3(-50, 5, 5),  
								new TrafficLight(StreetDirection.Left,
												null,
												false),
								0.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		streetsCounter ++ ;
		Street s7 = new Street( streetsCounter,
								new Vector3(-5, 5, -5), 
								new Vector3(-50, 5, -5),  
								new TrafficLight(StreetDirection.Left,
												null,
												false),
								0.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH,
								MAX_STREET_VEHICLES_NUMBER);
		
		Streets.Add(s0);
		Streets.Add(s1);
		Streets.Add(s2);
		Streets.Add(s3);
		Streets.Add(s4);
		Streets.Add(s5);
		Streets.Add(s6);
		Streets.Add(s7);
		
	}
	
	
	private void InitFirstLevelPaths(){
		Paths = new List<GamePath>();
		List<Street> tempPath;

		//Path0 (down, down)1
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[0]);
		tempPath.Add(Streets[1]);
		
		Paths.Add(new GamePath(tempPath, Streets[0].StartPoint , Streets[1].EndPoint, false));
		
		//Path1 (down, down)2
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[2]);
		tempPath.Add(Streets[3]);
		
		Paths.Add(new GamePath(tempPath, Streets[2].StartPoint, Streets[3].EndPoint, true));
		
		//Path2 (left, left)1
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[6]);
		
		Paths.Add(new GamePath(tempPath, Streets[4].StartPoint, Streets[6].EndPoint, true));
		
		
		//Path3 (left, left)2
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[5]);
		tempPath.Add(Streets[7]);
		
		Paths.Add(new GamePath(tempPath, Streets[5].StartPoint, Streets[7].EndPoint, false));
		
		
		//Path4 (left, down)
		
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[5]);
		tempPath.Add(Streets[3]);
		
		Paths.Add(new GamePath(tempPath, Streets[5].StartPoint, Streets[3].EndPoint, false));
		
		//Path5 (down, left)
		
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[0]);
		tempPath.Add(Streets[6]);
		
		Paths.Add(new GamePath(tempPath, Streets[0].StartPoint, Streets[6].EndPoint, false));
		
		
		
	}

	/*
	private void GenerateTempStreetsLevel3(){
		//*********** UPs
		Street s0 = new Street( new Vector3(15, 5, -50), 
								new Vector3(15, 5, 5),  
								new TrafficLight(Direction.Up,
												FindTagObject("lightUp"),
												true),
								-15.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		
		Street s1 = new Street( new Vector3(15, 5, 5), 
								new Vector3(15, 5, 50),  
								new TrafficLight(Direction.Up,
												null,
												false),
								0.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		
		Street s2 = new Street( new Vector3(5, 5, -50), 
								new Vector3(5, 5, -5),  
								new TrafficLight(Direction.Up,
												FindTagObject("lightUp"),
												true),
								-15.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		
		Street s3 = new Street( new Vector3(5, 5, -5), 
								new Vector3(5, 5, 50),  
								new TrafficLight(Direction.Up,
												null,
												false),
								0.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		//*********** DOWNs
		
		Street s4 = new Street( new Vector3(-5, 5, 50), 
								new Vector3(-5, 5, 5),  
								new TrafficLight(Direction.Down,
												FindTagObject("lightDown"),
												true),
								15.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		
		Street s5 = new Street( new Vector3(-5, 5, 5), 
								new Vector3(-5, 5, -50),  
								new TrafficLight(Direction.Down,
												null,
												false),
								0.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		
		Street s6 = new Street( new Vector3(-15, 5, 50), 
								new Vector3(-15, 5, -5),  
								new TrafficLight(Direction.Down,
												FindTagObject("lightDown"),
												true),
								15.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		
		Street s7 = new Street( new Vector3(-15, 5, -5), 
								new Vector3(-15, 5, -50),  
								new TrafficLight(Direction.Down,
												null,
												false),
								0.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		
		//*********** LEFTs
		
		Street s8 = new Street( new Vector3(50, 5, 5), 
								new Vector3(15, 5, 5),  
								new TrafficLight(Direction.Left,
												FindTagObject("lightLeft"),
												true),
								25.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		
		Street s9 = new Street( new Vector3(50, 5, -5), 
								new Vector3(5, 5, -5),  
								new TrafficLight(Direction.Left,
												FindTagObject("lightLeft"),
												true),
								25.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		
		//*********** RIGHTs
		
		Street s10 = new Street( new Vector3(-50, 5, 5), 
								new Vector3(-5, 5, -5),  
								new TrafficLight(Direction.Right,
												FindTagObject("lightRight"),
												true),
								-25.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		
		Street s11 = new Street( new Vector3(-50, 5, -5), 
								new Vector3(-15, 5, -5),  
								new TrafficLight(Direction.Right,
												FindTagObject("lightRight"),
												true),
								-25.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		
		Streets.Add(s0);
		Streets.Add(s1);
		Streets.Add(s2);
		Streets.Add(s3);
		Streets.Add(s4);
		Streets.Add(s5);
		Streets.Add(s6);
		Streets.Add(s7);
		Streets.Add(s8);
		Streets.Add(s9);
		Streets.Add(s10);
		Streets.Add(s11);

	}
	*/
	
	/*
	private void InitAllPathsForLevel3(){
		//Path0 (up, up)1
		List<Street> tempPath = new List<Street>() ;
		Paths = new List<Path>() ;
		tempPath.Add(Streets[0]);
		tempPath.Add(Streets[1]);
		
		Paths.Add(new Path(tempPath, new Vector3(15, 5, -50), new Vector3(15, 5, 50)));
		
		
		//Path1(up, up)2
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[2]);
		tempPath.Add(Streets[3]);
		
		Paths.Add(new Path(tempPath, new Vector3(5, 5, -50), new Vector3(5, 5, 50)));
		
		//Path2 (down, down)1
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[5]);
		
		Paths.Add(new Path(tempPath, new Vector3(-5, 5, 50), new Vector3(-5, 5, -50)));
		
		//Path3 (down, down)2
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[6]);
		tempPath.Add(Streets[7]);
		
		Paths.Add(new Path(tempPath, new Vector3(-15, 5, 50), new Vector3(-15, 5, -50)));
		
		//Path4 (left, up)1
		
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[8]);
		tempPath.Add(Streets[1]);
		
		Paths.Add(new Path(tempPath, new Vector3(50, 5, 5), new Vector3(15, 5, 50)));
		
		
		//Path5 (left, up)2
		
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[9]);
		tempPath.Add(Streets[3]);
		
		Paths.Add(new Path(tempPath, new Vector3(50, 5, -5), new Vector3(5, 5, 50)));
		
		//Path6 (right, down)1
		
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[10]);
		tempPath.Add(Streets[5]);
		
		Paths.Add(new Path(tempPath, new Vector3(-50, 5, 5), new Vector3(-5, 5, -50)));
		
		
		//Path7 (right, down)2
		
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[11]);
		tempPath.Add(Streets[7]);
		
		Paths.Add(new Path(tempPath, new Vector3(-50, 5, -5), new Vector3(-15, 5, -50)));
		
	}
	
	*/
	
	/*
	private void InitAllPathsForLevel2(){
		//Path0 (left, up)
		List<Street> tempPath = new List<Street>() ;
		Paths = new List<Path>() ;
		tempPath.Add(Streets[0]);
		tempPath.Add(Streets[6]);
		
		Paths.Add(new Path(tempPath, (FindTagObject("gpLeft")as GameObject).transform.position, new Vector3(10, 5, 50)));
		
		//Path1 (up, right)
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[3]);
		
		Paths.Add(new Path(tempPath, (FindTagObject("gpUp")as GameObject).transform.position, new Vector3(50, 5, 15)));
		
		//Path2 (up, up, up)
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[5]);
		tempPath.Add(Streets[6]);
		
		Paths.Add(new Path(tempPath,(FindTagObject("gpUp")as GameObject).transform.position, new Vector3(10, 5, 50)));
		
		//Path3 (down, down, down)
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[7]);
		tempPath.Add(Streets[8]);
		tempPath.Add(Streets[9]);
		
		Paths.Add(new Path(tempPath,(FindTagObject("gpDown")as GameObject).transform.position, new Vector3(-10, 5, -50)));
		
		//Path4 (right, down, down)
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[2]);
		tempPath.Add(Streets[8]);
		tempPath.Add(Streets[9]);
		
		Paths.Add(new Path(tempPath,(FindTagObject("gpRight1")as GameObject).transform.position, new Vector3(-10, 5, -50)));
		
		//Path5 (right, down)
		tempPath = new List<Street>() ;
		tempPath.Add(Streets[1]);
		tempPath.Add(Streets[9]);
		
		Paths.Add(new Path(tempPath,(FindTagObject("gpRight")as GameObject).transform.position, new Vector3(-10, 5, -50)));
		
		Debug.Log("All paths are inited" + Paths.Count);

	}
	
	*/
	// Use this for initialization
	void Start () {
		pressed = false;
		finished = true;
		
	}
	
	
	///// For temp test *****************
	/*
	private void GenerateTempStreetsLevel2(){
		Street s0 = new Street( new Vector3(50, 5, 15), 
								new Vector3(15, 5, 15),  
								new TrafficLight(Direction.Left,
												FindTagObject("lightLeft"),
												true),
								25.0f,					//stop position calculation based on the end point of the street
								STREET_WIDTH);
		
		Street s1 = new Street( new Vector3(-50, 5, 25), 
								new Vector3(-15, 5, 25), 
								new TrafficLight(Direction.Right,
												FindTagObject("lightRight"),
												true),
								-25.0f,
								STREET_WIDTH);
		
		Street s2 = new Street( new Vector3(-50, 5, -15), 
								new Vector3(-15, 5, -15), 
								new TrafficLight(Direction.Right,
												FindTagObject("lightRight1"),
												true),
								-25.0f,
								STREET_WIDTH);
		
		//without light
		Street s3 = new Street( new Vector3(10, 5, -35), 
								new Vector3(50, 5, 15), 
								new TrafficLight(Direction.Right,
												null,
												false),
								50.0f,
								STREET_WIDTH);
		//without light
		Street s4 = new Street( new Vector3(10, 5, -50), 
								new Vector3(10, 5, -35),
								new TrafficLight(Direction.Up,
												null,
												false),
								-42.0f,
								STREET_WIDTH);
		
		Street s5 = new Street( new Vector3(10, 5, -35), 
								new Vector3(10, 5, 15),
								new TrafficLight(Direction.Up,
												FindTagObject("lightUp"),
												true),
								5.0f,
								STREET_WIDTH);
		
		Street s6 = new Street( new Vector3(10, 5, 15), 
								new Vector3(10, 5, 50),
								new TrafficLight(Direction.Up,
												null,
												false),
								0.0f,
								STREET_WIDTH);
		
		Street s7 = new Street( new Vector3(-10, 5, 50), 
								new Vector3(-10, 5, 25),
								new TrafficLight(Direction.Down,
												FindTagObject("lightDown"),
												true),
								35.0f,
								STREET_WIDTH);
		
		Street s8 = new Street( new Vector3(-10, 5, 25), 
								new Vector3(-10, 5, -15),
								new TrafficLight(Direction.Down,
												null,
												false),
								0.0f,
								STREET_WIDTH);
		
		Street s9 = new Street( new Vector3(-10, 5, -15), 
								new Vector3(-10, 5, -50),
								new TrafficLight(Direction.Down,
												null,
												false),
								0.0f,
								STREET_WIDTH);
		Streets.Add(s0);
		Streets.Add(s1);
		Streets.Add(s2);
		Streets.Add(s3);
		Streets.Add(s4);
		Streets.Add(s5);
		Streets.Add(s6);
		Streets.Add(s7);
		Streets.Add(s8);
		Streets.Add(s9);
		
	}
	*/
	/*
	private void GenerateTempStreetsLevel1(){
		
		Street s1 = new Street( Vector3.zero,
								Vector3.zero,
								(FindTagObject("gpDown")as GameObject).transform.position , 
								new TrafficLight(Direction.Down,
												FindTagObject("lightDown"),
												true),
								20.0f,
								-45.0f, 
								STREET_WIDTH);
		Street s2 = new Street( Vector3.zero,
								Vector3.zero,
								(FindTagObject("gpUp")as GameObject).transform.position , 
								new TrafficLight(Direction.Up,
												FindTagObject("lightUp"),
												true),
								-20.0f,
								45.0f, 
								STREET_WIDTH);
		Street s3 = new Street( Vector3.zero,
								Vector3.zero,
								(FindTagObject("gpLeft")as GameObject).transform.position , 
								new TrafficLight(Direction.Left,
												FindTagObject("lightLeft"),
												true),
								20.0f,
								-45.0f, 
								STREET_WIDTH);
		Street s4 = new Street( Vector3.zero,
								Vector3.zero,
								(FindTagObject("gpRight")as GameObject).transform.position , 
								new TrafficLight(Direction.Right,
												FindTagObject("lightRight"),
												true),
								-20.0f,
								45.0f, 
								STREET_WIDTH);
		Streets.Add(s1);
		Streets.Add(s2);
		Streets.Add(s3);
		Streets.Add(s4);
	}
	
	*/
	private GameObject FindTagObject(string name){
		GameObject target = GameObject.FindGameObjectWithTag(name);
		return target;
	}
	
	// Update is called once per frame
	void Update () {
		
		// ====================================Use this code for graphics=============================================
		/*
		
		if(Input.GetMouseButtonDown(1)){
			Vector3 temp = Input.mousePosition;
			mousePos = (GameObject.FindGameObjectWithTag("MainCamera")).camera.ScreenToWorldPoint( new Vector3 (temp.x, 10, temp.y));
			Debug.Log(mousePos);
			pressed = true;
			finished = false;
			
		}
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			finished = true;
			
		}
		
		if(pressed){
			GenerateStreets();
		}
		*/
	}
	
	
	/*
	private void GenerateStreets(){
		//at the original version of the game we shoul call LoadStreets();
					
		//this piece of code is called to store the streets at first
		if(direction != "" && size != 0 ){
			//Street(Vector3 go, TrafficLight l)
			//TrafficLight(LightPositionType n, GameObject l, bool s)
			if(finished){
				Debug.Log(mousePos);
				GameObject go = Instantiate(lightPrefab, GetLightPosition(direction, size, mousePos) ,Quaternion.identity)as GameObject;
				Street s = new Street(mousePos, new TrafficLight(ConvertFromStringToLightEnum(direction),
																go,
																false), 0.0f, 0.0f, STREET_WIDTH);
				Streets.Add(s);
				go.renderer.material.color = Color.green;
				pressed = false;
			
			}	
				//store streets
		}
		
	}
	*/
	
	//this method returns the wanted position for the traffic light to be placed in (only for storing the streets)
	private Vector3 GetLightPosition(string s, float sz, Vector3 v){
		if(s == "l"){
			return new Vector3(v.x - sz, 10, v.z);
		}
		else if(s == "r"){
			return new Vector3(v.x + sz, 10, v.z);
		}
		else if(s == "u"){
			return new Vector3(v.x, 10, v.z + sz);
		}
		else{
			return new Vector3(v.x, 10, v.z - sz);
		}
		
		
	}
	
	//this method takes a string and returns the analogous light position type of type ENUM
	private StreetDirection ConvertFromStringToLightEnum(string s){
		if(s == "l"){
			return StreetDirection.Left;
		}
		else if(s == "r"){
			return StreetDirection.Right;
		}
		else if(s == "u"){
			return StreetDirection.Up;
		}
		else{
			return StreetDirection.Down;
		}
	}
	
	
	// loads streets 
	private void LoadStreets(){
		
	}
	
	
}
