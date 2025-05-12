using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeath : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private bool isDead =false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        col=GetComponent<Collider2D>();
    }
    public void Die(){
        if(isDead) return;
        isDead=true;
        col.enabled=false;
        rb.gravityScale=3f;
        rb.linearVelocity=Vector2.zero;
        Invoke("ReloadScene",1f);
    }
    private void OnCollisionEnter2D(Collision2D collision){
    if (collision.gameObject.CompareTag("Sea")){
        Die();
    }
}

    void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
