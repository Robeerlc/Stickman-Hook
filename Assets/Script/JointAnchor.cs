using UnityEngine;
using System.Collections;

public class JointAnchor : MonoBehaviour
{
    [SerializeField] private Sprite spriteSticked;
    [SerializeField] private Sprite spriteUnsticked;
    private Sprite defaultSprite;

    private SpriteRenderer spriteRenderer;
    private GameObject dashLine;

    private bool sticked = false;

    [SerializeField] private float animTime = 0.1f;
    [SerializeField] public AnimationCurve animationCurve;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        defaultSprite = spriteRenderer.sprite;

        if (spriteUnsticked == null) {
            spriteUnsticked = defaultSprite;
        }

        if (spriteSticked == null) {
            spriteSticked = defaultSprite;
        }

        dashLine = gameObject.transform.GetChild (1).gameObject;
    }

    public void SetSticked () {
        spriteRenderer.sprite = spriteSticked;
        sticked = true;
    }

    public void SetUnsticked () {
        spriteRenderer.sprite = spriteUnsticked;
        sticked = false;
        
    }

    public void Selected () {
        if (!sticked) StartCoroutine(SelectingJoint());
        else dashLine.transform.localScale = Vector3.zero;
    }

    public void Unselected () {
        StartCoroutine(SelectingJoint());
        dashLine.transform.localScale = Vector3.zero;
    }

    IEnumerator SelectingJoint () {
        float time = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = new Vector3 (1.13f, 1.13f, 1.13f);
        while (time <= animTime) {
            time += Time.deltaTime;
            dashLine.transform.localScale = Vector3.Lerp(startScale, endScale, animationCurve.Evaluate(time));
            yield return null;
        }
    }
}