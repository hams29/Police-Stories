using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrashbangSearch : MonoBehaviour
{
    public List<States> states { get; private set; } = new List<States>();
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "target" || other.tag == "Friend" || other.tag == "Player")
        {
            Core core = other.GetComponentInChildren<Core>();
            if (core == null) return;
            States ostates = core.GetCoreComponent<States>();
            if (ostates == null) return;

            Vector3 posDelta = other.transform.position - this.transform.position;
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            Debug.DrawRay(pos, posDelta, Color.yellow, 0.5f);
            if (Physics.Raycast(pos, posDelta, out RaycastHit hit))
            {
                if (hit.collider == other)
                {
                    if (!states.Contains(ostates))
                    {
                        states.Add(ostates);
                    }
                }
                else
                {
                    if (states.Contains(ostates))
                    {
                        states.Remove(ostates);
                    }
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Core core = other.GetComponentInChildren<Core>();
        if (core == null) return;
        States ostates = core.GetCoreComponent<States>();
        if (ostates == null) return;

        if (states.Contains(ostates))
            states.Remove(ostates);
    }
}
