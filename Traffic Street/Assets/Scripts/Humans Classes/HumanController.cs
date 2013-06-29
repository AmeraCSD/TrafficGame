using UnityEngine;
using System.Collections;

public class HumanController: MonoBehaviour {
	
	private GameMaster gameMasterScript;
		
	private float angerMount;
	
	private float stoppingTimerforAnger;
	private bool stoppingTimerforAngerSet;
	
	public GameObject angerSpriteGo;
	public GameObject myAngerSprite;
	
	
	HumanGenerator humanGeneratorScript;
	public int myAnimationId;
	
	public HumanPath myHumanPath;
	
	
	void Awake(){
		
		gameMasterScript = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>();
		angerSpriteGo = GameObject.FindGameObjectWithTag("human comic");
		
		myAngerSprite = Instantiate(angerSpriteGo, transform.position, Quaternion.identity) as GameObject;
				
		myAngerSprite.SetActive(false);
		
		stoppingTimerforAnger = 0;
		stoppingTimerforAngerSet = false;
		angerMount = .5f;
	}
	
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
		
		myAngerSprite.transform.localRotation = Quaternion.AngleAxis(90, Vector3.right);
		myAngerSprite.transform.parent = GameObject.FindGameObjectWithTag("panel").transform;
		myAngerSprite.transform.position = new Vector3(transform.position.x-5 , transform.position.y, transform.position.z+6);
		
		
		if(myHumanPath.PassAnimationName!="none"){
			if( isHumanWalked()) {
				if(isAllStreetsStopped()){
					animation.Play(myHumanPath.PassAnimationName);
				}
				else{
					CheckMyAnger();
				}
			}
			
			
			if(isHumanPassed()){
				
				
				humanGeneratorScript.humanPaths[GetMyPathIndex()].IsLocked = false;
				humanGeneratorScript.existedHumans.Enqueue(gameObject);
				gameObject.SetActive(false);
				myAngerSprite.SetActive(false);
			}	
		}
		else{
//			Debug.Log("it is none");
			if(isHumanWalked()){
				
				
				
				humanGeneratorScript.humanPaths[GetMyPathIndex()].IsLocked = false;
			//	Debug.Log("el ragel meshy 5lasss >>> "+humanGeneratorScript.humanPaths[humanGeneratorScript.humanPaths.IndexOf(myHumanPath)].IsLocked);
				humanGeneratorScript.existedHumans.Enqueue(gameObject);
				gameObject.SetActive(false);
				myAngerSprite.SetActive(false);
			}
	}
		
		
	}
	
	private void CheckMyAnger(){
		if(!animation.isPlaying){
			if(! stoppingTimerforAngerSet){
				stoppingTimerforAnger = gameMasterScript.gameTime - Globals.ANGER_TIMER ;
				stoppingTimerforAngerSet = true;
			}
		
			if(gameMasterScript.gameTime <= stoppingTimerforAnger){
				stoppingTimerforAngerSet = false;
				GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>().AddjustSatisfaction(angerMount);
				gameMasterScript.satisfyBar += angerMount;
				gameMasterScript.secondsCounterForAnger = 0;
				stoppingTimerforAnger =0;
				angerMount *= 2;
				/// comicccccccccccccccccccc and zamameeer
				myAngerSprite.SetActive(true);
				//audio.PlayOneShot(Globals.humanAngerCalled);
				
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
