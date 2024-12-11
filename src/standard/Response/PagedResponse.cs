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
   Created At: Wed 11 Dec 2024 23:43:21
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 23:43:21
*/

namespace Axolotl.Response;

public class PagedResponse<T> : Response<T> {
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 25;
    public int Total { get; set; }

    public new IEnumerable<T>? Data { get; set; }

    public int Pages => Convert.ToInt32(Math.Ceiling((double)this.Total / this.Size));

    public PagedResponse() { }

    public PagedResponse(IEnumerable<T>? data) => Data = data;

    public PagedResponse(IEnumerable<T>? data, int page, int size, int total, string message = "", bool success = true) {
        Message = message;
        Data = data;
        Page = page;
        Size = size;
        Total = total;
        Success = success;
    }
}