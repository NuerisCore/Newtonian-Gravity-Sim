using UnityEngine;
using System.Collections.Generic;

public class NetwonAgent : MonoBehaviour
{
    public float mass = 10f;
    public Vector3 velocity;
    public const float G = 6.67f; // Gravitational constant

    public static List<NetwonAgent> Actors = new List<NetwonAgent>();

    private void OnEnable()
    {
        (Actors ??= new List<NetwonAgent>()).Add(this);
    }

    private void OnDisable()
    {
        Actors?.Remove(this);
    }

    private void FixedUpdate()
    {
        foreach (var other in Actors)
        {
            if (this == other) continue;

            Vector3 direction = other.transform.position - transform.position;
            float distanceSqr = direction.sqrMagnitude;

            if (distanceSqr == 0f) continue; // Avoid division by zero

            // Newtons law of universal gravitation
            float forceMagnitude = G * (mass * other.mass) / distanceSqr;
            Vector3 force = direction.normalized * forceMagnitude;

            // F = m * a  =>  a = F / m
            Vector3 acceleration = force / mass;
            velocity += acceleration * Time.fixedDeltaTime;
        }

        transform.position += velocity * Time.fixedDeltaTime;
    }
}
