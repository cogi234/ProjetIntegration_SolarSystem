using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellarObject : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]private Vector3 velocity;
    [SerializeField]private float volume; // en m ou km
    [SerializeField] private float mass;
    [SerializeField]private float density;
   
    //Ajuste automatiquement les valeurs des variables lors du changement d<une
    public float Density 
    { 
        get => density; 
        set 
        { 
            density = value; 
            volume = mass / density; 
        } 
    }
    public float Mass 
    { 
        get => mass;  
        set 
        { 
            mass = value; 
            density = mass / volume; 
            volume = mass / density; 
        }
    }
    public float Radius => Mathf.Pow((volume / 4.1889f), (1 / 3f));
    public float Volume 
    { 
        get => volume; 
        set 
        { 
            volume = value; 
            density = mass / volume;
            transform.localScale = new Vector3(Radius, Radius, Radius);
        }  
    }
   
    public Vector3 Velocity 
    { 
        get => velocity; 
        set => velocity = value; 
    }

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gameManager.stellarObjectList.Add(this);
    }

    //prend chaque objet stellaire environnant (masse et distance) et calcule la force appliquée sur l'objet principal
    public void ApplyGravity(float time)
    {
        foreach (StellarObject X in gameManager.stellarObjectList)
        {
            if(X != this)
            {
                float GravityForce = (Mass * X.Mass * gameManager.gravityConstant) 
                    / Mathf.Pow(Vector3.Distance(X.transform.position, transform.position),2);
                Vector3 direction = (X.transform.position - transform.position)*GravityForce;
                ApplyForce(direction, time);
            }
        }  
    }
    
    //cree un vecteur force a partir dun un objet stellaire
    public void ApplyForce(Vector3 force, float time)
    {
        //Vector3 acc = force / Mass;
        Velocity += (force / Mass) * time;
    }

    //applique un vecteur force a partir dun objet stellaire
    public void ApplyVelocity(float time)
    {
        transform.position += Velocity * time;
    }
}
