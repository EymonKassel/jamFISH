using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubble : MonoBehaviour {
    [SerializeField]
    private float _amountOfBonusAir = 5f;

    private RadioactivePoisoning _radioactivePoisoning;
    private void Awake() {
        _radioactivePoisoning = GameObject.Find("Player").GetComponent<RadioactivePoisoning>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if ( collision.gameObject.CompareTag("Player") ) {
            _radioactivePoisoning.TimeRemaining += _amountOfBonusAir;
            Destroy(gameObject);
        }
    }
}
