using UnityEngine;
using System.Collections;

public class EventsCounter : MonoBehaviour {
	
	public static int eventsCompleted ;
	
	//public float rating = score;
	
	// Use this for initialization
	IEnumerator Start () {
		
		

		//for(float i=0; i<score; i = i+(rating/200) ){
		yield return new WaitForSeconds(3.5f);
		gameObject.GetComponent<UILabel>().text = eventsCompleted+" ";
		
		yield return new WaitForSeconds(.5f);
		gameObject.GetComponent<UILabel>().text += "X 10";
		yield return new WaitForSeconds(.5f);
		gameObject.GetComponent<UILabel>().text += " = " + eventsCompleted*10 + "";
		
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
