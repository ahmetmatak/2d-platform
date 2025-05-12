using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 velocity;
    float speedAmount = 5f;
    float jumpAmount = 5f;
    public int score;
    public Animator animator;
    public TextMeshProUGUI playerScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        score=0;
    }

    // Update is called once per frame
    void Update()
    {
        playerScoreText.text="Skor: "+score.ToString();
        velocity=new Vector3(Input.GetAxis("Horizontal"),0f);
        transform.position+=velocity*speedAmount*Time.deltaTime;
        animator.SetFloat("Speed",Mathf.Abs(Input.GetAxis("Horizontal")));
        
        if(Input.GetButtonDown("Jump") && !animator.GetBool("isJumping")){
            rb.AddForce(Vector3.up*jumpAmount,ForceMode2D.Impulse);
            animator.SetBool("isJumping",true);
        }
        if(Input.GetAxisRaw("Horizontal")==-1){
            transform.rotation=Quaternion.Euler(0f,180f,0f);
        }
        else if(Input.GetAxisRaw("Horizontal")==1){
            transform.rotation=Quaternion.Euler(0f,0f,0f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name=="Ground"){
            animator.SetBool("isJumping",false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name=="Ground"){
            animator.SetBool("isJumping",true);
        }
    }
}
