using System;
using UnityEngine;
using WinterJam.Managers;

namespace UI {
public class InWorldPlayerControls : MonoBehaviour {
    [SerializeField] private GameObject _ui;

    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private Vector3 _offset;


    private void Start() {
        _ui = this.gameObject;
    }

    private void LateUpdate() {
        _ui.transform.position = _playerManager.SelectedCharacter.transform.position + _offset;
    }
}
}