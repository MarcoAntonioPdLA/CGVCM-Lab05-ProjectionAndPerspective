using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionController : MonoBehaviour {
    private void OnTriggerEnter(Collider collider) {
        string colliderTag = collider.tag;
        switch (colliderTag) {
            case "TP":
                LevelManager.LoadNextLevel();
                break;
            case "Coin":
                Destroy(collider.gameObject);
                break;
        }
    }
}
