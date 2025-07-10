using System.Runtime.CompilerServices;
using UnityEngine;

public class Agressor : MonoBehaviour
{
    public static Agressor Instance { get; private set; }
    private string[] rpsChoice = { "Rock", "Paper", "Scissors", "Rock" };
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip noTell;

    private void Awake()
    {
        Instance = this;
    }
    private int TellOrNot()
    {
        int YesNo=0;
        if (GameManager.instance.playerSanity >= 70)
        {
            int[] Tell = { 1, 1, 0 };
            int ind = Random.Range(0, Tell.Length);
            YesNo = Tell[ind];
        }
        else if (GameManager.instance.playerSanity >= 40)
        {
            int[] Tell = { 1, 0, 0, 1 };
            int ind = Random.Range(0, Tell.Length);
            YesNo = Tell[ind];
        }
        else if (GameManager.instance.playerSanity > 0)
        {
            int[] Tell = { 1, 0, 0 };
            int ind = Random.Range(0, Tell.Length);
            YesNo = Tell[ind];
        }
            
        
        return YesNo;
    }
    public string EnemyMove()
    {
        int ind = Random.Range(0, rpsChoice.Length);
        return rpsChoice[ind];
    }
    public void EnemyTell(string move)
    {
        int tell = TellOrNot();
        if (tell == 1)
        {
            switch (move)
            {
                case "Rock":
                    anim.SetTrigger("Rock");
                    break;
                case "Paper":
                    anim.SetTrigger("Paper");
                    break;
                case "Scissors":
                    anim.SetTrigger("Scissors");
                    break;
            }
        }
        else SoundManager.instance.PlaySound(noTell); 
    }
} 
