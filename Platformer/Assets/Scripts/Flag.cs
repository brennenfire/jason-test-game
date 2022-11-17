using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] string sceneName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player == null)
        {
            return;
        }
        var animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");

        SceneManager.LoadScene(sceneName);
    }
}
