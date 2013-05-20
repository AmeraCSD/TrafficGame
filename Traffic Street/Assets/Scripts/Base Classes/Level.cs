using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	
	private int _id;
	private Map _map;
	private float _normalVehicleSpeed;
	private int _instantiationRate;
	private int _minScore;
	private float _gameTime;
	private List<VehicleType> _levelEvents;
	private List<int> _eventsNumbers;
	private EventTimes _eventsTimes;
	private List<GamePath> _eventsPaths;
	
	public Level(int anId, 
				Map aMap, 
				float theNormalVehicleSpeed, 
				int theInstantiationRate, 
				int theMinScore, 
				float theGameTime, 
				List<VehicleType> theLevelEvents, 
				List<int> theEventsNumbers, 
				EventTimes theEventTimes, 
				List<GamePath> theEventsPaths){
		
		
		_id = anId;
		_map = aMap;
		_normalVehicleSpeed = theNormalVehicleSpeed;
		_instantiationRate = theInstantiationRate;
		_minScore = theMinScore;
		_gameTime = theGameTime;
		_levelEvents = theLevelEvents;
		_eventsNumbers = theEventsNumbers;
		_eventsTimes = theEventTimes;
		_eventsPaths = theEventsPaths;
	}
	
	public int ID{
		get{return _id;}
		set{_id = value;}
	}
	
	public Map LevelMap{
		get{return _map;}
		set{_map = value;}
	}
	
	public float NormalVehicleMap{
		get{return _normalVehicleSpeed;}
		set{_normalVehicleSpeed = value;}
	}
	
	public int InstatiationRate{
		get{return _instantiationRate;}
		set{_instantiationRate = value;}
	}
	
	public int MinScore{
		get{return _minScore;}
		set{_minScore = value;}
	}
	
	public float GameTime{
		get{return _gameTime;}
		set{_gameTime = value;}
	}
	
	public List<VehicleType> LevelEvents{
		get{return _levelEvents;}
		set{_levelEvents = value;}
	}
		
	public List<int> EventsNumber{
		get{return _eventsNumbers;}
		set{_eventsNumbers = value;}
	}
	
	public EventTimes EventsTimesList{
		get{return _eventsTimes;}
		set{_eventsTimes = value;}
	}
	
	public List<GamePath> EventsPaths{ /////////////////////////////////////////////should be list of list
		get{return _eventsPaths;}
		set{_eventsPaths = value;}
	}
}
