using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            PlayerMovement player =collision.gameObject.GetComponent<PlayerMovement>();
            player.score+=1;
            gameObject.SetActive(false);
        }
    }
}
