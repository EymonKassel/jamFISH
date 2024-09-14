using UnityEngine;

public class DestructibleStructure : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if ( collision.gameObject.CompareTag("HugeStone") ) {
            Destroy(gameObject);
            Debug.Log("crashhh");
        }
    }
}
