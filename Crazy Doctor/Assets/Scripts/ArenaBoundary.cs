using System;
using UnityEngine;

public class ArenaBoundary : MonoBehaviour
{
    public static ArenaBoundary Instance;
    [SerializeField] private float arenaWidth;
    [SerializeField] private float arenaHeight;

    private void Awake()
    {
        Instance = this;
    }

    public void RestrictObjectPosition(Transform targetTransform)
    {
        Vector3 newPosition = targetTransform.position;

        newPosition.x = Mathf.Clamp(newPosition.x, -arenaWidth / 2, arenaWidth / 2);
        newPosition.y = Mathf.Clamp(newPosition.y, -arenaHeight / 2, arenaHeight / 2);

        targetTransform.position = newPosition;
    }
}