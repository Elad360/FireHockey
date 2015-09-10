using UnityEngine;

namespace Assets.Scripts
{
    public class OnEnterGate : MonoBehaviour 
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.LogError("Entered the gate");
        }
    }
}
