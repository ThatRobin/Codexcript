using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public bool useCollision;

    public static void loadScene(string scene) {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    private void OnTriggerEnter(Collider other) {
        if(useCollision) {
            SceneManager.LoadScene("Battle", LoadSceneMode.Single);
        }
    }

}
