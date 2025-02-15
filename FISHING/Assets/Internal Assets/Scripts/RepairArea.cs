using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RepairArea : MonoBehaviour {
    [SerializeField]
    private int _maxAmountOfQuestItems = 3;
    [SerializeField]
    private GameObject _heap;
    private int _currentAmountOfQuestItems = 0;

    private void Update() {
        if (_currentAmountOfQuestItems >= _maxAmountOfQuestItems ) {
            Debug.Log("end");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("QuestItem")) {
            Destroy(collision.gameObject);
            _currentAmountOfQuestItems++;
            if ( _currentAmountOfQuestItems == 1) {
                Instantiate(_heap, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
}
