using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntersectionsProducer : MonoBehaviour {
	
	private bool clicked;
	
	public  List<Vector3> intersections;
	public GameObject go;
	
	private UILabel label;
	private GameObject cam;
	private Vector3 mousePos;

	
	// Use this for initialization
	void Start () {
		clicked = false;
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		label = go.GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
		if(clicked){
			if(Input.GetMouseButtonDown(0)){
			Vector3 temp = Input.mousePosition;
			mousePos = cam.camera.ScreenToWorldPoint(new Vector3(temp.x, temp.y, temp.z));
			//Debug.Log("mouse position " + mousePos);
			intersections.Add(MathsCalculatios.GetApproximatedPosition(mousePos));
			}
		}
	}
	
	void OnClick(){
		
		if(!clicked){
			clicked = true;
			label.text = "Finish";
		}
		else{
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\USER\Documents\GitHub\TrafficGame\intersections.txt", true))
	        {
				for(int i = 0; i<intersections.Count; i++){
	            	file.WriteLine("intersectionsList.Add("+ intersections[i] +");"+"\n");
				}
				
	        }
		}
		
	}
}
