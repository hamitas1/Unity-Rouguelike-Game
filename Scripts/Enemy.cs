using UnityEngine;

public class Enemy : MovingObject
{

    public int playerDamage;

    private Animator animator;
    private Transform target;
    private bool skipMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();

    }

 
    protected override void AttemtMove<T>(int xDir, int yDir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }
        base.AttemtMove<T>(xDir, yDir);
        skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;
        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
            yDir = target.position.y > transform.position.y ? 1 : -1;
        else
            xDir=target.position.x>transform.position.x ? 1 : -1;
        AttemtMove<Player> (xDir, yDir);
    }

    protected override void OnCantMove<T>(T Component)
    {
        Player hitPlayer=Component as Player;
        animator.SetTrigger("enemyAttack");
        hitPlayer.LoseFood(playerDamage);
        throw new System.NotImplementedException();
    }
}
