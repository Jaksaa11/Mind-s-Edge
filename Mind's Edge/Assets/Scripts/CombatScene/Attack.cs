using System.CodeDom.Compiler;
using System.Collections;
using System.Xml.Xsl;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public static Attack instance { get; private set; }

    [Header("Player Components")]
    [SerializeField] private Button rockButton;
    [SerializeField] private Button paperButton;
    [SerializeField] private Button scissorsButton;
    [SerializeField] private TextMeshProUGUI healthNumber;
    [SerializeField] private TextMeshProUGUI sanityNumber;

    [Header("Enemy Components")]
    [SerializeField] private GameObject EnemyUI;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int sanityDamage;
    private GameObject monster;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI feedback;
    [SerializeField] private TextMeshProUGUI tellText;
    [SerializeField] private TextMeshProUGUI wins;

    [Header("UI")]
    [SerializeField] private GameObject gameOverScreen; 
    [SerializeField] private GameObject PauseScreen;

    [Header("SFX")]
    [SerializeField] private AudioClip rockSound;
    [SerializeField] private AudioClip paperSound;
    [SerializeField] private AudioClip scissorsSound;

    private string[] rpsChoices = { "Rock", "Paper", "Scissors" };
    private string enemyChoice;

    public int playerHealth {  get; private set; }
    private int maxHealth = 3;
    private int enemyHealth;

 
    private void Awake()
    {
        instance = this;
        rockButton.onClick.AddListener(() => PlayerChoose("Rock"));
        rockButton.onClick.AddListener(() => SoundManager.instance.PlaySound(rockSound));
        paperButton.onClick.AddListener(() => PlayerChoose("Paper"));
        paperButton.onClick.AddListener(() => SoundManager.instance.PlaySound(paperSound));
        scissorsButton.onClick.AddListener(() => PlayerChoose("Scissors"));
        scissorsButton.onClick.AddListener(() => SoundManager.instance.PlaySound(scissorsSound));
        

       
    }

    private void Start()
    {
        
        playerHealth = maxHealth;
        enemyHealth = maxHealth;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].activeInHierarchy)
                monster = enemies[i];
        }
        NewEnemyTurn();
        sanityNumber.text = "SP:" + GameManager.instance.playerSanity;
    }

    private void PlayerChoose(string playerChoice)
    {
              
        rockButton.interactable = false;
        paperButton.interactable = false;
        scissorsButton.interactable = false;
        
        Invoke("NewEnemyTurn", 1.5f);

        string result= DetermineOutcome(playerChoice,enemyChoice);
        feedback.text =$"You chose {playerChoice}, enemy chose {enemyChoice}. {result}";
        Damaging(result);
        healthNumber.text = "HP:" + playerHealth;
        sanityNumber.text = "SP:" + GameManager.instance.playerSanity;
        print("PlayerChoose odradjeno");
        print(Time.timeScale.ToString());
    }
    private void PlayerDead()
    {
        tellText.text = "";
        feedback.text = "";
        wins.text = "Monsters defeated: " + GameManager.instance.wins.ToString();
        rockButton.interactable = false;
        paperButton.interactable = false;
        scissorsButton.interactable = false;       
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        print("PlayerDead odradjeno");
    }
    private void PlayerLost()
    {
        if (playerHealth <= 0)
        {
            
            GameManager.instance.playerSanity -= sanityDamage;
            playerHealth = maxHealth;
            if(GameManager.instance.playerSanity >0)
            {
                SceneManager.LoadScene("DoorChoice");
            }
            

        }
    }
    private void EnemyLost()
    {
        if (enemyHealth <= 0)
        {
            GameManager.instance.wins += 1;
            EnemyUI.gameObject.SetActive(false);
            if (GameManager.instance.playerSanity + 10 <= GameManager.instance.maxSanity)
            {
                GameManager.instance.playerSanity += 10;
            }
            else
            {
                GameManager.instance.playerSanity = GameManager.instance.maxSanity;
            }
            enemyHealth = maxHealth;           
            StartCoroutine(WaitAndLoad(2f));
        }
    }

    private IEnumerator WaitAndLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("DoorChoice");
    }

    private void Damaging(string result)
    {
        if (result == "You Lose.")
        {
            playerHealth -= 1;
        }
        else if (result == "You Win.")
        {
            enemyHealth -= 1;
        }
      PlayerLost();
      EnemyLost();
        UIManager.Instance.HealthColor();
        UIManager.Instance.SanityColor();
        

    }

    private string DetermineOutcome(string player,string enemy)
    {
        if (player == enemyChoice) return "Draw.";
        if ((player == "Rock" && enemy == "Scissors") || (player == "Paper" && enemy == "Rock") || (player == "Scissors" && enemy == "Paper"))
            return "You Win.";
        else return "You Lose.";
               
    }
    
    private void NewEnemyTurn()
    {
        print("NewEnemyTurn Usao");
        if (GameManager.instance.playerSanity <=0)
        {
           PlayerDead();
        }
        else
        {
            if (monster != null)
            {
                string scriptName = monster.name;
                System.Type scriptType = System.Type.GetType(scriptName);

                if (scriptType != null)
                {
                    
                    Component scriptComponent = monster.GetComponent(scriptType);
                    if (scriptComponent != null)
                    {
                        var move = scriptType.GetMethod("EnemyMove");
                        var tell = scriptType.GetMethod("EnemyTell");
                      
                        enemyChoice=move.Invoke(scriptComponent,null).ToString();

                        object[] parameters = { enemyChoice };
                        var tellResult = tell.Invoke(scriptComponent, parameters);
                        

                        feedback.text = "";


                    }
                }
            }
            rockButton.interactable = true;
            paperButton.interactable = true;
            scissorsButton.interactable = true;
            print("NewEnemyTurn izasao");
        }
    }
}
