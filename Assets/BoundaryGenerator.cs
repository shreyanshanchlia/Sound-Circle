using UnityEngine;

public class BoundaryGenerator : MonoBehaviour
{
    public GameObject boundaryPrefab, shipPrefab;
    public int numberOfShips;
    [Range(1, 150)]public int LayerCount;
    [Range(2, 360)]public int ObjectsPerLayer;

    public float radius = 5f;
    public float layerDistance;
    private void Start()
    {
        GenerateBoundary();
        GenerateShips();
    }

    private void GenerateShips()
    {
        for (int i = 0; i < numberOfShips; i++)
        {
            Instantiate(shipPrefab, new Vector3(Random.Range(-1, 1f), Random.Range(-1f, 1f)) * radius / 1.5f,
                Quaternion.identity);
        }
    }

    public void GenerateBoundary()
    {
        for (int i = 0; i < LayerCount; i++)
        {
            float perimeter = 2 * ObjectsPerLayer * (radius + (i) * layerDistance) *
                              Mathf.Tan(Mathf.PI / ObjectsPerLayer);
            //print(perimeter);
            for (int j = 0; j < ObjectsPerLayer; j++)
            {
                GameObject boundaryNode = Instantiate(boundaryPrefab, this.transform);
                Vector3 nodePosition = Vector3.zero;
                float angle = 360f / ObjectsPerLayer * j;

                nodePosition.x = Mathf.Cos(Mathf.Deg2Rad * angle);
                nodePosition.y = Mathf.Sin(Mathf.Deg2Rad * angle);
                boundaryNode.transform.position = nodePosition * (radius + i * layerDistance);

                nodePosition = Vector3.zero;
                nodePosition.z = angle;
                boundaryNode.transform.eulerAngles = nodePosition;

                Vector3 transformLocalScale = boundaryNode.transform.localScale;
                transformLocalScale.y = (perimeter - transformLocalScale.x * ObjectsPerLayer / 3) / ObjectsPerLayer ;
                //transformLocalScale.y = perimeter / ObjectsPerLayer;// * (1 - (0.1f / (i + 1)));
                boundaryNode.transform.localScale = transformLocalScale;
                
                boundaryNode.GetComponent<Node>().layerID = i;
                boundaryNode.GetComponent<Node>().positionID = j;
                boundaryNode.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f);
            }
        }
    }
}
