using UnityEngine;

public class Player : MovingObject{

    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    public float restartLevelDelay = 1f;

    private Animator animator;
    private int food;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoints;
        base.Start();
    }

    private void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;

    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.playerTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            vertical = 0;

        if (horizontal!=0|| vertical != 0)
            AttemtMove<Wall> (horizontal, vertical);

    }
    protected override void AttemtMove<T>(int xDir, int yDir)
    {
        food--;
        base.AttemtMove<T>(xDir, yDir);
        RaycastHit2D hit;
        CheckIfGameOver();
        GameManager.instance.playerTurn = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag=="Food")
        {
            food += pointsPerFood;
            other.gameObject.SetActive(false);
        }
        else if (other.tag=="Soda")
        {
            food += pointsPerSoda;
            other.gameObject.SetActive(false);
        }
    }

    protected override void OnCantMove<T>(T Component)
    {
        Wall hitWall= Component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("PlayerHit");
      
    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);


    }

    public void LoseFood (int loss)
    {
        animator.SetTrigger("LessHealth");
        food -= loss;
        CheckIfGameOver();

    }
    private void CheckIfGameOver()
    {
        if (food <= 0)
            GameManager.instance.GameOver();

    }

}
