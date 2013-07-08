using UnityEngine;
using System.Collections;

public class ZoomInOut : MonoBehaviour {
	
	
	private int zoom = 20;
	private int normal = 60;
	private int smooth  = 5;
	private bool isZoomed = false;
	
	private Vector3 originalPos = new Vector3(0, 156, -9);
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("z")){
	    	isZoomed = !isZoomed; 
	    }
	 
	    if(isZoomed == true){
	        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,zoom,Time.deltaTime*smooth);
			if(Input.GetKey("up")){
			transform.Translate(Vector3.up * 80 * Time.deltaTime, Space.Self);
		}
		if(Input.GetKey("down")){
			transform.Translate(-1*Vector3.up * 80 * Time.deltaTime, Space.Self);
		}
		if(Input.GetKey("right")){
			transform.Translate(Vector3.right * 80 * Time.deltaTime, Space.Self);
		}
		if(Input.GetKey("left")){
			transform.Translate(-1*Vector3.right * 80 * Time.deltaTime, Space.Self);
		}
	    }
		
	    else{
	       	camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,normal,Time.deltaTime*smooth);
		//	transform.position = originalPos; comented for the temp test ********************************8
	    }
		
		
	}
}
