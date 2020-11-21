using UnityEngine.UI;
using UnityEngine;

public class posteTrigger : MonoBehaviour
{
    public Text result;

    void OnTriggerEnter(Collider other)
    {
        result.text = "1";
    }
}
