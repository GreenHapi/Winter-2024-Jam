﻿using UnityEngine;

namespace UI {
public class UIScreen : MonoBehaviour {
    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
}