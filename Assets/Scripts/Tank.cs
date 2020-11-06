using UnityEngine;using System.Timers;public class Tank : MonoBehaviour{
    public enum Color{
        purple,
        blue,
        orange,
    };
    // Start is called before the first frame update
    public int Health;    public static int maxHealth = 100;    public float Recharge;    public int Armor;    public int Ammunition = 3;    protected Timer t;    public Animator animator;    public Transform firePoint;
    public GameObject shellPrefab;    public AudioSource fireAudio;    public SimpleHealthBar healthBar;    protected bool fire;    protected bool fireAllowed = true;    public string keyFire;    void Start(){
        transform.position = new Vector2(UnityEngine.Random.Range(1, MazeGenerator.Width - 1) * Cell.size - Cell.size/2,
                                         UnityEngine.Random.Range(1, MazeGenerator.Height - 1) * Cell.size - Cell.size/2);
    }

    private void OnDestroy()
    {
        
    }    void Update()    {
        if (Time.timeScale == 1)
        {

            fire = (Input.GetKeyDown(keyFire)) ? true : false;
            //fire = (Input.GetKeyUp(keyFire)) ? false : fire;
            if (fire)            {                Fire();                Debug.Log("fire");            }        }    }    public void Fire()
    {
        if (t is null)
        {
            t = new Timer();
            t.Elapsed += new ElapsedEventHandler(onTimer);
        }
        if (fireAllowed && Ammunition > 0)
        {
            animator.SetTrigger("fire");
            Shell s = Instantiate(shellPrefab, firePoint.position, firePoint.rotation).GetComponent<ShellStandart>();
            s.owner = this;
            Ammunition--;
            fireAudio.PlayOneShot(fireAudio.clip);
            fireAllowed = false;
            t.Interval = 1000;
            t.Start();
        }
    }    protected void onTimer(object source, ElapsedEventArgs e)
    {
        fireAllowed = true;
        t.Stop();
    }}