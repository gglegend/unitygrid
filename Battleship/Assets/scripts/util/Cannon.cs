using UnityEngine;

public class Cannon : MonoBehaviour {

    private GameObject ball;
    private Tile target;
    private Battery battery;
    private Quaternion origRotation;

    void Start()
    {
        origRotation = transform.rotation;
    }

    public void Shoot(GameObject ballTemplate, Tile tgt)
    {
        target = tgt;       
        Aim(target);
        ball = Instantiate(ballTemplate, transform.position, Quaternion.identity);
        Invoke("Fire", 0.5f);
        Invoke("ReCenter", 1.5f);
    }

    void Aim(Tile target)
    {
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180f;  // need to rotate 90 deg for some reason
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Fire()
    {
        Cannonball script = ball.GetComponent<Cannonball>();
        script.Launch(transform.position, target); // the ball will destroy itself upon reaching the tgt
    }

    void ReCenter()
    {
        transform.rotation = origRotation;
    }
}
