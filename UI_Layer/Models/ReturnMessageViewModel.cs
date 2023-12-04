﻿using System.Text.Json.Serialization;

namespace UI_Layer.Models
{
    public class ReturnMessageViewModel<T>
    {

        [JsonPropertyName("success")]
        public virtual bool Success { get; set; }

        [JsonPropertyName("messageStatus")]
        public  virtual MessageStatus MessageStatus { get; set; }
        [JsonPropertyName("message")]
        public virtual string? Message { get; set; }
        [JsonPropertyName("generateNo")]
        public virtual string? GenerateNo {get;set;}
        [JsonPropertyName("returnValue")]
        public virtual string? ReturnValue { get; set; }
        [JsonPropertyName("dataList")]
        public virtual List<T>? DataList { get; set; }
        [JsonPropertyName("dataEntity")]
        public virtual T? DataEntity { get; set; }
    }

    public enum MessageStatus
    {
        Info = 0,
        Error = 1,
        Success = 2,
        Warning = 3
    }
}
