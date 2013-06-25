using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanGenerator : MonoBehaviour {
	
	GameMaster gameMasterScript;
	public Queue existedHumans;	
	public GameObject humanPrefab;
	public List <Material> humanMaterials;
	
	
	public List<HumanPath> humanPaths;
	private float HUMAN_GENERATION_RATE = 1;
	private float humanGenerationTimer;
	
	private HumansData data;
	
	
	// Use this for initialization
	void Start () {
		gameMasterScript = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>();
		humanGenerationTimer = 0;
		existedHumans = new Queue();
		data = new HumansData();
		
		humanPaths = data.map1_HumanPathsData(gameMasterScript.Streets);
		
		//initPathsList*****************************************************************************************
		
		InvokeRepeating("GenerateOneHuman", 1.0f, HUMAN_GENERATION_RATE);
	}
	
	void GenerateOneHuman ()
	{
		if(++humanGenerationTimer == 150)
			CancelInvoke("GenerateOneHuman");
			 
	//	if(Random.Range(0,1) == 0){
			
			int pathsListIndex = Random.Range(0, humanPaths.Count);
			
		
		//	Debug.Log("Path_"+pathsListIndex + " lock is "+humanPaths[pathsListIndex].IsLocked);
			if(!humanPaths[pathsListIndex].IsLocked && humanPrefab != null){
			//*****************************optimization
				GameObject human;
				if(existedHumans.Count == 0){
					human = Instantiate(humanPrefab, humanPaths[pathsListIndex].GenerationPosition ,Quaternion.identity) as GameObject;
					human.renderer.material = humanMaterials[Random.Range(0, humanMaterials.Count)];
					humanPaths[pathsListIndex].IsLocked = true;
					//public HumanPath(string walkAnimationName, string passAnimationName, List<Street> toBePassedStreets, char directionAxis, float passEndPos, bool locked){
					human.GetComponent<HumanController>().myHumanPath = humanPaths[pathsListIndex];
					
				}
				else{
					human = existedHumans.Dequeue() as GameObject;
					human.transform.position = humanPaths[pathsListIndex].GenerationPosition;
					human.transform.rotation = Quaternion.identity;
					human.renderer.material = humanMaterials[Random.Range(0, humanMaterials.Count)];
					human.SetActive(true);
					humanPaths[pathsListIndex].IsLocked = true;
					//public HumanPath(string walkAnimationName, string passAnimationName, List<Street> toBePassedStreets, char directionAxis, float passEndPos, bool locked){
					human.GetComponent<HumanController>().myHumanPath = humanPaths[pathsListIndex];
					human.GetComponent<HumanController>().ReStratHuman();
					
				}
				
				//humanPaths[pathsListIndex].IsLocked = true;
			}	
	//	}
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
