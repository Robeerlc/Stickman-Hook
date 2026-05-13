using UnityEngine;

public class MusicaMeta : MonoBehaviour
{
    public AudioSource musicaVictoria;
    public AudioSource musicaFondo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el jugador cruza la meta
        if (collision.CompareTag("Player"))
        {
            musicaFondo.Stop(); // Apaga la música del nivel
            musicaVictoria.Play(); // Enciende la de victoria
        }
    }
}