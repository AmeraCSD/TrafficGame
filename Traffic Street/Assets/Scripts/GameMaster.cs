using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	
	public Queue UD_queue;
	public Queue DU_queue;
	public Queue LR_queue;
	public Queue RL_queue;
	
	//public CarsGenerator carGenScript;
	
	public int score;
	public bool over;
	
	
	
	void Awake(){
		InitQueues();
		over = false;
		score = 0;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	public void InitQueues(){
		DU_queue = new Queue();
		UD_queue = new Queue();
		LR_queue = new Queue();
		RL_queue = new Queue();
		Debug.Log("queues initialized");
		
	/*	carGenScript = gameObject.GetComponent<CarsGenerator>();
		List<GameObject> cars = new List<GameObject>();
		cars = carGenScript.GetCars();
		
		foreach (GameObject go in cars){
			if(go.name.Contains("UD")){
				UD_queue.Enqueue(go);
				Debug.Log("enqqqqqqqqqqqqqueee "+ go.name);
			}
			else if(go.name.Contains("DU")){
				DU_queue.Enqueue(go);
			}
			else if(go.name.Contains("LR")){
				LR_queue.Enqueue(go);
			}
			else{
				RL_queue.Enqueue(go);
			}
		}
		*/
	}
	

	
	public void DoEnqueue(GameObject go, string q){
		if(q == "D to U"){
			lock(DU_queue){
				DU_queue.Enqueue(go);
			}
		}
		else if(q == "U to D"){
			lock(UD_queue){
				UD_queue.Enqueue(go);
			}
		}
		else if (q == "L to R" ){
			lock(LR_queue){
				LR_queue.Enqueue(go);
			}
		}
		else if (q  == "R to L" ){
			lock(RL_queue){
				RL_queue.Enqueue(go);
			}
		}
	}
	
	public void DoDequeue(string q){
		if(q == "D to U" && DU_queue.Count>0){
			DU_queue.Dequeue();
			Debug.Log("dequeue in DU");
		}
		else if(q == "U to D" && UD_queue.Count>0){
			UD_queue.Dequeue();
			Debug.Log("dequeue in UD");
		}
		else if (q == "L to R" && LR_queue.Count>0){
			LR_queue.Dequeue();
			Debug.Log("dequeue in LR");
		}
		else if (q == "R to L" && RL_queue.Count>0){
			RL_queue.Dequeue();
			Debug.Log("dequeue in RL");
		}
		
	}
	
	public int DoGetSize(string q){
		if(q == "D to U" ){
			return DU_queue.Count;
		}
		else if(q == "U to D"){
			return UD_queue.Count;
		}
		else if (q == "L to R" ){
			return LR_queue.Count;
		}
		else {
			return RL_queue.Count;
		}
	}
	
	
	
	// Update is called once per frame
	void Update () {
		if(UD_queue.Count == 6 || DU_queue.Count == 6 || LR_queue.Count == 6 || RL_queue.Count == 6)
			over = true;
	}
	
	void OnGUI(){
		
		GUI.Label(new Rect(10, 30, 100, 25), "Score : "+score.ToString());	
		
		if(over){
		//	st.fontSize = 50;
			Destroy( GameObject.Find("  Game Master"));
			GUI.Box(new Rect(0, 0, Screen.width , Screen.height ) , " "  );
			GUI.Label(new Rect(Screen.width/2 , Screen.height/2 , 100, 25), "Game Over !!");

			GUI.Label(new Rect(Screen.width/2 , Screen.height/2+30 , 100, 25), "Score : "+score.ToString());
			Application.Quit();
			
		}
				//Application.LoadLevel("Game Over_Scene");
		

		
	}
	
	
}
