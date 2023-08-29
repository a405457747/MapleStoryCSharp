/****************************************************************************
 * Copyright (c) 2019 ~ 2019.12 S.Allen
 * 
 * 894982165@qq.com
 * https://github.com/a405457747
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 ****************************************************************************/

using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public class Pool
    {
        private readonly List<GameObject> _objects;

        public Pool()
        {
            _objects = new List<GameObject>();
        }

        public  void DeSpawn(GameObject go)
        {
            if (_objects.Contains(go))
            {
                go.GetComponent<PoolBase>().OnDespawn();
                go.SetActive(false);
            }
        }

        public void DeSpawnAll()
        {
            foreach (var go in _objects)
                if (GameIsActive(go))
                    DeSpawn(go);
        }

        public GameObject Spawn(string name, LoadHelper loadHelper)
        {
            GameObject temp = null;
            foreach (var go in _objects)
                if (!GameIsActive(go))
                {
                    temp = go;
                    break;
                }

            if (temp == null)
            {
                temp = loadHelper.LoadThing<GameObject>(name).Instantiate();
                temp.name = name;
                _objects.Add(temp);
            }
            else
            {
                temp.SetActive(true);
            }

            temp.GetComponent<PoolBase>().OnSapwan();
            return temp;
        }

        private bool GameIsActive(GameObject go)
        {
            return go != null && go.activeSelf;
        }
    }
}