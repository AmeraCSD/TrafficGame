using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map1_Data : MonoBehaviour {
	
	//******************************************************************Vehicles Data***********************************************************//
	
	//public Globals globals = Globals.GetInstance();
	
	public static float NormalVehicleSpeed = 25;
	public static int MinScore = 200;
	public static float GameTime = 300;
	
	
	public static List<int> VehiclesGroups(){
		List<int> groups = new List<int>();
		
		
		//25
		//groups.Add(1);
		//groups.Add(2);
		//groups.Add(3);
		//groups.Add(8);
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
	
	public static int originalThiefStreetCapacity = 6;

	
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
		eventsNumbers.Add(3);		//bus
		eventsNumbers.Add(1);		//caravan
		eventsNumbers.Add(1);		//service car
		eventsNumbers.Add(3);		//taxi
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
		
		
		busTimeslist.Add(290);
		busTimeslist.Add(240);
		busTimeslist.Add(230);
		
		eventTimes.Add(new EventTimes(busTimeslist));
		
		
		//caravan
		List <float> caravanTimeslist = new List<float>();
		caravanTimeslist.Add(250);
		eventTimes.Add(new EventTimes(caravanTimeslist));
		
		//service car
		List <float> serviceCarTimeslist = new List<float>();
		serviceCarTimeslist.Add(270);
		eventTimes.Add(new EventTimes(serviceCarTimeslist));
		
		//taxi
		List <float> taxiTimeslist = new List<float>();
		taxiTimeslist.Add(285);
		taxiTimeslist.Add(213);
		taxiTimeslist.Add(184);
		eventTimes.Add(new EventTimes(taxiTimeslist));
		
		//thief
		List <float> thiefTimeslist = new List<float>();
		thiefTimeslist.Add(280);
		eventTimes.Add(new EventTimes(thiefTimeslist));
		
		//police
		List <float> policeTimeslist = new List<float>();
		policeTimeslist.Add(thiefTimeslist[0]-2);
		eventTimes.Add(new EventTimes(policeTimeslist));
		
		return eventTimes;
	}
	
	public static List <List<GamePath>> EventsPaths(List<GamePath> Paths){
	
		List <List<GamePath>> eventsPaths = new List<List<GamePath>>();
		
		
		//ambulance
		
		List <GamePath> ambulanceGamePathsList = new List<GamePath>();
		ambulanceGamePathsList.Add(Paths[5]);
		ambulanceGamePathsList.Add(Paths[4]);
		eventsPaths.Add(ambulanceGamePathsList);
		
		//Bus
		
		List <GamePath> busGamePathsList = new List<GamePath>();
		busGamePathsList.Add(Paths[0]);
		busGamePathsList.Add(Paths[2]);
		busGamePathsList.Add(Paths[0]);
		
		eventsPaths.Add(busGamePathsList);
		
		//Caravan
		
		List <GamePath> caravanGamePathsList = new List<GamePath>();
		caravanGamePathsList.Add(Paths[0]);
		//caravanGamePathsList.Add(Paths[3]);
		eventsPaths.Add(caravanGamePathsList);
		
		//service car
		
		List <GamePath> servicCarGamePathsList = new List<GamePath>();
		servicCarGamePathsList.Add(Paths[2]);
		eventsPaths.Add(servicCarGamePathsList);
		
		
		//taxi
		
		List <GamePath> taxiGamePathsList = new List<GamePath>();
		taxiGamePathsList.Add(Paths[12]);
		taxiGamePathsList.Add(Paths[12]);
		taxiGamePathsList.Add(Paths[2]);
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
	
	public static string streetsString = "0%(-115.0, 5.0, -35.0)%(25.0, 5.0, -35.0)%5%Right%lightRight%true%8\n" +
		"1%(25.0, 5.0, -75.0)%(25.0, 5.0, -35.0)%-50%Up%lightUp%true%1\n" +
		"2%(25.0, 5.0, -35.0)%(115.0, 5.0, -35.0)%0%Right%none%false%8\n" +
		"3%(-115.0, 5.0, -25.0)%(35.0, 5.0, -25.0)%5%Right%lightRight%true%8\n" +
		"4%(35.0, 5.0, -25.0)%(75.0, 5.0, -25.0)%0%Right%none%false%2\n" +
		"5%(75.0, 5.0, -25.0)%(115.0, 5.0, -25.0)%0%Right%none%false%2\n" +
		"6%(25.0, 5.0, -15.0)%(-105.0, 5.0, -15.0)%0%Left%none%false%8\n" +
		"7%(75.0, 5.0, -15.0)%(25.0, 5.0, -15.0)%50%Left%lightLeft1%true%2\n" +
		"8%(115.0, 5.0, -15.0)%(75.0, 5.0, -15.0)%100%Left%lightLeft%true%1\n" +
		"9%(25.0, 5.0, -5.0)%(-115.0, 5.0, -5.0)%0%Left%none%false%8\n" +
		"10%(85.0, 5.0, -5.0)%(25.0, 5.0, -5.0)%50%Left%lightLeft1%true%2\n" +
		"11%(115.0, 5.0, -5.0)%(85.0, 5.0, -5.0)%100%Left%lightLeft%true%1\n" +
		"12%(25.0, 5.0, 15.0)%(25.0, 5.0, -5.0)%0%Down%none%false%1\n" +
		"13%(35.0, 5.0, 35.0)%(35.0, 5.0, -25.0)%0%Down%none%false%3\n" +
		"14%(75.0, 5.0, -15.0)%(75.0, 5.0, 35.0)%0%Up%none%false%3\n" +
		"15%(85.0, 5.0, -5.0)%(85.0, 5.0, 45.0)%0%Up%none%false%4\n" +
		"16%(-105.0, 5.0, 15.0)%(-55.0, 5.0, 15.0)%-70%Right%lightRight1%true%2\n" +
		"17%(-55.0, 5.0, 15.0)%(25.0, 5.0, 15.0)%0%Right%none%false%7\n" +
		"18%(-105.0, 5.0, 25.0)%(-45.0, 5.0, 25.0)%-65%Right%lightRight1%true%2\n" +
		"19%(25.0, 5.0, 45.0)%(25.0, 5.0, 15.0)%0%Down%none%false%1\n" +
		"20%(-55.0, 5.0, 35.0)%(-55.0, 5.0, 15.0)%0%Down%none%false%1\n" +
		"21%(-45.0, 5.0, 25.0)%(-45.0, 5.0, 35.0)%0%Up%none%false%1\n" +
		"22%(-55.0, 5.0, 35.0)%(-115.0, 5.0, 35.0)%0%Left%none%false%5\n" +
		"23%(35.0, 5.0, 35.0)%(-55.0, 5.0, 35.0)%0%Left%none%false%6\n" +
		"24%(75.0, 5.0, 35.0)%(35.0, 5.0, 35.0)%45%Left%lightLeft2%true%2\n" +
		"25%(-45.0, 5.0, 45.0)%(-115.0, 5.0, 45.0)%0%Left%none%false%5\n" +
		"26%(25.0, 5.0, 45.0)%(-45.0, 5.0, 45.0)%0%Left%none%false%6\n" +
		"27%(85.0, 5.0, 45.0)%(25.0, 5.0, 45.0)%45%Left%lightLeft2%true%4\n" +
		"28%(-55.0, 5.0, 85.0)%(-55.0, 5.0, 35.0)%55%Down%lightDown1%true%2\n" +
		"29%(-45.0, 5.0, 45.0)%(-45.0, 5.0, 85.0)%0%Up%none%false%2\n" +
		"30%(25.0, 5.0, 85.0)%(25.0, 5.0, 45.0)%55%Down%lightDown%true%2\n" +
		"31%(35.0, 5.0, 85.0)%(35.0, 5.0, 35.0)%55%Down%lightDown%true%2\n" +
		"32%(75.0, 5.0, -25.0)%(75.0, 5.0, -15.0)%0%Up%none%false%1\n" +
		"33%(-45.0, 5.0, 35.0)%(-45.0, 5.0, 45.0)%0%Up%none%false%1\n"+
		"34%(25.0, 5.0, -35.0)%(25.0, 5.0, -15.0)%0%Up%none%false%2";
	
	
	public static List<Vector3> Intersections(){
		List<Vector3> intersectionsList = new List<Vector3>();
		
		//here weeee goooooooooooo
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
		
		
		for(int i= 0; i<Lights.Count; i++){
			temp = new List<Street>();
			
			if(Lights[i].tLight.name == "lightLeft"){
				temp.Add(Streets[11]);
				temp.Add(Streets[8]);
				Lights[i].AttachedStreets = temp;
				lightsList1.Add(Lights[i]);
			}
			else if(Lights[i].tLight.name == "lightLeft1"){
				temp.Add(Streets[10]);
				temp.Add(Streets[7]);
				Lights[i].AttachedStreets = temp;
				lightsList1.Add(Lights[i]);
			}
			else if(Lights[i].tLight.name == "lightUp"){
				temp.Add(Streets[1]);
				Lights[i].AttachedStreets = temp;
				lightsList2.Add(Lights[i]);
			}
			else if(Lights[i].tLight.name == "lightRight"){
				temp.Add(Streets[0]);
				temp.Add(Streets[3]);
				Lights[i].AttachedStreets = temp;
				lightsList2.Add(Lights[i]);
			}
			else if(Lights[i].tLight.name == "lightRight1"){
				temp.Add(Streets[16]);
				temp.Add(Streets[18]);
				Lights[i].AttachedStreets = temp;
				lightsList4.Add(Lights[i]);
			}
			else if(Lights[i].tLight.name == "lightDown1"){
				temp.Add(Streets[28]);
				Lights[i].AttachedStreets = temp;
				lightsList4.Add(Lights[i]);
			}
			else if(Lights[i].tLight.name == "lightDown"){
				temp.Add(Streets[30]);
				temp.Add(Streets[31]);
				Lights[i].AttachedStreets = temp;
				lightsList3.Add(Lights[i]);
			}
			else if(Lights[i].tLight.name == "lightLeft2"){
				temp.Add(Streets[24]);
				temp.Add(Streets[27]);
				Lights[i].AttachedStreets = temp;
				lightsList3.Add(Lights[i]);
			}
			
		}
		lightsGroups.Add(new LightsGroup(lightsList1));
		lightsGroups.Add(new LightsGroup(lightsList2));
		lightsGroups.Add(new LightsGroup(lightsList3));
		lightsGroups.Add(new LightsGroup(lightsList4));
		
		return lightsGroups;
	}
}
