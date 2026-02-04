using UnityEngine;

public class CheckpointControl : MonoBehaviour
{
    [SerializeField] private Transform currentCheckpoint;
    [SerializeField] private Transform levelStartPosition; // Levelin baþlangýç noktasý


    private void Start()
    {
        // Eðer levelStartPosition atanmamýþsa, oyuncunun baþlangýç pozisyonunu kullan
        if (levelStartPosition == null)
        {
            levelStartPosition = transform;
        }
    }

    public void setCheckpointPosition(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public Transform getCurrentCheckpointPosition()
    {
        if (currentCheckpoint == null)
        {
            return levelStartPosition;
        }

        return currentCheckpoint;
    }
}
