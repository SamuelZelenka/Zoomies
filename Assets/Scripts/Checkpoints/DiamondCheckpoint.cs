using System.Collections.Generic;
using UnityEngine;

public class DiamondCheckpoint : MonoBehaviour
{
    [SerializeField] private Vector2 _posOffset;
    [SerializeField] private GameObject _prefab;
    
    [Range(0, 25)] public int diamondCount = 3;
    public float radius = 1;
    
    private List<GameObject> _diamondMarkers = new();
    private float _angleOffset = 0;

    private void Start()
    {
        for (int i = 0; i < diamondCount; i++)
        {
            _diamondMarkers.Add(Instantiate(_prefab, transform)); 
        }
    }

    private void Update()
    {
        _angleOffset += Time.deltaTime;
        for (int i = 0; i < _diamondMarkers.Count; i++)
        {
            _diamondMarkers[i].transform.localPosition = CalculatePointPosition(i);
        }
    }

    Vector2 CalculatePointPosition(int currrentPointCount)
    {
        float theta = Mathf.PI * 2 / diamondCount;
        float angle = theta * currrentPointCount;
        float xPos = radius * Mathf.Cos(angle + _angleOffset);
        float yPos = radius * Mathf.Sin(angle + _angleOffset);

        return new Vector2(xPos, yPos) + _posOffset;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float radius = 0.1f;
        
        if (gameObject.activeSelf)
        {
            for (int i = 0; i < diamondCount; i++)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawSphere(CalculatePointPosition(i), radius);
            }
        }
    }
#endif
}
