using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class OnEnterGate : MonoBehaviour
    {
        public int PlayerId;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Puck"))
            {
                GameManager.Instance.UpdateScore(PlayerId);
            }
        }
    }
}
