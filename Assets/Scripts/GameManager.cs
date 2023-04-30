//--------------------------------------
using UnityEngine;
using System.Collections;
//--------------------------------------
public class GameManager : MonoBehaviour
{
	[SerializeField] private OpenDoor OpenDoorScript1;
	[SerializeField] private OpenDoor OpenDoorScript2;
	
	//--------------------------------------
	//Access to single instance
	public static GameManager Instance
	{
		get {
				if(!instance) instance = new GameManager();
				return instance;
			}
	}
	//--------------------------------------
	//Access to level complete flag
	public bool LevelCompleted
	{
		get{return bLevelCompleted;}
		set{
				bLevelCompleted = value;

				if(value)
				{
					//Disable input
					bAcceptInput=false;

					//Start win wait interval
					StartCoroutine(MoveToNextLevel());

					
				}
			}
	}
	//--------------------------------------
	//Can accept input - set to false to disable input
	public bool bAcceptInput = true;
	
	//Win wait interval
	public float WinWaitInterval = 2.0f;
	
	//Reference to level completed graphic
	// public Sprite LevelCompleteGraphic = null;

	//Reference to next level
	public int NextLevel = 0;

	//Internal reference to singleton
	private static GameManager instance = null;

	//Internal reference to texture display pos
	private Rect WinDisplayPos = new Rect();
	private Rect WinTexCoords = new Rect();

	//Internal reference to all crates in scene
	private Crate[] Crates = null;

	//Internal reference to player
	private PlayerController PC = null;

	//Flag indicating whether level is completed
	private bool bLevelCompleted = false;

	//Internal reference to DataManager
	private DataManager DM = null;

	//Get level counter
	private LevelCounter LC = null;
	//--------------------------------------
	// Use this for initialization
	void Awake () 
	{
		//If there is already an instance of this class, then remove
		if(instance) {DestroyImmediate(this);return;}

		//Assign this instance as singleton
		instance = this;

		// //Get texture coordinates
		// WinTexCoords.x = LevelCompleteGraphic.rect.x/LevelCompleteGraphic.texture.width;
		// WinTexCoords.y = LevelCompleteGraphic.rect.y/LevelCompleteGraphic.texture.height;
		// WinTexCoords.width = (LevelCompleteGraphic.rect.x + LevelCompleteGraphic.rect.width) / LevelCompleteGraphic.texture.width;
		// WinTexCoords.height = (LevelCompleteGraphic.rect.y + LevelCompleteGraphic.rect.height) / LevelCompleteGraphic.texture.height;

		//Get all crates in scene
		Crates = GameObject.FindObjectsOfType<Crate>();

		//Get Player Controller
		PC = GameObject.FindObjectOfType<PlayerController>();

		//Get level counter
		LC = GameObject.FindObjectOfType<LevelCounter>();

		//Create Data Manager
		DM = new DataManager();
	}
	//--------------------------------------
	//Update Player Preferences with current level or load last saved level
	void Start()
	{

		// //If this is first time level load, then restore game
		// if(LC.LevelLoadTimes <= 0)
		// {
		// 	//If we have saved a level previously then see if we should load
		// 	if(PlayerPrefs.HasKey("LastLevel"))
		// 	{
		// 		//Get last saved level
		// 		int LastLevel = PlayerPrefs.GetInt("LastLevel");
		//
		// 		//Load last level
		// 		if(LastLevel > Application.loadedLevel)
		// 		{
		// 			Application.LoadLevel(LastLevel);
		// 			return;
		// 		}
		// 	}
		//
		// 	//Update last saved level
		// 	PlayerPrefs.SetInt("LastLevel", Application.loadedLevel);
		//
		// 	//Restore Game if saved data exits
		// 	RestoreGame();
		// }
		//
		// //Increment level counter
		// ++LC.LevelLoadTimes;
	}
	//--------------------------------------
	//Function to restart current level
	public void RestartLevel()
	{
		//Restart current level
		Application.LoadLevel(Application.loadedLevel);
	}
	//--------------------------------------
	//Show level completed GUI graphic
	void OnGUI()
	{
		//If not completed then don't show win
		if(!bLevelCompleted)return;
	
		// GUI.DrawTextureWithTexCoords(WinDisplayPos, LevelCompleteGraphic.texture, WinTexCoords);
	}
	//--------------------------------------
	//Check scene for win condition and update
	public bool CheckForWin()
	{
		//If already won, then exit
		if(bLevelCompleted) return true;

		//Checks scene for win condition - returns true if game is won
		foreach(Crate C in Crates)
		{
			if(!C.bIsOnDestination) 
				return false; //If there is one or more crates not on destination then exit with false - no win situation
		}
	
		//If reached here, then we have a win situation
		LevelCompleted = true;
	
		OpenDoorScript1.puzzleSolved = true;
		OpenDoorScript2.puzzleSolved = true;
		
		//Level completed
		return true;
	}
	//--------------------------------------
	//Count down to next level
	public IEnumerator MoveToNextLevel()
	{
		//Wait for win interval
		yield return new WaitForSeconds(WinWaitInterval);

		// //Now load next level
		// Application.LoadLevel(NextLevel);
	}
	//--------------------------------------
	// Update is called once per frame
	void Update ()
	{
		// //Update win display pos
		// WinDisplayPos.x = Screen.width/2 - LevelCompleteGraphic.rect.width/2;
		// WinDisplayPos.y = Screen.height/2 - LevelCompleteGraphic.rect.height/2;
		// WinDisplayPos.width = LevelCompleteGraphic.rect.width;
		// WinDisplayPos.height = LevelCompleteGraphic.rect.height;

		//Check for win
		CheckForWin();
	}
	//--------------------------------------
	//Function to save game
	public void SaveGame()
	{
		//Clear Box Data
		DM.GD.BD.Clear();

		//Add Box Transforms
		foreach(Crate C in Crates)
		{
			//Create new box data structure
			DataManager.BoxData BD = new DataManager.BoxData();
			BD.BoxTransform.X = C.transform.position.x;
			BD.BoxTransform.Y = C.transform.position.y;
			BD.BoxTransform.Z = C.transform.position.z;
			BD.BoxTransform.RotX = C.transform.eulerAngles.x;
			BD.BoxTransform.RotY = C.transform.eulerAngles.y;
			BD.BoxTransform.RotZ = C.transform.eulerAngles.z;
			BD.BoxTransform.ScaleX = C.transform.localScale.x;
			BD.BoxTransform.ScaleY = C.transform.localScale.y;
			BD.BoxTransform.ScaleZ = C.transform.localScale.z;
			BD.OnDestination = C.bIsOnDestination;

			//Add Box
			DM.GD.BD.Add(BD);
		}

		//Update Player Data
		DM.GD.PD.PlayerTransform.X = PC.transform.position.x;
		DM.GD.PD.PlayerTransform.Y = PC.transform.position.y;
		DM.GD.PD.PlayerTransform.Z = PC.transform.position.z;
		DM.GD.PD.PlayerTransform.RotX = PC.transform.eulerAngles.x;
		DM.GD.PD.PlayerTransform.RotY = PC.transform.eulerAngles.y;
		DM.GD.PD.PlayerTransform.RotZ = PC.transform.eulerAngles.z;
		DM.GD.PD.PlayerTransform.ScaleX = PC.transform.localScale.x;
		DM.GD.PD.PlayerTransform.ScaleY = PC.transform.localScale.y;
		DM.GD.PD.PlayerTransform.ScaleZ = PC.transform.localScale.z;

		//Save Game Data
		DM.Save(Application.persistentDataPath + "/GameSave.xml");
	}
	//--------------------------------------
	//Function to restore game
	public void RestoreGame()
	{
		//Check if save game data exists
		if (System.IO.File.Exists(Application.persistentDataPath + "/GameSave.xml"))
		{
			//Restore game
			DM.Load(Application.persistentDataPath + "/GameSave.xml");

			//Cycle through box data
			for(int i=0; i<DM.GD.BD.Count; i++)
			{
				//Update position, rotation and scale
				Crates[i].transform.position = new Vector3(DM.GD.BD[i].BoxTransform.X, DM.GD.BD[i].BoxTransform.Y, DM.GD.BD[i].BoxTransform.Z);
				Crates[i].transform.rotation = Quaternion.Euler(DM.GD.BD[i].BoxTransform.RotX, DM.GD.BD[i].BoxTransform.RotY, DM.GD.BD[i].BoxTransform.RotZ);
				Crates[i].transform.localScale = new Vector3(DM.GD.BD[i].BoxTransform.ScaleX, DM.GD.BD[i].BoxTransform.ScaleY, DM.GD.BD[i].BoxTransform.ScaleZ);
				Crates[i].bIsOnDestination = DM.GD.BD[i].OnDestination;
			}

			//Update player transform
			PC.transform.position = new Vector3(DM.GD.PD.PlayerTransform.X, DM.GD.PD.PlayerTransform.Y, DM.GD.PD.PlayerTransform.Z);
			PC.transform.rotation = Quaternion.Euler(DM.GD.PD.PlayerTransform.RotX, DM.GD.PD.PlayerTransform.RotY, DM.GD.PD.PlayerTransform.RotZ);
			PC.transform.localScale = new Vector3(DM.GD.PD.PlayerTransform.ScaleX, DM.GD.PD.PlayerTransform.ScaleY, DM.GD.PD.PlayerTransform.ScaleZ);
		}
	}
	//--------------------------------------
	// //Save game data
	// void OnApplicationQuit()
	// {
	// 	SaveGame();
	// }
	//--------------------------------------
}