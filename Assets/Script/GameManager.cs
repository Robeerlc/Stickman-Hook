using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform finishLine;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Stickman stickman;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject particleEffect;

    private void Start(){
        stickman = player.GetComponent<Stickman>();
        initPos = player.transform.position;
    }

    private void Update(){
        
    }

}
