using UnityEngine;
[CreateAssetMenu]
public class Level : ScriptableObject
{
    //Size of the array determines how many of these are needed in a level
    //Elements of the array denote the location in grid in order to trigger level completion

    public int[] biConnectorLeftDown;
    public int[] biConnectorRightDown;
    public int[] biConnectorUpLeft;
    public int[] biConnectorUpRight;

    public int[] boltDown;
    public int[] boltLeft;
    public int[] boltRight;
    public int[] boltUp;

    public int[] bulbDown;
    public int[] bulbLeft;
    public int[] bulbRight;
    public int[] bulbUp;

    public int[] lineHorizontol;
    public int[] lineVertical;

    public int[] triConnectorRightLeftDown;
    public int[] triConnectorUpLeftDown;
    public int[] triConnectorUpRightDown;
    public int[] triConnectorUpRightLeft;

    public int[] quadConnector;
}