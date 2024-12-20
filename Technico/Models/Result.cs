﻿namespace Technico.Models;

public class Result<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? Token { get; set; }  
    public T? Data { get; set; }
}
