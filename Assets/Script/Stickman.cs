using UnityEngine;

public class Stickman : MonoBehaviour
{
    [Header("Sprites Player")]
    [SerializeField] Sprite ballSprite;
    [SerializeField] Sprite stopSprite;
    [SerializeField] Sprite goSprite;
    [SerializeField] Sprite backSprite;
    [SerializeField] Sprite winSprite;

    [Header("Componentes")]
    private HingeJoint2D hJoint;
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;

    [Header("Anchor")]
    [SerializeField] private GameObject anchor;

    [Header("variable Private")]
    private int lastBestPosJoint;
    private int lastBestPosSelected;
    private int touches;
    private int bestPos;
    private float bestDistance;
    private Vector3 actualJointPos;

    [Header("Variable Public")]
    [SerializeField] private float gravityRope = 2f;
    [SerializeField] private float gravityAir = 0.5f;
    [SerializeField] private float factorX = 1.2f;
    [SerializeField] private float factorY = 1f;

    [Header("Bool")]
    private bool sticked = false;
    private void Start()
    {
        hJoint = GetComponent<HingeJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        lastBestPosJoint = 0;
        lastBestPosSelected = 0;
        touches = 0;
        anchor.transform.GetChild(lastBestPosSelected).gameObject.GetComponent<JointAnchor>().Selected();
    }

    private void update()
    {
        bestPos = 0;
        bestDistance = float.MaxValue;

        for (int i = 0; i < anchor.transform.childCount; i++)
        {
            {
                float actualDistance = Vector2.Distance(gameObject.transform.position, anchor.transform.GetChild(i).transform.position);
                if (actualDistance < bestDistance)
                {
                    bestPos = i;
                    bestDistance = actualDistance;
                }
            }
            CheckInput();

            if (sticked)
            {
                lineRenderer.SetPosition(0, gameObject.transform.position);
                lineRenderer.SetPosition(1, actualJointPos);

                ChangeSprite();
            }

            if (lastBestPosJoint != bestPos)
            {
                anchor.transform.GetChild(lastBestPosSelected).gameObject.GetComponent<JointAnchor>().Unselected();
                anchor.transform.GetChild(bestPos).gameObject.GetComponent<JointAnchor>().Selected();

                lastBestPosSelected = bestPos;
            }
        }
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || ((Input.touchCount > 0) && (touches == 0)))
        {
            lineRenderer.enabled = true;
            hJoint.enabled = true;
            rb.gravityScale = gravityRope;

            hJoint.connectedBody = anchor.transform.GetChild(bestPos).transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
            actualJointPos = anchor.transform.GetChild(bestPos).gameObject.transform.position;
            anchor.transform.GetChild(bestPos).gameObject.GetComponent<JointAnchor>().SetSticked();
            anchor.transform.GetChild(bestPos).gameObject.GetComponent<JointAnchor>().Unselected();

            lastBestPosJoint = bestPos;
            rb.angularVelocity = 0f;

            sticked = !sticked;

        }

        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || ((Input.touchCount == 0) && (touches > 0)))
        {
            lineRenderer.enabled = false;
            hJoint.enabled = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * factorX, rb.linearVelocity.y + factorY);
            rb.gravityScale = gravityAir;

            anchor.transform.GetChild(lastBestPosJoint).gameObject.GetComponent<JointAnchor>().SetUnsticked();

            if (bestPos == lastBestPosJoint)
            {
                anchor.transform.GetChild(bestPos).gameObject.GetComponent<JointAnchor>().Selected();
                anchor.transform.GetChild(lastBestPosSelected).gameObject.GetComponent<JointAnchor>().Unselected();
            }

            spriteRenderer.sprite = ballSprite;
            rb.AddTorque(-rb.linearVelocity.magnitude);
            sticked = !sticked;
        }
        touches = Input.touchCount;
    }

    private void ChangeSprite()
    {

    }
}
