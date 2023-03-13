using UnityEngine;
using DG.Tweening;

public enum NodeType
{
    none,

    biConnectorLeftDown,
    biConnectorRightDown,
    biConnectorUpLeft,
    biConnectorUpRight,

    boltDown,
    boltLeft,
    boltRight,
    boltUp,

    bulbDown,
    bulbLeft,
    bulbRight,
    bulbUp,

    lineHorizontol,
    lineVertical,

    triConnectorRightLeftDown,
    triConnectorUpLeftDown,
    triConnectorUpRightDown,
    triConnectorUpRightLeft,

    quadConnector
}

public class Node : MonoBehaviour, INode
{
    public NodeType nodeType;
    public IHolder currentHolder;


    public Board board;
    public SpriteRenderer[] nodeSprites;
    public SpriteRenderer glowSprite;


    public bool selected;
    public LayerMask holderLayerMask;

    private void Awake()
    {
        board = FindObjectOfType<Board>();
    }

    public void onSelect()
    {
        selected = true;
        transform.SetParent(null);
        transform.DOScale(Vector3.one * 1.2f, 0.2f);
        setMaskInteraction(SpriteMaskInteraction.None);
    }

    public void onRelease()
    {
        bool drop = true;
        selected = false;
        transform.DOScale(Vector3.one, 0.2f);
        //If there's a holder beneath the node, assign the node piece to the holder
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 100, holderLayerMask);
        if (hit.collider != null)
        {
            //Do not drop the node on BOARD, if the board holder already has one
            if (hit.transform.GetComponent<BoardHolder>())
            {
                if (hit.transform.GetComponent<BoardHolder>().node != null)
                {
                    Handheld.Vibrate();
                    drop = false;
                }
            }
            if (drop)
            {
                currentHolder = hit.transform.GetComponent<IHolder>();
            }
        }
        currentHolder.hold(this);
    }

    public void setMaskInteraction(SpriteMaskInteraction spriteMaskInteraction)
    {
        for (int i = 0 ; i < nodeSprites.Length ; i++)
        {
            nodeSprites[i].maskInteraction = spriteMaskInteraction;
        }
    }

    public void eventLevelCompleted()
    {
        for (int i = 0; i < nodeSprites.Length; i++)
        {
            nodeSprites[i].DOColor(new Color(1,1,1,1), 0.2f);
        }
        if (glowSprite)
        {
            glowSprite.transform.localPosition = Vector3.zero;
            glowSprite.color = new Color(0,0,0,0);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(glowSprite.transform.DOScale(Vector3.one/2, 0.5f));
            mySequence.Join(glowSprite.DOColor(new Color(1, 1, 1, 0.5f), 0.25f));
            mySequence.Append(glowSprite.DOColor(new Color(0, 0, 0, 0), 0.25f));

        }
    }
}