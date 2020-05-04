using UnityEngine;
using UnityEngine.SceneManagement;
namespace GameScene.GameMaster
{
	public class MainMenu : MonoBehaviour
	{
		public string levelToLoad = "MainLevel";
		public static string mainMenu = "MainMenu";
		public void Play()
		{
			SceneManager.LoadScene(levelToLoad);
		}
		
		public void Quit()
		{
			//add exit choice here
			print("szfdgfyuio");
			Application.Quit();
		}
	}
}
