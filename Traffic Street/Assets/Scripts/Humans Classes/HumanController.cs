using UnityEngine;
using System.Collections;

public class HumanController: MonoBehaviour {
	
	HumanGenerator humanGeneratorScript;
	public int myAnimationId;
	
	public HumanPath myHumanPath;
	
	// Use this for initialization
	void Start () {
		//myAnimationId = Random.Range(0,7);
		humanGeneratorScript = GameObject.FindGameObjectWithTag("master").GetComponent<HumanGenerator>();
		
		animation.Play(myHumanPath.WalkAnimationName);
	//	humanGeneratorScript.humanPaths[humanGeneratorScript.humanPaths.IndexOf(myHumanPath)].IsLocked = true;
		
	}

	public void ReStratHuman(){
		humanGeneratorScript = GameObject.FindGameObjectWithTag("master").GetComponent<HumanGenerator>();
	//	humanGeneratorScript.humanPaths[humanGeneratorScript.humanPaths.IndexOf(myHumanPath)].IsLocked = true;
		animation.Play(myHumanPath.WalkAnimationName);
	}
	// Update is called once per frame
	void Update () {
		
		if(myHumanPath.PassAnimationName!="none"){
			if( isHumanWalked() && isAllStreetsStopped()){
				animation.Play(myHumanPath.PassAnimationName);
				
			}
			if(isHumanPassed()){
				
				
				humanGeneratorScript.humanPaths[GetMyPathIndex()].IsLocked = false;
				humanGeneratorScript.existedHumans.Enqueue(gameObject);
				gameObject.SetActive(false);
			}	
		}
		else{
//			Debug.Log("it is none");
			if(isHumanWalked()){
				
				
				
				humanGeneratorScript.humanPaths[GetMyPathIndex()].IsLocked = false;
			//	Debug.Log("el ragel meshy 5lasss >>> "+humanGeneratorScript.humanPaths[humanGeneratorScript.humanPaths.IndexOf(myHumanPath)].IsLocked);
				humanGeneratorScript.existedHumans.Enqueue(gameObject);
				gameObject.SetActive(false);
			}
		}
	}
	
	private bool isAllStreetsStopped(){
		for(int i=0; i<myHumanPath.ToBePassedStreets.Count; i++){
			if(!myHumanPath.ToBePassedStreets[i].StreetLight.Stopped){
				return false;
			}
		}
		
		return true;
	}
	
	private bool isHumanWalked(){
		if(myHumanPath.DirectionAxis == StreetDirection.Up){ ////////////////////////////////axisDirection
			if(gameObject.transform.position.z > myHumanPath.WalkEndPos){
				return true;
			}
		}
		
		else if(myHumanPath.DirectionAxis == StreetDirection.Down){ 
			if(gameObject.transform.position.z < myHumanPath.WalkEndPos){
				return true;
			}
		}
		
		else if(myHumanPath.DirectionAxis == StreetDirection.Right){ 
			if(gameObject.transform.position.x > myHumanPath.WalkEndPos){
				return true;
			}
		}
		
		else if(myHumanPath.DirectionAxis == StreetDirection.Left){ 
			if(gameObject.transform.position.x < myHumanPath.WalkEndPos){
				return true;
			}
		}
		
		return false;
	}
	
	private bool isHumanPassed(){
		if(myHumanPath.DirectionAxis == StreetDirection.Up){ ////////////////////////////////axisDirection
			if(gameObject.transform.position.z > myHumanPath.PassEndPos){
				return true;
			}
		}
		
		else if(myHumanPath.DirectionAxis == StreetDirection.Down){ 
			if(gameObject.transform.position.z < myHumanPath.PassEndPos){
				return true;
			}
		}
		
		else if(myHumanPath.DirectionAxis == StreetDirection.Right){ 
			if(gameObject.transform.position.x > myHumanPath.PassEndPos){
				return true;
			}
		}
		
		else if(myHumanPath.DirectionAxis == StreetDirection.Left){ 
			if(gameObject.transform.position.x < myHumanPath.PassEndPos){
				return true;
			}
		}
		
		return false;
	}
	
	private int GetMyPathIndex(){
		for(int i=0; i<humanGeneratorScript.humanPaths.Count; i++){
			if(humanGeneratorScript.humanPaths[i].WalkAnimationName == myHumanPath.WalkAnimationName){
				return i;
			}
		}
		return -1;
	}
	
	
}
