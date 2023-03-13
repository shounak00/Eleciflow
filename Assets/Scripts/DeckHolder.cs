using System;
using UnityEngine;
using DG.Tweening;

public class DeckHolder : MonoBehaviour, IHolder
{
    public Node node;
    private Deck deck;
    public int deckHolderID;

    private void Start()
    {
        deck = transform.root.GetComponent<Deck>();
        if (deck)
            assignHolderID(Array.IndexOf(deck.deckHolders, this));
    }

    private void Update()
    {
        if (node)
        {
            //If the node is not a child anymore, that means it has been selected and moved by player to someplace else, 
            //dereference it, so that the deck holder knows it's available to grab node
            if (node.transform.parent != transform)
            {
                node = null;
                return;
            }

            if (deckHolderID > 0)
            {
                if (deck.deckHolders[deckHolderID - 1].node == null)
                {
                    deck.deckHolders[deckHolderID - 1].hold(node);
                    node = null;
                    return;
                }
            }
        }
    }

    public void assignHolderID(int id)
    {
        deckHolderID = id;
    }

    public void hold(Node node)
    {
        //If the holder already has a node, then pass that node to the next holder
        //and make the holder available to grab the current node
        if (this.node)
        {
            if (deckHolderID < deck.deckHolders.Length - 1)
            {
                deck.deckHolders[deckHolderID + 1].hold(this.node);
                this.node = null;
            }
        }
        //Safe check
        if (this.node)
            return;
        node.transform.SetParent(transform);
        node.transform.DOMove(transform.position, 0.2f);
        node.setMaskInteraction(SpriteMaskInteraction.VisibleInsideMask);
        node.currentHolder = this;
        this.node = node;
    }
}