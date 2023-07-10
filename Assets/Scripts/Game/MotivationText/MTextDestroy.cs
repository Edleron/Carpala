namespace Game.MotivationText
{
    using UnityEngine;

    public class MTextDestroy : MonoBehaviour
    {
        [SerializeField] private int delay;
        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, delay);
        }
    }
}