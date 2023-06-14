using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClignotementHandle : MonoBehaviour
/*{
    public float every;   //The public variable "every" refers to "Lerp the color every X"
    float colorstep;
    Color[] colors = new Color[4]; //Insert how many colors you want to lerp between here, hard coded to 4
    int i;
    Color lerpedColor = Color.red;  //This should optimally be the color you are going to begin with

    void Start()
    {

        //In here, set the array colors you are going to use, optimally, repeat the first color in the EndContainer to keep transitions smooth

        colors[0] = Color.red;
        colors[1] = Color.yellow;
        colors[2] = Color.cyan;
        colors[3] = Color.red;

    }


    // Update is called once per frame
    void Update()
    {

        if (colorstep < every)
        { //As long as the step is less than "every"
            lerpedColor = Color.Lerp(colors[i], colors[i + 1], colorstep);
            this.GetComponent<Camera>().backgroundColor = lerpedColor;
            colorstep += 0.025f;  //The lower this is, the smoother the transition, set it yourself
        }
        else
        { //Once the step equals the time we want to wait for the color, increment to lerp to the next color

            colorstep = 0;

            if (i < (colors.Length - 2))
            { //Keep incrementing until i + 1 equals the Lengh
                i++;
            }
            else
            { //and then reset to zero
                i = 0;
            }
        }
    }
}*/


{
    Color lerpedColor = Color.white;
    public Slider slider;
    private Color handle;
    public Color colorNormal;
    public Color color1;
    public Color color2;
    public Color colorInvisible;
    public float timerBetweenChanges;
    private bool change;
    public Player player;

    private void Start()
    {
        handle = GetComponent<Image>().color;
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (change)
        {
            Color colorChange = Color.Lerp(color, color2, timerBetweenChanges * Time.deltaTime);
            //if (color == color2) change = false;
            handle.color = colorChange;
        }
        else
        {
            Color colorChange = Color.Lerp(color, color1, timerBetweenChanges * Time.deltaTime);
            if (color == color1) change = true;
            handle.color = colorChange;
        }*/
        if (player.EnergieDown)
        {
            handle = colorInvisible;
            ColorBlock cb = slider.colors;
            cb.normalColor = handle;
            slider.colors = cb;
        }
        else if (slider.value <= 10)
        {
            lerpedColor = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, timerBetweenChanges));
            handle = lerpedColor;
            ColorBlock cb = slider.colors;
            cb.normalColor = handle;
            slider.colors = cb;
        }
        else
        {
            handle = colorNormal;
            ColorBlock cb = slider.colors;
            cb.normalColor = handle;
            slider.colors = cb;
        }
        


    }
}
