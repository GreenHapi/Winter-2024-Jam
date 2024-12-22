using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers {
public class GameManager : MonoBehaviour {
    [SerializeField] private int _newGameSceneIndex = 1;

    public void NewGame() {
        SceneManager.LoadScene(_newGameSceneIndex);
    }

    public static void Quit() {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
}