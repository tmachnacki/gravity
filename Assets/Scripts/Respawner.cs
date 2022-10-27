using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawner : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString(), LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString(), LoadSceneMode.Single);
        }
    }

}
