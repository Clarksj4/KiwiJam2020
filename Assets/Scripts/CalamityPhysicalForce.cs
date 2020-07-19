using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalamityPhysicalForce : CalamityIncreaser
{
    public float forceFactor;
    private float force;
    private HashSet<Rigidbody> affectedBodies = new HashSet<Rigidbody>();

    private void FixedUpdate()
    {
        foreach (Rigidbody body in affectedBodies)
        {
            Vector3 direction = (body.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, body.position);
            body.AddForceAtPosition(direction * distance * force, transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
            affectedBodies.Add(other.attachedRigidbody);
    }

    public override void SetCalamity(float progress)
    {
        base.SetCalamity(progress);

        force = progress * forceFactor;
    }
}
