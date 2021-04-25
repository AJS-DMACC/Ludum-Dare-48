using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextAnimator : MonoBehaviour
{
    public TMP_Text textComponent;
    public TextMovementTypes movementType;
    public float magnitude;
    public float frequency;
    public bool animateAsWholeCharacters;


    void Update()
    {
        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo; 

        for(int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;//go to next loop itteration if verticy isn't visible
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;//get verticies 
            if (animateAsWholeCharacters)
            {
                float pos = verts[charInfo.vertexIndex].x;//find the first verticy index of first index
                for (int j = 0; j < 4; ++j)
                { 
                    var origin = verts[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = origin + vertexOffset(movementType, pos);
                }
            }
            else
            {
                for (int j = 0; j < 4; ++j)
                {
                    var pos = verts[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = pos + vertexOffset(movementType, pos.x);
                }
            }
        }

        //commit changes
        for(int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
      
    private Vector3 vertexOffset(TextMovementTypes movementType, float xCord)
    {
        switch (movementType)
        {
            case TextMovementTypes.wavy:
                return new Vector3(0, Mathf.Sin((Time.time + xCord)*frequency) * magnitude, 0);
                break;
            case TextMovementTypes.side_to_side:
                return new Vector3(Mathf.Cos((Time.time + xCord) * frequency) * magnitude, 0, 0);
                break;
            case TextMovementTypes.circle:
                return new Vector3(Mathf.Cos((Time.time + xCord) * frequency) * magnitude, Mathf.Sin((Time.time + xCord) * frequency) * magnitude, 0);
                break;
            default:
                break;
        }
        return Vector3.zero;
    }

    public void SetMagnitude(float value)
    {
        magnitude = value;
    }

    public void SetFrequency(float value)
    {
        frequency = value;
    }
}

public enum TextMovementTypes
{
    wavy,
    side_to_side,
    circle
}
