using UnityEngine;
using MapGenerator.UnityPort;

public class LayerOrderSetter : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private MapGeneratorTool mapGeneratorTool;

    void Start()
    {
        mapGeneratorTool = FindObjectOfType<MapGeneratorTool>();
        //spriteRenderer.sortingOrder = (int)(mapGeneratorTool.height - transform.position.z);
        transform.position = transform.position + Vector3.forward * (transform.position.y-transform.position.z);
    }
}
