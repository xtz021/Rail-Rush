using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MenuUIStartingAnimations : MonoBehaviour
{
    [SerializeField] private List<Transform> moveInFromRightList;
    [SerializeField] private List<Transform> moveInFromLeftList;
    [SerializeField] private List<Transform> moveInFromTopList;

    private List<Vector3> fromRightListPos = new List<Vector3>();
    private List<Vector3> fromLeftListPos = new List<Vector3>();
    private List<Vector3> fromTopListPos = new List<Vector3>();
    private float delayBetweenElements = 0.2f; // delay between each element's animation
    private float moveDuration = 0.3f; // duration of the move animation

    private void Awake()
    {
        //AssignStartingPos();
    }


    private void Start()
    {
        AssignStartingPos();
        ResetBackToOffScreen(); // Ensure elements are off-screen at start
    }

    private void OnEnable()
    {
        StartCoroutine(PlayMoveFromRightAnimtions());
        StartCoroutine(PlayMoveFromLeftAnimtions());
        StartCoroutine(PlayMoveFromTopAnimtions());
    }

    private void OnDisable()
    {
        ResetBackToOffScreen();
    }

    private void AssignStartingPos()
    {
        fromLeftListPos.Clear();
        fromRightListPos.Clear();
        fromTopListPos.Clear();
        foreach (Transform t in moveInFromLeftList)
        {
            fromLeftListPos.Add(t.position);
        }
        foreach (Transform t in moveInFromRightList)
        {
            fromRightListPos.Add(t.position);
        }
        foreach (Transform t in moveInFromTopList)
        {
            fromTopListPos.Add(t.position);
        }
    }

    private void ResetBackToOffScreen()
    {
        for (int i = 0; i < moveInFromRightList.Count; i++)
        {
            Transform element = moveInFromRightList[i];
            Vector3 originalPosition = fromRightListPos[i];
            element.position = new Vector3(originalPosition.x + 1500, originalPosition.y, originalPosition.z); // Move off-screen
        }
        for (int i = 0; i < moveInFromLeftList.Count; i++)
        {
            Transform element = moveInFromLeftList[i];
            Vector3 originalPosition = fromLeftListPos[i];
            element.position = new Vector3(originalPosition.x - 1500, originalPosition.y, originalPosition.z); // Move off-screen
        }
        for (int i = 0; i < moveInFromTopList.Count; i++)
        {
            Transform element = moveInFromTopList[i];
            Vector3 originalPosition = fromTopListPos[i];
            element.position = new Vector3(originalPosition.x, originalPosition.y + 3000, originalPosition.z); // Move off-screen
        }
    }



    private IEnumerator PlayMoveFromRightAnimtions()
    {
        yield return new WaitForSeconds(0.1f); // Delay before starting animations
        if (moveInFromRightList.Count == 0)
        {
            yield break; // No elements to animate
        }
        for (int i = 0; i < moveInFromRightList.Count; i++)
        {
            Transform element = moveInFromRightList[i];
            Vector3 originalPosition = fromRightListPos[i];
            element.DOMove(originalPosition, moveDuration); // Move to original position
            yield return new WaitForSeconds(delayBetweenElements);
        }
        yield break;
    }

    private IEnumerator PlayMoveFromLeftAnimtions()
    {
        yield return new WaitForSeconds(0.1f); // Delay before starting animations
        if (moveInFromLeftList.Count == 0)
        {
            yield break; // No elements to animate
        }
        for (int i = 0; i < moveInFromLeftList.Count; i++)
        {
            Transform element = moveInFromLeftList[i];
            Vector3 originalPosition = fromLeftListPos[i];
            element.DOMove(originalPosition, moveDuration); // Move to original position
            yield return new WaitForSeconds(delayBetweenElements);
        }
        yield break;
    }

    private IEnumerator PlayMoveFromTopAnimtions()
    {
        yield return new WaitForSeconds(0.1f); // Delay before starting animations
        if (moveInFromTopList.Count == 0)
        {
            yield break; // No elements to animate
        }
        for (int i = 0; i < moveInFromTopList.Count; i++)
        {
            Transform element = moveInFromTopList[i];
            Vector3 originalPosition = fromTopListPos[i];
            element.DOMove(originalPosition, moveDuration); // Move to original position
            yield return new WaitForSeconds(delayBetweenElements);
        }
        yield break;
    }
}
