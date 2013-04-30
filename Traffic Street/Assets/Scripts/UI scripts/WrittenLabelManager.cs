using UnityEngine;
using System.Collections;

public class WrittenLabelManager : MonoBehaviour {
	public static GameObject label;

	// Use this for initialization
	void Start () {
		label = GameObject.FindGameObjectWithTag("writting label");
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectWithTag("Panel-Stages") == null){
			label.GetComponent<UILabel>().text = " ";
		}
	}
}
