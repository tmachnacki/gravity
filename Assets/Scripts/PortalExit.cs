using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalExit : MonoBehaviour
{
    public int level;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        
        string nextlevel = "Level " + level;
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextlevel, LoadSceneMode.Single);
        }
    }
}
