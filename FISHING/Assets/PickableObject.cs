using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour {
    private HoldingObjects _holdingObjects;

    private void Awake() {
        _holdingObjects = GameObject.Find("Player").GetComponent<HoldingObjects>();
    }
    
}
