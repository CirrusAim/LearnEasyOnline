﻿namespace LearnEasyOnline.Api.Models
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public string? Token { get; set; } // Make Token nullable
    }
}
