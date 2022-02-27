using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public string currentRoom;

    #region Variaveis de Sobrecarga do Sangue
    private float overloadTime = 15;
    private float currentOverloadTime;
    public bool bloodOverloaded;
    #endregion

    #region Variaveis de Surto
    private float outbreakTime = 10;
    private float currentOutbreakTime;
    public bool isOutebreaking;
    #endregion

    #region Variaveis de Movimento
    private const float MOVESPEED = 5f;
    private Vector3 moveDir;
    private Rigidbody2D rgbd2d;
    #endregion

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rgbd2d = GetComponent<Rigidbody2D>();

        rgbd2d.interpolation = RigidbodyInterpolation2D.Interpolate;
        rgbd2d.gravityScale = 0;
    }

    private void Update()
    {
        if (!isOutebreaking)
        {
            Movement();
            OverloadBlood();
        }
    }

    private void FixedUpdate()
    {
        if (isOutebreaking)
            rgbd2d.velocity = Vector2.zero;
        else
            rgbd2d.velocity = moveDir * MOVESPEED;
    }

    #region Lucy Movement
    private void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(moveX, moveY).normalized;
    }
    #endregion

    #region Lucy Blood Overload and Outbreak
    private void OverloadBlood()
    {
        currentOverloadTime += Time.deltaTime;
        currentOutbreakTime += Time.deltaTime;

        if(currentOutbreakTime >= outbreakTime)
        {
            //Outbreak();
            currentOutbreakTime = 0;
        }

        if(currentOverloadTime >= overloadTime)
        {
            //BloodOverloaded();
            currentOverloadTime = 0;
        }

        //ChangerOutbreakTime();
    }

    private void ChangerOutbreakTime()
    {
        if (currentOverloadTime >= (overloadTime / 2))
            outbreakTime = outbreakTime - (outbreakTime / 3);
        else if (currentOverloadTime >= (overloadTime / 4) + (overloadTime / 2))
            outbreakTime = outbreakTime - (outbreakTime / 2);
    }

    private void Outbreak()
    {
        isOutebreaking = true;
        int outTime = 10;
        StartCoroutine(WaitBloodOverload(outTime));
    }

    private void BloodOverloaded()
    {
        //TODO destruir o jogador e ir para a tela de game over
        bloodOverloaded = true;
    }
    #endregion

    public void FreezeMovements()
    {
        Debug.Log("Teste");
    }

    IEnumerator WaitBloodOverload(float sec)
    {
        if (isOutebreaking)
        {
            yield return new WaitForSeconds(sec);
            isOutebreaking = false;
        }
    }
}
