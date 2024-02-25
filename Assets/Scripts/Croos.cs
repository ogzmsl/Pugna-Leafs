using UnityEngine;
using UnityEngine.UI;

public class Croos : MonoBehaviour
{
    public GameObject CroosH;
    public RawImage crosshairImage;
    public Color defaultColor = Color.white;
    public Color enemyColor = Color.red;
    public float colorChangeSpeed = 5f;
    public float scanningAngle = 5f;
    public float maxDetectionDistance = 1;

    private bool isOverEnemy = false;

    void Update()
    {
        Vector3 characterForward = Camera.main.transform.forward;
        Vector3 characterPosition = Camera.main.transform.position;

        bool foundEnemy = false;
        float closestEnemyDistance = Mathf.Infinity; // En yakýn düþmanýn mesafesini takip etmek için

        for (float horizontalAngle = -scanningAngle / 2; horizontalAngle < scanningAngle / 2; horizontalAngle += 1f)
        {
            for (float verticalAngle = -scanningAngle / 2; verticalAngle < scanningAngle / 2; verticalAngle += 1f)
            {
                Vector3 scanningVector = Quaternion.AngleAxis(horizontalAngle, Vector3.up) * Quaternion.AngleAxis(verticalAngle, Vector3.right) * characterForward;

                RaycastHit hit;

                if (Physics.Raycast(new Ray(characterPosition, scanningVector), out hit))
                {
                    // Eðer çarpýþma olduysa ve çarpýþan nesne düþman ise renk deðiþtir
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        float distanceToEnemy = Vector3.Distance(characterPosition, hit.point);

                        if (distanceToEnemy < maxDetectionDistance && distanceToEnemy < closestEnemyDistance)
                        {
                            foundEnemy = true;
                            closestEnemyDistance = distanceToEnemy;
                
                            
                        }
                       
                    }
                }
            }

            if (foundEnemy)
            {
                break;
            }
        }

        if (foundEnemy || !isOverEnemy)
        {
            isOverEnemy = foundEnemy;
            crosshairImage.color = Color.Lerp(crosshairImage.color, isOverEnemy ? enemyColor : defaultColor, Time.deltaTime * colorChangeSpeed);
        }
        else
        {
            crosshairImage.color = Color.Lerp(crosshairImage.color, defaultColor, Time.deltaTime * colorChangeSpeed);
        }
    }
}
