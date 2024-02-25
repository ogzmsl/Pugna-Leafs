using UnityEngine;
using UnityEngine.UI;

public class ShieldTime : MonoBehaviour
{

    public Image fillImage;
    public float fillSpeed = 0.1f;
    public float decreaseSpeed = 0.1f;

    public bool isFilling = false;






    void Update()
    {

    

        if (isFilling)
        {
            fillImage.fillAmount = Mathf.MoveTowards(fillImage.fillAmount, 1f, fillSpeed * Time.deltaTime);
        }
    }
}