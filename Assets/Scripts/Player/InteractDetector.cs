using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDetector : MonoBehaviour
{
    public static InteractZone CurrentZone { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out InteractZone zone))
        {
            CurrentZone = zone;
            Debug.Log(CurrentZone.name);
            zone.OnPlayerEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out InteractZone zone) && zone == CurrentZone)
        {
            zone.OnPlayerExit();
            CurrentZone = null;
        }
    }
}