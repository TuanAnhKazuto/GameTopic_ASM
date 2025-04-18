using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Ring"))
        {
            Destroy(other.gameObject);
            Debug.Log("Vai o");
        }
    }
}
