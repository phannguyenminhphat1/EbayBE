using System.Runtime.Serialization;
using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRolesName
{
    [EnumMember(Value = "Guest")]
    Guest = 1,

    [EnumMember(Value = "Doctor")]
    Doctor = 2,

    [EnumMember(Value = "Receiptionist")]
    Receptionist = 3,

    [EnumMember(Value = "Admin")]
    Admin = 4,

    [EnumMember(Value = "Technician")]
    Technician = 5

}


