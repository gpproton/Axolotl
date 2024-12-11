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
   Created At: Wed 11 Dec 2024 23:41:23
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 23:41:23
*/

using System.Reflection;

namespace Axolotl.Helpers;

public static class FactoryLoader {
    public static IEnumerable<T> LoadClassInstances<T>(Assembly? assembly = null) =>
        ResolveAssembly<T>(assembly)
            .Select(Activator.CreateInstance)
            .Cast<T>();

    public static IEnumerable<string> LoadClassNames<T>(Assembly? assembly = null) =>
        ResolveAssembly<T>(assembly).Select(t => t.Name);

    private static IEnumerable<Type> ResolveAssembly<T>(Assembly? assembly = null) {
        var type = typeof(T);
        var value = assembly ?? type.Assembly;

        return value
            .GetTypes()
            .Where(p => p is { IsClass: true, IsAbstract: false } && p.IsAssignableTo(type));
    }
}
