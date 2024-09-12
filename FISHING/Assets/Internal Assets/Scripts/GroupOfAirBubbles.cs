using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupOfAirBubbles : MonoBehaviour {
    private RadioactivePoisoning _radioactivePoisoning;
    private void Awake() {
        _radioactivePoisoning = GameObject.Find("Player").GetComponent<RadioactivePoisoning>();
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if ( collision.gameObject.CompareTag("Player") ) {
            _radioactivePoisoning.enabled = true;
        }
    }
}
