using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform finishLine;
    [SerializeField] private CameraFollow cameraFollow;
    private Stickman stickman;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private float speedOnWin;

    private Vector3 initPos;
    private bool won;

    private void Start(){
        stickman = player.GetComponent<Stickman>();
        initPos = player.transform.position;
    }

    private void Update(){
        if(stickman.getSticked() == false){
            if(player.transform.position.x < -5){
                ResetGame();
            }
            if(player.transform.position.y < -6){
                ResetGame();
            }
        }
        if(finishLine.position.x < player.transform.position.x && !won){
            won = true;
            Win();
        }
    }
    private void ResetGame(){
        stickman.Reset(initPos);
    }
    private void Win(){
        stickman.Win(speedOnWin);
        particleEffect.SetActive(true);
        particleEffect.transform.parent = null;

        cameraFollow.Win();

        StartCoroutine(FinishLevel());
    }

    IEnumerator FinishLevel (){
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

}
