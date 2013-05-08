using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MapProducer : MonoBehaviour {
	
	private List<TrafficLight> lights;
	private int streetsCounter;
	
	/*
	 Street(int id, Vector3 startPoint, Vector3 endPoint, TrafficLight trafficLight, float stopPosition, float minDistOpenLight, int streetCapacity)

	 */
	
	private Vector3 startPoint;
	private Vector3 endPoint;
	private float stopPoint;
	private string light;
	private StreetDirection direction;
	
	private int pressCounter;
	private Vector3 mousePos;
	
	private GameObject cam;
	
	
	// Use this for initialization
	void Start () {
		pressCounter = 0;
		streetsCounter = 0;
		//startPoint = Vector3.zero;
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		lights = new List<TrafficLight>();
		//Debug.Log(GetApproximatedPosition(new Vector3(-66.889f, 5, 12.6669f)));
	}
	
	// Update is called once per frame
	void Update () {
		
		
		// for the start point
		if(Input.GetMouseButtonDown(0) && pressCounter == 0){
			Vector3 temp = Input.mousePosition;
			mousePos = cam.camera.ScreenToWorldPoint(new Vector3(temp.x, temp.y, temp.z));
			//Debug.Log("mouse position " + mousePos);
			startPoint = MathsCalculatios.GetApproximatedPosition(mousePos);
			pressCounter ++;
			Debug.Log("Start Point: "+startPoint);
		}
		
		
		// for the end point
		else if(Input.GetMouseButtonDown(0) && pressCounter == 1){
			Vector3 temp = Input.mousePosition;
			mousePos = cam.camera.ScreenToWorldPoint( new Vector3(temp.x, temp.y, temp.z));
			endPoint = MathsCalculatios.GetApproximatedPosition(mousePos);
			pressCounter ++;
			Debug.Log("End Point: "+endPoint);
			SetTheDirection();
		}
		
		// for the stop position
		else if(Input.GetMouseButtonDown(0) && pressCounter == 2){
			Vector3 temp = Input.mousePosition;
			mousePos = (GameObject.FindGameObjectWithTag("MainCamera")).camera.ScreenToWorldPoint( new Vector3 (temp.x, temp.y, temp.z));
			
			if(direction == StreetDirection.Left || direction == StreetDirection.Right){
				stopPoint = MathsCalculatios.GetApproximatedPosition(mousePos).x;
			}
			else if(direction == StreetDirection.Up || direction == StreetDirection.Down){
				stopPoint = MathsCalculatios.GetApproximatedPosition(mousePos).z;
			}
			Debug.Log("Stop Position: "+stopPoint);
			pressCounter ++;
		}
		
		// for the light
		else if(Input.GetMouseButtonDown(0) && pressCounter == 3){
			Vector3 temp = Input.mousePosition;
			Ray ray = cam.camera.ScreenPointToRay(Input.mousePosition);
    		RaycastHit hit ;
    		if (Physics.Raycast(ray, out hit)){
				light = hit.collider.gameObject.tag;
   			}
			Debug.Log("Direction: "+direction);
			pressCounter ++;
			
		}
		
		
			
		
		
		
	}
	
	
	private void SetTheDirection(){
		if(startPoint.x < endPoint.x){
			direction = StreetDirection.Right;
		}
		if(startPoint.x > endPoint.x){
			direction = StreetDirection.Left;
		}
		if(startPoint.z < endPoint.z){
			direction = StreetDirection.Up;
		}
		if(startPoint.z < endPoint.z){
			direction = StreetDirection.Down;
		}
	}
	
	
	
	
	
	
	
	
	void OnClick (){
		//Debug.Log(mousePos);
		Debug.Log(" see thatt " + pressCounter);
		if(pressCounter <=3){
			light = "none";
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\USER\Documents\GitHub\TrafficGame\map.txt", true))
	        {
	            file.WriteLine(streetsCounter+"%"+startPoint+"%"+endPoint+"%"+"0"+"%"+direction+"%"+light+"%"+"false"+"\n");
				
	        }
		}
		else{
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\USER\Documents\GitHub\TrafficGame\map.txt", true))
	        {
	            file.WriteLine(streetsCounter+"%"+startPoint+"%"+endPoint+"%"+stopPoint+"%"+direction+"%"+light+"%"+"true"+"\n");
				
	        }
		}
		
		pressCounter = 0;
		streetsCounter++;
		Debug.Log("Street # " + streetsCounter + "has been added");
	}
}
