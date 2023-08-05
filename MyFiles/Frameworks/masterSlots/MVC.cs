using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class MVC : MonoBehaviour
{
    public static Dictionary<string, Model> Models = new Dictionary<string, Model>();
    public static Dictionary<string, View> Views = new Dictionary<string, View>();
    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();

    internal static T RegisterModel<T>() where T : Model
    {
        Model model = Game.I.AddModel<T>();
        Models[model.Name] = model;
        return model as T;
    }

    internal static T RegisterView<T>(PanelTier panelTier = PanelTier.Default) where T : View
    {
        View view = Game.I.AddView<T>(panelTier);
        Views[view.Name] = view;
        return view as T;
    }

    internal static void RegisterController(string typeName)
    {
        Assembly a = Assembly.GetAssembly(typeof(MVC));
        Type controllerType = a.GetType(typeName);
        string eventName = controllerType.Name;
        CommandMap[eventName] = controllerType;
    }

    internal static Model GetModel<T>() where T : Model
    {
        foreach (Model m in Models.Values)
        {
            if (m is T)
            {
                return m;
            }
        }
        return null;
    }

    internal static View GetView<T>() where T : View
    {
        foreach (View v in Views.Values)
        {
            if (v is T)
            {
                return v;
            }
        }
        return null;
    }

    internal static void SendEvent(string eventName, object data = null)
    {
        if (CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];
            Controller c = Activator.CreateInstance(t) as Controller;
            c.HandleEvent(data);

            foreach (var item in Views.Values)
            {
                if (item.AttentionEvents.Contains(eventName))
                {
                    item.HandleEvent(eventName, data);
                }
            }
        }
    }


    /*    static void Add<T>(Dictionary<string, T> dict, T value, string name)
        {
            if (dict.ContainsKey(name) == false)
            {
                dict[name] = value;
            }
            else
            {
                Log.LogWarning($"The {name} is not unique.");
            }
        }*/

}
