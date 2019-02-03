using UnityEngine;

public class Dragging : MainDraggingBehavior
{
    private Collider m_collider;
    private DropArea dropArea;
    private Vector3 dragScreenPos, dragWorldPos, dropWorldPos;
    private float dragScreenPosX, dragScreenPosY;
    private bool isDraggable, isDroppable, isMouseUp;

    public LayerMask layerMask;
    public short ID;

    private void Start()
    {
        m_collider = this.GetComponent<Collider>();
    }

    private void Update()
    {
        Physics.SyncTransforms();
        if (isMouseUp)
        {
            if (isDraggable)
            {
                this.transform.position = Vector3.Slerp(this.transform.position, dragWorldPos, 0.1f);
            }
            if (isDroppable)
            {
                //if (dropArea.isEmpty)
                //{
                    this.transform.position = Vector3.Slerp(this.transform.position, dropWorldPos, 0.1f);
                    
                //}
                //else
                //{
                   // this.transform.position = Vector3.Slerp(this.transform.position, dragWorldPos, 0.1f);

                //}
            }
        }
    }

    private void OnMouseDown()
    {
        isMouseUp = false;
        dragScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        dragScreenPosX = Input.mousePosition.x - dragScreenPos.x;
        dragScreenPosY = Input.mousePosition.y - dragScreenPos.y;
    }

    private void OnMouseDrag()
    {
        Vector3 curDragScreenPos = new Vector3(Input.mousePosition.x -dragScreenPosX, Input.mousePosition.y - dragScreenPosY, dragScreenPos.z);
        Vector3 curDragWorldPos = Camera.main.ScreenToWorldPoint(curDragScreenPos);
        this.transform.position = curDragWorldPos;
        CheckCollisions(curDragWorldPos, m_collider);
    }

    private void OnMouseUp()
    {
        if (isDroppable)
        {
            CheckAnswer();
        }
        isMouseUp = true;
    }

    private void CheckCollisions(Vector3 pos, Collider col)
    {
        Collider[] hitColliders = Physics.OverlapBox(pos, col.bounds.size, Quaternion.identity, layerMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            //drag
            if (hitColliders[i].gameObject.CompareTag("DragArea"))
            {
                dragWorldPos = hitColliders[i].gameObject.transform.position;
                isDraggable = true;
                isDroppable = false;
            }

            //drop
            else if (hitColliders[i].gameObject.CompareTag("DropArea"))
            {
                dropWorldPos = hitColliders[i].gameObject.transform.position;
                isDroppable = true;
                isDraggable = false;
                dropArea = hitColliders[i].GetComponent<DropArea>();
            }
            i++;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, this.GetComponent<Collider>().bounds.size);
    }

    private void CheckAnswer()
    {
        if (dropArea.ID == this.ID)
        {
            AudioManager.AM.audioSource.clip = AudioManager.AM.AfterCheckingAnswer[0];
            if (correctAnswers < maxCorrectAnswers)
            {
                correctAnswers++;

            }
        }
        else
        {
            AudioManager.AM.audioSource.clip = AudioManager.AM.AfterCheckingAnswer[1];
            if (correctAnswers > 0)
            {
                correctAnswers--;
            }
        }
        AudioManager.AM.audioSource.Play();
    }
}
