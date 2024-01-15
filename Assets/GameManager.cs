using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Generate, Fighting, MoveFree }
    public GameState CurrentState { get; private set; }

    private string gameOverScreen = "GameOverScreen"; // Replace with your game scene name

    private DoorManager[] allDoors;
    private RoomManager[] allRooms;

    public GameObject activeRoom;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    
    void Start()
    {
        SoundManager.Instance.PlaySoundForever("Fighting");
        SoundManager.Instance.PlaySoundForever("Exploring");
        ChangeState(GameState.Generate);
        //Time.timeScale = 1;
    }

    private void Update()
    {
        //bool interactablesleft = false;
        //foreach (var room in allRooms)
        //{
        //    if (room.hasInteractableObjects)
        //    {
        //        interactablesleft = true;
        //    }
        //}
        //if (!interactablesleft)
        //{
        //    ChangeState(GameManager.GameState.Generate);
        //}
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState != newState)
        {
            Debug.Log("Switching GameState from " + CurrentState + " to " + newState);
            if (newState == GameState.MoveFree)
            {
                SoundManager.Instance.PlaySoundOnce("DoorsOpen");
            }
        }
        CurrentState = newState;
        OnStateChanged(newState);
    }

    private void OnStateChanged(GameState newState)
    {

        switch (newState)
        {
            case GameState.Generate:
                Debug.Log("Generating Rooms");
                // Trigger level generation
                FindObjectOfType<LevelGenerator>().GenerateLevel();
                allDoors = FindObjectsOfType<DoorManager>();
                allRooms = FindObjectsOfType<RoomManager>();
                break;
            case GameState.Fighting:
                SoundManager.Instance.ChangeSoundVolumeOverTime("Fighting", 1f, 1f);
                SoundManager.Instance.ChangeSoundVolumeOverTime("Exploring", 0f, 1f);
                foreach (var door in allDoors)
                {
                    door.gameObject.SetActive(true);
                }

                break;
            case GameState.MoveFree:
                SoundManager.Instance.ChangeSoundVolumeOverTime("Fighting", 0f, 1f);
                SoundManager.Instance.ChangeSoundVolumeOverTime("Exploring", 1f, 1f);
                foreach (var door in allDoors)
                {
                    // Close doors or other logic for fighting state
                    door.HandleDoorState();
                }
                break;
        }
        
    }

    public void GameOver()
    {
        SoundManager.Instance.StopAllSounds();
        SceneManager.LoadScene(gameOverScreen);
    }

    // Other methods as needed...
}