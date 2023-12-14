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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    void Start()
    {
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
        }
        CurrentState = newState;
        OnStateChanged(newState);
    }

    private void OnStateChanged(GameState newState)
    {

        switch (newState)
        {
            case GameState.Generate:
                // Trigger level generation
                FindObjectOfType<LevelGenerator>().GenerateLevel();
                allDoors = FindObjectsOfType<DoorManager>();
                allRooms = FindObjectsOfType<RoomManager>();
                break;
            case GameState.Fighting:
                foreach (var door in allDoors)
                {
                    door.gameObject.SetActive(true);
                }

                break;
            case GameState.MoveFree:
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
        SceneManager.LoadScene(gameOverScreen);
    }

    // Other methods as needed...
}