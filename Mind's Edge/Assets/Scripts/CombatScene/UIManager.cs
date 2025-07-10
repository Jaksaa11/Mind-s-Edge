using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private Image sanityImage;
    [SerializeField] private Image healthImage;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject PauseScreen;

    private void Awake()
    {
        Instance = this;
        gameOverScreen.SetActive(false);
        PauseScreen.SetActive(false);
        UIManager.Instance.SanityColor();
    }
    public void SanityColor()
    {
        if (GameManager.instance.playerSanity >= 70)
            sanityImage.color = Color.green;
        else if (GameManager.instance.playerSanity >= 40)
            sanityImage.color = Color.yellow;
        else if(GameManager.instance.playerSanity > 0)
            sanityImage.color = Color.red;
    }
    public void HealthColor()
    {
        if (Attack.instance.playerHealth >2)
            healthImage.color = Color.green;
        else if (Attack.instance.playerHealth >1)
            healthImage.color = Color.yellow;
       else 
            healthImage.color = Color.red;
    }

    public void PauseUnpause(InputAction.CallbackContext context)
    {
        if (context.performed && !gameOverScreen.activeInHierarchy)
        {
            PauseGame(!PauseScreen.activeInHierarchy);
        }

    }
    public void PauseGame(bool status)
    {
        PauseScreen.SetActive(status);

        if (status)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

    }
    public void Continue()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }
    public void TryAgain()
    {
        Time.timeScale = 1f;
        GameManager.instance.ResetSanity();
        GameManager.instance.wins = 0;
        gameOverScreen.SetActive(false);       
        PauseScreen.SetActive(false);
        SceneManager.LoadScene("CombatTest");
       
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("_MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
