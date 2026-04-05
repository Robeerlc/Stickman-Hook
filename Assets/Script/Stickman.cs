using UnityEngine;

public class Stickman : MonoBehaviour
{
    [Header ("Sprites Player")]
    [SerializeField] Sprite ballSprite;
    [SerializeField] Sprite stopSprite;
    [SerializeField] Sprite goSprite;
    [SerializeField] Sprite backSprite;
    [SerializeField] Sprite winSprite;

    [Header ("Componentes")]
    private HingeJoint2D hJoint;
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;

    [Header ("Anchor")]
    [SerializeField] private GameObject anchor;

    [Header ("variable Private")]
    private int lastBestPosJoint;
    private int lastBestPosSelected;

    private void Start () {
        hJoint = GetComponent<HingeJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        anchor.transform.GetChild(lastBestPosSelected).gameObject.GetComponent<Joint>().Selected();
    }
}
