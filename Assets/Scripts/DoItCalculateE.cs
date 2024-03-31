using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoItCalculateE : MonoBehaviour
{
    public Image image;
    public float floatValue;
    public EButtonEffect eButton;

    void Update()
    {




        Color newColor = CalculateColor(eButton.distanceToCharacter);


        image.color = newColor;
    }

    Color CalculateColor(float value)
    {
        if (value >= 3f && value <= 25f&&eButton.Lightimage.fillAmount>0.99f)
        {
            return Color.green;
        }
        else if (value > 25f && value <= 26f && eButton.Lightimage.fillAmount > 0.99f)
        {

            float t = Mathf.InverseLerp(25f, 26f, value);
            return Color.Lerp(Color.green, Color.red, t);
        }
        else if (value < 3f && value >= 6.5f && eButton.Lightimage.fillAmount > 0.99f)
        {

            float t = Mathf.InverseLerp(3f, 6.5f, value);
            return Color.Lerp(Color.green, Color.red, t);
        }
        else
        {
            return Color.red;
        }
    }

}