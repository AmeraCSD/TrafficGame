using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StreetsGenerator : MonoBehaviour {
	
	private List<Street>  Streets;				//this is a list of the all of the streets in the game (should be used in GameMaster and LightsGamer)
	public const float STREET_WIDTH = 15;
	
	public GameObject lightPrefab = null;		//this should be initialized in unity with the traffic light
	
	// variables for storing the streets at first (GRAPHICS NEEDS)
	public string direction = "";				//this should be initialized in unity when the game starts
	public float size = 0;						//this should be initialized in unity when the game starts
	private bool pressed;
	private bool finished;
	private Vector3 mousePos;
	//
	
	
	
	public List<Street> getStreets(){
		return Streets;
	}
	
	void Awake(){
		Streets = new List<Street>();
		GenerateTempStreets(); 					//for temp test
	}
	
	// Use this for initialization
	void Start () {
		pressed = false;
		finished = true;
		
	}
	
	///// For temp test *****************
	private void GenerateTempStreets(){
		Street s1 = new Street((FindLightObject("gpDown")as GameObject).transform.position , 
								new TrafficLight(Direction.Down,
												FindLightObject("lightDown"),
												true),
								20.0f,
								-45.0f, 
								STREET_WIDTH);
		Street s2 = new Street((FindLightObject("gpUp")as GameObject).transform.position , 
								new TrafficLight(Direction.Up,
												FindLightObject("lightUp"),
												true),
								-20.0f,
								45.0f, 
								STREET_WIDTH);
		Street s3 = new Street((FindLightObject("gpLeft")as GameObject).transform.position , 
								new TrafficLight(Direction.Left,
												FindLightObject("lightLeft"),
												true),
								20.0f,
								-45.0f, 
								STREET_WIDTH);
		Street s4 = new Street((FindLightObject("gpRight")as GameObject).transform.position , 
								new TrafficLight(Direction.Right,
												FindLightObject("lightRight"),
												true),
								-20.0f,
								45.0f, 
								STREET_WIDTH);
		Streets.Add(s1);
		Streets.Add(s2);
		Streets.Add(s3);
		Streets.Add(s4);
	}
	
	private GameObject FindLightObject(string name){
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
	private Direction ConvertFromStringToLightEnum(string s){
		if(s == "l"){
			return Direction.Left;
		}
		else if(s == "r"){
			return Direction.Right;
		}
		else if(s == "u"){
			return Direction.Up;
		}
		else{
			return Direction.Down;
		}
	}
	
	
	// loads streets 
	private void LoadStreets(){
		
	}
	
	
}
