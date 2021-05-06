using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScene.GameMaster.UI
{
	public class MainMenu : MonoBehaviour
	{
		private string levelToLoad = "MainLevel";
		public static string mainMenu = "MainMenu";

		public void Play()
		{
			SceneManager.LoadScene(levelToLoad);
		}		
		public void Quit()
		{
			Application.Quit();
		}
	}
}

