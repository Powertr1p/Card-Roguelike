using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static async void LoadScene(string sceneName)
    {
        await LoadSceneAsync(sceneName);
    }

    private static async Task LoadSceneAsync(string sceneName)
    {
        Scene previousScene = SceneManager.GetActiveScene();

        await ShowLoadingScreen();

        if (previousScene.IsValid())
        {
            await UnloadPreviousScene(previousScene.name);
        }

        await LoadNewScene(sceneName);
    }

    private static async Task ShowLoadingScreen()
    {
        AsyncOperation operation =  SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            await Task.Yield();
        }
    }

    private static async Task LoadNewScene(string scene)
    {
        AsyncOperation operation =  SceneManager.LoadSceneAsync(scene);

        operation.allowSceneActivation = false;
        
        do
        {
            await Task.Yield();
        } while (operation.progress < 0.9f);
        
        operation.allowSceneActivation = true;
    }

    private static async Task UnloadPreviousScene(string scene)
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(scene);
        
        while (!operation.isDone)
        {
            await Task.Yield();
        }
    }
}
