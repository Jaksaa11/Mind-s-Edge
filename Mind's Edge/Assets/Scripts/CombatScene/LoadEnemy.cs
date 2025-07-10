using UnityEngine;

public class LoadEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyArray;

    private void Awake()
    {
        for (int i=0; i < enemyArray.Length; i++)
        {
            enemyArray[i].gameObject.SetActive(false);
        }
        int ind = Random.Range(0, enemyArray.Length);
        enemyArray[ind].gameObject.SetActive(true);
        
    }
}
