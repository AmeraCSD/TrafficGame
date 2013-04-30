using UnityEngine;
using System.Collections;

public class LockedMapButton : MonoBehaviour {
	GameObject label;
	
	void Start(){
		label = WrittenLabelManager.label;
	}
	
	IEnumerator  OnClick(){
		
		if(Input.touchCount <=1){
			label.AddComponent<TypewriterEffect>();
			//yield return new WaitForSeconds(.5f);
	
			label.GetComponent<UILabel>().text = " Go to the app store to buy the full vesion ";
			
			//label.SetActive(true);
			
			//WaitForSeconds(5);
			
			
			yield return new WaitForSeconds(3.5f);
			label.GetComponent<UILabel>().text = " ";
		}
	
	}
	
	
	
}
