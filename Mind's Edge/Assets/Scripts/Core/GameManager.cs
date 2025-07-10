using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Sanity")]
    public int playerSanity;
    public int maxSanity = 100;

    [Header("Wins")]
    public int wins;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        wins = 0;
    }

    public void ResetSanity()
    {
        playerSanity = maxSanity;
    }

}
