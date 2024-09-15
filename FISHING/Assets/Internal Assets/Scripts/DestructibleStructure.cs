using UnityEngine;

public class DestructibleStructure : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if ( collision.gameObject.CompareTag("HugeStone") ) {
            Destroy(gameObject);
            Debug.Log("crashhh");
        }
    }
}
