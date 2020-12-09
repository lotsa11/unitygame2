using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

 public bool onGround;
    public Text coinText;
public GameObject schuss;
	public int coinCount = 0;
    public GameObject explosion;
    
    [SerializeField] private float m_speed = 1f; //speed modifier
    
    private Rigidbody m_playerRigidbody = null; //reference to the players rigidbody

    private float m_movementX, m_movementY; //input vector components

    public int m_collectablesTotalCount, m_collectablesCounter; //everything we need to count the given collectables

    private Stopwatch m_stopwatch; //takes the time
    
    private void Start()
    {
     explosion.SetActive(false);
     onGround = true;
        m_playerRigidbody = GetComponent<Rigidbody>(); //get the rigidbody component

        m_collectablesTotalCount = m_collectablesCounter = GameObject.FindGameObjectsWithTag("Collectable").Length; //find all gameobjects in the scene which are tagged with "Collectable" and count them via Length property 
        
        m_stopwatch = Stopwatch.StartNew(); //start the stopwatch
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 movementVector = inputValue.Get<Vector2>(); //get the input


    }

    private void FixedUpdate()
    {
     m_movementX = Input.GetAxis("Horizontal");
        m_movementY = Input.GetAxis("Vertical");
    
    if (onGround)
        {

            if (Input.GetButtonDown("Jump"))
            {
                 m_playerRigidbody.velocity = new Vector3( m_playerRigidbody.velocity.x, 5f,  m_playerRigidbody.velocity.z);
                onGround = false;
            }


        }
        
     if (Input.GetKeyDown(KeyCode.S))
        {
        UnityEngine.Debug.Log("SPEED");
            m_speed = m_speed * 2f;
        }
        
coinText.text = coinCount.ToString();
Vector3 movement = new Vector3(m_movementX, 0f, m_movementY); //translate the 2d vector into a 3d vector
        
        m_playerRigidbody.AddForce(movement * m_speed); //apply a force to the rigidbody
        
         if (this.transform.position.y < -3)
        {                       //wenn y Wert der Kugel unter Ebene fällt

            this.transform.position = new Vector3(0, 1, -15);
            m_playerRigidbody.angularVelocity = new Vector3(0, 0, 0); //Drehgeschwindigkeit
            m_playerRigidbody.velocity = new Vector3(0, 0, 0);             //Bewegungsgeschwindigkeit

        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
            GameObject obj;
            obj = Instantiate(schuss, transform.position, transform.rotation);
            obj.GetComponent<Rigidbody>().velocity = transform.forward * 20;
        }
        
         transform.LookAt(new Vector3(transform.position.x + m_movementX, transform.position.y, transform.position.z + m_movementY));
    }
public void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.CompareTag("boden"))
        {
            onGround = true;
        }
        
        
    }
    private void OnTriggerEnter(Collider other)//executed when the player hits another collider (which is set to 'is trigger')
    {
     if (other.gameObject.CompareTag("power"))
        {
             explosion.SetActive(true);
            Destroy(other.transform.gameObject);
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(expl, 3);
            m_speed = 5f;
            UnityEngine.Debug.Log("Powerup");
            
        }
        if (other.gameObject.CompareTag("Collectable"))//has the other gameobject the tag "Collectable"
        {
        
		
			coinCount++;



            other.gameObject.SetActive(false); //set the hit collectable inactive

            m_collectablesCounter--; //count down the remaining collectables
            if (m_collectablesCounter == 8) //have we found all collectables? if so we won!
            {
                UnityEngine.Debug.Log("YOU WIN!");
                UnityEngine.Debug.Log($"It took you {m_stopwatch.Elapsed} to find all {m_collectablesTotalCount} collectables.");
#if UNITY_EDITOR //the following code is only included in the unity editor
                UnityEditor.EditorApplication.ExitPlaymode();//exits the playmode
#endif

            }
            else
            {
                UnityEngine.Debug.Log($"You've already found {m_collectablesTotalCount - m_collectablesCounter} of {m_collectablesTotalCount} collectables!");
            }
        }
        else if (other.gameObject.CompareTag("Enemy")) //has the other gameobject the tag "Enemy" / game over state
        {
            UnityEngine.Debug.Log("GAME OVER!");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
        }
    }
}
