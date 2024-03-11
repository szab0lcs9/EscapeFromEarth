using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float asteroidDetectionRange;
    [SerializeField] float avoidanceForce;

    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    internal void MoveTowardsPlayer(Transform player)
    {
        AStarPathfinding aStarPathfinding = gameObject.GetComponent<AStarPathfinding>();

        if (aStarPathfinding != null)
        {
            List<Vector3> path = aStarPathfinding.FindPath(new PathNode(transform.position), new PathNode(player.position));
            aStarPathfinding.MoveAlongPath(path);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.01f);
        }
    }

    internal void AvoidFromAsteroids()
    {
        Collider[] asteroids = Physics.OverlapSphere(transform.position, asteroidDetectionRange, LayerMask.GetMask("Asteroid"));

        if (asteroids.Length > 0)
        {
            Vector3 avoidanceVector = Vector3.zero;
            foreach (Collider asteroid in asteroids)
            {
                Vector3 avoidanceDirection = transform.position - asteroid.transform.position;
                avoidanceVector += avoidanceDirection.normalized / avoidanceDirection.magnitude;
                avoidanceVector = ChangeVectorDirection(avoidanceVector);
                avoidanceVector.y = 0;
            }
            rb.AddForce(avoidanceVector * avoidanceForce, ForceMode.Impulse);

        }
    }

    private Vector3 ChangeVectorDirection(Vector3 avoidanceVector)
    {
        float x = avoidanceVector.x;
        float z = avoidanceVector.z;

        return new Vector3(z, 0.0f, x);
    }

    public void StopMovement()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.0f)
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

}
