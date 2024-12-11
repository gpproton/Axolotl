/*
  Copyright (c) 2024 <Godwin peter. O>
  
  Permission is hereby granted, free of charge, to any person obtaining a copy
  of this software and associated documentation files (the "Software"), to deal
  in the Software without restriction, including without limitation the rights
  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
  copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:
  
  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.
  
  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
  SOFTWARE.
  
   Author: Godwin peter. O (me@godwin.dev)
   Created At: Wed 11 Dec 2024 20:43:54
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 20:43:54
*/

namespace Axolotl.EFCore.Base;

public abstract class ExtendedEntity<T> : BaseEntity<T> where T : notnull {
    public Dictionary<string, object> Attributes = [];
    public bool HasAttribute(string key) => Attributes.ContainsKey(key);
    public Dictionary<string, Object> GetAttributes() => Attributes;
    public void SetAttributes(Dictionary<string, object> attributes) => Attributes = attributes;
    public void Set(string key, bool value) => Attributes.TryAdd(key, value);
    public void Set(string key, byte value) => Attributes.TryAdd(key, value);
    public void Set(string key, int value) => Attributes.TryAdd(key, value);
    public void Set(string key, decimal value) => Attributes.TryAdd(key, value);
    public void Set(string key, double value) => Attributes.TryAdd(key, value);
    public void Set(string key, float value) => Attributes.TryAdd(key, value);
    public void Set(string key, string value) => Attributes.TryAdd(key, value);
    public void Set<TKey>(string key, TKey value) where TKey : notnull => Attributes.TryAdd(key, value);

    public TValue? GetAny<TValue>(string key) {
        Attributes.TryGetValue(key, out var value);
        return (TValue)value!;
    }

    public string GetString(string key) {
        Attributes.TryGetValue(key, out var value);
        return (string)value!;
    }

    public bool GetBoolean(string key) {
        Attributes.TryGetValue(key, out var value);
        return value is true;
    }

    public decimal GetDecimal(string key) {
        Attributes.TryGetValue(key, out var value);
        if (value is decimal d) return d;
        return 0;
    }

    public double GetDouble(string key) {
        Attributes.TryGetValue(key, out var value);
        if (value is double d) return d;
        return 0.0;
    }

    public int GetInteger(string key) {
        Attributes.TryGetValue(key, out var value);
        if (value is int i) return i;
        return 0;
    }

    public float GetFloat(string key) {
        Attributes.TryGetValue(key, out var value);
        if (value is float i) return i;
        return 0;
    }
}