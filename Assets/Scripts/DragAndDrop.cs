using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject selectedNode;
    public LayerMask layerMask;

    private void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            inputEditor();
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            inputTouch();
    }

    private void inputEditor()
    {
        //Select the node when mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, layerMask);
            if (hit.collider != null)
            {
                //Select the node, if the mouse click is detected right above the node
                if (hit.transform.CompareTag("Node"))
                    onSelect(hit);
            }
        }
        
        //Release the node, if the mousle click is released 
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedNode != null)
                onRelease();
        }

        //If the node is currently selected, move it to the position of cursor
        if (selectedNode != null)
        {
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedNode.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0);
        }
    }

    private void inputTouch()
    {
        RaycastHit2D hit;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                //Select the node when touch is detected
                case TouchPhase.Began:
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, 100, layerMask);
                    if (hit.collider != null)
                    {
                        if (hit.transform.CompareTag("Node"))
                            onSelect(hit);
                    }
                    break;
                //If the node is currently selected and player is moving the touch, move the node to the position of cursor
                case TouchPhase.Moved:
                    if (selectedNode != null)
                    {
                        Vector3 touchPoint = Camera.main.ScreenToWorldPoint(touch.position);
                        selectedNode.transform.position = new Vector3(touchPoint.x, touchPoint.y, 0);
                    }
                    break;
                //Release the node when the touch is released 
                case TouchPhase.Ended:
                    if (selectedNode != null)
                        onRelease();
                    break;
            }
        }
    }

    private void onSelect(RaycastHit2D hit)
    {
        Node node = hit.transform.GetComponent<Node>();
        if (!node.selected)
        {
            selectedNode = node.transform.gameObject;
            node.onSelect();
        }
    }

    private void onRelease()
    {
        Node node = selectedNode.GetComponent<Node>();
        node.onRelease();
        selectedNode = null;
    }
}