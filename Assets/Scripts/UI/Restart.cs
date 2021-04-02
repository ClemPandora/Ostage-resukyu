using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
   public void RestartScene()
   {
      Time.timeScale = 1;
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   public void Menu()
   {
      SceneManager.LoadScene(0);
   }

   public void Play()
   {
      SceneManager.LoadScene(1);
   }

   public void QuitGame()
   {
      Application.Quit();
   }
}
