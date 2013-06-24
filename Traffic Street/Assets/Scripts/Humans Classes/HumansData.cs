using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumansData : MonoBehaviour {
	
	public List<Street> mapStreets;
	
	public List<HumanPath> humanPathsObjects;
	
	
	public List<HumanPath> map1_HumanPathsData(List<Street> streets){
		InitLists();
		
		mapStreets = streets;
		//animation1
		
		List<Street> strList1 = new List<Street>();
		strList1.Add(mapStreets[0]);
		strList1.Add(mapStreets[3]);
		strList1.Add(mapStreets[7]);
		strList1.Add(mapStreets[10]);

		humanPathsObjects.Add(new HumanPath(new Vector3(40, -3, -70), "walk_anim1", "pass_anim1", strList1, StreetDirection.Up, -43, 2, false));
		
		//animation2

		humanPathsObjects.Add(new HumanPath(new Vector3(-71, -3, -70), "walk_anim2", "none", null, StreetDirection.Down, -79, 0, false));
		
		//animation3

		humanPathsObjects.Add(new HumanPath(new Vector3(-47, -3, -70), "walk_anim3", "none", null, StreetDirection.Left, -100, 0, false));
		
		//animation4
		
		List<Street> strList4 = new List<Street>();
		strList1.Add(mapStreets[0]);
		strList1.Add(mapStreets[3]);
		strList1.Add(mapStreets[7]);
		strList1.Add(mapStreets[10]);

		humanPathsObjects.Add(new HumanPath(new Vector3(33, -3, -70), "walk_anim4", "pass_anim4", strList1, StreetDirection.Up, -43, 2, false));
		
		
		return humanPathsObjects;
		
	}
	
	private void InitLists(){
		humanPathsObjects = new List<HumanPath>();
	}
	
}
