using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map2_Data : MonoBehaviour {
	
	//******************************************************************Vehicles Data***********************************************************//
	
	//private Globals globals = Globals.GetInstance();
	
	public static float NormalVehicleSpeed = 25;
	public static int MinScore = 200;
	public static float GameTime = 300;
	
	public static List<int> VehiclesGroups(){
		List<int> groups = new List<int>();
			
		//25
		groups.Add(1);
		groups.Add(2);
		groups.Add(3);
		groups.Add(8);
		groups.Add(15);
		groups.Add(20);
		
		
		//75
		groups.Add(25);
		groups.Add(30);
		groups.Add(35);
		groups.Add(40);
		
		//100
		groups.Add(50);
		groups.Add(50);
		
		return groups;
	}
	
	public static List<int []> InistantiationRatesIntervals(){
		List<int []> init_rates_intervals = new List<int[]>();
			
		init_rates_intervals.Add(new int[2]{1,280});
		init_rates_intervals.Add(new int[2]{2,270});
		init_rates_intervals.Add(new int[2]{4,260});
		init_rates_intervals.Add(new int[2]{5,250});
		init_rates_intervals.Add(new int[2]{8,200});
		
		init_rates_intervals.Add(new int[2]{10,0});
			
		return init_rates_intervals;
	}
	
	//******************************************************************Events Data***************************************************************//
	public static int originalThiefStreetCapacity = 2;
	
	public static List<VehicleType> Events(){
		List<VehicleType> events = new List<VehicleType>();
		
		events.Add(VehicleType.Ambulance);
		events.Add(VehicleType.Bus);
		events.Add(VehicleType.Caravan);
		events.Add(VehicleType.ServiceCar);
		events.Add(VehicleType.Taxi);
		events.Add(VehicleType.Thief);
		events.Add(VehicleType.Police);
		
		return events;
	}
	
	public static List<int> EventsNumbers(){
		List<int> eventsNumbers = new List<int>();
		
		eventsNumbers.Add(2); 		//ambulance
		eventsNumbers.Add(1);		//bus
		eventsNumbers.Add(1);		//caravan
		eventsNumbers.Add(1);		//service car
		eventsNumbers.Add(2);		//taxi
		eventsNumbers.Add(1);		//thief
		eventsNumbers.Add(1);		//police
		
		return eventsNumbers;
	}
	
	public static List<EventTimes> EventsTimes(){
		List<EventTimes> eventTimes = new List<EventTimes>();
		
		//ambulance
		List <float> ambulanceTimeslist = new List<float>();
		ambulanceTimeslist.Add(200);
		ambulanceTimeslist.Add(100);
		eventTimes.Add(new EventTimes(ambulanceTimeslist));
		
		//bus
		List <float> busTimeslist = new List<float>();
		busTimeslist.Add(140);
		eventTimes.Add(new EventTimes(busTimeslist));
		
		//caravan
		List <float> caravanTimeslist = new List<float>();
		caravanTimeslist.Add(252);
		eventTimes.Add(new EventTimes(caravanTimeslist));
		
		//service car
		List <float> serviceCarTimeslist = new List<float>();
		serviceCarTimeslist.Add(150);
		eventTimes.Add(new EventTimes(serviceCarTimeslist));
		
		//taxi
		List <float> taxiTimeslist = new List<float>();
		taxiTimeslist.Add(213);
		taxiTimeslist.Add(184);
		eventTimes.Add(new EventTimes(taxiTimeslist));
		
		//thief
		List <float> thiefTimeslist = new List<float>();
		thiefTimeslist.Add(250);
		eventTimes.Add(new EventTimes(thiefTimeslist));
		
		//police
		List <float> policeTimeslist = new List<float>();
		policeTimeslist.Add(248);
		eventTimes.Add(new EventTimes(policeTimeslist));
		
		return eventTimes;
	}
	
	public static List <List<GamePath>> EventsPaths(List<GamePath> Paths){
	
		List <List<GamePath>> eventsPaths = new List<List<GamePath>>();
		
		
		//ambulance
		
		List <GamePath> ambulanceGamePathsList = new List<GamePath>();
		ambulanceGamePathsList.Add(Paths[4]);
		eventsPaths.Add(ambulanceGamePathsList);
		
		//Bus
		
		List <GamePath> busGamePathsList = new List<GamePath>();
		busGamePathsList.Add(Paths[0]);
		eventsPaths.Add(busGamePathsList);
		
		//Caravan
		
		List <GamePath> caravanGamePathsList = new List<GamePath>();
		caravanGamePathsList.Add(Paths[6]);
		//caravanGamePathsList.Add(Paths[3]);
		eventsPaths.Add(caravanGamePathsList);
		
		//service car
		
		List <GamePath> servicCarGamePathsList = new List<GamePath>();
		servicCarGamePathsList.Add(Paths[0]);
		servicCarGamePathsList.Add(Paths[2]);
		eventsPaths.Add(servicCarGamePathsList);
		
		//taxi
		
		List <GamePath> taxiGamePathsList = new List<GamePath>();
		taxiGamePathsList.Add(Paths[10]);
		eventsPaths.Add(taxiGamePathsList);
		
		//Thief
		
		List <GamePath> thiefGamePathsList = new List<GamePath>();
		thiefGamePathsList.Add(Paths[3]);
		eventsPaths.Add(thiefGamePathsList);
		
		//Police
		
		List <GamePath> policeGamePathsList = new List<GamePath>();
		policeGamePathsList.Add(Paths[3]);
		eventsPaths.Add(policeGamePathsList);
		
		return eventsPaths;
		
	}
	
	
	//******************************************************************Map Data***************************************************************//
	
	public static string streetsString =  "0%(-45.0, 5.0, -25.0)%(-45.0, 5.0, -75.0)%0%Down%none%false%2\n" +
		"1%(-35.0, 5.0, -85.0)%(-35.0, 5.0, -35.0)%-50%Up%lightUp2%true%2\n" +
		"2%(-105.0, 5.0, -35.0)%(-35.0, 5.0, -35.0)%-60%Right%lightRight4%true%3\n" +
		"3%(-45.0, 5.0, -25.0)%(-105.0, 5.0, -25.0)%0%Left%none%false%3\n" +
		"4%(-35.0, 5.0, -35.0)%(35.0, 5.0, -35.0)%20%Right%lightRight3%true%3\n" +
		"5%(45.0, 5.0, -25.0)%(-45.0, 5.0, -25.0)%-20%Left%lightLeft2%true%3\n" +
		"6%(35.0, 5.0, -35.0)%(35.0, 5.0, -75.0)%0%Down%none%false%2\n" +
		"7%(45.0, 5.0, -75.0)%(45.0, 5.0, -25.0)%-50%Up%lightUp1%true%2\n" +
		"8%(35.0, 5.0, -35.0)%(105.0, 5.0, -35.0)%0%Right%none%false%3\n" +
		"9%(110.0, 5.0, -25.0)%(45.0, 5.0, -25.0)%60%Left%lightLeft4%true%3\n" +
		"10%(-45.0, 5.0, 25.0)%(-45.0, 5.0, -25.0)%-10%Down%lightDown4%true%3\n" +
		"11%(-35.0, 5.0, -35.0)%(-35.0, 5.0, 35.0)%10%Up%lightUp4%true%3\n" +
		"12%(35.0, 5.0, 35.0)%(35.0, 5.0, -35.0)%-10%Down%lightDown3%true%3\n" +
		"13%(45.0, 5.0, -25.0)%(45.0, 5.0, 25.0)%10%Up%lightUp3%true%3\n" +
		"14%(-110.0, 5.0, 25.0)%(-45.0, 5.0, 25.0)%-60%Right%lightRight1%true%3\n" +
		"15%(-35.0, 5.0, 35.0)%(-95.0, 5.0, 35.0)%0%Left%none%false%3\n" +
		"16%(-45.0, 5.0, 25.0)%(45.0, 5.0, 25.0)%25%Right%lightRight2%true%3\n" +
		"17%(35.0, 5.0, 35.0)%(-35.0, 5.0, 35.0)%-20%Left%lightLeft3%true%3\n" +
		"18%(45.0, 5.0, 25.0)%(95.0, 5.0, 25.0)%0%Right%none%false%3\n" +
		"19%(110.0, 5.0, 35.0)%(35.0, 5.0, 35.0)%60%Left%lightLeft1%true%3\n" +
		"20%(-45.0, 5.0, 75.0)%(-45.0, 5.0, 25.0)%50%Down%lightDown1%true%2\n" +
		"21%(-35.0, 5.0, 35.0)%(-35.0, 5.0, 75.0)%0%Up%none%false%2\n" +
		"22%(35.0, 5.0, 75.0)%(35.0, 5.0, 35.0)%50%Down%lightDown2%true%2\n" +
		"23%(45.0, 5.0, 25.0)%(45.0, 5.0, 75.0)%0%Up%none%false%2";
	
	public static List<Vector3> Intersections(){
		List<Vector3> intersectionsList = new List<Vector3>();
		
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(-35.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(-35.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(-35.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(-35.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(-45.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(45.0f, 5.0f, 35.0f));
		intersectionsList.Add(new Vector3(45.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, 25.0f));
		intersectionsList.Add(new Vector3(45.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, -25.0f));
		intersectionsList.Add(new Vector3(35.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(45.0f, 5.0f, -35.0f));
		intersectionsList.Add(new Vector3(25.0f, 5.0f, 5.0f));
		
		
		
		return intersectionsList;

	}
	
	public static List<GamePath> Paths(List<Street> Streets){
		List<GamePath> Paths = new List<GamePath>();
		
		List<Street> tempPath;
		
		//path 0
		tempPath = new List<Street>();
		tempPath.Add(Streets[2]);
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[8]);
		
		Paths.Add(new GamePath(tempPath, Streets[2].StartPoint , Streets[8].EndPoint, false));
		
		//path 1
		tempPath = new List<Street>();
		tempPath.Add(Streets[2]);
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[6]);
		
		Paths.Add(new GamePath(tempPath, Streets[2].StartPoint , Streets[6].EndPoint, false));
		
		
		//path 2
		tempPath = new List<Street>();
		tempPath.Add(Streets[1]);
		tempPath.Add(Streets[11]);
		tempPath.Add(Streets[21]);
		
		Paths.Add(new GamePath(tempPath, Streets[1].StartPoint , Streets[21].EndPoint, false));
		
		
		//path 3
		tempPath = new List<Street>();
		tempPath.Add(Streets[1]);
		tempPath.Add(Streets[4]);
		tempPath.Add(Streets[8]);
		
		Paths.Add(new GamePath(tempPath, Streets[1].StartPoint , Streets[8].EndPoint, false));
		
		//path 4
		tempPath = new List<Street>();
		tempPath.Add(Streets[7]);
		tempPath.Add(Streets[13]);
		tempPath.Add(Streets[23]);
		
		Paths.Add(new GamePath(tempPath, Streets[7].StartPoint , Streets[23].EndPoint, false));
		
		//path 5
		tempPath = new List<Street>();
		tempPath.Add(Streets[7]);
		tempPath.Add(Streets[13]);
		tempPath.Add(Streets[18]);
		
		Paths.Add(new GamePath(tempPath, Streets[7].StartPoint , Streets[18].EndPoint, false));
		
		//path 6
		tempPath = new List<Street>();
		tempPath.Add(Streets[9]);
		tempPath.Add(Streets[13]);
		tempPath.Add(Streets[23]);
		
		Paths.Add(new GamePath(tempPath, Streets[9].StartPoint , Streets[23].EndPoint, false));
		
		//path 7
		tempPath = new List<Street>();
		tempPath.Add(Streets[9]);
		tempPath.Add(Streets[5]);
		tempPath.Add(Streets[3]);
		
		Paths.Add(new GamePath(tempPath, Streets[9].StartPoint , Streets[3].EndPoint, false));
		
		//path 8
		tempPath = new List<Street>();
		tempPath.Add(Streets[14]);
		tempPath.Add(Streets[16]);
		tempPath.Add(Streets[18]);
		
		Paths.Add(new GamePath(tempPath, Streets[14].StartPoint , Streets[18].EndPoint, false));
		
		//path 9
		tempPath = new List<Street>();
		tempPath.Add(Streets[14]);
		tempPath.Add(Streets[10]);
		tempPath.Add(Streets[0]);
		
		Paths.Add(new GamePath(tempPath, Streets[14].StartPoint , Streets[0].EndPoint, false));
		
		//path 10
		tempPath = new List<Street>();
		tempPath.Add(Streets[19]);
		tempPath.Add(Streets[17]);
		tempPath.Add(Streets[15]);
		
		Paths.Add(new GamePath(tempPath, Streets[19].StartPoint , Streets[15].EndPoint, false));
		
		//path 11
		tempPath = new List<Street>();
		tempPath.Add(Streets[19]);
		tempPath.Add(Streets[17]);
		tempPath.Add(Streets[21]);
		
		Paths.Add(new GamePath(tempPath, Streets[19].StartPoint , Streets[21].EndPoint, false));
		
		//path 12
		tempPath = new List<Street>();
		tempPath.Add(Streets[22]);
		tempPath.Add(Streets[17]);
		tempPath.Add(Streets[15]);
		
		Paths.Add(new GamePath(tempPath, Streets[22].StartPoint , Streets[15].EndPoint, false));
		
		//path 13
		tempPath = new List<Street>();
		tempPath.Add(Streets[22]);
		tempPath.Add(Streets[12]);
		tempPath.Add(Streets[6]);
		
		Paths.Add(new GamePath(tempPath, Streets[22].StartPoint , Streets[6].EndPoint, false));
		
		//path 14
		tempPath = new List<Street>();
		tempPath.Add(Streets[20]);
		tempPath.Add(Streets[10]);
		tempPath.Add(Streets[0]);
		
		Paths.Add(new GamePath(tempPath, Streets[20].StartPoint , Streets[0].EndPoint, false));
		
		//path 15
		tempPath = new List<Street>();
		tempPath.Add(Streets[20]);
		tempPath.Add(Streets[10]);
		tempPath.Add(Streets[3]);
		
		Paths.Add(new GamePath(tempPath, Streets[20].StartPoint , Streets[3].EndPoint, false));
		
		return Paths;
	}
	
	
	public static List<LightsGroup> Lights(List<LightsGroup> lightsGroups, List<Street> Streets, List<TrafficLight> Lights){
		List<Street> temp;
		
		lightsGroups = new List<LightsGroup>() ;
		
		List<TrafficLight> lightsList1 = new List<TrafficLight>();
		List<TrafficLight> lightsList2 = new List<TrafficLight>();
		List<TrafficLight> lightsList3 = new List<TrafficLight>();
		List<TrafficLight> lightsList4 = new List<TrafficLight>();	
		
		
		TrafficLight [] temp1 = new TrafficLight[4];
		TrafficLight [] temp2 = new TrafficLight[4];
		TrafficLight [] temp3 = new TrafficLight[4];
		TrafficLight [] temp4 = new TrafficLight[4];
		
		for(int i= 0; i<Lights.Count; i++){
			temp = new List<Street>();
			//group 1
			if(Lights[i].tLight.name == "lightDown4"){
				temp.Add(Streets[10]);
				Lights[i].AttachedStreets = temp;
				temp1[0] = Lights[i];
			}
			else if(Lights[i].tLight.name == "lightRight4"){
				temp.Add(Streets[2]);
				Lights[i].AttachedStreets = temp;
				temp1[1] = Lights[i];
			}
			else if(Lights[i].tLight.name == "lightUp2"){
				temp.Add(Streets[1]);
				Lights[i].AttachedStreets = temp;
				temp1[2] = Lights[i];
			}
			else if(Lights[i].tLight.name == "lightLeft2"){
				temp.Add(Streets[5]);
				Lights[i].AttachedStreets = temp;
				temp1[3] = Lights[i];
			}
			
			//group 2
			else if(Lights[i].tLight.name == "lightDown1"){
				temp.Add(Streets[20]);
				Lights[i].AttachedStreets = temp;
				temp2[0] = Lights[i];
			}
			else if(Lights[i].tLight.name == "lightRight1"){
				temp.Add(Streets[14]);
				Lights[i].AttachedStreets = temp;
				temp2[1] = Lights[i];
			}
			else if(Lights[i].tLight.name == "lightUp4"){
				temp.Add(Streets[11]);
				Lights[i].AttachedStreets = temp;
				temp2[2] = Lights[i];
			}
			else if(Lights[i].tLight.name == "lightLeft3"){
				temp.Add(Streets[17]);
				Lights[i].AttachedStreets = temp;
				temp2[3] = Lights[i];
			}
			
			
			//group 3
			else if(Lights[i].tLight.name == "lightDown3"){
				temp.Add(Streets[12]);
				Lights[i].AttachedStreets = temp;
				temp3[0] = Lights[i];
			}
			else if(Lights[i].tLight.name == "lightRight3"){
				temp.Add(Streets[4]);
				Lights[i].AttachedStreets = temp;
				temp3[1] = Lights[i];
			}
			else if(Lights[i].tLight.name == "lightUp1"){
				temp.Add(Streets[7]);
				Lights[i].AttachedStreets = temp;
				temp3[2] = Lights[i];
			}
			else if(Lights[i].tLight.name == "lightLeft4"){
				temp.Add(Streets[9]);
				Lights[i].AttachedStreets = temp;
				temp3[3] = Lights[i];
			}
			//group 4
			else if(Lights[i].tLight.name == "lightDown2"){
				temp.Add(Streets[22]);
				Lights[i].AttachedStreets = temp;
				temp4[0] = Lights[i];
			}
			else if(Lights[i].tLight.name == "lightRight2"){
				temp.Add(Streets[16]);
				Lights[i].AttachedStreets = temp;
				temp4[1] = Lights[i];
			}
			else if(Lights[i].tLight.name == "lightUp3"){
				temp.Add(Streets[13]);
				Lights[i].AttachedStreets = temp;
				temp4[2] = Lights[i];
			}
			else if(Lights[i].tLight.name == "lightLeft1"){
				temp.Add(Streets[19]);
				Lights[i].AttachedStreets = temp;
				temp4[3] = Lights[i];
			}
			
		}
		
		for(int i =0 ; i<4; i++){
			lightsList1.Add(temp1[i]);
			lightsList2.Add(temp2[i]);
			lightsList3.Add(temp3[i]);
			lightsList4.Add(temp4[i]);
		}
		
		lightsGroups.Add(new LightsGroup(lightsList1));
		lightsGroups.Add(new LightsGroup(lightsList2));
		lightsGroups.Add(new LightsGroup(lightsList3));
		lightsGroups.Add(new LightsGroup(lightsList4));
		
		return lightsGroups;
	}
	
}
