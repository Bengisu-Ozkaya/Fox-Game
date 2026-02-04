using UnityEngine;

public class HookSystem : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private LineRenderer lineRenderer; // ip çizmek için kullanýcaz
    [SerializeField] private DistanceJoint2D distanceJoint; // salýným yapmak için
    private bool isHooked = false;
    void Start() 
    {
        distanceJoint.enabled = false; // týkladýðýmýzda aktif olacak
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            HookOn();
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            HookOff();
        }

        if (isHooked)
        {
            UpdateLine();
        }
    }

    public void HookOn()
    {
        isHooked = true;
        distanceJoint.enabled = true;
        distanceJoint.connectedAnchor = targetPosition.position; // nereden itibaren dönecek/salýnacak
        lineRenderer.positionCount = 2; // 2 nokta olacak
        lineRenderer.SetPosition(0, targetPosition.position); // ilk nokta - hedef pozisyon
        lineRenderer.SetPosition(1, transform.position); // ikinci nokta - oyuncunun pozisyonu
    }

    public void HookOff()
    {
        isHooked = false;
        distanceJoint.enabled = false;
        lineRenderer.positionCount = 0;
    }

    // Ýpi her frame güncelle
    private void UpdateLine()
    {
        lineRenderer.SetPosition(0, targetPosition.position); // hedef pozisyon
        lineRenderer.SetPosition(1, transform.position); // oyuncunun pozisyonu
    }

}
