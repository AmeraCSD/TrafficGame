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
	
	
	//GUIStyle style;
//	Texture2D texture;
	
	// Use this for initialization
	void Start () {
		satisfyBarValue = satisfyBarValueGo.GetComponent<UILabel>();
		satisfyBarFill = satisfyBarFillGo.GetComponent<UIFilledSprite>();
		
		barLength = 0 ;
	}
	
	// Update is called once per frame
	void Update () {
		satisfyBarValue.text = (int)curValue+"" + "/" + maxValue+"";
	  	satisfyBarFill.fillAmount = barLength;
		AddjustSatisfaction(0);
		
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
	}
}
