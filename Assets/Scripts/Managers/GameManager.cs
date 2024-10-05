using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(GameSceneManager))]
public class GameManager : MonoBehaviour
{
  // CAMERAS
  private CinemachineVirtualCamera vcam;

  // CHARACTERS
  [SerializeField] private GameObject playerPrefab;
  [NonSerialized] public Player.Player player;

  // CONTROLLERS
  private MainAudioController audioController;
  private GameSceneManager gameSceneManager;

  // BACKGROUND MUSICS
  [SerializeField] private AudioClip menuBGM;
  [SerializeField] private AudioClip ingameBGM;

  bool gamePaused = false;
  bool gameStarted = false;

  public void Awake()
  {
    Application.targetFrameRate = 60;

    gameSceneManager = GetComponent<GameSceneManager>();
    audioController = GetComponent<MainAudioController>();
  }

  public void Start()
  {
    GoToMainMenu();
    // SetupForLevel();
  }

  public void GoToMainMenu()
  {
    gameSceneManager.LoadMainMenu();
    audioController.PlayBGM(menuBGM);
  }

  public void GoToGameScene(string sceneName)
  {
    if (!gameStarted)
      gameStarted = true;

    gameSceneManager.LoadGameScene(sceneName, SetupForLevel);
    audioController.PlayBGM(ingameBGM);
  }

  private void SetupForLevel()
  {
    GameObject spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
    player = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation).GetComponent<Player.Player>();
    vcam = FindFirstObjectByType<CinemachineVirtualCamera>();
    vcam.Follow = player.transform;
    vcam.LookAt = player.transform;
    // player.state.deadState.OnEnter += HandlePlayerDeath;
  }


  void HandlePlayerDeath(Player.Player player)
  {
    // player.state.deadState.OnEnter -= HandlePlayerDeath;
    // Destroy(player.gameObject);
    // gameSceneManager.ReloadCurrentScene(SetupForLevel);
  }

  public void PauseResumeGame()
  {
    if (!gamePaused)
    {
      Time.timeScale = 0;
      player.input.controlsEnabled = false;
      gameSceneManager.LoadPauseScene();
    }
    else
    {
      gameSceneManager.UnloadPauseScene((AsyncOperation _) =>
      {
        Time.timeScale = 1;
        player.input.controlsEnabled = true;
      });
    }
    gamePaused = !gamePaused;
  }

  private void Update()
  {
    if (player && player.input.actions["Pause"].WasPressedThisFrame())
      PauseResumeGame();
  }
}
