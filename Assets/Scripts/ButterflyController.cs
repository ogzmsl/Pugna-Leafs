using UnityEngine;

public class ButterflyController : MonoBehaviour
{
    public LayerMask butterflyLayer;
    public float movementSpeed = 5f;
    public float minDistanceBetweenButterflies = 1f;

    private Camera mainCamera;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

   public void GetPullObject()
    {
        
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, butterflyLayer))
            {
                targetPosition = hit.point;
                isMoving = true;
            }
        

        if (isMoving)
        {
            MoveButterflies();
        }
    }

    void MoveButterflies()
    {
        GameObject[] butterflies = GameObject.FindGameObjectsWithTag("butterflyLayer");

        foreach (GameObject butterfly in butterflies)
        {
            Vector3 direction = (targetPosition - butterfly.transform.position).normalized;
            butterfly.transform.position += direction * movementSpeed * Time.deltaTime;
        }

        // Kelebekler arasýndaki mesafeyi kontrol et
        for (int i = 0; i < butterflies.Length - 1; i++)
        {
            for (int j = i + 1; j < butterflies.Length; j++)
            {
                if (Vector3.Distance(butterflies[i].transform.position, butterflies[j].transform.position) < minDistanceBetweenButterflies)
                {
                    // Ýki kelebeðin arasýndaki mesafe belirlenen mesafeden küçükse, kelebeklerin pozisyonunu güncelle
                    Vector3 offset = (butterflies[i].transform.position - butterflies[j].transform.position).normalized * minDistanceBetweenButterflies;
                    butterflies[i].transform.position += offset;
                    butterflies[j].transform.position -= offset;
                }
            }
        }
    }
}
