using UnityEngine;

// Bu script, bir düşmanın belirlenen iki nokta (leftPoint ve rightPoint) arasında
// yatay olarak hareket etmesini sağlar.
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Points")]
    [Tooltip("Düşmanın hareket edeceği sol sınır noktası.")]
    public GameObject leftPoint; // Sol hedef nokta
    [Tooltip("Düşmanın hareket edeceği sağ sınır noktası.")]
    public GameObject rightPoint; // Sağ hedef nokta

    [Header("Movement Settings")]
    [Tooltip("Düşmanın hareket hızı.")]
    public float speed = 5f; // Hareket hızı
    [Tooltip("Hedef noktaya ne kadar yaklaştığında yön değiştireceğini belirleyen mesafe.")]
    public float distanceThreshold = 0.6f; // Yön değiştirme mesafesi eşiği (biraz artırıldı)

    private Rigidbody2D rb; // Düşmanın Rigidbody2D bileşeni
    private Transform currentTarget; // Mevcut hedef nokta (leftPoint veya rightPoint)
    private Vector2 currentVelocity; // Mevcut hız vektörü

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D bileşenini al

        // Gerekli noktaların atanıp atanmadığını kontrol et
        if (leftPoint == null || rightPoint == null)
        {
            Debug.LogError($"{gameObject.name}: 'leftPoint' veya 'rightPoint' atanmamış! Script devre dışı bırakılıyor.", this);
            this.enabled = false; // Script'i devre dışı bırak
            return; // Fonksiyondan çık
        }

        // Başlangıç hedefi olarak sağ noktayı ayarla
        currentTarget = rightPoint.transform;
        // Başlangıç hızını sağa doğru ayarla
        currentVelocity = new Vector2(speed, 0);

        // Başlangıç pozisyonunun hedeflere göre mantıklı olup olmadığını kontrol et (isteğe bağlı)
        // Örneğin, düşman başlangıçta sağ noktanın sağındaysa, hedefi sola çevir.
        if (transform.position.x > rightPoint.transform.position.x)
        {
             currentTarget = leftPoint.transform;
             currentVelocity = new Vector2(-speed, 0);
        }
        else if (transform.position.x < leftPoint.transform.position.x)
        {
             currentTarget = rightPoint.transform;
             currentVelocity = new Vector2(speed, 0);
        }
        // Başlangıçta hedefe çok yakınsa, diğer hedefe yönelmesini sağla (nadiren gerekir)
        else if (Vector2.Distance(transform.position, currentTarget.position) < distanceThreshold)
        {
            SwitchTarget();
        }
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate.
    // Fizik hesaplamaları için FixedUpdate kullanmak daha doğrudur.
    void FixedUpdate()
    {
        // Rigidbody'nin uyku moduna geçmesini engelle (Önemli!)
        // Bazen Rigidbody'ler hareket etmediklerinde veya yavaşladıklarında performansı optimize etmek için uykuya geçerler.
        // Bu durum, velocity'nin sürekli ayarlandığı durumlarda beklenmedik durmalara neden olabilir.
        if (rb.IsSleeping())
        {
            rb.WakeUp();
        }

        // Hesaplanan hız vektörünü Rigidbody'ye uygula
        rb.linearVelocity = currentVelocity;

        // Mevcut hedefe yeterince yaklaşıp yaklaşmadığını kontrol et
        if (Vector2.Distance(transform.position, currentTarget.position) < distanceThreshold)
        {
            // Hedefi değiştir
            SwitchTarget();
        }
    }

    // Hedef noktayı değiştiren fonksiyon
    void SwitchTarget()
    {
        // Mevcut hedef sağ nokta ise, yeni hedef sol nokta olur
        if (currentTarget == rightPoint.transform)
        {
            currentTarget = leftPoint.transform;
            currentVelocity = new Vector2(-speed, 0); // Hızı sola doğru ayarla
        }
        // Mevcut hedef sol nokta ise, yeni hedef sağ nokta olur
        else // currentTarget == leftPoint.transform
        {
            currentTarget = rightPoint.transform;
            currentVelocity = new Vector2(speed, 0); // Hızı sağa doğru ayarla
        }
         // Debug.Log($"{gameObject.name} hedefini şuna değiştirdi: {currentTarget.name}", this); // Hata ayıklama için
    }

    // Bir çarpışma meydana geldiğinde çağrılır
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Çarpışan nesnenin etiketi "Player" ise
        if (collision.gameObject.CompareTag("Player"))
        {
            // Oyuncunun PlayerDeath script'indeki Die fonksiyonunu çağır
            // PlayerDeath script'inin oyuncu nesnesinde olduğundan emin ol
            PlayerDeath playerDeath = collision.gameObject.GetComponent<PlayerDeath>();
            if (playerDeath != null)
            {
                playerDeath.Die();
            }
            else
            {
                Debug.LogWarning($"Çarpışılan nesne ({collision.gameObject.name}) 'Player' etiketine sahip ancak PlayerDeath script'i bulunamadı.", collision.gameObject);
            }
        }
    }

    // Editörde hedef noktaları görselleştirmek için (isteğe bağlı)
    void OnDrawGizmosSelected()
    {
        // Noktalar atanmışsa aralarına bir çizgi çiz
        if (leftPoint != null && rightPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(leftPoint.transform.position, rightPoint.transform.position);
            Gizmos.DrawWireSphere(leftPoint.transform.position, distanceThreshold);
            Gizmos.DrawWireSphere(rightPoint.transform.position, distanceThreshold);
        }
    }
}
