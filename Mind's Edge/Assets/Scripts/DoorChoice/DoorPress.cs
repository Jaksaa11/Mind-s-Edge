using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPress : MonoBehaviour
{
    public void RandomRoom()
    {
        int[] brojSobe = { 2, 2, 2, 3 };
        int ind = Random.Range(0, brojSobe.Length);
        SceneManager.LoadScene(brojSobe[ind]);
    }
}
