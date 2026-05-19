using UnityEngine;
using UnityEngine.UI;

public class TransparencyController : MonoBehaviour
{
    [System.Serializable]
    public class TransparentItem
    {
        public GameObject target;          // Object to make transparent
        [Range(0f, 1f)] public float defaultAlpha = 1f; // Starting alpha
        public Slider transparencySlider;  // Reference to the UI slider
    }

    public TransparentItem[] items; // Geometries array: skin, subcutaneous, etc.

    void Start()
    {
        foreach (var item in items)
        {
            if (item.transparencySlider != null)
            {
                // Initialize slider
                item.transparencySlider.minValue = 0f;
                item.transparencySlider.maxValue = 1f;
                item.transparencySlider.value = item.defaultAlpha;

                // Apply initial transparency
                ApplyTransparency(item, item.defaultAlpha);

                // Wire slider event to update only this item
                item.transparencySlider.onValueChanged.AddListener(
                    alpha => ApplyTransparency(item, alpha)
                );
            }
        }
    }

     private void ApplyTransparency(TransparentItem item, float alpha)
    {
        if (item.target == null) return;

        Renderer renderer = item.target.GetComponent<Renderer>();
        if (renderer != null)
        {
            foreach (var mat in renderer.materials)
            {
                // Update the color alpha
                Color c = mat.color;
                c.a = alpha;
                mat.color = c;

                //Custom shader
                // Pass alpha to the shader property
                //mat.SetFloat("_Alpha", alpha);
                mat.SetFloat("Transparency", alpha);

                // Configure Standard Shader for transparency
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            }
        }
    }
}