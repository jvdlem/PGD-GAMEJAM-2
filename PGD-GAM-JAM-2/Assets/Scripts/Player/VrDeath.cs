using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VrDeath : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToggleDeathScreen()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
