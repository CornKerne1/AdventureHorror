using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    // Start is called before the first frame update
     public void Restart()
    {
        Debug.Log("papa");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
