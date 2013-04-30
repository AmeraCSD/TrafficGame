using UnityEngine;
using System.Collections;

public class CompanyNameLabel : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		
		
		yield return new WaitForSeconds(4); 
		gameObject.GetComponent<UILabel>().text = " ";
		
		gameObject.AddComponent<TypewriterEffect>();
		gameObject.GetComponent<TypewriterEffect>().charsPerSecond = 10;
		gameObject.GetComponent<UILabel>().text = " ";
		
		gameObject.GetComponent<UILabel>().text = " Zeeback";
		
		yield return new WaitForSeconds(4); 
		
		Application.LoadLevel("Main Menu");
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
