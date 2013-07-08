using UnityEngine;
using System.Collections;

public class MessageBar : MonoBehaviour {
	
	//public static bool notifyNow;
	
	public static Queue messagesQ;
	private int displayTimer;
	
	public AudioClip notification_sound;
	
	// Use this for initialization
	void Start () {
		messagesQ = new Queue();
		displayTimer = 2;
		InvokeRepeating("DisplayOneMessage", 1.0f, 2.0f);
	}
	
	void DisplayOneMessage(){
		if(messagesQ.Count != 0 ){
			
			gameObject.GetComponent<UILabel>().text = messagesQ.Dequeue()as string;
			//audio.PlayOneShot(notification_sound);
			displayTimer = 0;
		}
		else if(displayTimer == 2){
			gameObject.GetComponent<UILabel>().text = "";
		}
		displayTimer+=2;
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
