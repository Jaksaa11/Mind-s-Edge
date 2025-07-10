using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        GameManager.instance.ResetSanity();
        SceneManager.LoadScene("CombatTest");       
    }
    public void Quit()
    {
        Application.Quit();
    }
}
