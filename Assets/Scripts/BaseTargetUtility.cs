using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTargetUtility : MonoBehaviour
{
    [SerializeField] private int scoreValue = 0;
    [SerializeField] private string tagToLookFor;

    public delegate void TargetHitHandler(int scoreValue);
    public static event TargetHitHandler OnTargetHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToLookFor))
        {
            OnTargetHit?.Invoke(scoreValue);
            gameObject.SetActive(false);
        }
    }

}
