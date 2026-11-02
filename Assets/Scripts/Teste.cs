using UnityEngine;

public class Teste : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();

        if (box == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            box.bounds.center,
            box.bounds.size
        );
    }
}
