using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SanityRecovery : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentSanity;
    private void Awake()
    {
        if (GameManager.instance.playerSanity + 10 <= GameManager.instance.maxSanity)
        {
            GameManager.instance.playerSanity += 10;
        }
        else
        {
            GameManager.instance.playerSanity = GameManager.instance.maxSanity;
        }

            currentSanity.text = "Current sanity: " + GameManager.instance.playerSanity;

    }

    public void NextFight()
    {
        SceneManager.LoadScene("CombatTest");
    }
}
