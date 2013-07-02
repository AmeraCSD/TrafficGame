using UnityEngine;
using System.Collections;

public class SatisfyBar : MonoBehaviour {
	public float maxValue = 10;
	public float curValue = 0;
	
	public float barLength;
	
	public GameObject satisfyBarValueGo;
	public GameObject satisfyBarFillGo;
	
	private UIFilledSprite satisfyBarFill;
	private UILabel satisfyBarValue;
	
	private GameMaster gameMasterScript;

	private bool playedAlert;
	
	//GUIStyle style;
//	Texture2D texture;
	
	// Use this for initialization
	void Start () {
		satisfyBarValue = satisfyBarValueGo.GetComponent<UILabel>();
		satisfyBarFill = satisfyBarFillGo.GetComponent<UIFilledSprite>();
		
		barLength = 0 ;
		
		gameMasterScript = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>();
		playedAlert = false;
		AddjustSatisfaction(0);
	}
	
	// Update is called once per frame
	void Update () {
		satisfyBarValue.text = (int)curValue+"" + "/" + maxValue+"";
	  	satisfyBarFill.fillAmount = barLength;
		
	}
	/*
	void OnGUI(){
		style = new GUIStyle();
		texture = new Texture2D(128, 128);
       	for (int y = 0; y < texture.height; ++y){
            for (int x = 0; x < texture.width; ++x)
            {
                texture.SetPixel(x, y, Color.red);
            }
        }
		
        texture.Apply();
 
        style.normal.background = texture;
		
		GUI.Box(new Rect(10, 70, barLength , 15), curValue+ "/" + maxValue, style);
	}
	*/
	
	
	public void AddjustSatisfaction(float adj){
		
		
		
		
		curValue += adj;
		if(curValue < 0)
			curValue = 0;
		if(curValue >maxValue)
			curValue = maxValue;
		if(maxValue < 1)
			maxValue = 1;
		barLength = (float)(curValue/(float)maxValue);
		SendBarMessages();
	}
	
	private void SendBarMessages(){
	//	Debug.Log( "the bar length  " +barLength*10);
		
		if(barLength*10 >= 9){
			if(!MessageBar.messagesQ.Contains(Globals.SATISTFY_BAR_MSG_5))
				MessageBar.messagesQ.Enqueue(Globals.SATISTFY_BAR_MSG_5);
		//	gameMasterScript.eventWarningLabel.text = Globals.SATISTFY_BAR_MSG_5;
		//	MessageBar.notifyNow = true;
		}
		if(barLength*10 >= 8){
			if(!MessageBar.messagesQ.Contains(Globals.SATISTFY_BAR_MSG_4))
				MessageBar.messagesQ.Enqueue(Globals.SATISTFY_BAR_MSG_4);
		//	gameMasterScript.eventWarningLabel.text = Globals.SATISTFY_BAR_MSG_4;
		//	MessageBar.notifyNow = true;
		}
		if(barLength*10 >= 6){
			if(!MessageBar.messagesQ.Contains(Globals.SATISTFY_BAR_MSG_3))
				MessageBar.messagesQ.Enqueue(Globals.SATISTFY_BAR_MSG_3);
		//	gameMasterScript.eventWarningLabel.text = Globals.SATISTFY_BAR_MSG_3;
		//	MessageBar.notifyNow = true;
		}
		if(barLength*10 >= 4){
			if(!MessageBar.messagesQ.Contains(Globals.SATISTFY_BAR_MSG_2))
				MessageBar.messagesQ.Enqueue(Globals.SATISTFY_BAR_MSG_2);
		//	gameMasterScript.eventWarningLabel.text = Globals.SATISTFY_BAR_MSG_2;
		//	MessageBar.notifyNow = true;
		}
		if(barLength*10 <= 1.5f){
			if(!MessageBar.messagesQ.Contains(Globals.SATISTFY_BAR_MSG_1))
				MessageBar.messagesQ.Enqueue(Globals.SATISTFY_BAR_MSG_1);
		//	gameMasterScript.eventWarningLabel.text = Globals.SATISTFY_BAR_MSG_1;
		//	MessageBar.notifyNow = true;
		}
		
		
	}
}
