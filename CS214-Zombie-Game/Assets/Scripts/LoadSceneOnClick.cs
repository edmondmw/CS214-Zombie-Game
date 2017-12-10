using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}
    public void LoadScene(string name)
    {
        StartCoroutine(LoadAsynchronously(name));
    }
    IEnumerator LoadAsynchronously (string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while(!operation.isDone)
        {
            Debug.Log(operation.progress);
            yield return null;
        }
    }
}
