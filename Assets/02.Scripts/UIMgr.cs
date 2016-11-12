using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[ExecuteInEditMode]
public class UIMgr : MonoBehaviour {

//    void OnGUI() {
//        if(GUI.Button(new Rect(10, 10, 200, 50), "START GAME")) {
//            SceneManager.LoadScene("Level01");
//            SceneManager.LoadScene("Play", LoadSceneMode.Additive);
//        }
//    }

    public void OnClickStarButton() {
        SceneManager.LoadScene("Level01");
        SceneManager.LoadScene("Play", LoadSceneMode.Additive);
    }
}
