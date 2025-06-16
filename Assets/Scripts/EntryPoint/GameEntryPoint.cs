using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Zenject;

namespace Cifkor.Karpusha
{
    public class GameEntryPoint
    {
        public static GameEntryPoint BootstrapEntryPoint;
        private CorutinesInScene _corutinesInScene;

        private GameEntryPoint()
        {
            _corutinesInScene = new GameObject("[Coroutines]").AddComponent<CorutinesInScene>();
            Object.DontDestroyOnLoad(_corutinesInScene.gameObject);
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void AutoStartGame()
        {
            Application.targetFrameRate = 60;

            BootstrapEntryPoint = new GameEntryPoint();
            BootstrapEntryPoint.StartGame();
        }

        private void StartGame()
        {
            _corutinesInScene.StartCoroutine(LoadAndStartGame());
        }

        private IEnumerator LoadScene(string sceneName)
        {
            AsyncOperation LoadAsync = SceneManager.LoadSceneAsync(sceneName);

            while (!LoadAsync.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return LoadAsync;
        }

        private IEnumerator LoadAndStartGame()
        {
            yield return LoadScene("Bootstrap");
            yield return new WaitForEndOfFrame();
            yield return LoadScene("Application");
        }
    }
}