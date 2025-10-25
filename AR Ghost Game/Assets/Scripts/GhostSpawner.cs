using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ghostPrefab;

    [SerializeField]
    private ARRaycastManager raycastManager;

    [SerializeField]
    private float spawnRadius = 3f;

    [SerializeField]
    private int ghostCount = 5;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        for(int i = 0; i < ghostCount; i++)
        {
            SpawnGhost();
        }
    }

    private void SpawnGhost()
    {
        // Determine random spawn offset around player
        float angle = Random.Range(0f, 360f);
        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)) * spawnRadius;

        Vector3 spawnOrigin = Camera.main.transform.position + offset + Vector3.up * 1.5f;

        // Raycast downward to find a plane
        if (raycastManager.Raycast(spawnOrigin, hits, TrackableType.PlaneWithinBounds | TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            Instantiate(ghostPrefab, hitPose.position, Quaternion.identity);
        }
        else
        {
            // fallback: spawn at offset position anyway
            Instantiate(ghostPrefab, spawnOrigin, Quaternion.identity);
        }
    }
}