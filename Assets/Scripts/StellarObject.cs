using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellarObject : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]private Vector3 velocity;
    [SerializeField]private float volume = 1; // en m ou km
    [SerializeField]private float mass = 1;
    [SerializeField]private float density = 1;
    private Vector3 acceleration = new Vector3();
    
   
    //Ajuste automatiquement les valeurs des variables lors du changement d'une des variables
    public float Density 
    { 
        get => density; 
        set 
        { 
            density = value;
            volume = mass / density;
            transform.localScale = new Vector3(Radius*2, Radius*2, Radius*2);
        } 
    }
    public float Mass 
    { 
        get => mass;  
        set 
        { 
            mass = value; 
            density = mass / volume;
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
            transform.localScale = new Vector3(Radius*2, Radius*2, Radius*2);
        }  
    }
   
    public Vector3 Velocity 
    { 
        get => velocity; 
        set => velocity = value; 
    }
    public Vector3 Acceleration
    {
        get => acceleration;
        set => acceleration = value;
    }

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();//We find the gamemanager
        gameManager.stellarObjectList.Add(this);//We add ourselves to the list of stellarobjects
        gameManager.CreateStellarObjectUI(this);//We create a UIStellarObject paired to this stellar object

        Initialise();
    }

    public void Initialise()//This will make sure that the volume and density match
    {
        Density = density;
    }
    

    //prend chaque objet stellaire environnant (masse et distance) et calcule la force appliquée sur l'objet principal
    public void ApplyGravity(float time)
    {
        foreach (StellarObject X in gameManager.stellarObjectList)
        {
            if (X != this && Vector3.Distance(X.transform.position, transform.position) > 0.0001f)
            {
                float GravityForce = (Mass * X.Mass * gameManager.gravityConstant)
                    / Mathf.Pow(Vector3.Distance(X.transform.position, transform.position), 2);
                Vector3 direction = (X.transform.position - transform.position).normalized * GravityForce;
                ApplyForce(direction, time);
            }
        }
    }
    
    //cree un vecteur force a partir dun un objet stellaire
    public void ApplyForce(Vector3 force, float time)
    {
        Velocity += (force / Mass) * time;
    }

    //applique un vecteur force a partir dun objet stellaire
    public void ApplyVelocity(float time)
    {
        transform.position += Velocity * time;
    }

    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>().SelectedObject = this;
    }


    public void Save(Queue<string> saveContent)
    {
        saveContent.Enqueue(name);
        saveContent.Enqueue(mass.ToString());
        saveContent.Enqueue(density.ToString());
        saveContent.Enqueue(velocity.x.ToString());
        saveContent.Enqueue(velocity.y.ToString());
        saveContent.Enqueue(velocity.z.ToString());
        saveContent.Enqueue(transform.position.x.ToString());
        saveContent.Enqueue(transform.position.y.ToString());
        saveContent.Enqueue(transform.position.z.ToString());
    }

   
}
