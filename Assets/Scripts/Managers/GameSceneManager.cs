using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameManager))]
public class GameSceneManager : MonoBehaviour
{
  private bool IsLoading = false;

  private Action<AsyncOperation> buildSceneCallback(Action callback)
    => (AsyncOperation asyncOp) => callback();

  public void LoadMainMenu(Action callback = null)
  {
    if (IsLoading) return;
    IsLoading = true;
    StartCoroutine(LoadMainMenuCoroutine(callback));
  }

  public void LoadGameScene(string sceneName, Action callback = null)
  {
    if (IsLoading) return;
    IsLoading = true;
    StartCoroutine(LoadGameSceneCoroutine(sceneName, callback));
  }

  public void ReloadCurrentScene(Action callback = null)
  {
    if (IsLoading) return;
    IsLoading = true;
    StartCoroutine(ReloadCurrentSceneCoroutine(callback));
  }

  public void LoadPauseScene(Action<AsyncOperation> callback = null)
    => SceneManager.LoadSceneAsync("IngameMenu", LoadSceneMode.Additive).completed += callback;

  public void UnloadPauseScene(Action<AsyncOperation> callback = null)
    => SceneManager.UnloadSceneAsync("IngameMenu").completed += callback;

  public void UnloadIngameHUD(Action<AsyncOperation> callback = null)
    => SceneManager.UnloadSceneAsync("IngameHUD").completed += callback;


  private IEnumerator LoadMainMenuCoroutine(Action callback = null)
  {
    AsyncOperation loadNewSceneOp = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    while (!loadNewSceneOp.isDone)
      yield return null;

    SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
    IsLoading = false;
    callback?.Invoke();
  }

  private IEnumerator LoadGameSceneCoroutine(string sceneName, Action callback = null)
  {
    string activeScene = SceneManager.GetActiveScene().name;
    AsyncOperation unloadPreviousSceneOp = null;

    if (activeScene != "ManagerScene")
      unloadPreviousSceneOp = SceneManager.UnloadSceneAsync(activeScene);
    AsyncOperation loadNewSceneOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

    if (!SceneManager.GetSceneByName("IngameHUD").isLoaded)
      SceneManager.LoadSceneAsync("IngameHUD", LoadSceneMode.Additive);

    if (unloadPreviousSceneOp != null)
      while (!loadNewSceneOp.isDone)
        yield return null;
    while (!loadNewSceneOp.isDone)
      yield return null;

    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    IsLoading = false;
    callback?.Invoke();
  }

  private IEnumerator ReloadCurrentSceneCoroutine(Action callback = null)
  {
    string currentSceneName = SceneManager.GetActiveScene().name;
    AsyncOperation unloadPreviousSceneOp = SceneManager.UnloadSceneAsync(currentSceneName);
    AsyncOperation loadNewSceneOp = SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);

    while (!unloadPreviousSceneOp.isDone || !loadNewSceneOp.isDone)
      yield return null;
    Debug.Log(unloadPreviousSceneOp.isDone + " - " + loadNewSceneOp.isDone);
    SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentSceneName));
    IsLoading = false;
    callback?.Invoke();
  }
}
