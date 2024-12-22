using UnityEngine;

namespace UI {
public class MainMenu : MonoBehaviour {
    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
}