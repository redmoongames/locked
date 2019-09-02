using System.Collections;
using UnityEngine;

public static class AlphaChannelChange{
    enum WhichColorObject
    {
        nullColorObject,
        spriteColorObject,
        imageColorObject
    }

    public static IEnumerator AlphaChannelChangeIncrease(GameObject ColorObject, float timeWait, float timeWork, float nextValueAlphaChannel)
    {
        Debug.Log("AlphaChannelChangeIncrease - start.");
        Color colorGameObject = new Color();
        Component pointerColorObject = null;
        WhichColorObject whichColorObject = WhichColorObject.nullColorObject;
        if (timeWait > 0) yield return new WaitForSeconds(timeWait);
        else yield return null;
        if (ColorObject.GetComponent<SpriteRenderer>())
        {
            pointerColorObject = ColorObject.GetComponent<SpriteRenderer>();
            colorGameObject = pointerColorObject.GetComponent<SpriteRenderer>().color;
            whichColorObject = WhichColorObject.spriteColorObject;
        }
        else if (ColorObject.GetComponent<UnityEngine.UI.Image>())
        {
            pointerColorObject = ColorObject.GetComponent<UnityEngine.UI.Image>();
            colorGameObject = pointerColorObject.GetComponent<UnityEngine.UI.Image>().color;
            whichColorObject = WhichColorObject.imageColorObject;
        }
        else
        {
            Debug.Log("Error! This GameObject don't have color component.");
        }
        if (timeWork > 0 && (whichColorObject != WhichColorObject.nullColorObject) && colorGameObject.a < nextValueAlphaChannel)
        {
            float stepTime = 1.0f / 60.0f;
            float stepValue = stepTime * (nextValueAlphaChannel - colorGameObject.a) / timeWork;
            float timer = 0;
            while (timer < timeWork)
            {
                timer += stepTime;
                colorGameObject.a += stepValue;
                if (whichColorObject == WhichColorObject.spriteColorObject) pointerColorObject.GetComponent<SpriteRenderer>().color = colorGameObject;
                else pointerColorObject.GetComponent<UnityEngine.UI.Image>().color = colorGameObject;
                yield return new WaitForSeconds(stepTime);
            }
            if (whichColorObject == WhichColorObject.spriteColorObject) pointerColorObject.GetComponent<SpriteRenderer>().color = new Color(colorGameObject.r, colorGameObject.g, colorGameObject.b, nextValueAlphaChannel);
            else pointerColorObject.GetComponent<UnityEngine.UI.Image>().color = new Color(colorGameObject.r, colorGameObject.g, colorGameObject.b, nextValueAlphaChannel);
        }
        else
        {
            yield return null;
            Debug.Log("Error! Bad arguments.");
        }
        Debug.Log("AlphaChannelChangeIncrease - finish.");
    }

    public static IEnumerator AlphaChannelChangeDecrease(GameObject ColorObject, float timeWait, float timeWork, float nextValueAlphaChannel)
    {
        Debug.Log("AlphaChannelChangeDecrease - start.");
        Color colorGameObject = new Color();
        Component pointerColorObject = null;
        WhichColorObject whichColorObject = WhichColorObject.nullColorObject;
        if (timeWait > 0) yield return new WaitForSeconds(timeWait);
        else yield return null;
        if (ColorObject.GetComponent<SpriteRenderer>())
        {
            pointerColorObject = ColorObject.GetComponent<SpriteRenderer>();
            colorGameObject = pointerColorObject.GetComponent<SpriteRenderer>().color;
            whichColorObject = WhichColorObject.spriteColorObject;
        }
        else if (ColorObject.GetComponent<UnityEngine.UI.Image>())
        {
            pointerColorObject = ColorObject.GetComponent<UnityEngine.UI.Image>();
            colorGameObject = pointerColorObject.GetComponent<UnityEngine.UI.Image>().color;
            whichColorObject = WhichColorObject.imageColorObject;
        }
        else
        {
            Debug.Log("Error! This GameObject don't have color component.");
        }
        if (timeWork > 0 && (whichColorObject != WhichColorObject.nullColorObject) && colorGameObject.a > nextValueAlphaChannel)
        {
            float stepTime = 1.0f / 60.0f;
            float stepValue = stepTime * (colorGameObject.a - nextValueAlphaChannel) / timeWork;
            float timer = 0;
            while (timer < timeWork)
            {
                timer += stepTime;
                colorGameObject.a -= stepValue;
                if (whichColorObject == WhichColorObject.spriteColorObject) pointerColorObject.GetComponent<SpriteRenderer>().color = colorGameObject;
                else pointerColorObject.GetComponent<UnityEngine.UI.Image>().color = colorGameObject;
                yield return new WaitForSeconds(stepTime);
            }
            if (whichColorObject == WhichColorObject.spriteColorObject) pointerColorObject.GetComponent<SpriteRenderer>().color = new Color(colorGameObject.r, colorGameObject.g, colorGameObject.b, nextValueAlphaChannel);
            else pointerColorObject.GetComponent<UnityEngine.UI.Image>().color = new Color(colorGameObject.r, colorGameObject.g, colorGameObject.b, nextValueAlphaChannel);
        }
        else
        {
            yield return null;
            Debug.Log("Error! Bad arguments.");
        }
        Debug.Log("AlphaChannelChangeDecrease - finish.");
    }
}
