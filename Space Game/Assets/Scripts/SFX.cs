using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SFX : MonoBehaviour
{
    public AudioSource acc;
    public AudioSource dec;
    public AudioSource main_mt;
    public AudioSource left_mt;
    public AudioSource right_mt;
    public AudioSource front_mt;
    public AudioSource l_acc;
    public AudioSource r_acc;
    public AudioSource l_dec;
    public AudioSource r_dec;



    public bool deceleration;
    public bool acceleration;
    public bool back_motor;
    public bool r_turn;
    public bool l_turn;
    public bool front_motor;

    //SFX Functions
    public void Decelaration_SFX()
    {
        var deceleration_sfx = transform.Find("Deceleration").gameObject;
        var deceleration_sfx_source = deceleration_sfx.GetComponent<AudioSource>();
        deceleration_sfx_source.Play();
    }

    public void Accelaration_SFX()
    {
        var acceleration_sfx = transform.Find("Acceleration").gameObject;
        var acceleration_sfx_source = acceleration_sfx.GetComponent<AudioSource>();
        acceleration_sfx_source.Play();
    }
    public void Motor_SFX()
    {
        var motor_sfx = transform.Find("Motor").gameObject;
        var motor_sfx_source = motor_sfx.GetComponent<AudioSource>();
        motor_sfx_source.Play();
    }
    public void Right_T_SFX()
    {
        var right_t_sfx = transform.Find("Acceleration_R").gameObject;
        var right_t_sfx_source = right_t_sfx.GetComponent<AudioSource>();
        right_t_sfx_source.Play();
    }
    public void Left_T_SFX()
    {
        var left_t_sfx = transform.Find("Acceleration_L").gameObject;
        var left_t_sfx_source = left_t_sfx.GetComponent<AudioSource>();
        left_t_sfx_source.Play();
    }

}
