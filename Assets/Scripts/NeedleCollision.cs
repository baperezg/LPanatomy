using TMPro;
using UnityEngine;

public class NeedleCollision : MonoBehaviour
{
    [Header("UI Label")]
    public TMP_Text collisionLabel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "skin")
        {
            collisionLabel.text = "Skin";
        }
        if (other.gameObject.tag == "subcutaneous")
        {
            collisionLabel.text = "Subcutaneous tissue";
        }
        if (other.gameObject.tag == "inter")
        {
            collisionLabel.text = "Interspinous lig";
        }
        if (other.gameObject.tag == "flavum")
        {
            collisionLabel.text = "Ligamentum flavum";
        }
        if (other.gameObject.tag == "epidural")
        {
            collisionLabel.text = "Epidural space";
        }
        if (other.gameObject.tag == "dura")
        {
            collisionLabel.text = "Dura mater";
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }
}
