using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoItCalculateforQ : MonoBehaviour
{
    public Image image; 
    public float floatValue;
    public QSpeel speel;

    void Update()
    {
      



        Color newColor = CalculateColor(speel.distanceToCharacter);

        
        image.color = newColor;
    }

    Color CalculateColor(float value)
    {
        if (value >= 2f && value <= 18f && speel.QSpellImage.fillAmount > 0.99f)
        {
            return Color.green;
        }
        else if (value > 18f && value <= 18.5f&&speel.QSpellImage.fillAmount>0.99f)
        {
           
            float t = Mathf.InverseLerp(12f, 12.5f, value);
            return Color.Lerp(Color.green, Color.red, t);
        }
        else if (value < 2f && value >= 3.5f && speel.QSpellImage.fillAmount > 0.99f)
        {
       
            float t = Mathf.InverseLerp(2f, 1.5f, value);
            return Color.Lerp(Color.green, Color.red, t);
        }
        else
        {
            return Color.red;
        }
    }

}
