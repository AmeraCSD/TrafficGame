using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {
	public static bool vibrationEnabled;
	public static float STREET_WIDTH = 23;
	
	
	
	
	// Use this for initialization
	void Start () {
		//vibrationEnabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(vibrationEnabled);
	}
}
