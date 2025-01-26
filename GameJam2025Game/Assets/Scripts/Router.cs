using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Router : MonoBehaviour, IInteractable, IAlternativelyInteractible
{
    private enum MachineState
    {
        DORMANT, SPINNING_INPUT, SPINNING_OUTPUT
    }

    [SerializeField] private GameObject[] InputPipes = null;
    [SerializeField] private GameObject[] OutputPipes = null;

    private MachineState _machineState;
    private int _currentlySelectedInputIndex = 0;
    private int _currentlySelectedOutputIndex = 0;

    private GameObject _currentlySelectedInput => InputPipes[_currentlySelectedInputIndex];
    private GameObject _currentlySelectedOutput => OutputPipes[_currentlySelectedOutputIndex];

    private List<(GameObject, GameObject)> links = null;

    public void Interact(Player interactingPlayer)
    {
        if (_machineState == MachineState.DORMANT)
        {
            _machineState = MachineState.SPINNING_INPUT;
            _currentlySelectedInputIndex = 0;
            GameObjectFlasher.SetGameObjectFlashing(_currentlySelectedInput, true, Color.green);


        }
        else if (_machineState == MachineState.SPINNING_INPUT)
        {
            _machineState = MachineState.SPINNING_OUTPUT;
            _currentlySelectedOutputIndex = 0;
            GameObjectFlasher.SetGameObjectFlashing(_currentlySelectedOutput, true, Color.red);
        }
        else if (_machineState == MachineState.SPINNING_OUTPUT)
        {
            _machineState = MachineState.DORMANT;
            GameObjectFlasher.SetGameObjectFlashing(_currentlySelectedInput, false, Color.green);
            GameObjectFlasher.SetGameObjectFlashing(_currentlySelectedOutput, false, Color.red);

            ResetGOsIfPartOfLink(_currentlySelectedInput);
            ResetGOsIfPartOfLink(_currentlySelectedOutput);

            //link them together
            var color = GetUnusedColor();
            _currentlySelectedInput.GetComponent<Renderer>().material.color = color;
            _currentlySelectedOutput.GetComponent<Renderer>().material.color = color;

            links.Add((_currentlySelectedInput, _currentlySelectedOutput));

            _currentlySelectedInput.GetComponent<InputPipe>().LinkInventory(_currentlySelectedOutput.GetComponent<PlaceableInventory>());

        }
    }

    public void AlternativelyInteract(Player interactingPlayer)
    {
        if (_machineState == MachineState.SPINNING_INPUT)
        {
            GameObjectFlasher.SetGameObjectFlashing(_currentlySelectedInput, false, Color.green);
            _currentlySelectedInputIndex++;
            if (_currentlySelectedInputIndex >= InputPipes.Length) { _currentlySelectedInputIndex = 0; }
            GameObjectFlasher.SetGameObjectFlashing(_currentlySelectedInput, true, Color.green);
        }

        else if (_machineState == MachineState.SPINNING_OUTPUT)
        {
            GameObjectFlasher.SetGameObjectFlashing(_currentlySelectedOutput, false, Color.red);
            _currentlySelectedOutputIndex++;
            if (_currentlySelectedOutputIndex >= OutputPipes.Length) { _currentlySelectedOutputIndex = 0; }
            GameObjectFlasher.SetGameObjectFlashing(_currentlySelectedOutput, true, Color.red);
        }
    }

    Color[] colors = new Color[]
    {
        Color.blue,
        Color.cyan,
        Color.magenta,
        new Color(0.5f, 0.25f, 0.75f), // Purple-like
        new Color(0.2f, 0.5f, 0.8f),  // Sky blue
        new Color(0.7f, 0.3f, 0.6f),  // Light pinkish purple
        new Color(0.2f, 0.6f, 0.7f),  // Teal
        new Color(0.3f, 0.3f, 0.8f)   // Indigo
    };

    private Color GetUnusedColor()
    {
        foreach (var color in ShuffleArray(colors))
        {
            var isAvailableColor = true;
            foreach (var go in InputPipes.Union(OutputPipes))
            {
                if (go.GetComponent<Renderer>().material.color == color)
                {
                    isAvailableColor = false; break;
                }

            }
            if (isAvailableColor)
            {
                return color;
            }
        }

        Debug.LogError("shit fuck");
        return Color.white;
    }

    private void Awake()
    {
        links = new();
        if(!InputPipes.Any() || !OutputPipes.Any())
        {
            Debug.LogError("You forgot to link pipes in router.");
        }
    }

    private void ResetGOsIfPartOfLink(GameObject go)
    {
        var link = links.FirstOrDefault(x => x.Item1.name == go.name || x.Item2.name == go.name);
        if (link != default)
        {
            link.Item1.GetComponent<Renderer>().material.color = Color.white;
            link.Item2.GetComponent<Renderer>().material.color = Color.white;

            // remove inventory link
            link.Item1.GetComponent<InputPipe>().ResetLink();
        }
        links.Remove(link);
    }

    private Color[] ShuffleArray(Color[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            // Swap the current element with a random element
            Color temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }

        return array;
    }
}
