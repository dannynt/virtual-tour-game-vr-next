using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class Memory : MonoBehaviour
{
    // {?alien_awakened?}
    // {+cat_fined+}
    
    private Dictionary<string, bool> constraintsInMemory;

    private static Memory _instance;

    public static Memory instance 
    {
        get
        {
            if (!_instance) _instance = FindObjectOfType<Memory>();

            if (!_instance) _instance = new GameObject("Memory").AddComponent<Memory>();

            return _instance;
        }
    }

    private void Awake()
    {
        constraintsInMemory = new Dictionary<string, bool>();
    }

    private bool HasConstraint(string constraint)
    {
        return constraintsInMemory.ContainsKey(constraint);
    }

    public string FilterConstraints(string text)
    {
        string regexExpression = @"({\?.*\?})|({\+.*\+})|{{.*}}";
        return Regex.Replace(text, regexExpression, "");
    }

    private List<string> GetConstraints(string text, string regexExpression)
    {
        List<string> constraints = new List<string>();
        var regex = new Regex(regexExpression);
        MatchCollection matches = regex.Matches(text);
        
        foreach (Match match in matches)
        {
            constraints.Add(match.Value);    
        }

        return constraints;
    }

    public bool AreConstraintsInMemory(string text)
    {
        var constraints = GetConstraints(text, @"({\?.*\?})");
        foreach (var constraint in constraints)
        {
            if (constraint == "") continue;

            var filteredConstraint = constraint.Replace("?", "").Replace("{", "").Replace("}", "");

            if (!constraintsInMemory.ContainsKey(filteredConstraint))
            {
                return false;
            }
        }
        
        return true;
    }

    public bool AreConstraintsAdded(string text)
    {
        var constraints = GetConstraints(text, @"({\+.*\+})");
        foreach (var constraint in constraints)
        {
            if (constraint == "") continue;

            var filteredConstraint = constraint.Replace("+", "").Replace("{", "").Replace("}", "");

            if (constraintsInMemory.ContainsKey(filteredConstraint))
            {
                return true;
            }
        }
        
        return false;
    }

    public void AddConstraintsInMemory(string text)
    {
        var constraints = GetConstraints(text, @"({\+.*\+})");
        foreach (var constraint in constraints)
        {
            if (constraint == "") continue;

            var filteredConstraint = constraint.Replace("+", "").Replace("{", "").Replace("}", "");

            if (!constraintsInMemory.ContainsKey(filteredConstraint))
            {
                constraintsInMemory.Add(filteredConstraint, true);
            }
        }
    }
}
