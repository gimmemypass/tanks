﻿using UnityEngine;
    public enum Color{
        purple,
        blue,
        orange,
    };
    // Start is called before the first frame update
    public int Health;
    public GameObject shellPrefab;
        transform.position = new Vector2(UnityEngine.Random.Range(1, MazeGenerator.Width - 1) * Cell.size - Cell.size/2,
                                         UnityEngine.Random.Range(1, MazeGenerator.Height - 1) * Cell.size - Cell.size/2);
    }

    private void OnDestroy()
    {
        
    }
        if (Time.timeScale == 1)
        {

            fire = (Input.GetKeyDown(keyFire)) ? true : false;
            //fire = (Input.GetKeyUp(keyFire)) ? false : fire;
            if (fire)
    {
        if (t is null)
        {
            t = new Timer();
            t.Elapsed += new ElapsedEventHandler(onTimer);
        }
        if (fireAllowed && Ammunition > 0)
        {
            animator.SetTrigger("fire");
            Shell s = Instantiate(shellPrefab, firePoint.position, firePoint.rotation).GetComponent<ShellStandart>();
            s.owner = this;
            Ammunition--;
            fireAudio.PlayOneShot(fireAudio.clip);
            fireAllowed = false;
            t.Interval = 1000;
            t.Start();
        }
    }
    {
        fireAllowed = true;
        t.Stop();
    }