using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform exitPortal;
    [SerializeField] private Vector3 exitDistance;

    public void Teleport(GameObject player)
    {
       player.transform.position = exitPortal.position + exitDistance; // oyuncunun pozisyonunu çýkýþ portalýnýn pozisyonuna ayarla
    } 
}
