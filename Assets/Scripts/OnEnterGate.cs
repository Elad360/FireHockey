using UnityEngine;

namespace Assets.Scripts
{
    public class OnEnterGate : MonoBehaviour 
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Puck"))
            {
                Debug.Log("Puck is inside!");
            }
        }
    }
}
