using UnityEngine;

public class ConnectingLine : MonoBehaviour
{
    public Transform targetGeometry;   // The geometry to connect
    public Transform textLabel;        // The text label object
    public LineRenderer lineRenderer;
    public float lineWidth=0.001f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // start & end
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

    void Update()
    {
        if (targetGeometry != null && textLabel != null)
        {
            // Start at geometry
            lineRenderer.SetPosition(0, targetGeometry.position);

            // End at text label
            lineRenderer.SetPosition(1, textLabel.position);
        }
    }
}
