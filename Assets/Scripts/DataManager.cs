//Loads and Saves game state data to and from xml file
//-----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization; 
using System.IO;
//-----------------------------------------------
public class DataManager
{
	//Save game data
	[XmlRoot("GameData")]
	//-----------------------------------------------
	//Transform data for object in scene
	public struct DataTransform
	{
		public float X;
		public float Y;
		public float Z;
		public float RotX;
		public float RotY;
		public float RotZ;
		public float ScaleX;
		public float ScaleY;
		public float ScaleZ;
	}
	//-----------------------------------------------
	//Public class for box
	public class BoxData
	{
		//Transform data for box
		public DataTransform BoxTransform;

		//Is box on destination
		public bool OnDestination = false;
	}
	//-----------------------------------------------
	//Public class for Player
	public class PlayerData
	{
		//Transform for player
		public DataTransform PlayerTransform;
	}
	//-----------------------------------------------
	//Class for holding root data
	public class GameData
	{
		//Box Data
		public List<BoxData> BD = new List<BoxData>();

		//Main gamer data
		public PlayerData PD = new PlayerData();
	}
	//-----------------------------------------------

	//Game Data Member
	public GameData GD = new GameData();
	//-----------------------------------------------
	//Saves game data to XML file
	public void Save(string FileName = "GameData.xml")
	{
		//Now save game data
		XmlSerializer Serializer = new XmlSerializer(typeof(GameData));
		FileStream Stream = new FileStream(FileName, FileMode.Create);
		Serializer.Serialize(Stream, GD);
		Stream.Close();
	}
	//-----------------------------------------------
	//Load game data from XML file
	public void Load(string FileName = "GameData.xml")
	{
		XmlSerializer Serializer = new XmlSerializer(typeof(GameData));
		FileStream Stream = new FileStream(FileName, FileMode.Open);
		GD = Serializer.Deserialize(Stream) as GameData;
		Stream.Close();
	}
	//-----------------------------------------------
}
//-----------------------------------------------