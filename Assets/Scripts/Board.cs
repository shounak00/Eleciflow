using UnityEngine;
using DG.Tweening;

public class Board : MonoBehaviour
{
    public Node[] nodes;

    public int width = 6;
    public int height = 6;

    public BoardHolder[] boardHolders;

    public Transform holder;

    public Event levelCompleted;

    private void Awake()
    {
        DOTween.Init(true, true, LogBehaviour.ErrorsOnly).SetCapacity(500, 20);
        generateGrid();
    }

    private void generateGrid()
    {
        int boardHolderId = 0;
        boardHolders = new BoardHolder[width * height];
        GameObject boardHolderPrefab = Resources.Load("Prefabs/Board Holder") as GameObject;
        for (int row = height - 1; row >= 0 ; row--)
        {
            for (int column = 0; column < width; column++)
            {
                //Instantiate and center the board position
                GameObject boardHolder = Instantiate(boardHolderPrefab, holder) as GameObject;
                boardHolder.transform.position = new Vector2(column - width / 2, row - height / 2);
                //Grab the reference for later use
                boardHolders[boardHolderId] = boardHolder.GetComponent<BoardHolder>();
                boardHolders[boardHolderId].assignHolderID(boardHolderId);                
                boardHolderId += 1;
            }
        }
    }

    //This event is called when the node is dropped on board
    //When this happens, check for node placements on the board, and trigger level completion if the conditions meet
    public void eventNodeDropped()
    {
        bool levelCompleted = true;
        for (int i = 0; i < boardHolders.Length; i++)
        {
            NodeType childNodeType = NodeType.none;
            //Child node type of parent board holder
            if (boardHolders[i].node)
            {
                childNodeType = boardHolders[i].node.nodeType;
            }
            if (boardHolders[i].expectedNodeType != childNodeType)
            {
                levelCompleted = false;
                break;
            }
        }
        if (levelCompleted)
        {
            //Fire up the level complete event
            this.levelCompleted.occurred();
        }
    }
}