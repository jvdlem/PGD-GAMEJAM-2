using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectille : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigitbody;
    [SerializeField] private float timer = 10;
    [SerializeField] private float Speed = 500;
    public float dmg = 1;
    // Start is called before the first frame update
    [System.Flags]
    public enum Characteristics
    {
        None,
        Choclate,
        Candy,
        Gummy
    }
    public Projectille(float damage)
    {
        dmg += damage;
    }
    private void Awake()
    {
        myRigitbody.AddRelativeForce(new Vector3(0, 0, Speed));
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public Characteristics Enemytype = Characteristics.None;
    public int myCharacteristics;
    private void Start()
    {
        myCharacteristics = (int)Enemytype;
    }
}
