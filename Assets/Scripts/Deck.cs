using UnityEngine;

public class Deck : MonoBehaviour
{
    private Board board;
    public Transform content;
    public float initialPoint;
    //Distance between each deck holder spawn
    public float distance;

    public DeckHolder[] deckHolders;

    private void Awake()
    {
        board = FindObjectOfType<Board>();
        spawnDeckHolders();
    }

    private void spawnDeckHolders()
    {
        if (!board)
            return;
        float posX = initialPoint;
        deckHolders = new DeckHolder[board.width * board.height];
        GameObject deckHolderPrefab = Resources.Load("Prefabs/Deck Holder") as GameObject;
        for (int i = 0; i < board.width * board.height; i++)
        {
            GameObject deckHolder = Instantiate(deckHolderPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            deckHolder.transform.SetParent(content);
            deckHolder.transform.localPosition = new Vector3(posX, 0, 0);
            //Grab the reference for later use
            deckHolders[i] = deckHolder.GetComponent<DeckHolder>();
            //Increment posX for next deck holder spawn
            posX += distance;
        }
    }
}