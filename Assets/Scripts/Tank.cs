using UnityEngine;
using System.Timers;
using System;

public class Tank : MonoBehaviour
{
    public enum Color{
        purple,
        blue,
        orange,
    };
    // Start is called before the first frame update

    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;
    [SerializeField] protected float recharge;
    [SerializeField] protected int maxArmor;
    [SerializeField] protected int armor;
    [SerializeField] protected int maxAmmunition;
    [SerializeField] protected int ammunition = 3;
////////
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected GameObject shellPrefab;
    [SerializeField] protected AudioSource fireAudio;
    [SerializeField] protected SimpleHealthBar healthBar;
////////
    [SerializeField] protected string keyFire;
////////
    protected Timer timer;
    protected bool fire;
    protected bool fireAllowed = true;

    void Start(){
        transform.position = new Vector2(UnityEngine.Random.Range(1, MazeGenerator.Width - 1) * Cell.size - Cell.size/2,
                                         UnityEngine.Random.Range(1, MazeGenerator.Height - 1) * Cell.size - Cell.size/2);
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {

            fire = (Input.GetKeyDown(keyFire)) ? true : false;
            //fire = (Input.GetKeyUp(keyFire)) ? false : fire;
            if (fire)
            {
                Fire();
                Debug.Log("fire");
            }
        }

    }
    public bool IsAlive => health > 0; 
    public void ApplyDamage(int damage){
        if(damage < 0){
            throw new ArgumentOutOfRangeException(nameof(damage));
        }
        if(damage > armor){
            armor = 0;
            health -= damage;
        }
        else{
            armor -= damage;
        }
        Debug.Log(this.name + ' '+ health + ' ' + armor);
        healthBar.UpdateBar(health, maxHealth);
    }

    public void AddAmunition(int count){
        if(count > 0){
            if(ammunition + count <= maxAmmunition){
                ammunition += count;
            }
            else{
                ammunition = maxAmmunition;
            }
        }
        else{
            Debug.LogError($"{count} < 0");
        }
    }

    public void Fire()
    {
        if (timer is null)
        {
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(onTimer);
        }
        if (fireAllowed && ammunition > 0)
        {
            animator.SetTrigger("fire");
            Shell s = Instantiate(shellPrefab, firePoint.position, firePoint.rotation).GetComponent<ShellStandart>();
            s.owner = this;
            ammunition--;
            fireAudio.PlayOneShot(fireAudio.clip);
            fireAllowed = false;
            timer.Interval = recharge;
            timer.Start();
        }
    }

    protected void onTimer(object source, ElapsedEventArgs e)
    {
        fireAllowed = true;
        timer.Stop();
    }


}
