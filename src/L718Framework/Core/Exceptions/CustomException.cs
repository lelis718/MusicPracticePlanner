namespace L718Framework.Core.Exceptions;

/// <summary>
/// A wrapper class for custom system exceptions
/// </summary>
public class CustomException: Exception{
    public CustomException(string message):base(message){
    }
}