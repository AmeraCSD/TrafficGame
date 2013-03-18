using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StreetsGenerator : MonoBehaviour {
	private List<Street>  Streets;
	//public List<string> Directions;
	//public List<float> Sizes;
	public GameObject lightPrefab = null;
	
	public string direction = "";
	public float size = 0;
	
	private bool pressed;
	private bool finished;
	private Vector3 mousePos;
	
	
	
	public List<Street> getStreets(){
		return Streets;
	}
	
	void Awake(){
		Streets = new List<Street>();
		GenerateTempStreets(); //for temp test
	}
	
	// Use this for initialization
	void Start () {
		pressed = false;
		finished = true;
		
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
	
	private void GenerateTempStreets(){
		Street s1 = new Street((FindLightObject("gpDown")as GameObject).transform.position , 
								new TrafficLight(LightPositionType.Down,
												FindLightObject("lightDown"),
												true));
		Street s2 = new Street((FindLightObject("gpUp")as GameObject).transform.position , 
								new TrafficLight(LightPositionType.Up,
												FindLightObject("lightUp"),
												true));
		Street s3 = new Street((FindLightObject("gpLeft")as GameObject).transform.position , 
								new TrafficLight(LightPositionType.Left,
												FindLightObject("lightLeft"),
												true));
		Street s4 = new Street((FindLightObject("gpRight")as GameObject).transform.position , 
								new TrafficLight(LightPositionType.Right,
												FindLightObject("lightRight"),
												true));
		Streets.Add(s1);
		Streets.Add(s2);
		Streets.Add(s3);
		Streets.Add(s4);
	}
	
	private GameObject FindLightObject(string name){
		GameObject target = GameObject.FindGameObjectWithTag(name);
		return target;
	}
	
	private void GenerateStreets(){
		//LoadStreets();
					
		if(direction != "" && size != 0 ){
			//Street(Vector3 go, TrafficLight l)
			//TrafficLight(LightPositionType n, GameObject l, bool s)
			if(finished){
				Debug.Log(mousePos);
				GameObject go = Instantiate(lightPrefab, GetLightPosition(direction, size, mousePos) ,Quaternion.identity)as GameObject;
				Street s = new Street(mousePos, new TrafficLight(ConvertFromStringToEnum(direction),
																go,
																false));
				Streets.Add(s);
				go.renderer.material.color = Color.green;
				pressed = false;
			
			}	
				//store streets
		}
		
	}
	
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
	
	private LightPositionType ConvertFromStringToEnum(string s){
		if(s == "l"){
			return LightPositionType.Left;
		}
		else if(s == "r"){
			return LightPositionType.Right;
		}
		else if(s == "u"){
			return LightPositionType.Up;
		}
		else{
			return LightPositionType.Down;
		}
	}
	
	
	// loads streets 
	private void LoadStreets(){
		
	}
	
	
}
