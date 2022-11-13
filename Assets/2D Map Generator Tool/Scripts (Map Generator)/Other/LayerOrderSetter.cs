using UnityEngine;
using MapGenerator.UnityPort;

public class LayerOrderSetter : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private MapGeneratorTool mapGeneratorTool;
    private Transform trans;
    void Start()
    {
        mapGeneratorTool = FindObjectOfType<MapGeneratorTool>();
        //spriteRenderer.sortingOrder = (int)(mapGeneratorTool.height - transform.position.z);
        transform.position = transform.position + Vector3.forward * (transform.position.y-transform.position.z);
        trans = transform.GetChild(0);
        trans.localPosition = new Vector3(trans.localPosition.x,trans.localPosition.y,0);
    }
}
