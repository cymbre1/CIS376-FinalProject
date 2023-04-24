using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using 

public class FoxController : MonoBehaviour
{
    // Keeps track of different fox stats
    protected float horizontalSpeed = 5.0f;
    protected float verticalSpeed = 10.0f;
    protected float elapsedTime = 0;
    protected float duration;


    // Fox elements
    protected Transform tf;
    protected BoxCollider bc;
    protected Vector2 rotation;
    protected Animator anim;
    protected TerrainData terrain;
    protected AudioSource music;

    // Keeps track of the different states the fox can be in
    public bool hidden = false;
    public bool bearMusic = true;
    public bool chased = true;
    protected bool finished;

    // Keeps track of the foods the fox has colelcted
    public bool apple = false;
    public bool banana = false;
    public bool carrot = false;
    public bool onion = false;
    public bool garlic = false;
    public bool egg = false;
    public bool ham = false;
    public bool cake = false;
    public bool pumpkin = false;
    public bool tomato = false;

    // Music and audio sound effects
    protected AudioClip theBearMusic;
    protected AudioClip theBearIntro;
    protected AudioClip mainTheme;
    protected AudioClip mainThemeIntro;
    protected AudioClip mainThemeReprise;
    protected AudioClip footsteps;
    protected AudioClip collect;

    // Holds all of the bears
    GameObject[] bears;

    // Initializes all of the values for the fox and starts the main theme music
    void Start()
    {
        tf = GetComponent<Transform>();
        bc = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        music = GetComponent<AudioSource>();
        terrain = Terrain.activeTerrain.terrainData;

        theBearMusic = Resources.Load("TheBearTrimmed") as AudioClip;
        theBearIntro = Resources.Load("TheBearIntroTrimmed") as AudioClip;
        mainTheme = Resources.Load("InTheWoodsTrimmed") as AudioClip;
        mainThemeIntro = Resources.Load("InTheWoodsIntroTrimmed") as AudioClip;
        mainThemeReprise = Resources.Load("InTheWoodsRepriseTrimmed") as AudioClip;
        footsteps = Resources.Load("Footstep_29") as AudioClip;
        collect = Resources.Load("collect2") as AudioClip;

        bears = GameObject.FindGameObjectsWithTag("Bear");
        
        rotation.y = 95;

        duration = 0;

        StartMainTheme();        
    }

    // Update is called once per frame
    void Update()
    {
        // Sets all of the animation booleans to false
        foreach(AnimatorControllerParameter parameter in anim.parameters) {            
            anim.SetBool(parameter.name, false);            
        }

        // Figures how much time it has been
        elapsedTime += Time.deltaTime;

        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * verticalSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        
        // Move and animate the fox according to player input
        if(!finished)
        {
            transform.Translate(0, 0, translation);

            if(Input.GetAxis("Vertical") > 0.02 || Input.GetAxis("Vertical") < -0.02) {
                // Plays the footstep sound when the fox steps.
                if(duration > footsteps.length / 2) {
                    music.PlayOneShot(footsteps, 0.02f);
                    duration = 0;
                } else {
                    duration += Time.deltaTime;
                }

                // Controls the animation of the fox
                if(Input.GetAxis("Mouse X") > 0.3) {
                    anim.SetBool("WalkRight", true);
                } else if(Input.GetAxis("Mouse X") < -0.3) {
                    anim.SetBool("WalkLeft", true);
                } else {
                    anim.SetBool("WalkForward", true);
                }
            } else {
                // If the fox isn't moving, set the animation to stopped
                anim.SetBool("Stop", true);
            }
        } else {
            // If the player has reached the end, set the animation to stop
            anim.SetBool("Stop", true);
        }

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x = Mathf.Clamp(rotation.x, -10.0f, 10.0f);
		transform.eulerAngles = (Vector2)rotation * horizontalSpeed;

        // Finds the position of every red bush and checks to see if it is a red bush, and then checks to see if the player is in a red bush
        int treeCount = terrain.treeInstanceCount;
        for(int i =0; i< treeCount; i ++){
            TreeInstance tree = terrain.treeInstances[i];
            if(tree.prototypeIndex == 4){
                var treePos = tree.position;
                var xTree = treePos.x * terrain.size.x;
                var yTree = treePos.y * terrain.size.y;
                var zTree = treePos.z * terrain.size.z;
                treePos = new Vector3(xTree, yTree, zTree);

                // Checks to see if the fox is close enough to the bush to count as hidden
                if (Vector3.Distance(transform.position, treePos) <= 2.5f){
                    hidden = true;
                    break;
                }
                else{
                    hidden = false;
                }
            }
        }

        chased = false;

        // Checks to see if any of the bears are chasing the fox
        foreach(GameObject b in bears) {
            if(b.GetComponent<BearController>().chasing) {
                chased = true;
                break;
            }
        }

        // If the fox is not being chased and the bear theme is playing, start playing the main theme
        if(!chased && bearMusic) {
            StartMainTheme();
        }
    }

    void OnTriggerEnter(Collider col) {
        // If the player collides with a food item, it will check to see which one and then mark it as found.
        if(col.gameObject.tag == "Collectible") {
            if(col.gameObject.name == "apple") {
                apple = true;
            } else if(col.gameObject.name == "banana") {
                banana = true;
            } else if(col.gameObject.name == "carrot") {
                carrot = true;
            } else if(col.gameObject.name == "onion") {
                onion = true;
            } else if(col.gameObject.name == "garlic") {
                garlic = true;
            } else if(col.gameObject.name == "egg") {
                egg = true;
            } else if(col.gameObject.name == "ham") {
                ham = true;
            } else if(col.gameObject.name == "cake") {
                cake = true;
            } else if(col.gameObject.name == "pumpkin") {
                pumpkin = true;
            } else if(col.gameObject.name == "tomato") {
                tomato = true;
            }

            // Plays the collecting sound
            music.PlayOneShot(collect, 0.5f);
            
            // Destroys the food item because it has been collected.
            Destroy(col.gameObject);
        }

        // If the player goes through the plane at the end and has everything on the list, mark the game as done and start the finishing music
        if(col.gameObject.tag == "Finish") {
            if(apple && banana && cake && carrot && egg && onion && garlic && ham && pumpkin && tomato) {
                music.Stop();
                music.clip = mainThemeReprise;
                music.loop = false;
                music.Play();
                finished = true;
                StartCoroutine(triggerEnd(music.clip.length));
            }
        }
    }

    // Changes the scene to the end scene after sec seconds to wait for the music to play
    IEnumerator triggerEnd(float secs) {
        yield return new WaitForSeconds(secs);
        FindObjectOfType<GameManager>().ChangeScene("End_Scene"); 
    }

    public void StartBearMusic() {
        // Starts the bear music intro if it is not playing and then transitions to the rest of the song once the duration has passed.
        if(!bearMusic) {
            bearMusic = true;
            music.Stop();
            music.clip = theBearIntro;
            music.Play();
            StartCoroutine(BearMusicContinue(music.clip.length - (float)0.275));
        }
    }

    IEnumerator BearMusicContinue(float secs) {
        // waits for secs seconds and then plays the main bear theme
        yield return new WaitForSeconds(secs);
        if(bearMusic)
        {
            music.Stop();
            music.clip = theBearMusic;
            music.loop = true;
            music.Play();
        }
    }

    public void StartMainTheme() {
        // Starts the main theme intro and then transitions to the rest of the song once that is finished
        if(bearMusic)
        {
            bearMusic = false;
            music.Stop();
            music.clip = mainThemeIntro;
            music.Play();
            StartCoroutine(MainThemeContinue(music.clip.length - (float)0.15));
        }

    }

    IEnumerator MainThemeContinue(float secs) {
        // Waits for secs seconds and then transitions into the rest of the main theme.
        yield return new WaitForSeconds(secs);
        if(!bearMusic)
        {
            music.Stop();
            music.clip = mainTheme;
            music.loop = true;
            music.Play();
        }
    }
}
