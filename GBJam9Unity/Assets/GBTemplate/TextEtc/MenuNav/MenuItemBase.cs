using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuItemBase : MonoBehaviour
{
    [SerializeField]
    Material Regular;
    [SerializeField]
    Material Highlighted;

    protected bool isHighlighted;
    [SerializeField]
    Text Text;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        Text = GetComponent<Text>();
    }

    public abstract bool PerformAction();

    public virtual bool IsHighlighted
    {
        get { return isHighlighted; }
        set
        {
            isHighlighted = value;
            if (isHighlighted)
            {
                Text.material = Highlighted;
            }
            else
            {
                Text.material = Regular;
            }
        }
    }

    public virtual string CurrentText { get { return Text.text; } }
}
