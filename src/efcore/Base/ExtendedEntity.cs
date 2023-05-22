// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Proton.Common.EFCore.Base;

public abstract class ExtendedEntity<T> : AuditableEntity<T> {
    private Dictionary<string, Object> _attributes = new Dictionary<string, object>();

    public bool HasAttribute(string key) => _attributes.ContainsKey(key);

    public Dictionary<string, Object> GetAttributes() => _attributes;

    public void SetAttributes(Dictionary<string, Object> attributes) => _attributes = attributes;

    public void Set(string key, bool value) => _attributes.TryAdd(key, value);

    public void Set(string key, byte value) => _attributes.TryAdd(key, value);

    public void Set(string key, int value) => _attributes.TryAdd(key, value);

    public void Set(string key, decimal value) => _attributes.TryAdd(key, value);

    public void Set(string key, double value) => _attributes.TryAdd(key, value);

    public void Set(string key, float value) => _attributes.TryAdd(key, value);

    public void Set(string key, string value) => _attributes.TryAdd(key, value);

    public void Set<TKey>(string key, TKey value) where TKey : notnull => _attributes.TryAdd(key, value);

    public TValue? GetAny<TValue>(string key) {
        _attributes.TryGetValue(key, out var value);
        return (TValue)value!;
    }

    public string GetString(string key) {
        _attributes.TryGetValue(key, out var value);
        return (string)value!;
    }

    public bool GetBoolean(string key) {
        _attributes.TryGetValue(key, out var value);
        return value is true;
    }

    public decimal GetDecimal(string key) {
        _attributes.TryGetValue(key, out var value);
        if (value is decimal d) return d;
        return 0;
    }

    public double GetDouble(string key) {
        _attributes.TryGetValue(key, out var value);
        if (value is double d) return d;
        return 0.0;
    }

    public int GetInteger(string key) {
        _attributes.TryGetValue(key, out var value);
        if (value is int i) return i;
        return 0;
    }

    public float GetFloat(string key) {
        _attributes.TryGetValue(key, out var value);
        if (value is float i) return i;
        return 0;
    }
}