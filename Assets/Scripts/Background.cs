using UnityEngine;

public class Background : MonoBehaviour
{
    public Sprite[] backgroundConfigurations;
    public SpriteRenderer spriteRenderer;

    private int spriteNo;

    public void eventLevelStart()
    {
        int decision;
        do
        {
            decision = Random.Range(0, backgroundConfigurations.Length);
        } while (decision == spriteNo);
        spriteNo = decision;
        spriteRenderer.sprite = backgroundConfigurations[spriteNo];
    }
}