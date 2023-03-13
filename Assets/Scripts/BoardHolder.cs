using UnityEngine;
using DG.Tweening;

public class BoardHolder : MonoBehaviour, IHolder
{
    public Node node;
    public NodeType expectedNodeType;
    public int boardHolderID;
    public Event nodeDropped;
    private SpriteRenderer mainSprite;
    private void Awake()
    {
        mainSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //If the holder doesn't hold the node, derefernce it
        if (node)
        {
            if (node.transform.parent != transform)
                node = null;
        }
    }

    public void assignHolderID(int id)
    {
        boardHolderID = id;
    }

    public void hold(Node node)
    {
        node.transform.SetParent(transform);
        node.transform.DOMove(transform.position, 0.2f);
        node.currentHolder = this;
        this.node = node;
        nodeDropped.occurred();
    }

    public void eventLevelCompleted()
    {
        if (mainSprite)
        {
            mainSprite.DOColor(new Color(0, 0, 0, 0), 0.2f);
        }
    }

    public void eventLevelStart()
    {
        if (mainSprite)
        {
            mainSprite.DOColor(new Color(0, 0, 0, 1), 0.2f);
        }
    }
}