using UnityEngine;
using UnityEngine.AI;

public class RandomPointOnNavMesh : MonoBehaviour
{
    public float range = 10.0f;
    
    private void Start() {
        
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.onUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }

        }
        result = Vector3.zero;
        return false;
    }

    void Update()
    {
        Vector3 point;
       
        if (RandomPoint(transform.position, range, out point))
        {
            UnityEngine.Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
        }
    }
} 